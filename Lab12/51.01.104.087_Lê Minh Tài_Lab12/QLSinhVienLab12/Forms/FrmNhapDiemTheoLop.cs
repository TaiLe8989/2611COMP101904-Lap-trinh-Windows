using System.Data;
using System.Globalization;
using QLSinhVienLab12.DAL;

namespace QLSinhVienLab12.Forms
{
    public partial class FrmNhapDiemTheoLop : Form
    {
        private readonly KetQuaDAL ketQuaDAL = new KetQuaDAL();
        private readonly BindingSource ketQuaSource = new BindingSource();
        private readonly Color mauXanh = Color.FromArgb(0, 102, 204);
        private string maLopDangTai = "";
        private string maMonDangTai = "";
        private int dongDangTro = -1;

        public FrmNhapDiemTheoLop()
        {
            InitializeComponent();
        }

        private void FrmNhapDiemTheoLop_Load(object? sender, EventArgs e)
        {
            CauHinhLuoi();
            dgvKetQua.DataSource = ketQuaSource;

            try
            {
                LoadComboBox();
                CapNhatTrangThai("Đã tải danh sách lớp và môn học.");
            }
            catch (Exception ex)
            {
                CapNhatTrangThai("Không kết nối được cơ sở dữ liệu.");
                MessageBox.Show("Không thể tải dữ liệu ban đầu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBox()
        {
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            cboLop.DataSource = ketQuaDAL.GetLop();

            cboMonHoc.DisplayMember = "TenMon";
            cboMonHoc.ValueMember = "MaMon";
            cboMonHoc.DataSource = ketQuaDAL.GetMonHoc();
        }

        private void CauHinhLuoi()
        {
            dgvKetQua.AutoGenerateColumns = false;
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dgvKetQua.AllowUserToResizeRows = false;
            dgvKetQua.RowHeadersVisible = false;
            dgvKetQua.BackgroundColor = Color.White;
            dgvKetQua.BorderStyle = BorderStyle.FixedSingle;
            dgvKetQua.EnableHeadersVisualStyles = false;
            dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvKetQua.ColumnHeadersHeight = 34;
            dgvKetQua.RowTemplate.Height = 28;

            dgvKetQua.ColumnHeadersDefaultCellStyle.BackColor = mauXanh;
            dgvKetQua.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKetQua.ColumnHeadersDefaultCellStyle.SelectionBackColor = mauXanh;
            dgvKetQua.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvKetQua.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvKetQua.DefaultCellStyle.BackColor = Color.White;
            dgvKetQua.DefaultCellStyle.ForeColor = Color.Black;
            dgvKetQua.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvKetQua.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvKetQua.Columns.Clear();

            DataGridViewTextBoxColumn cotSTT = TaoCot("STT", "STT", "", 60, true);
            cotSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvKetQua.Columns.Add(cotSTT);

            dgvKetQua.Columns.Add(TaoCot("MaSV", "Mã SV", "MaSV", 110, true));
            dgvKetQua.Columns.Add(TaoCot("HoTen", "Họ tên", "HoTen", 260, true));

            DataGridViewTextBoxColumn cotDiem = TaoCot("Diem", "Điểm", "Diem", 90, false);
            cotDiem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cotDiem.DefaultCellStyle.Format = "0.##";
            dgvKetQua.Columns.Add(cotDiem);

            DataGridViewTextBoxColumn cotGhiChu = TaoCot("GhiChu", "Ghi chú", "GhiChu", 100, false);
            cotGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvKetQua.Columns.Add(cotGhiChu);
        }

        private DataGridViewTextBoxColumn TaoCot(string tenCot, string tieuDe, string tenTruong, int doRong, bool chiDoc)
        {
            DataGridViewTextBoxColumn cot = new DataGridViewTextBoxColumn();
            cot.Name = tenCot;
            cot.HeaderText = tieuDe;
            cot.DataPropertyName = tenTruong;
            cot.Width = doRong;
            cot.ReadOnly = chiDoc;
            cot.SortMode = DataGridViewColumnSortMode.NotSortable;
            return cot;
        }

        private void dgvKetQua_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvKetQua.Columns[e.ColumnIndex].Name != "STT")
            {
                return;
            }

            e.Value = (e.RowIndex + 1).ToString();
            e.FormattingApplied = true;

            if (e.RowIndex == dongDangTro)
            {
                e.CellStyle.BackColor = mauXanh;
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.SelectionBackColor = mauXanh;
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }

        private void dgvKetQua_CellMouseEnter(object? sender, DataGridViewCellEventArgs e)
        {
            DatDongDangTro(e.RowIndex);
        }

        private void dgvKetQua_MouseLeave(object? sender, EventArgs e)
        {
            DatDongDangTro(-1);
        }

        private void DatDongDangTro(int dongMoi)
        {
            if (dongMoi == dongDangTro)
            {
                return;
            }

            int dongCu = dongDangTro;
            dongDangTro = dongMoi;

            if (dongCu >= 0 && dongCu < dgvKetQua.RowCount)
            {
                dgvKetQua.InvalidateRow(dongCu);
            }
            if (dongMoi >= 0 && dongMoi < dgvKetQua.RowCount)
            {
                dgvKetQua.InvalidateRow(dongMoi);
            }
        }

        private void btnTaiDanhSach_Click(object? sender, EventArgs e)
        {
            string maLop = cboLop.SelectedValue?.ToString() ?? "";
            string maMon = cboMonHoc.SelectedValue?.ToString() ?? "";

            if (maLop.Length == 0 || maMon.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp và môn học.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TaiDanhSach(maLop, maMon);
        }

        private void TaiDanhSach(string maLop, string maMon)
        {
            try
            {
                DataTable table = ketQuaDAL.GetKetQuaByLopMon(maLop, maMon);
                txtTimKiem.Clear();
                ketQuaSource.Filter = "";
                ketQuaSource.DataSource = table;
                maLopDangTai = maLop;
                maMonDangTai = maMon;

                CapNhatThongKe();
                CapNhatTrangThai($"Đã tải {table.Rows.Count} sinh viên của lớp {cboLop.Text}, môn {cboMonHoc.Text}.");
            }
            catch (Exception ex)
            {
                CapNhatTrangThai("Tải danh sách thất bại.");
                MessageBox.Show("Không thể tải danh sách: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuuTatCa_Click(object? sender, EventArgs e)
        {
            if (ketQuaSource.DataSource is not DataTable table || table.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có danh sách sinh viên để lưu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!dgvKetQua.EndEdit())
            {
                return;
            }
            ketQuaSource.EndEdit();

            string loi = KiemTraDuLieu(table);
            if (loi.Length > 0)
            {
                CapNhatTrangThai("Dữ liệu không hợp lệ, chưa lưu.");
                MessageBox.Show(loi, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int soDong = ketQuaDAL.SaveAll(maMonDangTai, table);
                TaiDanhSach(maLopDangTai, maMonDangTai);
                CapNhatTrangThai($"Đã lưu thành công {soDong} dòng.");
                MessageBox.Show($"Đã lưu thành công {soDong} dòng.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CapNhatTrangThai("Lưu thất bại, toàn bộ thao tác đã được hoàn tác.");
                MessageBox.Show("Không thể lưu dữ liệu, toàn bộ thao tác đã được hoàn tác.\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string KiemTraDuLieu(DataTable table)
        {
            List<string> dsLoi = new List<string>();

            foreach (DataRow row in table.Rows)
            {
                row.ClearErrors();
                string maSV = row["MaSV"].ToString() ?? "";

                if (row["Diem"] == DBNull.Value)
                {
                    row.SetColumnError("Diem", "Chưa nhập điểm.");
                    dsLoi.Add($"Sinh viên {maSV}: chưa nhập điểm.");
                    continue;
                }

                double diem = Convert.ToDouble(row["Diem"]);
                if (double.IsNaN(diem) || diem < 0 || diem > 10)
                {
                    row.SetColumnError("Diem", "Điểm phải từ 0 đến 10.");
                    dsLoi.Add($"Sinh viên {maSV}: điểm phải nằm trong khoảng từ 0 đến 10.");
                    continue;
                }

                if ((row["GhiChu"].ToString() ?? "").Length > 255)
                {
                    row.SetColumnError("GhiChu", "Ghi chú tối đa 255 ký tự.");
                    dsLoi.Add($"Sinh viên {maSV}: ghi chú không được vượt quá 255 ký tự.");
                }
            }

            if (dsLoi.Count == 0)
            {
                return "";
            }

            string thongBao = string.Join(Environment.NewLine, dsLoi.Take(5));
            if (dsLoi.Count > 5)
            {
                thongBao += $"{Environment.NewLine}và {dsLoi.Count - 5} lỗi khác.";
            }
            return thongBao;
        }

        private bool TryParseDiem(string text, out double diem)
        {
            return double.TryParse(text.Trim().Replace(',', '.'), NumberStyles.Float,
                CultureInfo.InvariantCulture, out diem);
        }

        private void dgvKetQua_CellParsing(object? sender, DataGridViewCellParsingEventArgs e)
        {
            if (dgvKetQua.Columns[e.ColumnIndex].Name != "Diem")
            {
                return;
            }

            string text = e.Value?.ToString() ?? "";
            if (text.Trim().Length == 0)
            {
                e.Value = DBNull.Value;
                e.ParsingApplied = true;
            }
            else if (TryParseDiem(text, out double diem))
            {
                e.Value = diem;
                e.ParsingApplied = true;
            }
        }

        private void dgvKetQua_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvKetQua.Columns[e.ColumnIndex].Name != "Diem")
            {
                return;
            }

            string text = e.FormattedValue?.ToString()?.Trim() ?? "";
            DataGridViewCell cell = dgvKetQua.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (text.Length == 0)
            {
                cell.ErrorText = "";
                return;
            }

            if (!TryParseDiem(text, out double diem))
            {
                cell.ErrorText = "Điểm phải là số.";
                e.Cancel = true;
                MessageBox.Show("Điểm phải là một số, ví dụ 8.5.", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (diem < 0 || diem > 10)
            {
                cell.ErrorText = "Điểm phải từ 0 đến 10.";
                e.Cancel = true;
                MessageBox.Show("Điểm phải nằm trong khoảng từ 0 đến 10.", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cell.ErrorText = "";
        }

        private void dgvKetQua_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            CapNhatThongKe();
        }

        private void dgvKetQua_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void txtTimKiem_TextChanged(object? sender, EventArgs e)
        {
            if (ketQuaSource.DataSource == null)
            {
                return;
            }

            string tuKhoa = ChuanHoaTuKhoa(txtTimKiem.Text.Trim());
            ketQuaSource.Filter = tuKhoa.Length == 0
                ? ""
                : $"MaSV LIKE '%{tuKhoa}%' OR HoTen LIKE '%{tuKhoa}%'";
        }

        private string ChuanHoaTuKhoa(string tuKhoa)
        {
            return tuKhoa
                .Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("*", "[*]")
                .Replace("'", "''");
        }

        private void CapNhatThongKe()
        {
            if (ketQuaSource.DataSource is not DataTable table || table.Rows.Count == 0)
            {
                lblThongKe.Text = "Chưa có dữ liệu thống kê";
                return;
            }

            int daNhap = 0;
            int soDat = 0;
            double tong = 0;

            foreach (DataRow row in table.Rows)
            {
                if (row["Diem"] != DBNull.Value)
                {
                    double diem = Convert.ToDouble(row["Diem"]);
                    daNhap++;
                    tong += diem;
                    if (diem >= 5)
                    {
                        soDat++;
                    }
                }
            }

            string diemTrungBinh = daNhap > 0 ? (tong / daNhap).ToString("0.00") : "-";
            lblThongKe.Text = $"Tổng: {table.Rows.Count} SV  |  Đã nhập điểm: {daNhap}  |  Đạt: {soDat}  |  Điểm TB: {diemTrungBinh}";
        }

        private void CapNhatTrangThai(string noiDung)
        {
            tsslTrangThai.Text = noiDung;
        }
    }
}
