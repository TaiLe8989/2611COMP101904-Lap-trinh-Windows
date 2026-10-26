using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLySuKienVaBanVe.BLL;
using QuanLySuKienVaBanVe.Common;

namespace QuanLySuKienVaBanVe.GUI.Forms
{
    public class FrmDoiMatKhau : AuthFormBase
    {
        private readonly Label _title;
        private readonly AuthTextBoxField _oldPassword;
        private readonly AuthTextBoxField _newPassword;
        private readonly AuthTextBoxField _confirmPassword;
        private readonly Label _message;
        private readonly HoverBounceButton _saveButton;

        public FrmDoiMatKhau() : base("Đổi mật khẩu", 610, 520)
        {
            _title = AddLabel("Đổi mật khẩu", 30, 22, 550, 45, 23f, FontStyle.Bold, ContentAlignment.MiddleCenter);
            _oldPassword = AddField("▣", "Mật khẩu hiện tại", true, 95);
            _newPassword = AddField("▣", "Mật khẩu mới (ít nhất 8 ký tự)", true, 160);
            _confirmPassword = AddField("▣", "Xác nhận mật khẩu mới", true, 225);
            _message = AddLabel(string.Empty, 45, 290, 520, 46, 9.5f, FontStyle.Regular, ContentAlignment.MiddleLeft);
            _message.ForeColor = Color.FromArgb(195, 36, 73);
            _saveButton = AddPrimaryButton("Lưu mật khẩu mới", 350);
            _saveButton.Click += SaveButton_Click;
            AcceptButton = _saveButton;
            ApplyLanguage();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (AppSession.CurrentUser == null)
            {
                _message.Text = UiLanguage.Text("Phiên đăng nhập đã hết hạn. Hãy đăng nhập lại.",
                    "Your session has expired. Please sign in again.");
                return;
            }

            string message;
            bool success = new TaiKhoanBLL().DoiMatKhau(AppSession.CurrentUser.MaTK,
                _oldPassword.Input.Text, _newPassword.Input.Text, _confirmPassword.Input.Text, out message);
            _message.Text = message;
            _message.ForeColor = success ? Color.FromArgb(30, 135, 85) : Color.FromArgb(195, 36, 73);
            if (success)
            {
                MessageBox.Show(this, message, UiLanguage.Text("Hoàn tất", "Completed"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        protected override void ApplyLanguage()
        {
            if (_title == null) return;
            Text = UiLanguage.Text("Đổi mật khẩu", "Change password");
            _title.Text = UiLanguage.Text("Đổi mật khẩu", "Change password");
            _oldPassword.Placeholder = UiLanguage.Text("Mật khẩu hiện tại", "Current password");
            _newPassword.Placeholder = UiLanguage.Text("Mật khẩu mới (ít nhất 8 ký tự)", "New password (at least 8 characters)");
            _confirmPassword.Placeholder = UiLanguage.Text("Xác nhận mật khẩu mới", "Confirm new password");
            _saveButton.Text = UiLanguage.Text("Lưu mật khẩu mới", "Save new password");
        }
    }
}
