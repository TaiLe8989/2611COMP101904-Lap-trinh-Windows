using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLySuKienVaBanVe.BLL;
using QuanLySuKienVaBanVe.Common;

namespace QuanLySuKienVaBanVe.GUI.Forms
{
    /// <summary>
    /// Màn hình đăng ký là phần mở rộng theo ảnh mẫu, không nằm trong DoD đăng nhập 20–22/10.
    /// Đăng ký công khai chỉ tạo vai trò NhanVien; không bao giờ cho người dùng tự chọn QuanLy.
    /// </summary>
    public class FrmDangKy : AuthFormBase
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;
        private readonly Label _title;
        private readonly Label _subtitle;
        private readonly AuthTextBoxField _userField;
        private readonly AuthTextBoxField _passwordField;
        private readonly AuthTextBoxField _confirmField;
        private readonly CheckBox _agree;
        private readonly Label _message;
        private readonly HoverBounceButton _continueButton;
        private readonly Label _loginPrompt;
        private readonly Button _loginButton;
        private readonly ErrorProvider _errors;

        public string CreatedUsername { get; private set; }

        public FrmDangKy() : base("Đăng ký tài khoản", 640, 630)
        {
            _taiKhoanBLL = new TaiKhoanBLL();
            _errors = new ErrorProvider { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };

            _title = AddLabel("Đăng ký tài khoản", 36, 24, 568, 48, 25f, FontStyle.Bold, ContentAlignment.MiddleCenter);
            _subtitle = AddLabel("Tạo tài khoản nhân viên mới", 45, 70, 550, 28, 10.5f,
                FontStyle.Regular, ContentAlignment.MiddleCenter);
            _subtitle.ForeColor = Color.FromArgb(116, 128, 150);

            _userField = AddField("♙", "Tên đăng nhập (3–50 ký tự)", false, 116);
            _passwordField = AddField("▣", "Mật khẩu (ít nhất 8 ký tự)", true, 182);
            _confirmField = AddField("▣", "Xác nhận mật khẩu", true, 248);

            _agree = new CheckBox
            {
                Text = "Tôi đồng ý với điều kiện sử dụng",
                Location = new Point(50, 320),
                Size = new Size(500, 30),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(42, 57, 83),
                Font = new Font("Segoe UI", 9.5f),
                Cursor = Cursors.Hand
            };
            Card.Controls.Add(_agree);

            var termsLink = AddLinkButton("Điều kiện", 420, 320, 170, 30);
            termsLink.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            termsLink.TextAlign = ContentAlignment.MiddleRight;
            termsLink.Click += delegate
            {
                MessageBox.Show(this,
                    UiLanguage.Text("Tài khoản tự đăng ký chỉ được cấp quyền Nhân viên. Quyền Quản lý do nhóm tạo bằng dữ liệu quản trị.",
                                    "Self-registered accounts receive Staff access only. Manager accounts are provisioned by an administrator."),
                    UiLanguage.Text("Điều kiện sử dụng", "Terms of use"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            _message = AddLabel(string.Empty, 50, 355, 540, 42, 9.2f, FontStyle.Regular, ContentAlignment.MiddleLeft);
            _message.ForeColor = Color.FromArgb(195, 36, 73);
            _message.AutoEllipsis = true;

            _continueButton = AddPrimaryButton("Tiếp tục", 410);
            _continueButton.Click += ContinueButton_Click;

            _loginPrompt = AddLabel("Bạn đã có tài khoản?", 96, 535, 275, 30, 9.5f,
                FontStyle.Regular, ContentAlignment.MiddleRight);
            _loginPrompt.ForeColor = Color.FromArgb(50, 61, 83);
            _loginButton = AddLinkButton("Đăng nhập", 370, 535, 170, 30);
            _loginButton.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };

            AcceptButton = _continueButton;
            ApplyLanguage();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            _errors.Clear();
            _message.Text = string.Empty;
            if (!_agree.Checked)
            {
                _message.Text = UiLanguage.Text("Bạn cần đồng ý điều kiện sử dụng để tiếp tục.",
                    "Please accept the terms to continue.");
                return;
            }

            string message;
            bool success = _taiKhoanBLL.DangKyNhanVien(_userField.Input.Text,
                _passwordField.Input.Text, _confirmField.Input.Text, out message);
            _message.Text = message;
            if (!success)
            {
                _message.ForeColor = Color.FromArgb(195, 36, 73);
                return;
            }

            CreatedUsername = _userField.Input.Text.Trim();
            _message.ForeColor = Color.FromArgb(30, 135, 85);
            MessageBox.Show(this, message, UiLanguage.Text("Hoàn tất", "Completed"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void ApplyLanguage()
        {
            if (_title == null) return;
            Text = UiLanguage.Text("Đăng ký tài khoản", "Create an account");
            _title.Text = UiLanguage.Text("Đăng ký tài khoản", "Create an account");
            _subtitle.Text = UiLanguage.Text("Tạo tài khoản nhân viên mới", "Create a new staff account");
            _userField.Placeholder = UiLanguage.Text("Tên đăng nhập (3–50 ký tự)", "Username (3–50 characters)");
            _passwordField.Placeholder = UiLanguage.Text("Mật khẩu (ít nhất 8 ký tự)", "Password (at least 8 characters)");
            _confirmField.Placeholder = UiLanguage.Text("Xác nhận mật khẩu", "Confirm password");
            _agree.Text = UiLanguage.Text("Tôi đồng ý với điều kiện sử dụng", "I agree to the terms of use");
            _continueButton.Text = UiLanguage.Text("Tiếp tục", "Continue");
            _loginPrompt.Text = UiLanguage.Text("Bạn đã có tài khoản?", "Already have an account?");
            _loginButton.Text = UiLanguage.Text("Đăng nhập", "Sign in");
        }
    }
}
