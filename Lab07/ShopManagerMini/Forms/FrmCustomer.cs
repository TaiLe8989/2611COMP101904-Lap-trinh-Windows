using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{

    public class FrmCustomer : Form
    {
        private readonly BindingList<Customer> _customers;

        private GroupBox grpInfo;
        private Label lblId, lblFullName, lblPhone, lblType, lblSearch;
        private TextBox txtId, txtFullName, txtPhone, txtSearch;
        private ComboBox cboType;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnSearch;
        private Panel pnlButtons, pnlSearch;
        private DataGridView dgvCustomers;

        public FrmCustomer(BindingList<Customer> customers)
        {
            _customers = customers;
            InitializeUI();
            ShowAll();
        }

        private void InitializeUI()
        {
            Text = "Quản lý khách hàng";
            Font = new Font("Segoe UI", 10F);
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(820, 520);

          
            grpInfo = new GroupBox
            {
                Name = "grpInfo",
                Text = "Thông tin khách hàng",
                Dock = DockStyle.Top,
                Height = 130
            };

            lblId = new Label { Name = "lblId", Text = "Mã khách hàng:", Location = new Point(20, 33), AutoSize = true };
            txtId = new TextBox { Name = "txtId", Location = new Point(150, 30), Size = new Size(195, 25) };

            lblFullName = new Label { Name = "lblFullName", Text = "Họ tên:", Location = new Point(20, 78), AutoSize = true };
            txtFullName = new TextBox { Name = "txtFullName", Location = new Point(150, 75), Size = new Size(195, 25) };

            lblPhone = new Label { Name = "lblPhone", Text = "Số điện thoại:", Location = new Point(385, 33), AutoSize = true };
            txtPhone = new TextBox { Name = "txtPhone", Location = new Point(540, 30), Size = new Size(200, 25) };

            lblType = new Label { Name = "lblType", Text = "Loại khách hàng:", Location = new Point(385, 78), AutoSize = true };
            cboType = new ComboBox
            {
                Name = "cboType",
                Location = new Point(540, 75),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboType.Items.AddRange(new object[] { "Thường", "Thân thiết", "VIP" });
            cboType.SelectedIndex = 0;

            grpInfo.Controls.AddRange(new Control[]
            {
                lblId, txtId, lblFullName, txtFullName, lblPhone, txtPhone, lblType, cboType
            });

  
            pnlButtons = new Panel { Name = "pnlButtons", Dock = DockStyle.Top, Height = 50 };
            btnAdd = MakeButton("btnAdd", "Thêm", 20);
            btnEdit = MakeButton("btnEdit", "Sửa", 140);
            btnDelete = MakeButton("btnDelete", "Xóa", 260);
            btnRefresh = MakeButton("btnRefresh", "Làm mới", 380);
            pnlButtons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

       
            pnlSearch = new Panel { Name = "pnlSearch", Dock = DockStyle.Top, Height = 45 };
            lblSearch = new Label { Name = "lblSearch", Text = "Tìm kiếm (mã/tên/SĐT):", Location = new Point(20, 13), AutoSize = true };
            txtSearch = new TextBox { Name = "txtSearch", Location = new Point(210, 10), Size = new Size(230, 25) };
            btnSearch = MakeButton("btnSearch", "Tìm kiếm", 455);
            pnlSearch.Controls.AddRange(new Control[] { lblSearch, txtSearch, btnSearch });


            dgvCustomers = new DataGridView
            {
                Name = "dgvCustomers",
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
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Mã KH", Name = "colId", FillWeight = 15 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Họ tên", Name = "colFullName", FillWeight = 35 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Phone", HeaderText = "Số điện thoại", Name = "colPhone", FillWeight = 25 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerType", HeaderText = "Loại khách hàng", Name = "colType", FillWeight = 25 });

            Controls.Add(dgvCustomers);
            Controls.Add(pnlSearch);
            Controls.Add(pnlButtons);
            Controls.Add(grpInfo);


            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnSearch.Click += btnSearch_Click;
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { btnSearch.PerformClick(); e.SuppressKeyPress = true; }
            };
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
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

   

        private void ShowAll()
        {
            dgvCustomers.DataSource = _customers;
            _customers.ResetBindings();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            cboType.SelectedIndex = 0;
            txtId.Focus();
        }

        private Customer FindById(string id)
        {
            return _customers.FirstOrDefault(c => string.Equals(c.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;
            Customer c = dgvCustomers.CurrentRow.DataBoundItem as Customer;
            if (c == null) return;

            txtId.Text = c.Id;
            txtFullName.Text = c.FullName;
            txtPhone.Text = c.Phone;
            cboType.SelectedItem = c.CustomerType;
        }

    

        private bool ValidateInput()
        {
            if (txtId.Text.Trim() == "")
            {
                Warn("Vui lòng nhập mã khách hàng!", txtId);
                return false;
            }
            if (txtFullName.Text.Trim() == "")
            {
                Warn("Vui lòng nhập họ tên khách hàng!", txtFullName);
                return false;
            }

            string phone = txtPhone.Text.Trim();
            if (phone == "" || !phone.All(char.IsDigit) || phone.Length < 9 || phone.Length > 11)
            {
                Warn("Số điện thoại phải gồm 9 đến 11 chữ số!", txtPhone);
                return false;
            }
            return true;
        }

        private void Warn(string message, Control focusControl)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focusControl.Focus();
        }

     

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (FindById(txtId.Text.Trim()) != null)
            {
                Warn("Mã khách hàng đã tồn tại!", txtId);
                return;
            }

            _customers.Add(new Customer(txtId.Text.Trim(), txtFullName.Text.Trim(),
                txtPhone.Text.Trim(), cboType.SelectedItem.ToString()));
            ShowAll();
            ClearInputs();
            MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            Customer c = FindById(txtId.Text.Trim());
            if (c == null)
            {
                Warn("Không tìm thấy khách hàng có mã này để sửa!", txtId);
                return;
            }

            c.FullName = txtFullName.Text.Trim();
            c.Phone = txtPhone.Text.Trim();
            c.CustomerType = cboType.SelectedItem.ToString();
            ShowAll();
            MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Customer c = FindById(txtId.Text.Trim());
            if (c == null)
            {
                Warn("Vui lòng chọn khách hàng cần xóa!", txtId);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa khách hàng \"" + c.FullName + "\"?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            _customers.Remove(c);
            ShowAll();
            ClearInputs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ShowAll();
            ClearInputs();
        }

        private void InitializeComponent()
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (keyword == "")
            {
                ShowAll();
                return;
            }

            var result = _customers.Where(c =>
                c.Id.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                c.FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                c.Phone.Contains(keyword)).ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng phù hợp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvCustomers.DataSource = new BindingList<Customer>(result);
        }
    }
}
