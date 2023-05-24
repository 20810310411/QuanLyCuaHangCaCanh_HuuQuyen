using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangCaCanh_HuuQuyen.CacForm
{
    public partial class HeThongQuanLy : Form
    {
        public string HoTen = "", TaiKhoan = "", MatKhau = "", Email = "", Quyen = "";
        //tb.Rows[0][0].ToString(), tb.Rows[0][1].ToString(), tb.Rows[0][2].ToString(), tb.Rows[0][3].ToString(), tb.Rows[0][4].ToString()
        public HeThongQuanLy(string ten, string tk, string mk, string email, string quyen)
        {
            InitializeComponent();
            this.HoTen = ten;
            this.TaiKhoan = tk;
            this.MatKhau = mk;
            this.Email = email;
            this.Quyen = quyen;
        }
        public HeThongQuanLy()
        {
            InitializeComponent();
        }

        private void hàngCáCảnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Quyen == "Admin")
            {
                CacForm.HangCaCanh dn = new CacForm.HangCaCanh();
                //this.Hide();
                dn.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!\n Chỉ ADMIN mới được phép truy cập vào!", "Xin Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void hóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CacForm.HoaDonBanHang dn = new CacForm.HoaDonBanHang();
            //this.Hide();
            dn.Show();
        }

        private void tìmKiếmHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CacForm.TimKiemHoaDon dn = new CacForm.TimKiemHoaDon();
            //this.Hide();
            dn.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CacForm.DangNhap dn = new CacForm.DangNhap();
            this.Hide();
            dn.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát hệ thống này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Quyen == "Admin")
            {
                CacForm.NhanVien dn = new CacForm.NhanVien();
                //this.Hide();
                dn.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!\n Chỉ ADMIN mới được phép truy cập vào!", "Xin Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*CacForm.NhanVien dn = new CacForm.NhanVien();
            //this.Hide();
            dn.Show();*/
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* if (Quyen == "Admin")
             {
                 CacForm.KhachHang dn = new CacForm.KhachHang();
                 //this.Hide();
                 dn.Show();
             }
             else
             {
                 MessageBox.Show("Bạn không có quyền truy cập vào đây!\n Chỉ ADMIN mới được phép truy cập vào!", "Xin Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }*/
            CacForm.KhachHang dn = new CacForm.KhachHang();
            //this.Hide();
            dn.Show();
        }
    }
}
