using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace QuanLyCuaHangCaCanh_HuuQuyen.KetNoi
{
    internal class Modify
    {
        public Modify()
        {

        }
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public List<TaiKhoan> TaiKhoans(string query)
        {
            List<TaiKhoan> taikhoans = new List<TaiKhoan>();
            using(SqlConnection sql_conn = Connection.GetSqlConnection())
            {
                sql_conn.Open();
                sqlCommand = new SqlCommand(query, sql_conn);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    taikhoans.Add(new TaiKhoan(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(2)));
                }
                sql_conn.Close();
            }
            return taikhoans;
        } 
    }
}
