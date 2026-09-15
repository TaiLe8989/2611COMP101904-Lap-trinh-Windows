using System;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

  
        /// Xử lý sự kiện khi nhấn nút "Hiển thị":
        /// Kiểm tra dữ liệu nhập, nếu hợp lệ thì tính tuổi và hiển thị thông tin.
     
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra họ tên không được rỗng
            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Yêu cầu nhập họ tên",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // 2. Kiểm tra năm sinh không rỗng và phải là số nguyên
            string namSinhText = txtNamSinh.Text.Trim();
            if (string.IsNullOrEmpty(namSinhText))
            {
                MessageBox.Show("Vui lòng nhập năm sinh.", "Yêu cầu nhập năm sinh",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            if (!int.TryParse(namSinhText, out int namSinh))
            {
                MessageBox.Show("Năm sinh phải là số nguyên.", "Năm sinh không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // 3. Kiểm tra năm sinh trong khoảng hợp lệ (1900 -> năm hiện tại)
            int namHienTai = DateTime.Now.Year;
            if (namSinh < 1900 || namSinh > namHienTai)
            {
                MessageBox.Show($"Năm sinh phải nằm trong khoảng từ 1900 đến {namHienTai}.",
                    "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // 4. Kiểm tra email không được rỗng
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email.", "Yêu cầu nhập email",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            // 4b. Kiểm tra định dạng email: không khoảng trắng, không ký tự đặc biệt
            // (ngoại trừ @), và bắt buộc kết thúc bằng "@gmail.com"
            string emailPattern = @"^[a-zA-Z0-9]+@gmail\.com$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Email nhập chưa đúng, yêu cầu nhập lại.",
                    "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            // 5. Kiểm tra đã chọn giới tính chưa
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Yêu cầu nhập giới tính",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";

            // 6. Kiểm tra đã chọn khoa/lớp chưa
            if (cboKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa hoặc lớp.", "Yêu cầu nhập khoa hoặc lớp",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string khoa = cboKhoa.SelectedItem.ToString();

            // 7. Tính tuổi
            int tuoi = namHienTai - namSinh;

            // 8. Tổng hợp và hiển thị kết quả
            StringBuilder ketQua = new StringBuilder();
            ketQua.AppendLine("THÔNG TIN SINH VIÊN");
            ketQua.AppendLine($"Họ tên: {hoTen}");
            ketQua.AppendLine($"Tuổi: {tuoi}");
            ketQua.AppendLine($"Email: {email}");
            ketQua.AppendLine($"Giới tính: {gioiTinh}");
            ketQua.AppendLine($"Khoa/Lớp: {khoa}");

            txtKetQua.Text = ketQua.ToString();
        }

   
        /// Xử lý sự kiện khi nhấn nút "Xóa":
        /// Đưa toàn bộ dữ liệu đã nhập về trạng thái ban đầu.
    
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtNamSinh.Clear();
            txtEmail.Clear();

            // Bỏ chọn giới tính
            radNam.Checked = false;
            radNu.Checked = false;

            // Đưa ComboBox về trạng thái không chọn
            cboKhoa.SelectedIndex = -1;

            // Xóa kết quả hiển thị
            txtKetQua.Clear();

            // Đưa con trỏ về ô đầu tiên
            txtHoTen.Focus();
        }

      
        /// Xử lý sự kiện khi nhấn nút "Thoát":
        /// Hỏi xác nhận trước khi đóng chương trình.
        
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
