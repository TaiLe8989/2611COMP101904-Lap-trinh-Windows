using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05_AppDangKyKhoaHoc
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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

            lblTieuDe = new Label();
            grpHocVien = new GroupBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblDemKyTu = new Label();
            lblSoDienThoai = new Label();
            txtSoDienThoai = new TextBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            chkNhanEmail = new CheckBox();
            lblTrangThaiEmail = new Label();
            grpKhoaHoc = new GroupBox();
            lblKhoaHoc = new Label();
            cboKhoaHoc = new ComboBox();
            lblHinhThuc = new Label();
            radOnline = new RadioButton();
            radOffline = new RadioButton();
            lblSoThang = new Label();
            numSoThang = new NumericUpDown();
            lblHocPhiThang = new Label();
            lblGiamGia = new Label();
            lblTongTienText = new Label();
            lblTongTien = new Label();
            pnlNutLenh = new Panel();
            btnDangKy = new Button();
            btnLamMoi = new Button();
            btnThoat = new Button();

            grpHocVien.SuspendLayout();
            grpKhoaHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoThang).BeginInit();
            pnlNutLenh.SuspendLayout();
            SuspendLayout();

            // lblTieuDe
            lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            lblTieuDe.Location = new System.Drawing.Point(0, 40);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new System.Drawing.Size(920, 45);
            lblTieuDe.Text = "ĐĂNG KÝ KHÓA HỌC NGẮN HẠN";
            lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // grpHocVien
            grpHocVien.BackColor = System.Drawing.Color.FromArgb(184, 204, 232);
            grpHocVien.Font = new System.Drawing.Font("Times New Roman", 12F);
            grpHocVien.Location = new System.Drawing.Point(30, 145);
            grpHocVien.Name = "grpHocVien";
            grpHocVien.Size = new System.Drawing.Size(405, 285);
            grpHocVien.TabIndex = 0;
            grpHocVien.TabStop = false;
            grpHocVien.Text = "Thông tin học viên";
            grpHocVien.Controls.Add(lblHoTen);
            grpHocVien.Controls.Add(txtHoTen);
            grpHocVien.Controls.Add(lblDemKyTu);
            grpHocVien.Controls.Add(lblSoDienThoai);
            grpHocVien.Controls.Add(txtSoDienThoai);
            grpHocVien.Controls.Add(lblNgaySinh);
            grpHocVien.Controls.Add(dtpNgaySinh);
            grpHocVien.Controls.Add(chkNhanEmail);
            grpHocVien.Controls.Add(lblTrangThaiEmail);

            // lblHoTen
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new System.Drawing.Point(28, 55);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Text = "Họ tên:";

            // txtHoTen
            txtHoTen.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtHoTen.Location = new System.Drawing.Point(118, 50);
            txtHoTen.MaxLength = 50;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new System.Drawing.Size(170, 27);
            txtHoTen.TabIndex = 0;
            txtHoTen.TextChanged += txtHoTen_TextChanged;

            // lblDemKyTu
            lblDemKyTu.AutoSize = true;
            lblDemKyTu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblDemKyTu.ForeColor = System.Drawing.Color.FromArgb(220, 60, 60);
            lblDemKyTu.Location = new System.Drawing.Point(320, 54);
            lblDemKyTu.Name = "lblDemKyTu";
            lblDemKyTu.Text = "0/50";

            // lblSoDienThoai
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Location = new System.Drawing.Point(28, 105);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Text = "SĐT:";

            // txtSoDienThoai
            txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtSoDienThoai.Location = new System.Drawing.Point(118, 100);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.Size = new System.Drawing.Size(170, 27);
            txtSoDienThoai.TabIndex = 1;

            // lblNgaySinh
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new System.Drawing.Point(28, 155);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Text = "Ngày sinh:";

            // dtpNgaySinh
            dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11F);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new System.Drawing.Point(118, 150);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new System.Drawing.Size(170, 27);
            dtpNgaySinh.TabIndex = 2;

            // chkNhanEmail
            chkNhanEmail.AutoSize = true;
            chkNhanEmail.Location = new System.Drawing.Point(88, 200);
            chkNhanEmail.Name = "chkNhanEmail";
            chkNhanEmail.TabIndex = 3;
            chkNhanEmail.Text = "Nhận email thông báo";
            chkNhanEmail.UseVisualStyleBackColor = true;
            chkNhanEmail.CheckedChanged += chkNhanEmail_CheckedChanged;

            // lblTrangThaiEmail
            lblTrangThaiEmail.AutoSize = true;
            lblTrangThaiEmail.ForeColor = System.Drawing.Color.Purple;
            lblTrangThaiEmail.Location = new System.Drawing.Point(88, 250);
            lblTrangThaiEmail.Name = "lblTrangThaiEmail";
            lblTrangThaiEmail.Text = "Không nhận email thông báo";

            // grpKhoaHoc
            grpKhoaHoc.BackColor = System.Drawing.Color.FromArgb(255, 240, 224);
            grpKhoaHoc.Font = new System.Drawing.Font("Times New Roman", 12F);
            grpKhoaHoc.Location = new System.Drawing.Point(500, 145);
            grpKhoaHoc.Name = "grpKhoaHoc";
            grpKhoaHoc.Size = new System.Drawing.Size(390, 285);
            grpKhoaHoc.TabIndex = 1;
            grpKhoaHoc.TabStop = false;
            grpKhoaHoc.Text = "Thông tin khóa học";
            grpKhoaHoc.Controls.Add(lblKhoaHoc);
            grpKhoaHoc.Controls.Add(cboKhoaHoc);
            grpKhoaHoc.Controls.Add(lblHinhThuc);
            grpKhoaHoc.Controls.Add(radOnline);
            grpKhoaHoc.Controls.Add(radOffline);
            grpKhoaHoc.Controls.Add(lblSoThang);
            grpKhoaHoc.Controls.Add(numSoThang);
            grpKhoaHoc.Controls.Add(lblHocPhiThang);
            grpKhoaHoc.Controls.Add(lblGiamGia);
            grpKhoaHoc.Controls.Add(lblTongTienText);
            grpKhoaHoc.Controls.Add(lblTongTien);

            // lblKhoaHoc
            lblKhoaHoc.AutoSize = true;
            lblKhoaHoc.Location = new System.Drawing.Point(30, 62);
            lblKhoaHoc.Name = "lblKhoaHoc";
            lblKhoaHoc.Text = "Khóa học:";

            // cboKhoaHoc
            cboKhoaHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoaHoc.Font = new System.Drawing.Font("Segoe UI", 11F);
            cboKhoaHoc.Location = new System.Drawing.Point(140, 57);
            cboKhoaHoc.Name = "cboKhoaHoc";
            cboKhoaHoc.Size = new System.Drawing.Size(180, 28);
            cboKhoaHoc.TabIndex = 0;
            cboKhoaHoc.SelectedIndexChanged += cboKhoaHoc_SelectedIndexChanged;

            // lblHinhThuc
            lblHinhThuc.AutoSize = true;
            lblHinhThuc.Location = new System.Drawing.Point(30, 112);
            lblHinhThuc.Name = "lblHinhThuc";
            lblHinhThuc.Text = "Hình thức học:";

            // radOnline
            radOnline.AutoSize = true;
            radOnline.Checked = true;
            radOnline.Location = new System.Drawing.Point(150, 111);
            radOnline.Name = "radOnline";
            radOnline.TabIndex = 1;
            radOnline.TabStop = true;
            radOnline.Text = "Online";
            radOnline.UseVisualStyleBackColor = true;

            // radOffline
            radOffline.AutoSize = true;
            radOffline.Location = new System.Drawing.Point(255, 111);
            radOffline.Name = "radOffline";
            radOffline.TabIndex = 2;
            radOffline.Text = "Trực tiếp";
            radOffline.UseVisualStyleBackColor = true;

            // lblSoThang
            lblSoThang.AutoSize = true;
            lblSoThang.Location = new System.Drawing.Point(30, 162);
            lblSoThang.Name = "lblSoThang";
            lblSoThang.Text = "Số tháng:";

            // numSoThang
            numSoThang.Font = new System.Drawing.Font("Segoe UI", 11F);
            numSoThang.Location = new System.Drawing.Point(140, 157);
            numSoThang.Name = "numSoThang";
            numSoThang.Size = new System.Drawing.Size(130, 27);
            numSoThang.TabIndex = 3;
            numSoThang.ValueChanged += numSoThang_ValueChanged;

            // lblHocPhiThang
            lblHocPhiThang.AutoSize = true;
            lblHocPhiThang.Location = new System.Drawing.Point(30, 212);
            lblHocPhiThang.Name = "lblHocPhiThang";
            lblHocPhiThang.Text = "0 VND";

            // lblGiamGia
            lblGiamGia.AutoSize = true;
            lblGiamGia.Location = new System.Drawing.Point(190, 212);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Text = "0 VND";

            // lblTongTienText
            lblTongTienText.AutoSize = true;
            lblTongTienText.Location = new System.Drawing.Point(60, 252);
            lblTongTienText.Name = "lblTongTienText";
            lblTongTienText.Text = "Tổng tiền:";

            // lblTongTien
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            lblTongTien.ForeColor = System.Drawing.Color.Green;
            lblTongTien.Location = new System.Drawing.Point(190, 252);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Text = "0 VND";

            // pnlNutLenh
            pnlNutLenh.BorderStyle = BorderStyle.FixedSingle;
            pnlNutLenh.Location = new System.Drawing.Point(70, 465);
            pnlNutLenh.Name = "pnlNutLenh";
            pnlNutLenh.Size = new System.Drawing.Size(785, 90);
            pnlNutLenh.TabIndex = 2;
            pnlNutLenh.Controls.Add(btnDangKy);
            pnlNutLenh.Controls.Add(btnLamMoi);
            pnlNutLenh.Controls.Add(btnThoat);

            // btnDangKy
            btnDangKy.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            btnDangKy.ForeColor = System.Drawing.Color.White;
            btnDangKy.Location = new System.Drawing.Point(30, 15);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new System.Drawing.Size(190, 55);
            btnDangKy.TabIndex = 0;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += btnDangKy_Click;

            // btnLamMoi
            btnLamMoi.BackColor = System.Drawing.Color.FromArgb(0, 0, 192);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            btnLamMoi.ForeColor = System.Drawing.Color.White;
            btnLamMoi.Location = new System.Drawing.Point(298, 15);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new System.Drawing.Size(190, 55);
            btnLamMoi.TabIndex = 1;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;

            // btnThoat
            btnThoat.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            btnThoat.ForeColor = System.Drawing.Color.White;
            btnThoat.Location = new System.Drawing.Point(568, 15);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new System.Drawing.Size(190, 55);
            btnThoat.TabIndex = 2;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;

            // Form1
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(920, 580);
            Controls.Add(lblTieuDe);
            Controls.Add(grpHocVien);
            Controls.Add(grpKhoaHoc);
            Controls.Add(pnlNutLenh);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ĐĂNG KÝ KHÓA HỌC";
            Load += Form1_Load;

            grpHocVien.ResumeLayout(false);
            grpHocVien.PerformLayout();
            grpKhoaHoc.ResumeLayout(false);
            grpKhoaHoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoThang).EndInit();
            pnlNutLenh.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTieuDe;
        private GroupBox grpHocVien;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblDemKyTu;
        private Label lblSoDienThoai;
        private TextBox txtSoDienThoai;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private CheckBox chkNhanEmail;
        private Label lblTrangThaiEmail;
        private GroupBox grpKhoaHoc;
        private Label lblKhoaHoc;
        private ComboBox cboKhoaHoc;
        private Label lblHinhThuc;
        private RadioButton radOnline;
        private RadioButton radOffline;
        private Label lblSoThang;
        private NumericUpDown numSoThang;
        private Label lblHocPhiThang;
        private Label lblGiamGia;
        private Label lblTongTienText;
        private Label lblTongTien;
        private Panel pnlNutLenh;
        private Button btnDangKy;
        private Button btnLamMoi;
        private Button btnThoat;
    }
}
