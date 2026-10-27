namespace StudentReportDemo.Forms
{
    partial class FrmBaoCaoSinhVien
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

        private void InitializeComponent()
        {
            gbDieuKien = new GroupBox();
            lblLop = new Label();
            cboLop = new ComboBox();
            lblKetQua = new Label();
            cboKetQua = new ComboBox();
            lblTuKhoa = new Label();
            txtTuKhoa = new TextBox();
            btnXemBaoCao = new Button();
            btnThongKe = new Button();
            btnXuatFile = new Button();
            dgvBaoCao = new DataGridView();
            statusStrip = new StatusStrip();
            lblThongKe = new ToolStripStatusLabel();
            gbDieuKien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();

            gbDieuKien.Controls.Add(lblLop);
            gbDieuKien.Controls.Add(cboLop);
            gbDieuKien.Controls.Add(lblKetQua);
            gbDieuKien.Controls.Add(cboKetQua);
            gbDieuKien.Controls.Add(lblTuKhoa);
            gbDieuKien.Controls.Add(txtTuKhoa);
            gbDieuKien.Controls.Add(btnXemBaoCao);
            gbDieuKien.Controls.Add(btnThongKe);
            gbDieuKien.Controls.Add(btnXuatFile);
            gbDieuKien.Dock = DockStyle.Top;
            gbDieuKien.Location = new Point(0, 0);
            gbDieuKien.Name = "gbDieuKien";
            gbDieuKien.Padding = new Padding(10);
            gbDieuKien.Size = new Size(900, 135);
            gbDieuKien.TabIndex = 0;
            gbDieuKien.TabStop = false;
            gbDieuKien.Text = "Điều kiện báo cáo";

            lblLop.AutoSize = true;
            lblLop.Location = new Point(20, 33);
            lblLop.Name = "lblLop";
            lblLop.Text = "Lớp:";

            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(105, 30);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(190, 29);
            cboLop.TabIndex = 0;

            lblKetQua.AutoSize = true;
            lblKetQua.Location = new Point(320, 33);
            lblKetQua.Name = "lblKetQua";
            lblKetQua.Text = "Kết quả:";

            cboKetQua.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKetQua.Location = new Point(395, 30);
            cboKetQua.Name = "cboKetQua";
            cboKetQua.Size = new Size(150, 29);
            cboKetQua.TabIndex = 1;

            lblTuKhoa.AutoSize = true;
            lblTuKhoa.Location = new Point(570, 33);
            lblTuKhoa.Name = "lblTuKhoa";
            lblTuKhoa.Text = "Từ khóa:";

            txtTuKhoa.Location = new Point(650, 30);
            txtTuKhoa.Name = "txtTuKhoa";
            txtTuKhoa.PlaceholderText = "Mã SV hoặc họ tên";
            txtTuKhoa.Size = new Size(230, 29);
            txtTuKhoa.TabIndex = 2;

            btnXemBaoCao.Location = new Point(105, 78);
            btnXemBaoCao.Name = "btnXemBaoCao";
            btnXemBaoCao.Size = new Size(140, 38);
            btnXemBaoCao.TabIndex = 3;
            btnXemBaoCao.Text = "Xem báo cáo";
            btnXemBaoCao.UseVisualStyleBackColor = true;
            btnXemBaoCao.Click += btnXemBaoCao_Click;

            btnThongKe.Location = new Point(260, 78);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(140, 38);
            btnThongKe.TabIndex = 4;
            btnThongKe.Text = "Thống kê";
            btnThongKe.UseVisualStyleBackColor = true;
            btnThongKe.Click += btnThongKe_Click;

            btnXuatFile.Location = new Point(415, 78);
            btnXuatFile.Name = "btnXuatFile";
            btnXuatFile.Size = new Size(140, 38);
            btnXuatFile.TabIndex = 5;
            btnXuatFile.Text = "Xuất file";
            btnXuatFile.UseVisualStyleBackColor = true;
            btnXuatFile.Click += btnXuatFile_Click;

            dgvBaoCao.AllowUserToAddRows = false;
            dgvBaoCao.AllowUserToDeleteRows = false;
            dgvBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBaoCao.BackgroundColor = SystemColors.Window;
            dgvBaoCao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBaoCao.Dock = DockStyle.Fill;
            dgvBaoCao.Location = new Point(0, 135);
            dgvBaoCao.MultiSelect = false;
            dgvBaoCao.Name = "dgvBaoCao";
            dgvBaoCao.ReadOnly = true;
            dgvBaoCao.RowHeadersVisible = false;
            dgvBaoCao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBaoCao.TabIndex = 1;

            statusStrip.Items.AddRange(new ToolStripItem[] { lblThongKe });
            statusStrip.Location = new Point(0, 478);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(900, 22);
            statusStrip.TabIndex = 2;

            lblThongKe.Name = "lblThongKe";
            lblThongKe.Text = "Sẵn sàng";

            AutoScaleDimensions = new SizeF(8F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 500);
            Controls.Add(dgvBaoCao);
            Controls.Add(gbDieuKien);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 11F);
            MinimumSize = new Size(916, 420);
            Name = "FrmBaoCaoSinhVien";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo danh sách sinh viên theo lớp";
            Load += FrmBaoCaoSinhVien_Load;
            gbDieuKien.ResumeLayout(false);
            gbDieuKien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private GroupBox gbDieuKien;
        private Label lblLop;
        private ComboBox cboLop;
        private Label lblKetQua;
        private ComboBox cboKetQua;
        private Label lblTuKhoa;
        private TextBox txtTuKhoa;
        private Button btnXemBaoCao;
        private Button btnThongKe;
        private Button btnXuatFile;
        private DataGridView dgvBaoCao;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblThongKe;
    }
}
