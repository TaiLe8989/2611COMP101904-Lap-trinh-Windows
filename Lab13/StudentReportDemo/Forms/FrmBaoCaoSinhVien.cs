using StudentReportDemo.Models;
using System.Text;

namespace StudentReportDemo.Forms
{
    public partial class FrmBaoCaoSinhVien : Form
    {
        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();

        public FrmBaoCaoSinhVien()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoSinhVien_Load(object sender, EventArgs e)
        {
            TaoDuLieuMau();
            LoadComboBox();
            XemBaoCao(false);
        }

        private void TaoDuLieuMau()
        {
            danhSachSinhVien = new List<SinhVien>
            {
new SinhVien { MaSV = "SV001", HoTen = "Nguyễn Văn A", MaLop = "CTK51A", TenLop = "K51.CNTT.A", Diem = 8.2 },
new SinhVien { MaSV = "SV002", HoTen = "Trần Thị B", MaLop = "CTK51A", TenLop = "K51.CNTT.A", Diem = 6.5 },
new SinhVien { MaSV = "SV003", HoTen = "Lê Minh C", MaLop = "CTK51A", TenLop = "K51.CNTT.A", Diem = 4.8 },
new SinhVien { MaSV = "SV004", HoTen = "Phạm Thanh D", MaLop = "CTK51A", TenLop = "K51.CNTT.A", Diem = 9.1 },
new SinhVien { MaSV = "SV005", HoTen = "Hoàng Quốc H", MaLop = "CTK51B", TenLop = "K51.CNTT.B", Diem = 7.4 },
new SinhVien { MaSV = "SV006", HoTen = "Võ Gia K", MaLop = "CTK51B", TenLop = "K51.CNTT.B", Diem = 3.9 },
new SinhVien { MaSV = "SV007", HoTen = "Đặng Minh K", MaLop = "CTK51B", TenLop = "K51.CNTT.B", Diem = 5.5 },
new SinhVien { MaSV = "SV008", HoTen = "Bùi Thảo L", MaLop = "CTK51B", TenLop = "K51.CNTT.B", Diem = 8.0 },
new SinhVien { MaSV = "SV009", HoTen = "Ngô Tuấn M", MaLop = "CTK51C", TenLop = "K51.CNTT.C", Diem = 6.0 },
new SinhVien { MaSV = "SV010", HoTen = "Đỗ Bảo N", MaLop = "CTK51C", TenLop = "K51.CNTT.C", Diem = 9.3 },
new SinhVien { MaSV = "SV011", HoTen = "Huỳnh Gia P", MaLop = "CTK51C", TenLop = "K51.CNTT.C", Diem = 4.2 },
new SinhVien { MaSV = "SV012", HoTen = "Mai Anh T", MaLop = "CTK51C", TenLop = "K51.CNTT.C", Diem = 7.8 }
            };
        }

        private void LoadComboBox()
        {
            var dsLop = danhSachSinhVien
                .Select(sv => new { sv.MaLop, sv.TenLop })
                .Distinct()
                .OrderBy(lop => lop.MaLop)
                .ToList();

            cboLop.DataSource = dsLop;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";

            cboKetQua.Items.Clear();
            cboKetQua.Items.Add("Tất cả");
            cboKetQua.Items.Add("Đạt");
            cboKetQua.Items.Add("Không đạt");
            cboKetQua.SelectedIndex = 0;
        }

        private List<SinhVien> LocDuLieu()
        {
            string maLop = cboLop.SelectedValue?.ToString() ?? "";
            string ketQua = cboKetQua.SelectedItem?.ToString() ?? "Tất cả";
            string tuKhoa = txtTuKhoa.Text.Trim().ToLower();

            var query = danhSachSinhVien.Where(sv => sv.MaLop == maLop);

            if (ketQua == "Đạt")
            {
                query = query.Where(sv => sv.Diem >= 5);
            }
            else if (ketQua == "Không đạt")
            {
                query = query.Where(sv => sv.Diem < 5);
            }

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(sv => sv.MaSV.ToLower().Contains(tuKhoa)
                                       || sv.HoTen.ToLower().Contains(tuKhoa));
            }

            return query
                .OrderByDescending(sv => sv.Diem)
                .ThenBy(sv => sv.HoTen)
                .ToList();
        }

        private void XemBaoCao(bool baoKhiRong = true)
        {
            if (cboLop.SelectedValue == null)
            {
                return;
            }

            List<SinhVien> ketQuaLoc = LocDuLieu();

            dgvBaoCao.DataSource = ketQuaLoc
                .Select(sv => new { sv.MaSV, sv.HoTen, sv.TenLop, sv.Diem, sv.KetQua })
                .ToList();

            DatTieuDeCot(new Dictionary<string, string>
            {
                { "MaSV", "Mã SV" },
                { "HoTen", "Họ tên" },
                { "TenLop", "Lớp" },
                { "Diem", "Điểm" },
                { "KetQua", "Kết quả" }
            });

            CapNhatThongKe(ketQuaLoc, "Báo cáo");

            if (ketQuaLoc.Count == 0 && baoKhiRong)
            {
                MessageBox.Show("Không có dữ liệu phù hợp với điều kiện đã chọn.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CapNhatThongKe(List<SinhVien> ds, string tieuDe)
        {
            int tongSo = ds.Count;
            int soDat = ds.Count(sv => sv.Diem >= 5);
            int soKhongDat = ds.Count(sv => sv.Diem < 5);
            double diemTB = tongSo > 0 ? ds.Average(sv => sv.Diem) : 0;

            lblThongKe.Text = $"{tieuDe}  |  Tổng số sinh viên: {tongSo}  |  Đạt: {soDat}  |  " +
                              $"Không đạt: {soKhongDat}  |  Điểm trung bình: {diemTB:0.00}";
        }

        private void HienThiThongKeTheoLop()
        {
            var thongKe = danhSachSinhVien
                .GroupBy(sv => new { sv.MaLop, sv.TenLop })
                .Select(g => new
                {
                    g.Key.MaLop,
                    g.Key.TenLop,
                    SoLuong = g.Count(),
                    SoDat = g.Count(sv => sv.Diem >= 5),
                    SoKhongDat = g.Count(sv => sv.Diem < 5),
                    DiemTrungBinh = Math.Round(g.Average(sv => sv.Diem), 2)
                })
                .OrderBy(x => x.MaLop)
                .ToList();

            dgvBaoCao.DataSource = thongKe;

            DatTieuDeCot(new Dictionary<string, string>
            {
                { "MaLop", "Mã lớp" },
                { "TenLop", "Tên lớp" },
                { "SoLuong", "Số lượng" },
                { "SoDat", "Số đạt" },
                { "SoKhongDat", "Số không đạt" },
                { "DiemTrungBinh", "Điểm trung bình" }
            });

            CapNhatThongKe(danhSachSinhVien, "Thống kê theo lớp");
        }

        private void DatTieuDeCot(Dictionary<string, string> tieuDe)
        {
            foreach (DataGridViewColumn cot in dgvBaoCao.Columns)
            {
                if (tieuDe.TryGetValue(cot.DataPropertyName, out string ten))
                {
                    cot.HeaderText = ten;
                }
            }

            if (dgvBaoCao.Columns.Contains("Diem"))
            {
                dgvBaoCao.Columns["Diem"].DefaultCellStyle.Format = "0.0";
            }
        }

        private string GhepDong(IEnumerable<string> cacO, bool laCsv)
        {
            if (!laCsv)
            {
                return string.Join("\t", cacO);
            }

            return string.Join(",", cacO.Select(o =>
                o.Contains(',') || o.Contains('"') ? "\"" + o.Replace("\"", "\"\"") + "\"" : o));
        }

        private void XuatFile()
        {
            if (dgvBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog hopThoaiLuu = new SaveFileDialog();
            hopThoaiLuu.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";
            hopThoaiLuu.FileName = "BaoCaoSinhVien";
            hopThoaiLuu.Title = "Xuất báo cáo";

            if (hopThoaiLuu.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                bool laCsv = !string.Equals(Path.GetExtension(hopThoaiLuu.FileName), ".txt",
                    StringComparison.OrdinalIgnoreCase);
                StringBuilder noiDung = new StringBuilder();

                var tieuDeCot = dgvBaoCao.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText);
                noiDung.AppendLine(GhepDong(tieuDeCot, laCsv));

                foreach (DataGridViewRow dong in dgvBaoCao.Rows)
                {
                    var cacO = dong.Cells.Cast<DataGridViewCell>()
                        .Select(o => o.FormattedValue?.ToString() ?? "");
                    noiDung.AppendLine(GhepDong(cacO, laCsv));
                }

                File.WriteAllText(hopThoaiLuu.FileName, noiDung.ToString(), new UTF8Encoding(true));

                MessageBox.Show("Xuất file thành công:\n" + hopThoaiLuu.FileName, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            XemBaoCao();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            HienThiThongKeTheoLop();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            XuatFile();
        }
    }
}
