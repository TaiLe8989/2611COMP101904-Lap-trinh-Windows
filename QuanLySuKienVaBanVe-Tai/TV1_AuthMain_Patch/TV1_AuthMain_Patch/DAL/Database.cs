using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLySuKienVaBanVe.DAL
{
    /// <summary>
    /// Bản triển khai tham khảo theo hợp đồng kỹ thuật của nhóm.
    /// Nếu TV3 đã có Database.cs thì KHÔNG thêm trùng file này; chỉ đối chiếu chữ ký hàm.
    /// Để truyền tham số, object[] nhận các đối tượng SqlParameter.
    /// </summary>
    public static class Database
    {
        public static SqlConnection GetConnection()
        {
            ConnectionStringSettings setting =
                ConfigurationManager.ConnectionStrings["ConnStr"] ??
                ConfigurationManager.ConnectionStrings["QuanLySuKienDB"];

            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                throw new ConfigurationErrorsException(
                    "Không tìm thấy connection string 'ConnStr' hoặc 'QuanLySuKienDB' trong App.config.");

            return new SqlConnection(setting.ConnectionString);
        }

        public static DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                AddParameters(command, parameters);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static int ExecuteNonQuery(string query, object[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                AddParameters(command, parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string query, object[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                AddParameters(command, parameters);
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        public static int ExecuteNonQuery(string query, SqlConnection connection,
            SqlTransaction transaction, object[] parameters = null)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                AddParameters(command, parameters);
                return command.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string query, SqlConnection connection,
            SqlTransaction transaction, object[] parameters = null)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                AddParameters(command, parameters);
                return command.ExecuteScalar();
            }
        }

        private static void AddParameters(SqlCommand command, object[] parameters)
        {
            if (parameters == null) return;
            foreach (object item in parameters)
            {
                SqlParameter parameter = item as SqlParameter;
                if (parameter == null)
                    throw new ArgumentException(
                        "Mỗi phần tử parameters phải là SqlParameter (ví dụ new SqlParameter(\"@MaTK\", value)).");

                SqlParameter copy = command.Parameters.Add(parameter.ParameterName, parameter.SqlDbType);
                if (parameter.Size > 0) copy.Size = parameter.Size;
                copy.Direction = parameter.Direction;
                copy.Value = parameter.Value ?? DBNull.Value;
            }
        }
    }
}
