using System;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    
    /// Lớp chứa điểm khởi đầu (entry point) của chương trình.

    internal static class Program
    {
      
        /// Điểm khởi đầu chính của ứng dụng.
    
        [STAThread]
        static void Main()
        {
            // Khởi tạo cấu hình mặc định cho ứng dụng WinForms
            ApplicationConfiguration.Initialize();

            // Chạy Form chính
            Application.Run(new Form1());
        }
    }
}
