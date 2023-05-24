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
using System.Configuration;

namespace QuanLyCuaHangCaCanh_HuuQuyen.CacForm
{
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
            dongtbLayMK.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkHienThiMK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHienThiMK.Checked)
            {
                txtMK_HienTai_dmk.PasswordChar = (char)0;
                txtMK_Moi_dmk.PasswordChar = (char)0;
                txtXacNhanMK_Moi_dmk.PasswordChar = (char)0;
            }
            else
            {
                txtMK_HienTai_dmk.PasswordChar = '*';
                txtMK_Moi_dmk.PasswordChar = '*';
                txtXacNhanMK_Moi_dmk.PasswordChar = '*';
            }
        }

        private void btnThoat_dmk_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Close();
            }
        }
        //kiểm tra xem người dùng đã nhập đầy đủ thông tin hay chưa
        public bool KiemTra()
        {
            if (txtTK_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng nhập tên tài khoản !!";
                txtTK_dmk.Focus();
                return false;
            }
            else if (txtMK_HienTai_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng nhập mật khẩu hiện tại !!";
                txtMK_HienTai_dmk.Focus();
                return false;
            }
            else if (txtMK_Moi_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng nhập mật khẩu mới !!";
                txtMK_Moi_dmk.Focus();
                return false;
            }
            else if (txtXacNhanMK_Moi_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng xác nhận mật khẩu !!";
                txtXacNhanMK_Moi_dmk.Focus();
                return false;
            }
            else if (txtXacNhanMK_Moi_dmk.Text != txtMK_Moi_dmk.Text)
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Mật khẩu mới và mật khẩu xác nhận không trùng khớp";
                txtXacNhanMK_Moi_dmk.Focus();
                txtXacNhanMK_Moi_dmk.SelectAll();
                return false;
            }
            return true;
        }
        private string contr = "Data Source=DESKTOP-LGH1FNQ\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangBanCaCanh;Integrated Security=True";
        KetNoi.KetNoi kn = new KetNoi.KetNoi();

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            if (txtTK_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng nhập tên tài khoản !!";
                txtTK_dmk.Focus();
                return;
            }
            else if (txtMK_HienTai_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng nhập mật khẩu hiện tại !!";
                txtMK_HienTai_dmk.Focus();
                return;
            }
            else if (txtMK_Moi_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng nhập mật khẩu mới !!";
                txtMK_Moi_dmk.Focus();
                return;
            }
            else if (txtXacNhanMK_Moi_dmk.Text == "")
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Vui lòng xác nhận mật khẩu !!";
                txtXacNhanMK_Moi_dmk.Focus();
                return;
            }
            else if (txtXacNhanMK_Moi_dmk.Text != txtMK_Moi_dmk.Text)
            {
                dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                dongtb_dmk.Text = "Mật khẩu mới và mật khẩu xác nhận không trùng khớp";
                txtXacNhanMK_Moi_dmk.Focus();
                txtXacNhanMK_Moi_dmk.SelectAll();
                return;
            }
            else
            {
               // string query2 = String.Format("select count(*) from tb_TaiKhoan where    PassWord = '{0}'",txtTK_dmk.Text);
                string query = String.Format("update tb_TaiKhoan set PassWord = '{0}' where UserName = '{1}' ", txtMK_Moi_dmk.Text,txtTK_dmk.Text);
                //bool kt2 = kn.HienThi(query2);
                bool kt = kn.HienThi(query);
                //if (kt2 == true)
                //{
                    if (kt == true)
                    {
                        dongtb_dmk.ForeColor = System.Drawing.Color.Blue;
                        dongtb_dmk.Text = "Bạn Đã Đổi Mật Khẩu Thành Công";
                        MessageBox.Show("Bạn Đã Đổi Mật Khẩu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTK_dmk.Text = "";
                        txtMK_HienTai_dmk.Text = "";
                        txtMK_Moi_dmk.Text = "";
                        txtXacNhanMK_Moi_dmk.Text = "";
                        dongtb_dmk.Text = "";
                    }
                    else
                    {
                        dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                        dongtb_dmk.Text = "Đổi Mật Khẩu Thất Bại. Vui Lòng Kiểm Tra Lại Tài Khoản Hoặc Mật Khẩu !!";
                        //MessageBox.Show("Đổi Mật Khẩu Thất Bại! ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
               // }
               /* else
                {
                    dongtb_dmk.ForeColor = System.Drawing.Color.Red;
                    dongtb_dmk.Text = "Mật Khẩu Cũ Không Đúng. Vui Lòng Kiểm Tra Lại !";
                    //MessageBox.Show("Đổi Mật Khẩu Thất Bại! ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }*/
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        KetNoi.Modify modify = new KetNoi.Modify();
        private void btnLayMK_Click(object sender, EventArgs e)
        {
            string email = txtEmailDKY.Text;
            if(email.Trim() == "")
            {
                dongtbLayMK.Text = "";
                MessageBox.Show("Vui lòng nhập email đăng kí!");
            }
            else
            {
                string query = String.Format("select * from tb_TaiKhoan where Email = '{0}'",email);
                if(modify.TaiKhoans(query).Count != 0)
                {
                    dongtbLayMK.ForeColor = Color.Blue;
                    //dongtbLayMK.Text = "Email này chưa được đăng kí!";
                    dongtbLayMK.Text = "Mật Khẩu: " + modify.TaiKhoans(query)[0].MatKhau+" ";
                }
                else
                {
                    dongtbLayMK.ForeColor = Color.Red;
                    dongtbLayMK.Text = "Email này chưa được đăng kí!";
                }
            }
        }
    }
}
