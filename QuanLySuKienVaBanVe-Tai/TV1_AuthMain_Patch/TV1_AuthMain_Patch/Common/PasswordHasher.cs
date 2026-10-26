using System;
using System.Globalization;
using System.Security.Cryptography;

namespace QuanLySuKienVaBanVe.Common
{
    /// <summary>
    /// Hash mật khẩu mới bằng PBKDF2 + salt ngẫu nhiên.
    /// Đồng thời nhận diện mật khẩu dạng văn bản thuần để hỗ trợ tài khoản demo cũ;
    /// sau lần đăng nhập thành công đầu tiên, BLL sẽ nâng cấp sang hash.
    /// </summary>
    public static class PasswordHasher
    {
        private const int Iterations = 120000;
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const string Prefix = "PBKDF2";

        public static string Hash(string password)
        {
            if (password == null) throw new ArgumentNullException("password");

            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            byte[] hash;
            using (var derive = new Rfc2898DeriveBytes(password, salt, Iterations))
                hash = derive.GetBytes(HashSize);

            return Prefix + "$" + Iterations.ToString(CultureInfo.InvariantCulture) + "$" +
                   Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
        }

        public static bool Verify(string password, string storedValue, out bool needsUpgrade)
        {
            needsUpgrade = false;
            if (password == null || string.IsNullOrEmpty(storedValue)) return false;

            string[] parts = storedValue.Split('$');
            if (parts.Length == 4 && string.Equals(parts[0], Prefix, StringComparison.Ordinal))
            {
                int iterations;
                byte[] salt;
                byte[] expected;
                try
                {
                    iterations = int.Parse(parts[1], CultureInfo.InvariantCulture);
                    salt = Convert.FromBase64String(parts[2]);
                    expected = Convert.FromBase64String(parts[3]);
                }
                catch (FormatException) { return false; }
                catch (ArgumentException) { return false; }

                if (iterations < 1 || salt.Length < 8 || expected.Length < 16) return false;

                byte[] actual;
                using (var derive = new Rfc2898DeriveBytes(password, salt, iterations))
                    actual = derive.GetBytes(expected.Length);

                return FixedTimeEquals(actual, expected);
            }

            // Tương thích với dữ liệu mẫu ban đầu (admin/123456 và staff/123456).
            // Chỉ dùng để di chuyển dữ liệu demo; tài khoản sẽ được hash lại khi đăng nhập thành công.
            needsUpgrade = true;
            return FixedTimeEquals(System.Text.Encoding.UTF8.GetBytes(password),
                                   System.Text.Encoding.UTF8.GetBytes(storedValue));
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length) return false;
            int difference = 0;
            for (int i = 0; i < left.Length; i++) difference |= left[i] ^ right[i];
            return difference == 0;
        }
    }
}
