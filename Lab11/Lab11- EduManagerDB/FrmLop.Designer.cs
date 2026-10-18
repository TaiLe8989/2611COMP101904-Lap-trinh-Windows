namespace COMP1019_Buoi11_Lab11
{
    partial class FrmLop
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaLop = new System.Windows.Forms.Label();
            this.txtMaLop = new System.Windows.Forms.TextBox();
            this.lblTenLop = new System.Windows.Forms.Label();
            this.txtTenLop = new System.Windows.Forms.TextBox();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.cboKhoa = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvLop = new System.Windows.Forms.DataGridView();
            this.grpThongTin.SuspendLayout();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).BeginInit();
            this.SuspendLayout();
            //
            // lblTieuDe
            //
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(784, 50);
            this.lblTieuDe.Text = "QUẢN LÝ LỚP HỌC";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // grpThongTin
            //
            this.grpThongTin.Controls.Add(this.lblMaLop);
            this.grpThongTin.Controls.Add(this.txtMaLop);
            this.grpThongTin.Controls.Add(this.lblTenLop);
            this.grpThongTin.Controls.Add(this.txtTenLop);
            this.grpThongTin.Controls.Add(this.lblKhoa);
            this.grpThongTin.Controls.Add(this.cboKhoa);
            this.grpThongTin.Controls.Add(this.btnThem);
            this.grpThongTin.Controls.Add(this.btnSua);
            this.grpThongTin.Controls.Add(this.btnXoa);
            this.grpThongTin.Controls.Add(this.btnLamMoi);
            this.grpThongTin.Location = new System.Drawing.Point(15, 55);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(754, 150);
            this.grpThongTin.Text = "Thông tin lớp học";
            //
            // lblMaLop
            //
            this.lblMaLop.AutoSize = true;
            this.lblMaLop.Location = new System.Drawing.Point(20, 33);
            this.lblMaLop.Name = "lblMaLop";
            this.lblMaLop.Text = "Mã lớp:";
            //
            // txtMaLop
            //
            this.txtMaLop.Location = new System.Drawing.Point(100, 30);
            this.txtMaLop.MaxLength = 20;
            this.txtMaLop.Name = "txtMaLop";
            this.txtMaLop.Size = new System.Drawing.Size(200, 25);
            this.txtMaLop.TabIndex = 0;
            //
            // lblTenLop
            //
            this.lblTenLop.AutoSize = true;
            this.lblTenLop.Location = new System.Drawing.Point(330, 33);
            this.lblTenLop.Name = "lblTenLop";
            this.lblTenLop.Text = "Tên lớp:";
            //
            // txtTenLop
            //
            this.txtTenLop.Location = new System.Drawing.Point(410, 30);
            this.txtTenLop.MaxLength = 100;
            this.txtTenLop.Name = "txtTenLop";
            this.txtTenLop.Size = new System.Drawing.Size(325, 25);
            this.txtTenLop.TabIndex = 1;
            //
            // lblKhoa
            //
            this.lblKhoa.AutoSize = true;
            this.lblKhoa.Location = new System.Drawing.Point(20, 73);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Text = "Khoa:";
            //
            // cboKhoa
            //
            this.cboKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoa.Location = new System.Drawing.Point(100, 70);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new System.Drawing.Size(200, 25);
            this.cboKhoa.TabIndex = 2;
            //
            // btnThem
            //
            this.btnThem.Location = new System.Drawing.Point(100, 105);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            //
            // btnSua
            //
            this.btnSua.Location = new System.Drawing.Point(215, 105);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.TabIndex = 4;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            //
            // btnXoa
            //
            this.btnXoa.Location = new System.Drawing.Point(330, 105);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Location = new System.Drawing.Point(445, 105);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.TabIndex = 6;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // grpTimKiem
            //
            this.grpTimKiem.Controls.Add(this.txtTimKiem);
            this.grpTimKiem.Controls.Add(this.btnTimKiem);
            this.grpTimKiem.Location = new System.Drawing.Point(15, 215);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(754, 65);
            this.grpTimKiem.Text = "Tìm kiếm (theo mã hoặc tên lớp)";
            //
            // txtTimKiem
            //
            this.txtTimKiem.Location = new System.Drawing.Point(20, 27);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(580, 25);
            this.txtTimKiem.TabIndex = 7;
            //
            // btnTimKiem
            //
            this.btnTimKiem.Location = new System.Drawing.Point(615, 24);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(120, 32);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            //
            // dgvLop
            //
            this.dgvLop.AllowUserToAddRows = false;
            this.dgvLop.AllowUserToDeleteRows = false;
            this.dgvLop.AllowUserToResizeRows = false;
            this.dgvLop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLop.BackgroundColor = System.Drawing.Color.White;
            this.dgvLop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLop.Location = new System.Drawing.Point(15, 290);
            this.dgvLop.MultiSelect = false;
            this.dgvLop.Name = "dgvLop";
            this.dgvLop.ReadOnly = true;
            this.dgvLop.RowHeadersVisible = false;
            this.dgvLop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLop.Size = new System.Drawing.Size(754, 261);
            this.dgvLop.TabIndex = 9;
            this.dgvLop.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLop_CellClick);
            //
            // FrmLop
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dgvLop);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmLop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMP1019 - Lab 11 - Quản lý lớp học";
            this.Load += new System.EventHandler(this.FrmLop_Load);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.Label lblMaLop;
        private System.Windows.Forms.TextBox txtMaLop;
        private System.Windows.Forms.Label lblTenLop;
        private System.Windows.Forms.TextBox txtTenLop;
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.ComboBox cboKhoa;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvLop;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
    }
}
