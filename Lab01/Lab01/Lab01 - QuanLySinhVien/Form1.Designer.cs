using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuanLySinhVien
{
    partial class Form1
    {
       
        private System.ComponentModel.IContainer components = null;
        private Label lblHoTen;
        private Label lblNamSinh;
        private Label lblEmail;
        private Label lblKhoa;

        private TextBox txtHoTen;
        private TextBox txtNamSinh;
        private TextBox txtEmail;

        private GroupBox grpGioiTinh;
        private RadioButton radNam;
        private RadioButton radNu;

        private ComboBox cboKhoa;

        private Button btnHienThi;
        private Button btnXoa;
        private Button btnThoat;

        private TextBox txtKetQua;

      

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        
        private void InitializeComponent()
        {
            lblHoTen = new Label();
            lblNamSinh = new Label();
            lblEmail = new Label();
            lblKhoa = new Label();
            txtHoTen = new TextBox();
            txtNamSinh = new TextBox();
            txtEmail = new TextBox();
            grpGioiTinh = new GroupBox();
            radNam = new RadioButton();
            radNu = new RadioButton();
            cboKhoa = new ComboBox();
            btnHienThi = new Button();
            btnXoa = new Button();
            btnThoat = new Button();
            txtKetQua = new TextBox();
            lblTitle = new Label();
            grpGioiTinh.SuspendLayout();
            SuspendLayout();
            
            // lblHoTen
            
            lblHoTen.Location = new Point(20, 70);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(90, 23);
            lblHoTen.TabIndex = 1;
            lblHoTen.Text = "Họ tên:";
            
            // lblNamSinh
            
            lblNamSinh.Location = new Point(20, 110);
            lblNamSinh.Name = "lblNamSinh";
            lblNamSinh.Size = new Size(90, 23);
            lblNamSinh.TabIndex = 3;
            lblNamSinh.Text = "Năm sinh:";
            
            // lblEmail
             
            lblEmail.Location = new Point(20, 150);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(90, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email:";
            
            // lblKhoa
            
            lblKhoa.Location = new Point(20, 260);
            lblKhoa.Name = "lblKhoa";
            lblKhoa.Size = new Size(90, 23);
            lblKhoa.TabIndex = 8;
            lblKhoa.Text = "Khoa/Lớp:";
            
            // txtHoTen
            
            txtHoTen.Location = new Point(130, 67);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(320, 27);
            txtHoTen.TabIndex = 2;
            
            // txtNamSinh
            
            txtNamSinh.Location = new Point(130, 107);
            txtNamSinh.Name = "txtNamSinh";
            txtNamSinh.Size = new Size(320, 27);
            txtNamSinh.TabIndex = 4;
            
            // txtEmail
             
            txtEmail.Location = new Point(130, 147);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 27);
            txtEmail.TabIndex = 6;
             
            // grpGioiTinh
             
            grpGioiTinh.Controls.Add(radNam);
            grpGioiTinh.Controls.Add(radNu);
            grpGioiTinh.Location = new Point(20, 190);
            grpGioiTinh.Name = "grpGioiTinh";
            grpGioiTinh.Size = new Size(200, 55);
            grpGioiTinh.TabIndex = 7;
            grpGioiTinh.TabStop = false;
            grpGioiTinh.Text = "Giới tính";
             
            // radNam
            
            radNam.Location = new Point(15, 22);
            radNam.Name = "radNam";
            radNam.Size = new Size(70, 23);
            radNam.TabIndex = 0;
            radNam.Text = "Nam";
             
            // radNu
             
            radNu.Location = new Point(100, 22);
            radNu.Name = "radNu";
            radNu.Size = new Size(70, 23);
            radNu.TabIndex = 1;
            radNu.Text = "Nữ";
            
            // cboKhoa
            // 
            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoa.Items.AddRange(new object[] { "Công nghệ thông tin", "Toán - Tin học ",
"Vật lý",

"Hóa học",

"Sinh học",

"Ngữ văn",

"Lịch sử",

"Địa lý",

"Tiếng Anh",

"Giáo dục Tiểu học" });
            cboKhoa.Location = new Point(130, 257);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new Size(320, 28);
            cboKhoa.TabIndex = 9;
            // 
            // btnHienThi
            // 
            btnHienThi.Location = new Point(20, 300);
            btnHienThi.Name = "btnHienThi";
            btnHienThi.Size = new Size(140, 35);
            btnHienThi.TabIndex = 10;
            btnHienThi.Text = "Hiển thị";
            btnHienThi.Click += btnHienThi_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(170, 300);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(140, 35);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "Xóa hết";
            btnXoa.Click += btnXoa_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(320, 300);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(130, 35);
            btnThoat.TabIndex = 12;
            btnThoat.Text = "Thoát";
            btnThoat.Click += btnThoat_Click;
            // 
            // txtKetQua
            // 
            txtKetQua.Font = new Font("Consolas", 10F);
            txtKetQua.Location = new Point(20, 350);
            txtKetQua.Multiline = true;
            txtKetQua.Name = "txtKetQua";
            txtKetQua.ReadOnly = true;
            txtKetQua.ScrollBars = ScrollBars.Vertical;
            txtKetQua.Size = new Size(430, 160);
            txtKetQua.TabIndex = 13;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(460, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN SINH VIÊN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Click += lblTitle_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(470, 530);
            Controls.Add(lblTitle);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblNamSinh);
            Controls.Add(txtNamSinh);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(grpGioiTinh);
            Controls.Add(lblKhoa);
            Controls.Add(cboKhoa);
            Controls.Add(btnHienThi);
            Controls.Add(btnXoa);
            Controls.Add(btnThoat);
            Controls.Add(txtKetQua);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lab01 - Thông tin sinh viên";
            grpGioiTinh.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
    }
}
