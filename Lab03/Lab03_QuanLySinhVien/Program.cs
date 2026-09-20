using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVienOOP
{
  
    /// Điều khiển luồng chương trình: hiển thị menu, nhập dữ liệu từ bàn phím,
    /// sau đó gọi QuanLySinhVien để xử lý và in kết quả.

    internal class Program
    {
        private const string DinhDangBang = "{0,-5}{1,-12}{2,-28}{3,-12}{4,6}  {5}";
        private const string DinhDangNgaySinh = "d/M/yyyy";

        private static readonly QuanLySinhVien _quanLy = new QuanLySinhVien();

        private static void Main()
        {
            // Để hiển thị tiếng Việt
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool tiepTuc = true;
            while (tiepTuc)
            {
                HienThiMenu();
                Console.WriteLine();

                switch (NhapLuaChon())
                {
                    case 1: ThemSinhVien(); break;
                    case 2: XuatDanhSach(); break;
                    case 3: TimTheoMa(); break;
                    case 4: TimTheoTen(); break;
                    case 5: SuaDiem(); break;
                    case 6: XoaSinhVien(); break;
                    case 7: SapXepTheoDiem(); break;
                    case 8: LocSinhVienDat(); break;
                    case 0:
                        Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                        tiepTuc = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn số từ 0 đến 8.");
                        break;
                }

                if (tiepTuc)
                {
                    Console.WriteLine();
                    DocDong("Nhấn Enter để quay lại menu...");
                }
            }
        }

        // MENU 

        private static void HienThiMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
        }

        /// Đọc lựa chọn menu. Nhập sai kiểu dữ liệu sẽ trả về -1 
        private static int NhapLuaChon()
        {
            string s = DocDong("Chon chuc nang: ");
            return int.TryParse(s, out int luaChon) ? luaChon : -1;
        }

        //CÁC CHỨC NĂNG

        private static void ThemSinhVien()
        {
            Console.WriteLine("--- THÊM SINH VIÊN ---");

            string maSinhVien = NhapChuoiKhongRong("Nhập mã sinh viên: ");
            if (_quanLy.TonTaiMa(maSinhVien))
            {
                Console.WriteLine($"Mã sinh viên '{maSinhVien}' đã tồn tại! Không thể thêm.");
                return;
            }

            string hoTen = NhapChuoiKhongRong("Nhập họ tên: ");
            DateTime ngaySinh = NhapNgaySinh("Nhập ngày sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoiKhongRong("Nhập mã lớp: ");
            double diem = NhapDiem("Nhập điểm trung bình (0 - 10): ");

            try
            {
                SinhVien sinhVien = new SinhVien(maSinhVien, hoTen, ngaySinh, maLop, diem);
                if (_quanLy.Them(sinhVien))
                {
                    Console.WriteLine("Thêm sinh viên thành công!");
                    Console.WriteLine(sinhVien.LayThongTin());
                }
                else
                {
                    Console.WriteLine($"Mã sinh viên '{maSinhVien}' đã tồn tại! Không thể thêm.");
                }
            }
            catch (ArgumentException ex)
            {
                // Lớp chốt an toàn: dữ liệu sai sẽ bị property của lớp Nguoi/SinhVien từ chối
                Console.WriteLine($"Dữ liệu không hợp lệ: {ex.Message}");
            }
        }

        private static void XuatDanhSach()
        {
            Console.WriteLine("--- DANH SÁCH SINH VIÊN ---");
            InBangSinhVien(_quanLy.LayDanhSach());
        }

        private static void TimTheoMa()
        {
            Console.WriteLine("--- TÌM SINH VIÊN THEO MÃ ---");
            string maSinhVien = NhapChuoiKhongRong("Nhập mã sinh viên cần tìm: ");

            SinhVien? sinhVien = _quanLy.TimTheoMa(maSinhVien);
            if (sinhVien == null)
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSinhVien}'.");
                return;
            }

            Console.WriteLine("Đã tìm thấy sinh viên:");
            Console.WriteLine(sinhVien.LayThongTin());
        }

        private static void TimTheoTen()
        {
            Console.WriteLine("--- TÌM SINH VIÊN THEO TÊN ---");
            string tuKhoa = NhapChuoiKhongRong("Nhập từ khóa họ tên: ");

            List<SinhVien> ketQua = _quanLy.TimTheoTen(tuKhoa);
            if (ketQua.Count == 0)
            {
                Console.WriteLine($"Không có sinh viên nào có họ tên chứa '{tuKhoa}'.");
                return;
            }

            Console.WriteLine($"Tìm thấy {ketQua.Count} sinh viên có họ tên chứa '{tuKhoa}':");
            InBangSinhVien(ketQua);
        }

        private static void SuaDiem()
        {
            Console.WriteLine("--- SỬA ĐIỂM TRUNG BÌNH ---");
            string maSinhVien = NhapChuoiKhongRong("Nhập mã sinh viên cần sửa điểm: ");

            SinhVien? sinhVien = _quanLy.TimTheoMa(maSinhVien);
            if (sinhVien == null)
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSinhVien}'.");
                return;
            }

            Console.WriteLine($"Sinh viên: {sinhVien.HoTen} - Điểm hiện tại: {sinhVien.DiemTrungBinh:0.00}");
            double diemMoi = NhapDiem("Nhập điểm trung bình mới (0 - 10): ");

            if (_quanLy.SuaDiem(maSinhVien, diemMoi))
            {
                Console.WriteLine("Cập nhật điểm thành công!");
                Console.WriteLine(sinhVien.LayThongTin());
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSinhVien}'.");
            }
        }

        private static void XoaSinhVien()
        {
            Console.WriteLine("--- XÓA SINH VIÊN ---");
            string maSinhVien = NhapChuoiKhongRong("Nhập mã sinh viên cần xóa: ");

            if (_quanLy.Xoa(maSinhVien))
            {
                Console.WriteLine($"Đã xóa sinh viên có mã '{maSinhVien}'.");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSinhVien}'.");
            }
        }

        private static void SapXepTheoDiem()
        {
            Console.WriteLine("--- DANH SÁCH SẮP XẾP THEO ĐIỂM GIẢM DẦN ---");
            InBangSinhVien(_quanLy.SapXepTheoDiem());
        }

        private static void LocSinhVienDat()
        {
            Console.WriteLine($"--- SINH VIÊN ĐẠT (ĐIỂM TRUNG BÌNH TỪ {SinhVien.DiemDat:0} TRỞ LÊN) ---");
            InBangSinhVien(_quanLy.LocSinhVienDat());
        }

        //  HÀM IN DỮ LIỆU

        /// In danh sách sinh viên dạng bảng: STT, mã, họ tên, lớp, điểm, xếp loại.
        private static void InBangSinhVien(IReadOnlyList<SinhVien> danhSach)
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống.");
                return;
            }

            string duongKe = new string('-', 78);
            Console.WriteLine(duongKe);
            Console.WriteLine(string.Format(DinhDangBang, "STT", "Mã SV", "Họ tên", "Lớp", "Điểm", "Xếp loại"));
            Console.WriteLine(duongKe);

            for (int i = 0; i < danhSach.Count; i++)
            {
                SinhVien sv = danhSach[i];
                Console.WriteLine(string.Format(
                    DinhDangBang,
                    i + 1,
                    sv.MaSinhVien,
                    sv.HoTen,
                    sv.MaLop,
                    sv.DiemTrungBinh.ToString("0.00"),
                    sv.XepLoai()));
            }

            Console.WriteLine(duongKe);
            Console.WriteLine($"Tổng cộng: {danhSach.Count} sinh viên.");
        }

        //HÀM NHẬP DỮ LIỆU AN TOÀN 

        /// Đọc một dòng từ bàn phím, cắt khoảng trắng thừa và đúng tiếng Việt
        private static string DocDong(string thongBao)
        {
            Console.Write(thongBao);
            string? dong = Console.ReadLine();
            if (dong == null)
            {
                // Luồng nhập đã đóng (Ctrl+Z / Ctrl+D): kết thúc để tránh lặp vô hạn
                Console.WriteLine();
                Environment.Exit(0);
            }
            return dong.Trim().Normalize(NormalizationForm.FormC);
        }

        ///Bắt buộc nhập chuỗi không rỗng, nhập rỗng sẽ yêu cầu nhập lại.
        private static string NhapChuoiKhongRong(string thongBao)
        {
            while (true)
            {
                string giaTri = DocDong(thongBao);
                if (!string.IsNullOrWhiteSpace(giaTri))
                {
                    return giaTri;
                }
                Console.WriteLine("Không được để trống, vui lòng nhập lại!");
            }
        }

        ///Nhập ngày sinh theo dạng dd/MM/yyyy, sai định dạng hoặc ngày không hợp lệ sẽ nhập lại.
        private static DateTime NhapNgaySinh(string thongBao)
        {
            while (true)
            {
                string s = DocDong(thongBao);
                if (!DateTime.TryParseExact(s, DinhDangNgaySinh, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime ngaySinh))
                {
                    Console.WriteLine("Ngày sinh không đúng định dạng dd/MM/yyyy (ví dụ: 25/12/2005), vui lòng nhập lại!");
                    continue;
                }
                if (ngaySinh.Date > DateTime.Today || ngaySinh.Year < 1900)
                {
                    Console.WriteLine("Ngày sinh không hợp lệ (không được ở tương lai hoặc trước năm 1900), vui lòng nhập lại!");
                    continue;
                }
                return ngaySinh;
            }
        }

      
        /// Nhập điểm trung bình. Chấp nhận cả dấu chấm và dấu phẩy (8.2 hoặc 8,2).
        /// Nhập chữ hoặc điểm ngoài khoảng 0 - 10 sẽ báo lỗi và yêu cầu nhập lại.
      
        private static double NhapDiem(string thongBao)
        {
            while (true)
            {
                string s = DocDong(thongBao).Replace(',', '.');
                if (!double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double diem))
                {
                    Console.WriteLine("Điểm phải là một số (ví dụ: 8.2), vui lòng nhập lại!");
                    continue;
                }
                if (!SinhVien.LaDiemHopLe(diem))
                {
                    Console.WriteLine("Điểm không hợp lệ! Điểm phải nằm trong khoảng từ 0 đến 10, vui lòng nhập lại.");
                    continue;
                }
                return diem;
            }
        }
    }
}
