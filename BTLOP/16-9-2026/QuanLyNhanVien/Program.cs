using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyNhanVien
{
    class Program
    {
        static List<NhanVien> danhSach = new List<NhanVien>();

        static void Main(string[] args)
        {
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== NHẬP DANH SÁCH NHÂN VIÊN (tối thiểu 5 người) ===");
            NhapDanhSachBanDau();

            bool tiepTuc = true;
            while (tiepTuc)
            {
                HienThiMenu();
                string luaChon = Console.ReadLine() ?? "";

                switch (luaChon.Trim())
                {
                    case "1":
                        XuatDanhSach();
                        break;
                    case "2":
                        TimTheoMa();
                        break;
                    case "3":
                        TimLuongCaoNhat();
                        break;
                    case "4":
                        TinhTongLuong();
                        break;
                    case "5":
                        ThemNhanVien();
                        break;
                    case "0":
                        tiepTuc = false;
                        Console.WriteLine("Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.\n");
                        break;
                }
            }
        }

        static void HienThiMenu()
        {
            Console.WriteLine();
            Console.WriteLine("========== MENU ==========");
            Console.WriteLine("1. Xuất danh sách nhân viên");
            Console.WriteLine("2. Tìm nhân viên theo mã");
            Console.WriteLine("3. Tìm nhân viên có lương cao nhất");
            Console.WriteLine("4. Tính tổng lương công ty phải trả");
            Console.WriteLine("5. Thêm nhân viên mới");
            Console.WriteLine("0. Thoát");
            Console.Write("Nhập lựa chọn của bạn: ");
        }

        //  CÁC CHỨC NĂNG CHÍNH (dùng ĐA HÌNH, KHÔNG if/switch theo loại lớp) 

        static void XuatDanhSach()
        {
            Console.WriteLine("\n--- DANH SÁCH NHÂN VIÊN ---");
            Console.WriteLine($"{"Mã NV",-8} | {"Họ tên",-22} | {"Loại NV",-20} | {"Lương",15}");
            Console.WriteLine(new string('-', 85));

            foreach (var nv in danhSach)
            {
                // Gọi HienThiThongTin() -> tự động chạy đúng phiên bản override của từng lớp con (đa hình)
                nv.HienThiThongTin();
            }
        }

        static void TimTheoMa()
        {
            Console.Write("\nNhập mã nhân viên cần tìm: ");
            string ma = (Console.ReadLine() ?? "").Trim();

            var nv = danhSach.FirstOrDefault(x => x.MaNV.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (nv == null)
            {
                Console.WriteLine($"Không tìm thấy nhân viên có mã \"{ma}\".");
            }
            else
            {
                Console.WriteLine("Tìm thấy nhân viên:");
                nv.HienThiThongTin();
            }
        }

        static void TimLuongCaoNhat()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách nhân viên đang trống.");
                return;
            }

            // Thuật toán chỉ dựa vào TinhLuong() (đa hình) -> không cần sửa khi thêm loại NV mới
            NhanVien? nvLuongCaoNhat = null;
            double luongCaoNhat = double.MinValue;

            foreach (var nv in danhSach)
            {
                double luong = nv.TinhLuong();
                if (luong > luongCaoNhat)
                {
                    luongCaoNhat = luong;
                    nvLuongCaoNhat = nv;
                }
            }

            Console.WriteLine("\nNhân viên có lương cao nhất là:");
            nvLuongCaoNhat!.HienThiThongTin();
        }

        static void TinhTongLuong()
        {
            // Thuật toán chỉ dựa vào TinhLuong() (đa hình) -> không cần sửa khi thêm loại NV mới
            double tongLuong = 0;
            foreach (var nv in danhSach)
            {
                tongLuong += nv.TinhLuong();
            }

            Console.WriteLine($"\nTổng lương công ty phải trả: {tongLuong:N0} VNĐ");
        }

        //  NHẬP LIỆU (chọn loại NV ở đây LÀ HỢP LỆ vì đây là bước KHỞI TẠO đối tượng,
        // khác với các chức năng xử lý/tính toán bên trên phải dùng đa hình) 

        static void NhapDanhSachBanDau()
        {
            int soLuong = DocSoNguyen("Nhập số lượng nhân viên muốn nhập (tối thiểu 5): ", min: 5);

            for (int i = 1; i <= soLuong; i++)
            {
                Console.WriteLine($"\n--- Nhập nhân viên thứ {i} ---");
                ThemNhanVien();
            }
        }

        static void ThemNhanVien()
        {
            Console.WriteLine("Chọn loại nhân viên:");
            Console.WriteLine("  1. Nhân viên văn phòng");
            Console.WriteLine("  2. Nhân viên kinh doanh");
            Console.WriteLine("  3. Nhân viên thời vụ (bonus)");
            int loai = DocSoNguyen("Nhập lựa chọn (1-3): ", min: 1, max: 3);

            Console.Write("Mã nhân viên: ");
            string maNV = (Console.ReadLine() ?? "").Trim();

            Console.Write("Họ tên: ");
            string hoTen = (Console.ReadLine() ?? "").Trim();

            double luongCoBan = DocSoThuc("Lương cơ bản (> 0): ", min: 0.01);

            try
            {
                NhanVien nv;
                switch (loai)
                {
                    case 1:
                        int soNgay = DocSoNguyen("Số ngày làm việc (0-31): ", min: 0, max: 31);
                        nv = new NhanVienVanPhong(maNV, hoTen, luongCoBan, soNgay);
                        break;
                    case 2:
                        double doanhSo = DocSoThuc("Doanh số (>= 0): ", min: 0);
                        nv = new NhanVienKinhDoanh(maNV, hoTen, luongCoBan, doanhSo);
                        break;
                    default:
                        int soGio = DocSoNguyen("Số giờ làm (>= 0): ", min: 0);
                        double luongTheoGio = DocSoThuc("Lương theo giờ (> 0): ", min: 0.01);
                        nv = new NhanVienThoiVu(maNV, hoTen, luongCoBan, soGio, luongTheoGio);
                        break;
                }

                danhSach.Add(nv);
                Console.WriteLine("Thêm nhân viên thành công!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message} Vui lòng nhập lại nhân viên này.");
                ThemNhanVien();
            }
        }

        //  HÀM HỖ TRỢ NHẬP LIỆU AN TOÀN

        static int DocSoNguyen(string thongBao, int min = int.MinValue, int max = int.MaxValue)
        {
            int giaTri;
            while (true)
            {
                Console.Write(thongBao);
                if (int.TryParse(Console.ReadLine(), out giaTri) && giaTri >= min && giaTri <= max)
                    return giaTri;

                Console.WriteLine($"Giá trị không hợp lệ. Vui lòng nhập số nguyên từ {min} đến {max}.");
            }
        }

        static double DocSoThuc(string thongBao, double min = double.MinValue, double max = double.MaxValue)
        {
            double giaTri;
            while (true)
            {
                Console.Write(thongBao);
                if (double.TryParse(Console.ReadLine(), out giaTri) && giaTri >= min && giaTri <= max)
                    return giaTri;

                Console.WriteLine($"Giá trị không hợp lệ. Vui lòng nhập số từ {min} đến {max}.");
            }
        }
    }
}
