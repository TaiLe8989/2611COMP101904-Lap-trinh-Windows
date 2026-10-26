using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QuanLySuKienVaBanVe.Common;
using QuanLySuKienVaBanVe.DAL;
using QuanLySuKienVaBanVe.DTO;

namespace QuanLySuKienVaBanVe.BLL
{
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL _dal;
        private const int MaxFailedAttempts = 5;
        private static readonly TimeSpan LockDuration = TimeSpan.FromMinutes(1);
        private static readonly object AttemptLock = new object();
        private static readonly Dictionary<string, LoginAttempt> Attempts =
            new Dictionary<string, LoginAttempt>(StringComparer.OrdinalIgnoreCase);

        public TaiKhoanBLL() : this(new TaiKhoanDAL()) { }

        public TaiKhoanBLL(TaiKhoanDAL dal)
        {
            _dal = dal ?? throw new ArgumentNullException("dal");
        }

        public DangNhapResult DangNhap(string tenDangNhap, string matKhau)
        {
            tenDangNhap = (tenDangNhap ?? string.Empty).Trim();
            if (tenDangNhap.Length == 0 || string.IsNullOrEmpty(matKhau))
                return DangNhapResult.Failure("Vui lòng nhập tên đăng nhập và mật khẩu.");

            string waitMessage;
            if (IsLocked(tenDangNhap, out waitMessage))
                return DangNhapResult.Failure(waitMessage);

            try
            {
                TaiKhoanDTO taiKhoan = _dal.LayTheoTenDangNhap(tenDangNhap);
                bool needsUpgrade;
                if (taiKhoan == null || !PasswordHasher.Verify(matKhau, taiKhoan.MatKhau, out needsUpgrade))
                {
                    string message = RegisterFailure(tenDangNhap);
                    return DangNhapResult.Failure(message);
                }

                if (!taiKhoan.TrangThai)
                    return DangNhapResult.Failure("Tài khoản đang bị khóa. Vui lòng liên hệ quản lý.");

                ClearFailures(tenDangNhap);

                // Nâng cấp dần tài khoản demo lưu mật khẩu thuần sang PBKDF2 sau lần đăng nhập hợp lệ.
                if (needsUpgrade)
                {
                    try
                    {
                        _dal.CapNhatMatKhau(taiKhoan.MaTK, PasswordHasher.Hash(matKhau));
                    }
                    catch
                    {
                        // Không làm mất phiên đăng nhập hợp lệ chỉ vì bước nâng cấp hash lỗi.
                        // Lần đăng nhập sau có thể thử nâng cấp lại; lỗi kết nối chính vẫn đã xảy ra ở truy vấn đầu.
                    }
                }

                // Không mang mật khẩu/hash trong DTO gửi lên GUI hoặc lưu trong phiên.
                taiKhoan.MatKhau = string.Empty;
                return DangNhapResult.Success(taiKhoan);
            }
            catch (Exception ex)
            {
                return DangNhapResult.Failure(
                    "Không truy cập được cơ sở dữ liệu. Hãy kiểm tra SQL Server và App.config. Chi tiết: " + ex.Message);
            }
        }

        public bool DangKyNhanVien(string tenDangNhap, string matKhau, string xacNhan,
            out string thongBao)
        {
            tenDangNhap = (tenDangNhap ?? string.Empty).Trim();
            if (!Regex.IsMatch(tenDangNhap, "^[A-Za-z0-9._-]{3,50}$"))
            {
                thongBao = "Tên đăng nhập phải có 3–50 ký tự: chữ, số, dấu chấm, gạch ngang hoặc gạch dưới.";
                return false;
            }
            if (string.IsNullOrEmpty(matKhau) || matKhau.Length < 8)
            {
                thongBao = "Mật khẩu mới phải có ít nhất 8 ký tự.";
                return false;
            }
            if (!string.Equals(matKhau, xacNhan, StringComparison.Ordinal))
            {
                thongBao = "Mật khẩu xác nhận không khớp.";
                return false;
            }

            try
            {
                return _dal.ThemNhanVien(tenDangNhap, PasswordHasher.Hash(matKhau), out thongBao);
            }
            catch (Exception ex)
            {
                thongBao = "Không thể tạo tài khoản. Kiểm tra kết nối CSDL. Chi tiết: " + ex.Message;
                return false;
            }
        }

        public bool DoiMatKhau(int maTK, string matKhauCu, string matKhauMoi,
            string xacNhan, out string thongBao)
        {
            if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi))
            {
                thongBao = "Vui lòng nhập đầy đủ mật khẩu cũ và mật khẩu mới.";
                return false;
            }
            if (matKhauMoi.Length < 8)
            {
                thongBao = "Mật khẩu mới phải có ít nhất 8 ký tự.";
                return false;
            }
            if (string.Equals(matKhauCu, matKhauMoi, StringComparison.Ordinal))
            {
                thongBao = "Mật khẩu mới phải khác mật khẩu cũ.";
                return false;
            }
            if (!string.Equals(matKhauMoi, xacNhan, StringComparison.Ordinal))
            {
                thongBao = "Mật khẩu xác nhận không khớp.";
                return false;
            }

            try
            {
                TaiKhoanDTO taiKhoan = _dal.LayTheoMa(maTK);
                bool needsUpgrade;
                if (taiKhoan == null || !PasswordHasher.Verify(matKhauCu, taiKhoan.MatKhau, out needsUpgrade))
                {
                    thongBao = "Mật khẩu hiện tại không đúng.";
                    return false;
                }
                bool updated = _dal.CapNhatMatKhau(maTK, PasswordHasher.Hash(matKhauMoi));
                thongBao = updated ? "Đổi mật khẩu thành công." : "Không tìm thấy tài khoản để cập nhật.";
                return updated;
            }
            catch (Exception ex)
            {
                thongBao = "Không thể đổi mật khẩu. Chi tiết: " + ex.Message;
                return false;
            }
        }

        private static bool IsLocked(string username, out string message)
        {
            lock (AttemptLock)
            {
                LoginAttempt item;
                if (Attempts.TryGetValue(username, out item) && item.LockUntil > DateTime.UtcNow)
                {
                    int seconds = Math.Max(1, (int)Math.Ceiling((item.LockUntil - DateTime.UtcNow).TotalSeconds));
                    message = "Bạn đã nhập sai quá nhiều lần. Hãy thử lại sau " + seconds + " giây.";
                    return true;
                }
                message = null;
                return false;
            }
        }

        private static string RegisterFailure(string username)
        {
            lock (AttemptLock)
            {
                LoginAttempt item;
                if (!Attempts.TryGetValue(username, out item)) item = new LoginAttempt();
                if (item.LockUntil <= DateTime.UtcNow) item.FailedCount++;
                if (item.FailedCount >= MaxFailedAttempts)
                {
                    item.LockUntil = DateTime.UtcNow.Add(LockDuration);
                    item.FailedCount = 0;
                    Attempts[username] = item;
                    return "Bạn đã nhập sai 5 lần. Tài khoản đăng nhập tạm thời bị khóa trong 1 phút.";
                }
                Attempts[username] = item;
                return "Tên đăng nhập hoặc mật khẩu không chính xác. Còn " +
                       (MaxFailedAttempts - item.FailedCount) + " lần thử trước khi khóa tạm.";
            }
        }

        private static void ClearFailures(string username)
        {
            lock (AttemptLock) Attempts.Remove(username);
        }

        private sealed class LoginAttempt
        {
            public int FailedCount { get; set; }
            public DateTime LockUntil { get; set; }
        }
    }
}
