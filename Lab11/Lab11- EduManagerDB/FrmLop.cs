using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace COMP1019_Buoi11_Lab11
{
    public partial class FrmLop : Form
    {
    
        private readonly string connectionString =
            @"Data Source=.;Initial Catalog=EduManagerDB;Integrated Security=True;TrustServerCertificate=True";

        private const string SelectLop = @"
            SELECT l.MaLop, l.TenLop, l.MaKhoa, k.TenKhoa, COUNT(sv.MaSV) AS SoSinhVien
            FROM LOP l
            INNER JOIN KHOA k ON l.MaKhoa = k.MaKhoa
            LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop";

        private const string GroupLop = @"
            GROUP BY l.MaLop, l.TenLop, l.MaKhoa, k.TenKhoa
            ORDER BY l.MaLop";

        public FrmLop()
        {
            InitializeComponent();
        }

   
        private void FrmLop_Load(object sender, EventArgs e)
        {
            LoadKhoa();
            LoadData();
        }



        private void LoadKhoa()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand("SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY TenKhoa", conn);
                using SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(SelectLop + GroupLop, conn);
                BindGrid(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(SqlCommand cmd)
        {
            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvLop.DataSource = dt;

            dgvLop.Columns["MaLop"].HeaderText = "Mã lớp";
            dgvLop.Columns["TenLop"].HeaderText = "Tên lớp";
            dgvLop.Columns["MaKhoa"].Visible = false;
            dgvLop.Columns["TenKhoa"].HeaderText = "Khoa";
            dgvLop.Columns["SoSinhVien"].HeaderText = "Số sinh viên";
            dgvLop.ClearSelection();
        }

  

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng nhập mã lớp.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLop.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập tên lớp.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLop.Focus();
                return false;
            }

            if (cboKhoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khoa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoa.Focus();
                return false;
            }

            return true;
        }

        private bool IsMaLopExists(string maLop)
        {
            const string sql = "SELECT COUNT(*) FROM LOP WHERE MaLop = @ma";

            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ma", maLop);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private int CountSinhVienByLop(string maLop)
        {
            const string sql = "SELECT COUNT(*) FROM SINHVIEN WHERE MaLop = @ma";

            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ma", maLop);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void ClearInput()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtTimKiem.Clear();
            txtMaLop.ReadOnly = false;      // mở khóa mã lớp
            if (cboKhoa.Items.Count > 0) cboKhoa.SelectedIndex = 0;
            txtMaLop.Focus();
        }

  


        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvLop.Rows[e.RowIndex];
            txtMaLop.Text = row.Cells["MaLop"].Value.ToString();
            txtTenLop.Text = row.Cells["TenLop"].Value.ToString();
            cboKhoa.SelectedValue = row.Cells["MaKhoa"].Value.ToString();
            txtMaLop.ReadOnly = true;      
        }

 
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string ma = txtMaLop.Text.Trim();
            string ten = txtTenLop.Text.Trim();
            string khoa = cboKhoa.SelectedValue.ToString();

            try
            {
                if (IsMaLopExists(ma))
                {
                    MessageBox.Show("Mã lớp đã tồn tại.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaLop.Focus();
                    return;
                }

                const string sql = "INSERT INTO LOP(MaLop, TenLop, MaKhoa) VALUES(@ma, @ten, @khoa)";

                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@khoa", khoa);

                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm lớp thành công.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtMaLop.ReadOnly)
            {
                MessageBox.Show("Vui lòng chọn lớp cần sửa trong danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            string ma = txtMaLop.Text.Trim();
            string ten = txtTenLop.Text.Trim();
            string khoa = cboKhoa.SelectedValue.ToString();

            try
            {
                const string sql = "UPDATE LOP SET TenLop = @ten, MaKhoa = @khoa WHERE MaLop = @ma";

                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@khoa", khoa);

                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Cập nhật lớp thành công.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy lớp cần sửa.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaLop.Text.Trim();

            if (!txtMaLop.ReadOnly || string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa trong danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int soSinhVien = CountSinhVienByLop(ma);
                if (soSinhVien > 0)
                {
                    MessageBox.Show($"Không thể xóa lớp đang có {soSinhVien} sinh viên.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa lớp {ma}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No) return;

                const string sql = "DELETE FROM LOP WHERE MaLop = @ma";

                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", ma);

                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xóa lớp thành công.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
          
                MessageBox.Show("Không thể xóa lớp vì còn dữ liệu liên quan.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                const string where = "\n WHERE l.MaLop LIKE @kw OR l.TenLop LIKE @kw";

                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(SelectLop + where + GroupLop, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                BindGrid(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            LoadData();
        }
    }
}
