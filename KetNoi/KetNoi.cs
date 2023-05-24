using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangCaCanh_HuuQuyen.KetNoi
{
    internal class KetNoi
    {
        private string con_tr = "Data Source=DESKTOP-LGH1FNQ\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangBanCaCanh;Integrated Security=True";
        public DataSet LayDL(string query, string tenbang)
        {
            SqlConnection conn = new SqlConnection(con_tr);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, tenbang);
            return ds;
        }
        public bool HienThi(string query)
        {
            SqlConnection conn = new SqlConnection(con_tr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            int kt = cmd.ExecuteNonQuery();
            conn.Close();
            return kt > 0;
        }
    }
}
