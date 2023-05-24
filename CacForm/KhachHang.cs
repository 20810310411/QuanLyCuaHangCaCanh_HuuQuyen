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
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
        }
        KetNoi.KetNoi kn = new KetNoi.KetNoi();
        private void getData()
        {
            string query = "select * from tb_KhachHang";
            DataSet ds = kn.LayDL(query, "tb_KhachHang");
            dgviewKhachHang.DataSource = ds.Tables["tb_KhachHang"];
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Hide();
            }
        }
        private void ResetValues()
        {
            txtMaKhachHang.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            //btnSave.Enabled = false;
            txtMaKhachHang.Enabled = true;
            getData();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            getData();
            dgviewKhachHang.Columns[0].HeaderText = "Mã Khách Hàng";
            dgviewKhachHang.Columns[1].HeaderText = "Tên Khách Hàng";
            dgviewKhachHang.Columns[2].HeaderText = "Địa Chỉ";
            dgviewKhachHang.Columns[3].HeaderText = "Số Điện Thoại";

            dgviewKhachHang.Columns[0].Width = 140;
            dgviewKhachHang.Columns[1].Width = 172;
            dgviewKhachHang.Columns[2].Width = 150;
            dgviewKhachHang.Columns[3].Width = 130;

        }

        private void dgviewKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = this.dgviewKhachHang.Rows[e.RowIndex];
                //Đưa dữ liệu vào textbox
                txtMaKhachHang.Text = row.Cells[0].Value.ToString();
                txtTenKH.Text = row.Cells[1].Value.ToString();
                txtDiaChi.Text = row.Cells[2].Value.ToString();
                txtSDT.Text = row.Cells[3].Value.ToString();
            }
            txtMaKhachHang.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaKhachHang.Text;
            string a2 = txtTenKH.Text;
            string a3 = txtDiaChi.Text;
            string a4 = txtSDT.Text;

            /*if (Quyen == "Admin")
            {*/
            //  KIỂM TRA 
            if (txtMaKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKhachHang.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }          
            if (txtSDT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            // Thêm thông tin
            string query = String.Format("insert into tb_KhachHang (MaKhachHang, TenKhachHang, DiaChi, SoDienThoai) " +
                " values('{0}', N'{1}', N'{2}', '{3}')",a1,a2,a3,a4);
            bool kt = kn.HienThi(query);
            if (kt == true)
            {
                getData();
                
                txtMaKhachHang.Enabled = true;
                MessageBox.Show("Bạn Đã Thêm Mới Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetValues();

            }
            else
            {
                MessageBox.Show("Thêm Mới Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            /*}
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!\n Chỉ ADMIN mới được phép thêm mới nhân viên", "Xin Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaKhachHang.Text;
            string a2 = txtTenKH.Text;
            string a3 = txtDiaChi.Text;
            string a4 = txtSDT.Text;

            //  KIỂM TRA 
            if (txtMaKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKhachHang.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtSDT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            // SỬA THÔNG TIN
            string query = String.Format("update tb_KhachHang set TenKhachHang = N'{0}', DiaChi = N'{1}', SoDienThoai = '{2}' where MaKhachHang = '{3}'",a2,a3,a4,a1);
            bool kt = kn.HienThi(query);
            if (kt == true)
            {
                getData();
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnBoQua.Enabled = true;
                //btnSave.Enabled = false;
                txtMaKhachHang.Enabled = false;
                MessageBox.Show("Bạn Đã Sửa Lại Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValues();
            }
            else
            {
                MessageBox.Show("Sửa Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaKhachHang.Text;
            
            if (txtMaKhachHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dg = MessageBox.Show("Bạn có muốn xóa thông tin này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                // XÓA 
                string query = String.Format("delete from tb_KhachHang where MaKhachHang = '{0}'", a1);
                bool kt = kn.HienThi(query);
                if (kt == true)
                {
                    getData();
                    MessageBox.Show("Bạn Đã Xóa Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    txtMaKhachHang.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Xóa Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }
    }
}
