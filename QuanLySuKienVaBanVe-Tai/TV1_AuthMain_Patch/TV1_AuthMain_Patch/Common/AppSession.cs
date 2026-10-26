using System;
using System.IO;
using System.Text;
using QuanLySuKienVaBanVe.DTO;

namespace QuanLySuKienVaBanVe.Common
{
    public static class AppSession
    {
        public static TaiKhoanDTO CurrentUser { get; private set; }
        public static DateTime? LoginTime { get; private set; }
        public static bool IsAuthenticated { get { return CurrentUser != null; } }

        public static void SignIn(TaiKhoanDTO account)
        {
            if (account == null) throw new ArgumentNullException("account");
            CurrentUser = account;
            LoginTime = DateTime.Now;
        }

        public static void SignOut()
        {
            CurrentUser = null;
            LoginTime = null;
        }

        public static bool IsManager
        {
            get
            {
                if (CurrentUser == null) return false;
                return string.Equals(CurrentUser.VaiTro, "QuanLy", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(CurrentUser.VaiTro, "Admin", StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    /// <summary>Chỉ lưu tên đăng nhập, không bao giờ lưu mật khẩu.</summary>
    public static class RememberedUsernameStore
    {
        private static string FilePath
        {
            get
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "QuanLySuKienVaBanVe");
                return Path.Combine(folder, "remembered-user.txt");
            }
        }

        public static string Load()
        {
            try { return File.Exists(FilePath) ? File.ReadAllText(FilePath, Encoding.UTF8).Trim() : string.Empty; }
            catch { return string.Empty; }
        }

        public static void Save(string username)
        {
            try
            {
                string folder = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                if (string.IsNullOrWhiteSpace(username))
                {
                    if (File.Exists(FilePath)) File.Delete(FilePath);
                }
                else File.WriteAllText(FilePath, username.Trim(), Encoding.UTF8);
            }
            catch
            {
                // Việc ghi nhớ tên tài khoản là tiện ích; lỗi ghi file không được làm hỏng đăng nhập.
            }
        }
    }
}
