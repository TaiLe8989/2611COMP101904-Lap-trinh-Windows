using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManagerMini.Forms
{
    /// <summary>
    /// Form giới thiệu ứng dụng.
    /// </summary>
    public class FrmAbout : Form
    {
        // TODO: sửa lại thông tin của bạn trước khi nộp
        private const string StudentName = "Họ và tên sinh viên";
        private const string ClassName = "Tên lớp";

        private Label lblAppName, lblInfo;
        private Button btnClose;

        public FrmAbout()
        {
            Text = "Giới thiệu";
            Font = new Font("Segoe UI", 10F);
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(400, 250);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            lblAppName = new Label
            {
                Name = "lblAppName",
                Text = "ShopManagerMini",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 20),
                Size = new Size(400, 40)
            };

            lblInfo = new Label
            {
                Name = "lblInfo",
                Text = "Ứng dụng quản lý cửa hàng mini\r\n"
                     + "Môn học: COMP1019 - Lập trình trên Windows\r\n"
                     + "Phiên bản: 1.0\r\n"
                     + "Sinh viên: " + StudentName + "\r\n"
                     + "Lớp: " + ClassName,
                TextAlign = ContentAlignment.TopCenter,
                Location = new Point(20, 75),
                Size = new Size(360, 110)
            };

            btnClose = new Button
            {
                Name = "btnClose",
                Text = "Đóng",
                Location = new Point(145, 195),
                Size = new Size(110, 35),
                DialogResult = DialogResult.OK
            };

            Controls.AddRange(new Control[] { lblAppName, lblInfo, btnClose });
            AcceptButton = btnClose;
            CancelButton = btnClose;
        }
    }
}
