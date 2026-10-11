namespace ShopManagerMini.Models
{
    /// <summary>
    /// Thông tin tài khoản đăng nhập.
    /// </summary>
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

        public User(string username, string password, string fullName, string role)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            Role = role;
        }
    }
}
