using QuanLySuKienVaBanVe.DTO;

namespace QuanLySuKienVaBanVe.BLL
{
    public sealed class DangNhapResult
    {
        public bool ThanhCong { get; private set; }
        public string ThongBao { get; private set; }
        public TaiKhoanDTO TaiKhoan { get; private set; }

        private DangNhapResult(bool thanhCong, string thongBao, TaiKhoanDTO taiKhoan)
        {
            ThanhCong = thanhCong;
            ThongBao = thongBao;
            TaiKhoan = taiKhoan;
        }

        public static DangNhapResult Success(TaiKhoanDTO taiKhoan)
        {
            return new DangNhapResult(true, "Đăng nhập thành công.", taiKhoan);
        }

        public static DangNhapResult Failure(string thongBao)
        {
            return new DangNhapResult(false, thongBao, null);
        }
    }
}
