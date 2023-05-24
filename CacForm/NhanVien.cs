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
    public partial class NhanVien : Form
    {
        public string HoTen = "", TaiKhoan = "", MatKhau = "", Email = "", Quyen = "";
        public NhanVien()
        {
            InitializeComponent();
        }
        public NhanVien(string ten, string tk, string mk, string email, string quyen)
        {
            InitializeComponent();
            this.HoTen = ten;
            this.TaiKhoan = tk;
            this.MatKhau = mk;
            this.Email = email;
            this.Quyen = quyen;
        }
        KetNoi.KetNoi kn = new KetNoi.KetNoi();
        private void getData()
        {
            string query = "select * from tb_NhanVien";
            DataSet ds = kn.LayDL(query, "tb_NhanVien");
            dgviewNhanVien.DataSource = ds.Tables["tb_NhanVien"];
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            getData();
            dgviewNhanVien.Columns[0].HeaderText = "Mã Nhân Viên";
            dgviewNhanVien.Columns[1].HeaderText = "Tên Nhân Viên";
            dgviewNhanVien.Columns[2].HeaderText = "Giới Tính";
            dgviewNhanVien.Columns[3].HeaderText = "Địa Chỉ";
            dgviewNhanVien.Columns[4].HeaderText = "Số Điện Thoại";
            dgviewNhanVien.Columns[5].HeaderText = "Ngày Sinh";

            dgviewNhanVien.Columns[0].Width = 100;
            dgviewNhanVien.Columns[1].Width = 172;
            dgviewNhanVien.Columns[2].Width = 70;
            dgviewNhanVien.Columns[3].Width = 130;
            dgviewNhanVien.Columns[4].Width = 130;
            dgviewNhanVien.Columns[5].Width = 130;
        }
        private void ResetValues()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtGioiTinh.Text = "";
            txtNgaySinh.Text = "";
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
            txtMaNV.Enabled = true;
            getData();
            //txtTimKiem.Text = "";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Hide();
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            KetNoi.Function Functions = new KetNoi.Function();
            string sql;
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNV.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtSDT.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return;
            }
            //Kiểm tra đã tồn tại mã khách chưa

            sql = "SELECT MaNhanVien FROM tb_NhanVien WHERE MaNhanVien=N'" + txtMaNV.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            //Chèn thêm
            sql = "INSERT INTO tb_NhanVien VALUES (N'" + txtMaNV.Text.Trim() +
                "',N'" + txtTenNV.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() + "','" + txtSDT.Text + "')";

            bool kt = kn.HienThi(sql);
            if (kt == true)
            {
                getData();
                MessageBox.Show("Bạn Đã Lưu Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }

            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            //btnSave.Enabled = false;
            txtMaNV.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
           // KetNoi.Function Functions = new KetNoi.Function();
            //
            string a1 = txtMaNV.Text;
            string a2 = txtTenNV.Text;
            string a3 = txtGioiTinh.Text;
            string a4 = txtDiaChi.Text;
            string a5 = txtSDT.Text;
            string a6 = txtNgaySinh.Text;
            /*if (Quyen == "Admin")
            {*/
                //  KIỂM TRA 
                if (txtMaNV.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtTenNV.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNV.Focus();
                    return;
                }
                if (txtGioiTinh.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGioiTinh.Focus();
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
                if (txtNgaySinh.Text == "  /  /")
                {
                    MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgaySinh.Focus();
                    return;
                }
                /*if (!Functions.IsDate(txtNgaySinh.Text))
                {
                    MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgaySinh.Text = "";
                    txtNgaySinh.Focus();
                    return;
                }*/
                // SỬA THÔNG TIN
                string query = String.Format("update tb_NhanVien set TenNhanVien = N'{0}', GioiTinh = N'{1}', DiaChi = N'{2}', SoDienThoai = '{3}', NgaySinh = '{4}' where MaNhanVien = '{5}'", a2, a3, a4, a5, a6, a1);
                bool kt = kn.HienThi(query);
                if (kt == true)
                {
                    getData();
                    btnXoa.Enabled = false;
                    btnThem.Enabled = false;
                    btnSua.Enabled = true;
                    btnBoQua.Enabled = true;
                    //btnSave.Enabled = false;
                    txtMaNV.Enabled = false;
                    MessageBox.Show("Bạn Đã Sửa Lại Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                }
                else
                {
                    MessageBox.Show("Sửa Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            /*}
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!\n Chỉ ADMIN mới được phép sửa thông tin nhân viên", "Xin Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void dgviewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = this.dgviewNhanVien.Rows[e.RowIndex];
                //Đưa dữ liệu vào textbox
                txtMaNV.Text = row.Cells[0].Value.ToString();
                txtTenNV.Text = row.Cells[1].Value.ToString();
                txtGioiTinh.Text = row.Cells[2].Value.ToString();
                txtDiaChi.Text = row.Cells[3].Value.ToString();
                txtSDT.Text = row.Cells[4].Value.ToString();
                txtNgaySinh.Text = row.Cells[5].Value.ToString();
                
            }
            txtMaNV.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //KetNoi.Function Functions = new KetNoi.Function();
            //
            string a1 = txtMaNV.Text;
            string a2 = txtTenNV.Text;
            string a3 = txtGioiTinh.Text;
            string a4 = txtDiaChi.Text;
            string a5 = txtSDT.Text;
            string a6 = txtNgaySinh.Text;
            /*if (Quyen == "Admin")
            {*/
                //  KIỂM TRA 
                if (txtMaNV.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaNV.Focus();
                    return;
                }
                if (txtTenNV.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNV.Focus();
                    return;
                }
                if (txtGioiTinh.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGioiTinh.Focus();
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
                if (txtNgaySinh.Text == "  /  /")
                {
                    MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgaySinh.Focus();
                    return;
                }

                // Thêm thông tin
                string query = String.Format("insert into tb_NhanVien (MaNhanVien, TenNhanVien, GioiTinh, DiaChi, SoDienThoai, NgaySinh) " +
                    " values('{0}', N'{1}', N'{2}', N'{3}', '{4}', '{5}')", a1, a2, a3, a4, a5, a6);
                bool kt = kn.HienThi(query);
                if (kt == true)
                {
                    getData();
                    /*btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnBoQua.Enabled = true;*/
                    //btnSave.Enabled = false;
                    txtMaNV.Enabled = true;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dg = MessageBox.Show("Bạn có muốn xóa thông tin này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                string a1 = txtMaNV.Text;
                /*if (Quyen == "Admin")
                {*/
                //  KIỂM TRA 
                

                // XÓA 
                string query = String.Format("delete from tb_NhanVien where MaNhanVien = '{0}'", a1);
                bool kt = kn.HienThi(query);
                if (kt == true)
                {
                    getData();
                    MessageBox.Show("Bạn Đã Xóa Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    txtMaNV.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Xóa Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                /*}
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập vào đây!\n Chỉ ADMIN mới được phép xóa thông tin nhân viên", "Xin Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaNV.Text;
            string a2 = txtTenNV.Text;
            string a3 = txtGioiTinh.Text;
            string a4 = txtDiaChi.Text;
            string a5 = txtSDT.Text;

            // kiểm tra
            if (a1 == "" && a2 == "" && a3 == "" && a4 == "" && a5 == "" )
            {
                MessageBox.Show("Bạn hãy nhập thông tin cần tìm kiếm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // search
            string query = String.Format("select * from tb_NhanVien where MaNhanVien like '%{0}%' and TenNhanVien like N'%{1}%' " +
                " and GioiTinh like N'%{2}%' and DiaChi like N'%{3}%' and SoDienThoai like '%{4}%' ", a1,a2,a3,a4,a5);
            DataSet ds = kn.LayDL(query, "tb_NhanVien");
            dgviewNhanVien.DataSource = ds.Tables["tb_NhanVien"];
        }
    }
}
