using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    /// <summary>
    /// Form đăng nhập. Trả DialogResult.OK khi đăng nhập thành công.
    /// </summary>
    public class FrmLogin : Form
    {
        // Tài khoản mẫu
        private static readonly List<User> Accounts = new List<User>
        {
            new User("admin", "123", "Quản trị viên", "Admin"),
            new User("user", "123", "Nhân viên bán hàng", "User")
        };

        private Label lblTitle, lblUsername, lblPassword;
        private TextBox txtUsername, txtPassword;
        private Button btnLogin, btnExit;

        /// <summary>Người dùng đăng nhập thành công (truyền sang FrmMain).</summary>
        public User CurrentUser { get; private set; }

        public FrmLogin()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Đăng nhập - ShopManagerMini";
            Font = new Font("Segoe UI", 10F);
            AutoScaleDimensions = new SizeF(96F, 96F); // co giãn theo DPI màn hình, tránh nhãn đè ô nhập
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(380, 250);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;

            lblTitle = new Label
            {
                Name = "lblTitle",
                Text = "ĐĂNG NHẬP HỆ THỐNG",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 20),
                Size = new Size(380, 35)
            };

            lblUsername = new Label
            {
                Name = "lblUsername",
                Text = "Tên đăng nhập:",
                Location = new Point(30, 75),
                AutoSize = true
            };
            txtUsername = new TextBox
            {
                Name = "txtUsername",
                Location = new Point(165, 72),
                Size = new Size(185, 25)
            };

            lblPassword = new Label
            {
                Name = "lblPassword",
                Text = "Mật khẩu:",
                Location = new Point(30, 120),
                AutoSize = true
            };
            txtPassword = new TextBox
            {
                Name = "txtPassword",
                Location = new Point(165, 117),
                Size = new Size(185, 25),
                UseSystemPasswordChar = true
            };

            btnLogin = new Button
            {
                Name = "btnLogin",
                Text = "Đăng nhập",
                Location = new Point(70, 180),
                Size = new Size(110, 35)
            };
            btnLogin.Click += btnLogin_Click;

            btnExit = new Button
            {
                Name = "btnExit",
                Text = "Thoát",
                Location = new Point(200, 180),
                Size = new Size(110, 35),
                DialogResult = DialogResult.Cancel // bấm Thoát thì đóng form, trả Cancel
            };

            Controls.AddRange(new Control[]
            {
                lblTitle, lblUsername, txtUsername, lblPassword, txtPassword, btnLogin, btnExit
            });

            AcceptButton = btnLogin; // Enter = Đăng nhập
            CancelButton = btnExit;  // Esc = Thoát
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Bỏ trống username hoặc password
            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                (username == "" ? txtUsername : txtPassword).Focus();
                return;
            }

            // Kiểm tra tài khoản mẫu
            User found = Accounts.Find(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)
                && u.Password == password);

            if (found == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!",
                    "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            CurrentUser = found;
            DialogResult = DialogResult.OK; // đóng form và trả OK
        }
    }
}
