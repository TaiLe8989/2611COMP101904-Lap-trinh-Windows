using System.Globalization;

namespace Lab04_QuanLySanPham.Utils
{
   
    /// Các hàm nhập dữ liệu từ bàn phím. Nhập sai định dạng thì báo lỗi và yêu cầu nhập lại,
    /// không để chương trình bị dừng đột ngột.
    
    public static class NhapLieu
    {
        public static string DocChuoi(string loiNhac)
        {
            Console.Write(loiNhac);
            string? dong = Console.ReadLine();

            // Hết luồng nhập (Ctrl+Z / Ctrl+D): báo cho Program để thoát êm.
            if (dong == null)
            {
                throw new EndOfStreamException("Đã hết dữ liệu nhập.");
            }
            return dong.Trim();
        }

        public static string DocChuoiKhongRong(string loiNhac)
        {
            while (true)
            {
                string giaTri = DocChuoi(loiNhac);
                if (!string.IsNullOrWhiteSpace(giaTri))
                {
                    return giaTri;
                }
                InLoi("Giá trị không được để trống, vui lòng nhập lại.");
            }
        }

        public static decimal DocSoThuc(string loiNhac)
        {
            while (true)
            {
                string giaTri = DocChuoi(loiNhac);
                if (decimal.TryParse(
                        giaTri,
                        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out decimal ketQua))
                {
                    return ketQua;
                }
                InLoi("Vui lòng nhập một số hợp lệ (ví dụ: 15000).");
            }
        }

        public static int DocSoNguyen(string loiNhac)
        {
            while (true)
            {
                string giaTri = DocChuoi(loiNhac);
                if (int.TryParse(
                        giaTri,
                        NumberStyles.AllowLeadingSign,
                        CultureInfo.InvariantCulture,
                        out int ketQua))
                {
                    return ketQua;
                }
                InLoi("Vui lòng nhập một số nguyên hợp lệ (ví dụ: 10).");
            }
        }

        public static void InLoi(string thongDiep)
        {
            InMau("[Lỗi] " + thongDiep, ConsoleColor.Red);
        }

        public static void InThanhCong(string thongDiep)
        {
            InMau(thongDiep, ConsoleColor.Green);
        }

        public static void InThongBao(string thongDiep)
        {
            InMau(thongDiep, ConsoleColor.Yellow);
        }

        private static void InMau(string noiDung, ConsoleColor mau)
        {
            ConsoleColor mauCu = Console.ForegroundColor;
            Console.ForegroundColor = mau;
            Console.WriteLine(noiDung);
            Console.ForegroundColor = mauCu;
        }
    }
}
