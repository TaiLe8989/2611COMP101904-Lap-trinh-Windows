using System;
using System.Text;
namespace Lab02_QuanLyMangSoNguyen
{

    class Program
    {

        static int[] mang = null;
        static bool daNhapMang = false;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            int luaChon;

            do
            {
                HienThiMenu();
                luaChon = NhapLuaChonMenu();

                switch (luaChon)
                {
                    case 1:
                        mang = NhapMang();
                        daNhapMang = true;
                        Console.WriteLine("Nhập mảng thành công.");
                        break;

                    case 2:
                        if (KiemTraDaNhapMang())
                            XuatMang(mang);
                        break;

                    case 3:
                        if (KiemTraDaNhapMang())
                            Console.WriteLine("Tổng = " + TinhTong(mang));
                        break;

                    case 4:
                        if (KiemTraDaNhapMang())
                        {
                            Console.WriteLine("Max = " + TimMax(mang));
                            Console.WriteLine("Min = " + TimMin(mang));
                        }
                        break;

                    case 5:
                        if (KiemTraDaNhapMang())
                        {
                            Console.WriteLine("Chẵn = " + DemChan(mang));
                            Console.WriteLine("Lẻ = " + DemLe(mang));
                        }
                        break;

                    case 6:
                        if (KiemTraDaNhapMang())
                        {
                            SapXepTangDan(mang);
                            Console.WriteLine("Mảng sau khi sắp xếp tăng dần:");
                            XuatMang(mang);
                        }
                        break;

                    case 7:
                        if (KiemTraDaNhapMang())
                        {
                            int x = NhapSoNguyen("Nhập giá trị x cần tìm: ");
                            int viTri = TimKiem(mang, x);
                            if (viTri != -1)
                                Console.WriteLine("Có tìm thấy, tìm thấy x = " + x + " tại vị trí " + viTri + " nếu tính từ 0");
                            else
                                Console.WriteLine("Không tìm thấy x = " + x + " trong mảng.");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Kết thúc chương trình. Tạm biệt!");
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }

                Console.WriteLine();

            } while (luaChon != 0);
        }

        // Hiển thị menu chức năng

        static void HienThiMenu()
        {
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1. Nhập mảng");
            Console.WriteLine("2. Xuất mảng");
            Console.WriteLine("3. Tính tổng");
            Console.WriteLine("4. Tìm max/min");
            Console.WriteLine("5. Đếm chẵn/lẻ");
            Console.WriteLine("6. Sắp xếp tăng dần");
            Console.WriteLine("7. Tìm kiếm");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn chức năng: ");
        }

        // Nhập lựa chọn menu
        static int NhapLuaChonMenu()
        {
            string input = Console.ReadLine();
            int luaChon;

            if (int.TryParse(input, out luaChon))
                return luaChon;

            
            return -1;
        }

        
        static bool KiemTraDaNhapMang()
        {
            if (!daNhapMang)
            {
                Console.WriteLine("Bạn chưa nhập mảng. Vui lòng chọn chức năng 1 trước..");
                return false;
            }
            return true;
        }

        // Nhập 1 số nguyên bất kì, nếu sai định dạng sẽ cho nhập lại
        static int NhapSoNguyen(string message)
        {
            int soNguyen;
            bool hopLe;

            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                hopLe = int.TryParse(input, out soNguyen);

                if (!hopLe)
                    Console.WriteLine("Dữ liệu nhập không hợp lệ. Vui lòng nhập lại một số nguyên.");

            } while (!hopLe);

            return soNguyen;
        }

        // Nhập 1 số nguyên dương, nếu sai định dạng sẽ cho nhập lại
        static int NhapSoNguyenDuong(string message)
        {
            int soNguyen;

            do
            {
                soNguyen = NhapSoNguyen(message);

                if (soNguyen <= 0)
                    Console.WriteLine("Giá trị phải là số nguyên dương. Vui lòng nhập lại.");

            } while (soNguyen <= 0);

            return soNguyen;
        }

        // Nhập số lượng phần tử và giá trị từng phần tử của mảng
        static int[] NhapMang()
        {
            int n = NhapSoNguyenDuong("Nhập số lượng phần tử của mảng (n > 0): ");
            int[] a = new int[n];

            for (int i = 0; i < n; i++)
            {
                a[i] = NhapSoNguyen("Nhập phần tử thứ " + (i + 1) + ": ");
            }

            return a;
        }

        // In toàn bộ phần tử của mảng ra màn hình
        static void XuatMang(int[] a)
        {
            Console.Write("Mảng: ");
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }

        // "Tính tổng 
        static int TinhTong(int[] a)
        {
            int tong = 0;
            for (int i = 0; i < a.Length; i++)
            {
                tong += a[i];
            }
            return tong;
        }

        // Tìm Max
        static int TimMax(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max;
        }

        // Tìm min
        static int TimMin(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] < min)
                    min = a[i];
            }
            return min;
        }

        // Đếm chẵn 
        static int DemChan(int[] a)
        {
            int demChan = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 == 0)
                    demChan++;
            }
            return demChan;
        }

        // Đếm lẻ
        static int DemLe(int[] a)
        {
            int demLe = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 != 0)
                    demLe++;
            }
            return demLe;
        }

        // Sắp xếp tăng dần 
        static void SapXepTangDan(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                int viTriNhoNhat = i;

                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < a[viTriNhoNhat])
                        viTriNhoNhat = j;
                }

                if (viTriNhoNhat != i)
                {
                    int tam = a[i];
                    a[i] = a[viTriNhoNhat];
                    a[viTriNhoNhat] = tam;
                }
            }
        }

        // Tìm kiếm phần tử
        static int TimKiem(int[] a, int x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == x)
                    return i;
            }
            return -1;
        }
    }
}
