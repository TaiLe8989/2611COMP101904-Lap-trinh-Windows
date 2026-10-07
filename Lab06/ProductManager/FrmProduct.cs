using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProductManager
{
    public partial class FrmProduct : Form
    {
        // ===== Dữ liệu trong bộ nhớ =====
        private readonly BindingList<Product> products = new BindingList<Product>();
        private readonly BindingSource productSource = new BindingSource();
        private string selectedImagePath = string.Empty;
        private Product selectedProduct = null;   // sản phẩm đang được chọn để sửa/xóa

        public FrmProduct()
        {
            InitializeComponent();
        }

        // ===================== LOAD FORM =====================
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });
            cboLoaiSP.SelectedIndex = 0;

            LoadSampleData();
            SetupGrid();

            productSource.DataSource = products;
            dgvProducts.DataSource = productSource;

            // Xóa lỗi ErrorProvider ngay khi người dùng sửa lại ô nhập
            txtMaSP.TextChanged += ClearErrorOnChange;
            txtTenSP.TextChanged += ClearErrorOnChange;
            numDonGia.ValueChanged += ClearErrorOnChange;
            dtpNgayNhap.ValueChanged += ClearErrorOnChange;

            ClearInput();
            UpdateStatus("Sẵn sàng");
        }

        private void LoadSampleData()
        {
            products.Add(new Product
            {
                MaSP = "SP01", TenSP = "Chuột không dây", LoaiSP = "Điện tử",
                DonGia = 150000, SoLuong = 20, NgayNhap = DateTime.Today.AddDays(-10), ConKinhDoanh = true
            });
            products.Add(new Product
            {
                MaSP = "SP02", TenSP = "Bút bi", LoaiSP = "Văn phòng",
                DonGia = 5000, SoLuong = 100, NgayNhap = DateTime.Today.AddDays(-5), ConKinhDoanh = true
            });
            products.Add(new Product
            {
                MaSP = "SP03", TenSP = "Nồi cơm điện", LoaiSP = "Gia dụng",
                DonGia = 850000, SoLuong = 8, NgayNhap = DateTime.Today.AddDays(-3), ConKinhDoanh = true
            });
            products.Add(new Product
            {
                MaSP = "SP04", TenSP = "Áo thun", LoaiSP = "Thời trang",
                DonGia = 120000, SoLuong = 0, NgayNhap = DateTime.Today, ConKinhDoanh = false
            });
        }

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.FixedSingle;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersHeight = 34;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 246, 252);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 214, 153);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProducts.RowTemplate.Height = 28;

            dgvProducts.Columns.Clear();
            AddTextColumn("MaSP", "Mã SP", 8, null, DataGridViewContentAlignment.MiddleCenter);
            AddTextColumn("TenSP", "Tên sản phẩm", 19, null, DataGridViewContentAlignment.MiddleLeft);
            AddTextColumn("LoaiSP", "Loại SP", 12, null, DataGridViewContentAlignment.MiddleCenter);
            AddTextColumn("DonGia", "Đơn giá", 12, "N0", DataGridViewContentAlignment.MiddleRight);
            AddTextColumn("SoLuong", "Số lượng", 9, "N0", DataGridViewContentAlignment.MiddleRight);
            AddTextColumn("NgayNhap", "Ngày nhập", 12, "dd/MM/yyyy", DataGridViewContentAlignment.MiddleCenter);
            dgvProducts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "ConKinhDoanh",
                Name = "colConKinhDoanh",
                HeaderText = "Còn kinh doanh",
                FillWeight = 14
            });
            AddTextColumn("ThanhTien", "Thành tiền", 14, "N0", DataGridViewContentAlignment.MiddleRight);
        }

        private void AddTextColumn(string property, string header, float weight, string format,
                                   DataGridViewContentAlignment align)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = property;
            col.Name = "col" + property;
            col.HeaderText = header;
            col.FillWeight = weight;
            col.DefaultCellStyle.Alignment = align;
            if (format != null) col.DefaultCellStyle.Format = format;
            dgvProducts.Columns.Add(col);
        }

        // ===================== THÊM =====================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;

            Product p = new Product
            {
                MaSP = txtMaSP.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                LoaiSP = cboLoaiSP.Text,
                DonGia = numDonGia.Value,
                SoLuong = (int)numSoLuong.Value,
                NgayNhap = dtpNgayNhap.Value.Date,
                ConKinhDoanh = chkConKinhDoanh.Checked,
                DuongDanAnh = selectedImagePath
            };

            products.Add(p);
            ShowAll();
            ClearInput();
            UpdateStatus("Đã thêm sản phẩm mới");
        }

        // ===================== SỬA =====================
        private void btnSua_Click(object sender, EventArgs e)
        {
            Product p = GetSelectedProduct();
            if (p == null) return;
            if (!ValidateInput(p)) return;

            p.MaSP = txtMaSP.Text.Trim();
            p.TenSP = txtTenSP.Text.Trim();
            p.LoaiSP = cboLoaiSP.Text;
            p.DonGia = numDonGia.Value;
            p.SoLuong = (int)numSoLuong.Value;
            p.NgayNhap = dtpNgayNhap.Value.Date;
            p.ConKinhDoanh = chkConKinhDoanh.Checked;
            p.DuongDanAnh = selectedImagePath;

            productSource.ResetBindings(false);
            ClearInput();
            UpdateStatus("Đã cập nhật sản phẩm");
        }

        // ===================== XÓA =====================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            Product p = GetSelectedProduct();
            if (p == null) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa sản phẩm \"{p.TenSP}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            products.Remove(p);
            if (txtTimKiem.Text.Trim().Length > 0) ApplyFilter();   // đang lọc thì lọc lại
            ClearInput();
            UpdateStatus("Đã xóa sản phẩm");
        }

        // ===================== TÌM KIẾM / LÀM MỚI =====================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim().Length == 0)
            {
                ShowAll();
                ClearInput();
                UpdateStatus("Hiển thị toàn bộ sản phẩm");
                return;
            }

            int count = ApplyFilter();
            ClearInput();
            lblStatus.Text = $"Tìm thấy {count} sản phẩm | Số sản phẩm: {products.Count}";
        }

        // Lọc theo mã hoặc tên (không phân biệt hoa/thường), trả về số kết quả
        private int ApplyFilter()
        {
            string keyword = txtTimKiem.Text.Trim();
            List<Product> result = products
                .Where(p => Contains(p.MaSP, keyword) || Contains(p.TenSP, keyword))
                .ToList();
            productSource.DataSource = result;
            return result.Count;
        }

        private static bool Contains(string text, string keyword)
        {
            return text != null && text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void ShowAll()
        {
            txtTimKiem.Clear();
            productSource.DataSource = products;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ShowAll();
            ClearInput();
            UpdateStatus("Đã làm mới dữ liệu");
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem.PerformClick();
            }
        }

        // ===================== CHỌN ẢNH =====================
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh sản phẩm";
                dialog.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialog.ShowDialog() != DialogResult.OK) return;

                if (ShowImage(dialog.FileName))
                {
                    selectedImagePath = dialog.FileName;
                    UpdateStatus("Đã chọn ảnh sản phẩm");
                }
                else
                {
                    MessageBox.Show("Không thể mở file ảnh đã chọn.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Đọc ảnh qua stream để không khóa file trên đĩa
        private bool ShowImage(string path)
        {
            Image old = picSanPham.Image;
            picSanPham.Image = null;
            if (old != null) old.Dispose();

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return false;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image img = Image.FromStream(fs))
                {
                    picSanPham.Image = new Bitmap(img);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ===================== CHỌN DÒNG TRÊN DATAGRIDVIEW =====================
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) LoadCurrentRowToForm();
        }

        private void dgvProducts_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Nhấn chuột phải: chọn dòng trước khi hiện ContextMenuStrip
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvProducts.ClearSelection();
                dgvProducts.Rows[e.RowIndex].Selected = true;
                dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[0];
                LoadCurrentRowToForm();
            }
        }

        private void dgvProducts_KeyUp(object sender, KeyEventArgs e)
        {
            // Di chuyển dòng bằng bàn phím
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageUp ||
                e.KeyCode == Keys.PageDown || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
            {
                LoadCurrentRowToForm();
            }
        }

        private void LoadCurrentRowToForm()
        {
            if (dgvProducts.CurrentRow == null) return;
            Product p = dgvProducts.CurrentRow.DataBoundItem as Product;
            if (p != null) LoadProductToForm(p);
        }

        // ===================== CONTEXT MENU =====================
        private void cmsSua_Click(object sender, EventArgs e)
        {
            Product p = GetSelectedProduct();
            if (p == null) return;
            LoadProductToForm(p);
            txtTenSP.Focus();
            UpdateStatus("Chỉnh sửa thông tin rồi nhấn nút Sửa để lưu");
        }

        private void cmsXoa_Click(object sender, EventArgs e)
        {
            btnXoa.PerformClick();
        }

        private void cmsChiTiet_Click(object sender, EventArgs e)
        {
            Product p = GetSelectedProduct();
            if (p == null) return;

            string anh = string.IsNullOrWhiteSpace(p.DuongDanAnh) ? "(chưa có)" : p.DuongDanAnh;
            MessageBox.Show(
                $"Mã SP: {p.MaSP}\n" +
                $"Tên SP: {p.TenSP}\n" +
                $"Loại SP: {p.LoaiSP}\n" +
                $"Đơn giá: {p.DonGia:N0}\n" +
                $"Số lượng: {p.SoLuong}\n" +
                $"Ngày nhập: {p.NgayNhap:dd/MM/yyyy}\n" +
                $"Trạng thái: {(p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh")}\n" +
                $"Thành tiền: {p.ThanhTien:N0}\n" +
                $"Ảnh: {anh}",
                "Chi tiết sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===================== MENU & TOOLSTRIP (gọi lại các nút chính) =====================
        private void tsbThem_Click(object sender, EventArgs e) { btnThem.PerformClick(); }
        private void tsbSua_Click(object sender, EventArgs e) { btnSua.PerformClick(); }
        private void tsbXoa_Click(object sender, EventArgs e) { btnXoa.PerformClick(); }
        private void tsbLamMoi_Click(object sender, EventArgs e) { btnLamMoi.PerformClick(); }

        private void tsbTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.Focus();
            if (txtTimKiem.Text.Trim().Length > 0) btnTimKiem.PerformClick();
        }

        private void mnuChonAnh_Click(object sender, EventArgs e) { btnChonAnh.PerformClick(); }
        private void mnuFileExit_Click(object sender, EventArgs e) { Close(); }

        private void mnuGioiThieu_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "ProductManager - Quản lý sản phẩm\nMôn: COMP1019 - Lập trình trên Windows\nLab 06: Windows Forms nâng cao và DataGridView",
                "Giới thiệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===================== HÀM PHỤ TRỢ =====================
        // Kiểm tra dữ liệu nhập. editing = sản phẩm đang sửa (null khi thêm mới)
        private bool ValidateInput(Product editing)
        {
            errorProvider1.Clear();
            List<string> errors = new List<string>();
            Control firstError = null;

            Action<Control, string> fail = (control, message) =>
            {
                errorProvider1.SetError(control, message);
                errors.Add(message);
                if (firstError == null) firstError = control;
            };

            string ma = txtMaSP.Text.Trim();
            if (ma.Length == 0)
            {
                fail(txtMaSP, "Mã sản phẩm không được rỗng");
            }
            else if (products.Any(p => !ReferenceEquals(p, editing) &&
                                       string.Equals(p.MaSP, ma, StringComparison.OrdinalIgnoreCase)))
            {
                fail(txtMaSP, "Mã sản phẩm đã tồn tại");
            }

            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
                fail(txtTenSP, "Tên sản phẩm không được rỗng");

            if (numDonGia.Value <= 0)
                fail(numDonGia, "Đơn giá phải lớn hơn 0");

            if (numSoLuong.Value < 0)
                fail(numSoLuong, "Số lượng phải lớn hơn hoặc bằng 0");

            if (dtpNgayNhap.Value.Date > DateTime.Today)
                fail(dtpNgayNhap, "Ngày nhập không được lớn hơn ngày hiện tại");

            if (errors.Count == 0) return true;

            MessageBox.Show("Dữ liệu chưa hợp lệ:\n- " + string.Join("\n- ", errors),
                "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firstError.Focus();
            UpdateStatus("Dữ liệu nhập chưa hợp lệ");
            return false;
        }

        // Xóa nội dung nhập, bỏ lỗi ErrorProvider, bỏ chọn dòng
        private void ClearInput()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = 0;
            numDonGia.Value = 1;
            numSoLuong.Value = 0;
            dtpNgayNhap.Value = DateTime.Today;
            chkConKinhDoanh.Checked = true;
            ShowImage(null);
            selectedImagePath = string.Empty;
            selectedProduct = null;
            errorProvider1.Clear();

            dgvProducts.ClearSelection();
            dgvProducts.CurrentCell = null;
            txtMaSP.Focus();
        }

        // Đưa dữ liệu sản phẩm lên các control nhập liệu
        private void LoadProductToForm(Product p)
        {
            selectedProduct = p;
            txtMaSP.Text = p.MaSP;
            txtTenSP.Text = p.TenSP;
            cboLoaiSP.Text = p.LoaiSP;
            numDonGia.Value = Math.Min(Math.Max(p.DonGia, numDonGia.Minimum), numDonGia.Maximum);
            numSoLuong.Value = Math.Min(Math.Max(p.SoLuong, numSoLuong.Minimum), numSoLuong.Maximum);
            dtpNgayNhap.Value = p.NgayNhap;
            chkConKinhDoanh.Checked = p.ConKinhDoanh;
            selectedImagePath = p.DuongDanAnh;
            ShowImage(p.DuongDanAnh);
            errorProvider1.Clear();
        }

        // Lấy sản phẩm đang chọn; chưa chọn thì báo và trả về null (không gây lỗi)
        private Product GetSelectedProduct()
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trong danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            return selectedProduct;
        }

        // Cập nhật thanh trạng thái: thao tác + số lượng sản phẩm
        private void UpdateStatus(string message)
        {
            lblStatus.Text = $"{message} | Số sản phẩm: {products.Count}";
        }

        private void ClearErrorOnChange(object sender, EventArgs e)
        {
            errorProvider1.SetError((Control)sender, string.Empty);
        }
    }
}
