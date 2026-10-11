using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    /// <summary>
    /// Quản lý sản phẩm: CRUD trong bộ nhớ, hiển thị bằng DataGridView.
    /// </summary>
    public class FrmProduct : Form
    {
        private readonly BindingList<Product> _products;

        private GroupBox grpInfo;
        private Label lblId, lblName, lblCategory, lblPrice, lblQuantity, lblSearch;
        private TextBox txtId, txtName, txtPrice, txtQuantity, txtSearch;
        private ComboBox cboCategory;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnSearch;
        private Panel pnlButtons, pnlSearch;
        private DataGridView dgvProducts;

        public FrmProduct(BindingList<Product> products)
        {
            _products = products;
            InitializeUI();
            ShowAll();
        }

        private void InitializeUI()
        {
            Text = "Quản lý sản phẩm";
            Font = new Font("Segoe UI", 10F);
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(820, 520);

            // ===== Nhóm thông tin sản phẩm =====
            grpInfo = new GroupBox
            {
                Name = "grpInfo",
                Text = "Thông tin sản phẩm",
                Dock = DockStyle.Top,
                Height = 130
            };

            lblId = new Label { Name = "lblId", Text = "Mã sản phẩm:", Location = new Point(20, 33), AutoSize = true };
            txtId = new TextBox { Name = "txtId", Location = new Point(145, 30), Size = new Size(195, 25) };

            lblName = new Label { Name = "lblName", Text = "Tên sản phẩm:", Location = new Point(20, 78), AutoSize = true };
            txtName = new TextBox { Name = "txtName", Location = new Point(145, 75), Size = new Size(195, 25) };

            lblCategory = new Label { Name = "lblCategory", Text = "Loại sản phẩm:", Location = new Point(375, 33), AutoSize = true };
            cboCategory = new ComboBox
            {
                Name = "cboCategory",
                Location = new Point(510, 30),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboCategory.Items.AddRange(new object[] { "Đồ uống", "Thực phẩm", "Gia dụng", "Văn phòng phẩm", "Khác" });
            cboCategory.SelectedIndex = 0;

            lblPrice = new Label { Name = "lblPrice", Text = "Đơn giá:", Location = new Point(375, 78), AutoSize = true };
            txtPrice = new TextBox { Name = "txtPrice", Location = new Point(510, 75), Size = new Size(90, 25) };

            lblQuantity = new Label { Name = "lblQuantity", Text = "Số lượng:", Location = new Point(620, 78), AutoSize = true };
            txtQuantity = new TextBox { Name = "txtQuantity", Location = new Point(705, 75), Size = new Size(85, 25) };

            grpInfo.Controls.AddRange(new Control[]
            {
                lblId, txtId, lblName, txtName, lblCategory, cboCategory,
                lblPrice, txtPrice, lblQuantity, txtQuantity
            });

            // ===== Hàng nút chức năng =====
            pnlButtons = new Panel { Name = "pnlButtons", Dock = DockStyle.Top, Height = 50 };
            btnAdd = MakeButton("btnAdd", "Thêm", 20);
            btnEdit = MakeButton("btnEdit", "Sửa", 140);
            btnDelete = MakeButton("btnDelete", "Xóa", 260);
            btnRefresh = MakeButton("btnRefresh", "Làm mới", 380);
            pnlButtons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

            // ===== Hàng tìm kiếm =====
            pnlSearch = new Panel { Name = "pnlSearch", Dock = DockStyle.Top, Height = 45 };
            lblSearch = new Label { Name = "lblSearch", Text = "Tìm kiếm (mã/tên):", Location = new Point(20, 13), AutoSize = true };
            txtSearch = new TextBox { Name = "txtSearch", Location = new Point(180, 10), Size = new Size(250, 25) };
            btnSearch = MakeButton("btnSearch", "Tìm kiếm", 445);
            pnlSearch.Controls.AddRange(new Control[] { lblSearch, txtSearch, btnSearch });

            // ===== DataGridView =====
            dgvProducts = new DataGridView
            {
                Name = "dgvProducts",
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Mã SP", Name = "colId", FillWeight = 15 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Tên sản phẩm", Name = "colName", FillWeight = 35 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Loại", Name = "colCategory", FillWeight = 20 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Đơn giá",
                Name = "colPrice",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số lượng",
                Name = "colQuantity",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            // Thứ tự add: Fill trước, các control Top sau (control add sau nằm trên cùng)
            Controls.Add(dgvProducts);
            Controls.Add(pnlSearch);
            Controls.Add(pnlButtons);
            Controls.Add(grpInfo);

            // ===== Sự kiện =====
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnSearch.Click += btnSearch_Click;
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { btnSearch.PerformClick(); e.SuppressKeyPress = true; }
            };
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        }

        private Button MakeButton(string name, string text, int x)
        {
            return new Button
            {
                Name = name,
                Text = text,
                Location = new Point(x, 7),
                Size = new Size(110, 32)
            };
        }

        // ================= Hiển thị =================

        private void ShowAll()
        {
            dgvProducts.DataSource = _products;
            _products.ResetBindings();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            cboCategory.SelectedIndex = 0;
            txtId.Focus();
        }

        private Product FindById(string id)
        {
            return _products.FirstOrDefault(p => string.Equals(p.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            Product p = dgvProducts.CurrentRow.DataBoundItem as Product;
            if (p == null) return;

            txtId.Text = p.Id;
            txtName.Text = p.Name;
            cboCategory.SelectedItem = p.Category;
            txtPrice.Text = p.Price.ToString("0.##");
            txtQuantity.Text = p.Quantity.ToString();
        }

        // ================= Kiểm tra dữ liệu nhập =================

        /// <summary>Kiểm tra dữ liệu nhập; hợp lệ thì trả về giá và số lượng đã chuyển kiểu.</summary>
        private bool ValidateInput(out decimal price, out int quantity)
        {
            price = 0;
            quantity = 0;

            if (txtId.Text.Trim() == "")
            {
                Warn("Vui lòng nhập mã sản phẩm!", txtId);
                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                Warn("Vui lòng nhập tên sản phẩm!", txtName);
                return false;
            }
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
            {
                Warn("Đơn giá phải là một số hợp lệ!", txtPrice);
                return false;
            }
            if (price < 0)
            {
                Warn("Đơn giá không được âm!", txtPrice);
                return false;
            }
            if (!int.TryParse(txtQuantity.Text.Trim(), out quantity))
            {
                Warn("Số lượng phải là một số nguyên hợp lệ!", txtQuantity);
                return false;
            }
            if (quantity < 0)
            {
                Warn("Số lượng không được âm!", txtQuantity);
                return false;
            }
            return true;
        }

        private void Warn(string message, Control focusControl)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focusControl.Focus();
        }

        // ================= Các chức năng =================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal price, out int quantity)) return;

            // Không cho thêm trùng mã
            if (FindById(txtId.Text.Trim()) != null)
            {
                Warn("Mã sản phẩm đã tồn tại!", txtId);
                return;
            }

            _products.Add(new Product(txtId.Text.Trim(), txtName.Text.Trim(),
                cboCategory.SelectedItem.ToString(), price, quantity));
            ShowAll();
            ClearInputs();
            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal price, out int quantity)) return;

            Product p = FindById(txtId.Text.Trim());
            if (p == null)
            {
                Warn("Không tìm thấy sản phẩm có mã này để sửa!", txtId);
                return;
            }

            p.Name = txtName.Text.Trim();
            p.Category = cboCategory.SelectedItem.ToString();
            p.Price = price;
            p.Quantity = quantity;
            ShowAll();
            MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product p = FindById(txtId.Text.Trim());
            if (p == null)
            {
                Warn("Vui lòng chọn sản phẩm cần xóa!", txtId);
                return;
            }

            // Xóa phải hỏi xác nhận
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm \"" + p.Name + "\"?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            _products.Remove(p);
            ShowAll();
            ClearInputs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ShowAll();
            ClearInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (keyword == "")
            {
                ShowAll();
                return;
            }

            var result = _products.Where(p =>
                p.Id.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm phù hợp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvProducts.DataSource = new BindingList<Product>(result);
        }
    }
}
