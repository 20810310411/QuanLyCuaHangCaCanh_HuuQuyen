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
using System.Text.RegularExpressions;

namespace QuanLyCuaHangCaCanh_HuuQuyen.CacForm
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }
        KetNoi.KetNoi kn = new KetNoi.KetNoi();

        public bool CheckTK(string ac) // check tk 
        {
            return Regex.IsMatch(ac, "^[a-z-0-9]{4,24}$"); // 4 chu cai do len, < 24 ki tu, từ a-z , 0-9
        }
        public bool CheckMK(string ac) // check mk
        {
            return Regex.IsMatch(ac, "^[a-z-0-9]{6,24}$"); // 6 chu cai do len, < 24 ki tu, a-z,  0-9
        }
        public bool CheckEmail(string ac) // check email
        {
            return Regex.IsMatch(ac, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        private string contr = "Data Source=DESKTOP-LGH1FNQ\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangBanCaCanh;Integrated Security=True";
        private bool KiemTra(string tk)
        {
            try
            {
                string query = String.Format("select count(*) from tb_TaiKhoan where UserName = '{0}'  ", tk);
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

        private void btnDky_Click(object sender, EventArgs e)
        {
            string hoten = txtHoVaTen.Text;
            string username = txtTaiKhoan.Text;
            string email = txtEmail.Text;
            string password = txtMatKhau.Text;
            string checkMK = txtXacNhanMK.Text;
            string quyendn = comboQuyen.Text;

            if (hoten == "") {
                MessageBox.Show("Vui lòng nhập họ và tên của bạn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CheckTK(username)) {
                MessageBox.Show("Vui lòng nhập tên tài khoản dài 4-24 kí tự, với các ký tự chữ và số, Không có kí tự đặc biệt!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTaiKhoan.Clear();
                return;
            }
            if (!CheckEmail(email))
            {
                MessageBox.Show("Vui lòng nhập Email theo đúng định dạng! VD: xxx@gmail.com", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Clear();
                return;
            }
            /*if (KiemTra(email) == true)
            {
                MessageBox.Show("Email này đã tồn tại. Vui lòng chọn Email khác !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            if (!CheckMK(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 kí tự, với các ký tự chữ và số, chữ hoa và chữ thường!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtXacNhanMK.Clear();
                return;
            }
            if (password != checkMK) {
                MessageBox.Show("Xác nhận mật khẩu không khớp. Vui lòng xác nhận lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtXacNhanMK.Clear();
                return;
            }
            if (KiemTra(username) == true)
            {
                MessageBox.Show("Tài khoản này đã tồn tại. Vui lòng tạo lại tài khoản khác !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTaiKhoan.Clear();
                return;
            }
            if (quyendn == "")
            {
                MessageBox.Show("Hãy Chọn Quyền Đăng Nhập Của Bạn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else {
                string query = String.Format("insert into tb_TaiKhoan (HoTen, UserName, PassWord, Email, Quyen) values(N'{0}','{1}','{2}', '{3}', '{4}')", hoten,username,password, email, quyendn);
                bool kt = kn.HienThi(query);
                if (kt == true)
                {
                    MessageBox.Show("Đăng Ký Tài Khoàn Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoVaTen.Clear();
                    txtTaiKhoan.Clear();
                    txtEmail.Clear();  
                    txtMatKhau.Clear();
                    txtXacNhanMK.Clear();
                    comboQuyen.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Đăng Ký Tài Khoàn Thất Bại! Vui Lòng Đăng Ký Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void checkHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHienMK.Checked)
            {
                txtMatKhau.PasswordChar = (char)0;
                txtXacNhanMK.PasswordChar = (char)0;
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
                txtXacNhanMK.PasswordChar = '*';
            }
        }
    }
}
