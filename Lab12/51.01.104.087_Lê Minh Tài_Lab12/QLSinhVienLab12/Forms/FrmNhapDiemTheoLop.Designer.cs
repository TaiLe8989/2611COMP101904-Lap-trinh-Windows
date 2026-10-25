namespace QLSinhVienLab12.Forms
{
    partial class FrmNhapDiemTheoLop
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpChon = new GroupBox();
            lblLop = new Label();
            cboLop = new ComboBox();
            lblMonHoc = new Label();
            cboMonHoc = new ComboBox();
            btnTaiDanhSach = new Button();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            dgvKetQua = new DataGridView();
            lblThongKe = new Label();
            btnLuuTatCa = new Button();
            statusStrip = new StatusStrip();
            tsslTrangThai = new ToolStripStatusLabel();
            grpChon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();

            grpChon.Controls.Add(lblLop);
            grpChon.Controls.Add(cboLop);
            grpChon.Controls.Add(lblMonHoc);
            grpChon.Controls.Add(cboMonHoc);
            grpChon.Controls.Add(btnTaiDanhSach);
            grpChon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpChon.Location = new Point(12, 12);
            grpChon.Name = "grpChon";
            grpChon.Size = new Size(836, 76);
            grpChon.TabStop = false;
            grpChon.Text = "Chọn lớp và môn học";

            lblLop.AutoSize = true;
            lblLop.Location = new Point(16, 33);
            lblLop.Name = "lblLop";
            lblLop.Text = "Lớp:";

            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(60, 29);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(220, 28);

            lblMonHoc.AutoSize = true;
            lblMonHoc.Location = new Point(300, 33);
            lblMonHoc.Name = "lblMonHoc";
            lblMonHoc.Text = "Môn học:";

            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.Location = new Point(372, 29);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(250, 28);

            btnTaiDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaiDanhSach.Location = new Point(646, 26);
            btnTaiDanhSach.Name = "btnTaiDanhSach";
            btnTaiDanhSach.Size = new Size(172, 34);
            btnTaiDanhSach.Text = "Tải danh sách";
            btnTaiDanhSach.UseVisualStyleBackColor = true;
            btnTaiDanhSach.Click += btnTaiDanhSach_Click;

            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(14, 105);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Text = "Tìm kiếm:";

            txtTimKiem.Location = new Point(90, 101);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập mã hoặc tên sinh viên";
            txtTimKiem.Size = new Size(280, 27);
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            dgvKetQua.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKetQua.Location = new Point(12, 138);
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.Size = new Size(836, 316);
            dgvKetQua.CellFormatting += dgvKetQua_CellFormatting;
            dgvKetQua.CellMouseEnter += dgvKetQua_CellMouseEnter;
            dgvKetQua.MouseLeave += dgvKetQua_MouseLeave;
            dgvKetQua.CellParsing += dgvKetQua_CellParsing;
            dgvKetQua.CellValidating += dgvKetQua_CellValidating;
            dgvKetQua.CellEndEdit += dgvKetQua_CellEndEdit;
            dgvKetQua.DataError += dgvKetQua_DataError;

            lblThongKe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblThongKe.AutoSize = true;
            lblThongKe.Location = new Point(14, 474);
            lblThongKe.Name = "lblThongKe";
            lblThongKe.Text = "Chưa có dữ liệu thống kê";

            btnLuuTatCa.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLuuTatCa.Location = new Point(688, 466);
            btnLuuTatCa.Name = "btnLuuTatCa";
            btnLuuTatCa.Size = new Size(160, 38);
            btnLuuTatCa.Text = "Lưu tất cả";
            btnLuuTatCa.UseVisualStyleBackColor = true;
            btnLuuTatCa.Click += btnLuuTatCa_Click;

            statusStrip.Items.AddRange(new ToolStripItem[] { tsslTrangThai });
            statusStrip.Name = "statusStrip";

            tsslTrangThai.Name = "tsslTrangThai";
            tsslTrangThai.Text = "Sẵn sàng";

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 540);
            Controls.Add(grpChon);
            Controls.Add(lblTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(dgvKetQua);
            Controls.Add(lblThongKe);
            Controls.Add(btnLuuTatCa);
            Controls.Add(statusStrip);
            MinimumSize = new Size(760, 480);
            Name = "FrmNhapDiemTheoLop";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý kết quả học tập - Nhập điểm theo lớp";
            Load += FrmNhapDiemTheoLop_Load;
            grpChon.ResumeLayout(false);
            grpChon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private GroupBox grpChon;
        private Label lblLop;
        private ComboBox cboLop;
        private Label lblMonHoc;
        private ComboBox cboMonHoc;
        private Button btnTaiDanhSach;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private DataGridView dgvKetQua;
        private Label lblThongKe;
        private Button btnLuuTatCa;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tsslTrangThai;
    }
}
