using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyCuaHangCaCanh_HuuQuyen.CacForm
{
    public partial class DangNhap : Form
    {       
        public DangNhap()
        {
            InitializeComponent();
        }
        
        KetNoi.ConnectSQL con = new KetNoi.ConnectSQL();

        private void rdoHienMK_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkMK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMK.Checked)
            {
                txtMK.PasswordChar = (char)0;
            }
            else
            {
                txtMK.PasswordChar = '♥';
            }
        }
        private string contr = "Data Source=DESKTOP-3CF995N\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangBanCaCanh_KhacTu;Integrated Security=True";
        private bool KiemTra(string tk, string mk, string quyendn)
        {
            try
            {
                string query = String.Format("select count(*) from tb_TaiKhoan where UserName = '{0}' and PassWord = '{1}' and Quyen = '{2}'", tk, mk,quyendn);
                SqlConnection conn = new SqlConnection(contr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                int kt = (int)cmd.ExecuteScalar();
                conn.Close();
                if (kt == 1) return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            /*string tk = txtTK.Text;
            string mk = txtMK.Text;
            string quyendn = comboQuyendn.Text;
            if (txtTK.Text == "" || txtMK.Text == "" || quyendn == "") {
                MessageBox.Show("Vui lòng nhập tài khoản, mật khẩu và chọn quyền để đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (KiemTra(tk, mk, quyendn) == true )
            {
                dongtb.Text = "";
                MessageBox.Show("Đăng Nhập Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataTable tb = new DataTable();
                CacForm.HeThongQuanLy home = new CacForm.HeThongQuanLy(tb.Rows[0][0].ToString(), tb.Rows[0][1].ToString(), tb.Rows[0][2].ToString(), tb.Rows[0][3].ToString(), tb.Rows[0][4].ToString());
                //CacForm.HeThongQuanLy home = new CacForm.HeThongQuanLy();
                this.Hide();
                //home.MdiParent = this;
                home.Show();

            }
            else
            {
                dongtb.Text = "Sai Tài Khoản Hoặc Mật Khẩu \n Hoặc Chọn Lại Quyền Đăng Nhập Của Bạn";
               // MessageBox.Show("Sai Tài Khoản Hoặc Mật Khẩu. Vui Lòng Kiểm Tra Lại!");
                *//*txtTK.Clear();
                txtMK.Clear();
                comboQuyendn.Text = "";*//*
            }*/

            //--------------------------
            string query = String.Format("select * from tb_TaiKhoan where UserName = '{0}' and PassWord = '{1}' and Quyen = '{2}'", txtTK.Text, txtMK.Text, comboQuyendn.Text);

            string tk = txtTK.Text;
            string mk = txtMK.Text;
            string quyendn = comboQuyendn.Text;
            DataTable tb = new DataTable();
            tb = con.GetData(query);
            if (txtTK.Text == "" || txtMK.Text == "" || quyendn == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản, mật khẩu và chọn quyền để đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tb.Rows.Count > 0)
            {
                dongtb.Text = "";
                MessageBox.Show("Đăng Nhập Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CacForm.HeThongQuanLy home = new CacForm.HeThongQuanLy(tb.Rows[0][0].ToString(), tb.Rows[0][1].ToString(), tb.Rows[0][2].ToString(), tb.Rows[0][3].ToString(), tb.Rows[0][4].ToString());
               // CacForm.NhanVien nv = new CacForm.NhanVien(tb.Rows[0][0].ToString(), tb.Rows[0][1].ToString(), tb.Rows[0][2].ToString(), tb.Rows[0][3].ToString(), tb.Rows[0][4].ToString());
                this.Hide();
                //home.MdiParent = this;
                home.Show();
            }
            else
            {
                dongtb.Text = "Sai Tài Khoản Hoặc Mật Khẩu \n Hoặc Chọn Lại Quyền Đăng Nhập Của Bạn";               
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CacForm.DangKy dky = new CacForm.DangKy();
            //this.Hide();
            //home.MdiParent = this;
            dky.Show();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CacForm.QuenMatKhau quenmk = new CacForm.QuenMatKhau();
            quenmk.Show();
        }
    }
}
