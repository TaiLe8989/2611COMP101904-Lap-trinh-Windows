using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Lab05_AppDangKyKhoaHoc
{
    public partial class Form1 : Form
    {
        // Khóa học và học phí/tháng
        private class KhoaHoc
        {
            public string Ten { get; set; }
            public decimal HocPhi { get; set; }
        }

        private readonly List<KhoaHoc> _dsKhoaHoc = new List<KhoaHoc>
        {
            new KhoaHoc { Ten = "C# WinForms cơ bản", HocPhi = 800000 },
            new KhoaHoc { Ten = "SQL Server cơ bản", HocPhi = 700000 },
            new KhoaHoc { Ten = "Web Frontend cơ bản", HocPhi = 750000 },
            new KhoaHoc { Ten = "Lập trình Python cơ bản", HocPhi = 650000 }
        };

        public Form1()
        {
            InitializeComponent();
        }

        // ===== 5.1. Form Load =====
        private void Form1_Load(object sender, EventArgs e)
        {
            // Giới hạn độ dài sdt
            txtSoDienThoai.MaxLength = 10;
            txtSoDienThoai.KeyPress += txtSoDienThoai_KeyPress;


            cboKhoaHoc.DataSource = _dsKhoaHoc;
            cboKhoaHoc.DisplayMember = "Ten";
            cboKhoaHoc.SelectedIndex = 0;

            radOnline.Checked = true;

            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;

            CapNhatHocPhi();
        }

        // ===== Tính & hiển thị học phí =====
        private decimal TinhTongTien()
        {
            KhoaHoc khoaHoc = cboKhoaHoc.SelectedItem as KhoaHoc;
            if (khoaHoc == null)
                return 0;

            return khoaHoc.HocPhi * numSoThang.Value;
        }

        private static string DinhDangTien(decimal soTien)
        {
            return soTien.ToString("N0", CultureInfo.InvariantCulture) + " VND";
        }

        private void CapNhatHocPhi()
        {
            KhoaHoc khoaHoc = cboKhoaHoc.SelectedItem as KhoaHoc;
            if (khoaHoc != null)
                lblHocPhiThang.Text = DinhDangTien(khoaHoc.HocPhi);

            lblGiamGia.Text = DinhDangTien(0);
            lblTongTien.Text = DinhDangTien(TinhTongTien());
        }

        // ===== Sự kiện thay đổi dữ liệu =====
        private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }

        private void numSoThang_ValueChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            lblDemKyTu.Text = txtHoTen.TextLength + "/" + txtHoTen.MaxLength;
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho nhập chữ số; vẫn cho phép phím Backspace (ký tự điều khiển)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // chặn ký tự không phải số
            }
        }

        private void chkNhanEmail_CheckedChanged(object sender, EventArgs e)
        {
            lblTrangThaiEmail.Text = chkNhanEmail.Checked
                ? "Có nhận email thông báo"
                : "Không nhận email thông báo";
        }

        // ===== 5.2. Nút Đăng ký =====
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }
            //Thông Báo Nhập Không Đủ Số Điện Thoại
            string soDienThoai = txtSoDienThoai.Text.Trim();
            if (soDienThoai.Length != 10 || !soDienThoai.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại không đúng định dạng, yêu cầu nhập lại",
                    "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                txtSoDienThoai.SelectAll();
                return;
            }

            KhoaHoc khoaHoc = cboKhoaHoc.SelectedItem as KhoaHoc;
            if (khoaHoc == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return;
            }

            string hinhThuc = radOnline.Checked ? "Online" : "Trực tiếp";
            string email = chkNhanEmail.Checked ? "Có" : "Không";

            string phieu =
                "PHIẾU ĐĂNG KÝ KHÓA HỌC\n" +
                "-------------------------------------\n" +
                "Họ tên: " + txtHoTen.Text.Trim() + "\n" +
                "Số điện thoại: " + txtSoDienThoai.Text.Trim() + "\n" +
                "Ngày sinh: " + dtpNgaySinh.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "\n" +
                "Khóa học: " + khoaHoc.Ten + "\n" +
                "Hình thức học: " + hinhThuc + "\n" +
                "Số tháng: " + numSoThang.Value + "\n" +
                "Tổng tiền: " + DinhDangTien(TinhTongTien()) + "\n" +
                "Nhận email thông báo: " + email;

            MessageBox.Show(phieu, "Đăng ký thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===== 5.3. Nút Làm mới =====
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            chkNhanEmail.Checked = false;
            cboKhoaHoc.SelectedIndex = 0;
            radOnline.Checked = true;
            numSoThang.Value = 1;
            txtHoTen.Focus();
        }

        // ===== 5.4. Nút Thoát =====
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
                Close();
        }
    }
}
