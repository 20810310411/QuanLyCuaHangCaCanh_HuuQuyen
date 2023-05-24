using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangCaCanh_HuuQuyen.KetNoi
{
    internal class Connection
    {
        private static string con_tr = "Data Source=DESKTOP-LGH1FNQ\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangBanCaCanh;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(con_tr);
        }
    }
}
