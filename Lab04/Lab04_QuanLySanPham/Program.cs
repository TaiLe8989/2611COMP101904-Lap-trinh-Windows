using System.Text;
using Lab04_QuanLySanPham.Exceptions;
using Lab04_QuanLySanPham.Models;
using Lab04_QuanLySanPham.Services;
using Lab04_QuanLySanPham.Utils;

namespace Lab04_QuanLySanPham
{
  
    /// Điều khiển chương trình: hiển thị menu, nhập/xuất dữ liệu và bắt exception.
    /// Nghiệp vụ được giao cho ProductService, lưu trữ do Repository&lt;T&gt; đảm nhiệm.
   
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            ProductService service = new ProductService();
            service.ProductAdded += OnProductAdded;
            service.ProductRemoved += OnProductRemoved;

            bool tiepTuc = true;
            while (tiepTuc)
            {
                try
                {
                    HienThiMenu();
                    string luaChon = NhapLieu.DocChuoi("Chọn: ");
                    Console.WriteLine();
                    tiepTuc = XuLyLuaChon(service, luaChon);
                }
                catch (EndOfStreamException)
                {
                    break; // Hết dữ liệu nhập: thoát êm, không báo lỗi.
                }
                catch (DuplicateProductException ex)
                {
                    NhapLieu.InLoi(ex.Message);
                }
                catch (ProductNotFoundException ex)
                {
                    NhapLieu.InLoi(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    NhapLieu.InLoi(ex.Message);
                }
                catch (Exception ex)
                {
                    // Lưới an toàn cuối cùng: mọi lỗi bất ngờ đều không làm chương trình sập.
                    NhapLieu.InLoi("Đã xảy ra lỗi không mong muốn: " + ex.Message);
                }

                if (tiepTuc)
                {
                    TamDung();
                }
            }

            Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
        }

        // ---------- Menu ----------

        private static void HienThiMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== QUẢN LÝ SẢN PHẨM =====");
            Console.WriteLine("1. Thêm sản phẩm");
            Console.WriteLine("2. Xuất danh sách");
            Console.WriteLine("3. Tìm theo mã");
            Console.WriteLine("4. Tìm theo tên");
            Console.WriteLine("5. Lọc theo khoảng giá");
            Console.WriteLine("6. Xóa sản phẩm");
            Console.WriteLine("7. Tính tổng giá trị kho");
            Console.WriteLine("0. Thoát");
        }

        /// Xử lý lựa chọn của người dùng. Trả về false khi người dùng chọn thoát.
        private static bool XuLyLuaChon(ProductService service, string luaChon)
        {
            switch (luaChon)
            {
                case "1":
                    ThemSanPham(service);
                    break;
                case "2":
                    XuatDanhSach(service);
                    break;
                case "3":
                    TimTheoMa(service);
                    break;
                case "4":
                    TimTheoTen(service);
                    break;
                case "5":
                    LocTheoKhoangGia(service);
                    break;
                case "6":
                    XoaSanPham(service);
                    break;
                case "7":
                    TinhTongGiaTriKho(service);
                    break;
                case "0":
                    return false;
                default:
                    NhapLieu.InLoi("Lựa chọn không hợp lệ, vui lòng chọn từ 0 đến 7.");
                    break;
            }
            return true;
        }

        // ---------- Các chức năng ----------

        private static void ThemSanPham(ProductService service)
        {
            Console.WriteLine("--- THÊM SẢN PHẨM ---");
            string maSP = NhapLieu.DocChuoiKhongRong("Nhập mã sản phẩm: ");
            string tenSP = NhapLieu.DocChuoiKhongRong("Nhập tên sản phẩm: ");
            decimal donGia = NhapLieu.DocSoThuc("Nhập đơn giá: ");
            int soLuong = NhapLieu.DocSoNguyen("Nhập số lượng: ");

            // Product kiểm tra đơn giá/số lượng âm (ArgumentException),
            // Service kiểm tra trùng mã (DuplicateProductException).
            Product sanPham = new Product(maSP, tenSP, donGia, soLuong);
            service.AddProduct(sanPham);
        }

        private static void XuatDanhSach(ProductService service)
        {
            Console.WriteLine("--- DANH SÁCH SẢN PHẨM ---");
            IReadOnlyList<Product> danhSach = service.GetAll();
            if (danhSach.Count == 0)
            {
                NhapLieu.InThongBao("Danh sách sản phẩm đang trống.");
                return;
            }

            InDanhSach(danhSach);
        }

        private static void TimTheoMa(ProductService service)
        {
            Console.WriteLine("--- TÌM THEO MÃ ---");
            string maSP = NhapLieu.DocChuoiKhongRong("Nhập mã sản phẩm cần tìm: ");

            Product? sanPham = service.SearchById(maSP);
            if (sanPham == null)
            {
                NhapLieu.InThongBao($"Không tìm thấy sản phẩm có mã '{maSP}'.");
                return;
            }

            Console.WriteLine(sanPham);
        }

        private static void TimTheoTen(ProductService service)
        {
            Console.WriteLine("--- TÌM THEO TÊN ---");
            string tuKhoa = NhapLieu.DocChuoiKhongRong("Nhập từ khóa tên sản phẩm: ");

            List<Product> ketQua = service.SearchByName(tuKhoa);
            if (ketQua.Count == 0)
            {
                NhapLieu.InThongBao($"Không có sản phẩm nào có tên chứa '{tuKhoa}'.");
                return;
            }

            InDanhSach(ketQua);
        }

        private static void LocTheoKhoangGia(ProductService service)
        {
            Console.WriteLine("--- LỌC THEO KHOẢNG GIÁ ---");
            decimal giaNhoNhat = NhapLieu.DocSoThuc("Nhập giá nhỏ nhất: ");
            decimal giaLonNhat = NhapLieu.DocSoThuc("Nhập giá lớn nhất: ");

            List<Product> ketQua = service.FilterByPriceRange(giaNhoNhat, giaLonNhat);
            if (ketQua.Count == 0)
            {
                NhapLieu.InThongBao(
                    $"Không có sản phẩm nào có giá từ {DinhDangTien.SangVnd(giaNhoNhat)} đến {DinhDangTien.SangVnd(giaLonNhat)}.");
                return;
            }

            InDanhSach(ketQua);
        }

        private static void XoaSanPham(ProductService service)
        {
            Console.WriteLine("--- XÓA SẢN PHẨM ---");
            string maSP = NhapLieu.DocChuoiKhongRong("Nhập mã sản phẩm cần xóa: ");

            // Nếu mã không tồn tại, service ném ProductNotFoundException để Main bắt.
            service.RemoveProduct(maSP);
        }

        private static void TinhTongGiaTriKho(ProductService service)
        {
            Console.WriteLine("--- TỔNG GIÁ TRỊ KHO ---");
            decimal tong = service.CalculateTotalInventoryValue();
            Console.WriteLine($"Tổng giá trị kho: {DinhDangTien.SangVnd(tong)}");
        }

        // ---------- Xử lý event ----------

        private static void OnProductAdded(Product sanPham)
        {
            NhapLieu.InThanhCong($"[Thông báo] Đã thêm sản phẩm thành công: {sanPham.TenSP} (Mã: {sanPham.MaSP}).");
        }

        private static void OnProductRemoved(Product sanPham)
        {
            NhapLieu.InThanhCong($"[Thông báo] Đã xóa sản phẩm thành công: {sanPham.TenSP} (Mã: {sanPham.MaSP}).");
        }

        // ---------- Hàm hỗ trợ hiển thị ----------

        private static void InDanhSach(IReadOnlyList<Product> danhSach)
        {
            for (int i = 0; i < danhSach.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}. {danhSach[i]}");
            }
            Console.WriteLine($"Tổng cộng: {danhSach.Count} sản phẩm.");
        }

        private static void TamDung()
        {
            Console.Write("\nNhấn Enter để tiếp tục...");
            Console.ReadLine();
        }
    }
}
