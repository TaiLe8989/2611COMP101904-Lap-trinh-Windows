using System;
using System.Data;
using System.Data.SqlClient;
using QuanLySuKienVaBanVe.DTO;

namespace QuanLySuKienVaBanVe.DAL
{
    public class TaiKhoanDAL
    {
        public TaiKhoanDTO LayTheoTenDangNhap(string tenDangNhap)
        {
            const string sql = @"SELECT TOP (1) MaTK, TenDangNhap, MatKhau, VaiTro, TrangThai
                                 FROM dbo.TaiKhoan WHERE TenDangNhap = @TenDangNhap;";
            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 50).Value = tenDangNhap;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read()) return null;
                    return Map(reader);
                }
            }
        }

        public TaiKhoanDTO LayTheoMa(int maTK)
        {
            const string sql = @"SELECT TOP (1) MaTK, TenDangNhap, MatKhau, VaiTro, TrangThai
                                 FROM dbo.TaiKhoan WHERE MaTK = @MaTK;";
            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@MaTK", SqlDbType.Int).Value = maTK;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read()) return null;
                    return Map(reader);
                }
            }
        }

        public bool CapNhatMatKhau(int maTK, string matKhauHash)
        {
            const string sql = "UPDATE dbo.TaiKhoan SET MatKhau = @MatKhau WHERE MaTK = @MaTK;";
            using (SqlConnection connection = Database.GetConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 255).Value = matKhauHash;
                command.Parameters.Add("@MaTK", SqlDbType.Int).Value = maTK;
                connection.Open();
                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>Đăng ký tài khoản nhân viên; GUI không được truyền vai trò quản lý.</summary>
        public bool ThemNhanVien(string tenDangNhap, string matKhauHash, out string thongBao)
        {
            const string sql = @"INSERT INTO dbo.TaiKhoan(TenDangNhap, MatKhau, VaiTro, TrangThai)
                                 VALUES(@TenDangNhap, @MatKhau, N'NhanVien', 1);";
            try
            {
                using (SqlConnection connection = Database.GetConnection())
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 50).Value = tenDangNhap;
                    command.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 255).Value = matKhauHash;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                thongBao = "Tạo tài khoản nhân viên thành công.";
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    thongBao = "Tên đăng nhập đã tồn tại. Hãy chọn tên khác.";
                    return false;
                }
                throw;
            }
        }

        private static TaiKhoanDTO Map(SqlDataReader reader)
        {
            return new TaiKhoanDTO
            {
                MaTK = Convert.ToInt32(reader["MaTK"]),
                TenDangNhap = Convert.ToString(reader["TenDangNhap"]),
                MatKhau = Convert.ToString(reader["MatKhau"]),
                VaiTro = Convert.ToString(reader["VaiTro"]),
                TrangThai = reader["TrangThai"] != DBNull.Value && Convert.ToBoolean(reader["TrangThai"])
            };
        }
    }
}
