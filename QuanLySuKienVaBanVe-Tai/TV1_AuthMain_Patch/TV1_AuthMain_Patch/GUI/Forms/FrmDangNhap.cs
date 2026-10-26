using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLySuKienVaBanVe.BLL;
using QuanLySuKienVaBanVe.Common;

namespace QuanLySuKienVaBanVe.GUI.Forms
{
    public class FrmDangNhap : AuthFormBase
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;
        private readonly AuthTextBoxField _userField;
        private readonly AuthTextBoxField _passwordField;
        private readonly CheckBox _remember;
        private readonly Label _title;
        private readonly Label _subtitle;
        private readonly Label _message;
        private readonly Label _registerPrompt;
        private readonly HoverBounceButton _loginButton;
        private readonly Button _registerButton;
        private readonly Button _forgotButton;
        private readonly ErrorProvider _errors;

        public FrmDangNhap() : base("Đăng nhập - Quản lý sự kiện và bán vé", 640, 590)
        {
            _taiKhoanBLL = new TaiKhoanBLL();
            _errors = new ErrorProvider { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };

            _title = AddLabel("Đăng nhập", 36, 25, 568, 48, 26f, FontStyle.Bold, ContentAlignment.MiddleCenter);
            _subtitle = AddLabel("Chào mừng bạn trở lại hệ thống", 45, 73, 550, 30, 10.5f,
                FontStyle.Regular, ContentAlignment.MiddleCenter);
            _subtitle.ForeColor = Color.FromArgb(116, 128, 150);

            _userField = AddField("♙", "Tên đăng nhập", false, 126);
            _passwordField = AddField("▣", "Mật khẩu", true, 196);

            _remember = new CheckBox
            {
                Text = "Ghi nhớ tên đăng nhập",
                AutoSize = false,
                Location = new Point(50, 266),
                Size = new Size(250, 30),
                ForeColor = Color.FromArgb(37, 51, 75),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9.5f),
                Cursor = Cursors.Hand
            };
            Card.Controls.Add(_remember);

            _forgotButton = AddLinkButton("Quên mật khẩu?", 425, 266, 165, 30);
            _forgotButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _forgotButton.TextAlign = ContentAlignment.MiddleRight;
            _forgotButton.Click += delegate
            {
                MessageBox.Show(this,
                    UiLanguage.Text("Bạn hãy đăng nhập rồi vào Hệ thống → Đổi mật khẩu, hoặc liên hệ quản lý để được hỗ trợ.",
                                    "Sign in and choose System → Change password, or contact your manager."),
                    UiLanguage.Text("Hỗ trợ tài khoản", "Account help"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            _message = AddLabel(string.Empty, 50, 307, 540, 38, 9.5f, FontStyle.Regular, ContentAlignment.MiddleLeft);
            _message.ForeColor = Color.FromArgb(195, 36, 73);
            _message.AutoEllipsis = true;

            _loginButton = AddPrimaryButton("Đăng nhập", 355);
            _loginButton.Click += LoginButton_Click;

            _registerPrompt = AddLabel("Bạn chưa có tài khoản?", 96, 475, 275, 32, 9.5f,
                FontStyle.Regular, ContentAlignment.MiddleRight);
            _registerPrompt.ForeColor = Color.FromArgb(50, 61, 83);
            _registerButton = AddLinkButton("Đăng ký ngay", 370, 475, 170, 32);
            _registerButton.Click += RegisterButton_Click;

            AcceptButton = _loginButton;
            _userField.Input.Text = RememberedUsernameStore.Load();
            _remember.Checked = !string.IsNullOrWhiteSpace(_userField.Input.Text);
            ApplyLanguage();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            _errors.Clear();
            _message.Text = string.Empty;
            string username = _userField.Input.Text.Trim();
            string password = _passwordField.Input.Text;

            if (username.Length == 0) _errors.SetError(_userField, UiLanguage.Text("Hãy nhập tên đăng nhập.", "Enter your username."));
            if (password.Length == 0) _errors.SetError(_passwordField, UiLanguage.Text("Hãy nhập mật khẩu.", "Enter your password."));
            if (username.Length == 0 || password.Length == 0)
            {
                _message.Text = UiLanguage.Text("Vui lòng điền đầy đủ thông tin.", "Please complete both fields.");
                return;
            }

            UseWaitCursor = true;
            _loginButton.Enabled = false;
            try
            {
                DangNhapResult result = _taiKhoanBLL.DangNhap(username, password);
                if (!result.ThanhCong)
                {
                    _message.Text = result.ThongBao;
                    _passwordField.Input.SelectAll();
                    _passwordField.Input.Focus();
                    return;
                }

                AppSession.SignIn(result.TaiKhoan);
                RememberedUsernameStore.Save(_remember.Checked ? username : null);
                _passwordField.Input.Clear();
                _message.Text = string.Empty;

                // FrmDangNhap vẫn là form gốc của Application.Run; FrmMain mở modal để đăng xuất có thể quay lại đây.
                using (var main = new FrmMain(result.TaiKhoan))
                {
                    Hide();
                    main.ShowDialog(this);
                }
                AppSession.SignOut();
                Show();
                Activate();
                _passwordField.Input.Clear();
            }
            finally
            {
                UseWaitCursor = false;
                _loginButton.Enabled = true;
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            using (var registerForm = new FrmDangKy())
            {
                if (registerForm.ShowDialog(this) == DialogResult.OK)
                {
                    _userField.Input.Text = registerForm.CreatedUsername;
                    _passwordField.Input.Clear();
                    _message.ForeColor = Color.FromArgb(30, 135, 85);
                    _message.Text = UiLanguage.Text("Đã tạo tài khoản nhân viên. Hãy đăng nhập.",
                        "Staff account created. You can now sign in.");
                    _passwordField.Input.Focus();
                }
            }
        }

        protected override void ApplyLanguage()
        {
            if (_title == null) return;
            Text = UiLanguage.Text("Đăng nhập - Quản lý sự kiện và bán vé", "Sign in - Event and Ticket Management");
            _title.Text = UiLanguage.Text("Đăng nhập", "Sign in");
            _subtitle.Text = UiLanguage.Text("Chào mừng bạn trở lại hệ thống", "Welcome back to the system");
            _userField.Placeholder = UiLanguage.Text("Tên đăng nhập", "Username");
            _passwordField.Placeholder = UiLanguage.Text("Mật khẩu", "Password");
            _remember.Text = UiLanguage.Text("Ghi nhớ tên đăng nhập", "Remember username");
            _forgotButton.Text = UiLanguage.Text("Quên mật khẩu?", "Forgot password?");
            _loginButton.Text = UiLanguage.Text("Đăng nhập", "Sign in");
            _registerPrompt.Text = UiLanguage.Text("Bạn chưa có tài khoản?", "Don't have an account?");
            _registerButton.Text = UiLanguage.Text("Đăng ký ngay", "Register");
        }
    }
}
