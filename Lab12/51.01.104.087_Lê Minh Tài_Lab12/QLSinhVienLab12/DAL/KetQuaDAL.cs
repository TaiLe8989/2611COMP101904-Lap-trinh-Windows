using System.Data;
using Microsoft.Data.SqlClient;

namespace QLSinhVienLab12.DAL
{
    public class KetQuaDAL
    {
        public DataTable GetLop()
        {
            return LayDuLieu("sp_Lop_GetAll");
        }

        public DataTable GetMonHoc()
        {
            return LayDuLieu("sp_MonHoc_GetAll");
        }

        public DataTable GetKetQuaByLopMon(string maLop, string maMon)
        {
            SqlParameter[] thamSo =
            {
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = maLop },
                new SqlParameter("@MaMon", SqlDbType.NVarChar, 20) { Value = maMon }
            };
            return LayDuLieu("sp_KetQua_GetByLopMon", thamSo);
        }

        public int SaveAll(string maMon, DataTable table)
        {
            using SqlConnection conn = new SqlConnection(Database.ConnectionString);
            conn.Open();
            using SqlTransaction trans = conn.BeginTransaction();

            try
            {
                int soDong = 0;

                foreach (DataRow row in table.Rows)
                {
                    using SqlCommand cmd = new SqlCommand("sp_KetQua_Save", conn, trans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar, 20).Value = row["MaSV"].ToString();
                    cmd.Parameters.Add("@MaMon", SqlDbType.NVarChar, 20).Value = maMon;
                    cmd.Parameters.Add("@Diem", SqlDbType.Float).Value = Convert.ToDouble(row["Diem"]);
                    cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 255).Value = (row["GhiChu"].ToString() ?? "").Trim();
                    cmd.ExecuteNonQuery();
                    soDong++;
                }

                trans.Commit();
                return soDong;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        private DataTable LayDuLieu(string tenProc, params SqlParameter[] thamSo)
        {
            using SqlConnection conn = new SqlConnection(Database.ConnectionString);
            using SqlCommand cmd = new SqlCommand(tenProc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(thamSo);

            using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
