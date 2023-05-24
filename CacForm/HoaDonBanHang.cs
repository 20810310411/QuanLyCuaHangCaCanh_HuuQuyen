using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangCaCanh_HuuQuyen.CacForm
{
    public partial class HoaDonBanHang : Form
    {
        public HoaDonBanHang()
        {
            InitializeComponent();
        }
        KetNoi.KetNoi kn = new KetNoi.KetNoi();
        KetNoi.Function Functions = new KetNoi.Function();
        DataTable tblCTHDB; //Bảng chi tiết hoá đơn bán
        private void getData()
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
           // btnExcel.Enabled = true;
            txtMaHD_hienthi.ReadOnly = false;
            txtTenNV.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDiaChi_KH.ReadOnly = true;
            txtSDT_KH.ReadOnly = true;
            txtMaCaCanh.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";

            string query = "  select tb_HangCa.TenCa, tb_ChiTietHDBan.SoLuong, tb_HangCa.DonGiaBan, tb_ChiTietHDBan.GiamGia, tb_ChiTietHDBan.ThanhTien" +
                " from tb_HangCa, tb_ChiTietHDBan   where tb_HangCa.MaCa = tb_ChiTietHDBan.MaCa ";
            DataSet ds = kn.LayDL(query, "tb_ChiTietHDBan");
            dgviewHoaDonBanHang.DataSource = ds.Tables["tb_ChiTietHDBan"];    
            
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NgayBan FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
            txtDate_NgayBan.Text = Functions.ConvertDateTime(Functions.GetFieldValues(str));
            str = "SELECT MaNhanVien FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
            txtMaNV.Text = Functions.GetFieldValues(str);
            str = "SELECT MaKhachHang FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
            txtMaKH.Text = Functions.GetFieldValues(str);
            str = "SELECT TongTien FROM tb_HDBan WHERE MaHDBan = '" + txtMaHD_hienthi.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
            //label_thongbao_ThanhChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(txtTongTien.Text);

            /*string str;
            str = "SELECT NgayBan FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
            txtDate_NgayBan.Text = Functions.ConvertDateTime(Functions.GetFieldValues(str));
            str = "SELECT MaNhanVien FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
            txtMaNV.Text = Functions.GetFieldValues(str);
            str = "SELECT MaKhachHang FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
            txtMaKH.Text = Functions.GetFieldValues(str);

            // cập nhật lại tổng tiền cho hóa đơn
            double tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tb_HDBan WHERE MaHDBan = '" + txtMaHD_hienthi.Text + "'"));
            double tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            string sql = "update tb_HDBan set TongTien = " + tongmoi + " where MaHDBan = '" + txtMaHD_hienthi.Text + "' ";
            Functions.RunSQL(sql);  
            txtTongTien.Text = tongmoi.ToString();
            label_thongbao_ThanhChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(txtTongTien.Text);*/


        }
        private void LoadDataGridView()
        {
            string sql;
            /*sql = "SELECT  b.TenCa, a.SoLuong, b.DonGiaBan, a.GiamGia,a.ThanhTien FROM tb_ChiTietHDBan AS a, tb_HangCa AS b WHERE a.MaHDBan = N'"
                + txtMaHD_hienthi.Text + "' AND a.MaCa=b.MaCa";*/
            sql = "SELECT a.MaCa, b.TenCa, a.SoLuong, b.DonGiaBan, a.GiamGia,a.ThanhTien FROM tb_ChiTietHDBan AS a, tb_HangCa AS b WHERE a.MaHDBan = N'"
                + txtMaHD_hienthi.Text + "' AND a.MaCa=b.MaCa";
            Functions.Connect();
            tblCTHDB = Functions.GetDataToTable(sql);
            dgviewHoaDonBanHang.DataSource = tblCTHDB;
            dgviewHoaDonBanHang.Columns[0].HeaderText = "Mã Cá";
            dgviewHoaDonBanHang.Columns[1].HeaderText = "Tên Cá Cảnh";
            dgviewHoaDonBanHang.Columns[2].HeaderText = "Số lượng";
            dgviewHoaDonBanHang.Columns[3].HeaderText = "Đơn giá";
            dgviewHoaDonBanHang.Columns[4].HeaderText = "Giảm giá %";
            dgviewHoaDonBanHang.Columns[5].HeaderText = "Thành tiền";
            dgviewHoaDonBanHang.Columns[0].Width = 100;
            dgviewHoaDonBanHang.Columns[1].Width = 160;
            dgviewHoaDonBanHang.Columns[2].Width = 130;
            dgviewHoaDonBanHang.Columns[3].Width = 130;
            dgviewHoaDonBanHang.Columns[4].Width = 140;
            dgviewHoaDonBanHang.Columns[5].Width = 180;
            dgviewHoaDonBanHang.AllowUserToAddRows = false;
            dgviewHoaDonBanHang.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgviewHoaDonBanHang.DataSource = tblCTHDB;

            //getData();
        }
        private void HoaDonBanHang_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnExcel.Enabled = true;
           // txtMaHD_hienthi.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtMaCaCanh.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDiaChi_KH.ReadOnly = true;
            txtSDT_KH.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";
            Functions.FillCombo("SELECT MaKhachHang, TenKhachHang FROM tb_KhachHang", txtMaKH, "MaKhachHang", "MaKhachHang");
            txtMaKH.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaNhanVien, TenNhanVien FROM tb_NhanVien", txtMaNV, "MaNhanVien", "TenKhachHang");
            txtMaNV.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaCa, TenCa FROM tb_HangCa", txtSearch, "TenCa", "TenCa");
            txtSearch.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHD_hienthi.Text != "")
            {
                LoadInfoHoaDon();
                btnXoa.Enabled = true;
                btnExcel.Enabled = true;
            }
            LoadDataGridView();

           // getData();
            

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            /* btnXoa.Enabled = false;
             //btnLuu.Enabled = true;
             btnExcel.Enabled = false;
             btnThem.Enabled = false;*/
            //ResetValues();
            //txtMaHD_hienthi.Text = Functions.CreateKey("HDB");
            // LoadDataGridView();
            //
            /*string a0 = txtMaHD_hienthi.Text;
            string a1 = txtMaCaCanh.Text;
            string a2 = txtDonGia.Text;
            string a3 = txtSoLuong.Text;
            string a4 = txtGiamGia.Text;
            string a5 = txtThanhTien.Text;

            *//*if (Quyen == "Admin")
            {*//*
            //  KIỂM TRA 


            // Thêm thông tin
            string query = String.Format("insert into tb_ChiTietHDBan (MaHDBan, MaCa, DonGia, SoLuong, GiamGia, ThanhTien) " +
                " values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", a0, a1, a2, a3, a4, a5);
            bool kt = kn.HienThi(query);
            if (kt == true)
            {
                getData();

                //txtMaKhachHang.Enabled = true;
                MessageBox.Show("Bạn Đã Thêm Mới Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //ResetValues();

            }
            else
            {
                MessageBox.Show("Thêm Mới Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/

            //------------
            string sql;
            double sl, SLcon, tong, Tongmoi;
            if (txtMaHD_hienthi.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDate_NgayBan.Focus();
                return;
            }
            sql = "SELECT MaHDBan FROM tb_HDBan WHERE MaHDBan=N'" + txtMaHD_hienthi.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                
                if (txtDate_NgayBan.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate_NgayBan.Focus();
                    return;
                }
                if (txtMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaNV.Focus();
                    return;
                }
                if (txtMaKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaKH.Focus();
                    return;
                }
                sql = "INSERT INTO tb_HDBan (MaHDBan, NgayBan, MaNhanVien, MaKhachHang, TongTien) VALUES (N'" + txtMaHD_hienthi.Text.Trim() + "','" +
                        Functions.ConvertDateTime(txtDate_NgayBan.Text.Trim()) + "',N'" + txtMaNV.SelectedValue + "',N'" +
                        txtMaKH.SelectedValue + "'," + txtTongTien.Text + ")";
                Functions.RunSQL(sql);
            }
            // Lưu thông tin của các mặt hàng
            if (txtMaKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã cá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "SELECT MaCa FROM tb_ChiTietHDBan WHERE MaCa=N'" + txtMaCaCanh.Text + "' AND MaHDBan = N'" + txtMaHD_hienthi.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hóa đơn này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ResetValuesHang();
                txtMaCaCanh.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tb_HangCa WHERE MaCa = N'" + txtMaCaCanh.Text + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "INSERT INTO tb_ChiTietHDBan (MaHDBan,MaCa,SoLuong,DonGia, GiamGia,ThanhTien) VALUES(N'" + txtMaHD_hienthi.Text.Trim() + "',N'" + txtMaCaCanh.Text + "'," + txtSoLuong.Text + "," + txtDonGia.Text + "," + txtGiamGia.Text + "," + txtThanhTien.Text + ")";
            Functions.RunSQL(sql);
            LoadDataGridView();
            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE tb_HangCa SET SoLuong =" + SLcon + " WHERE MaCa= N'" + txtMaCaCanh.Text + "'";
            Functions.RunSQL(sql);
            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'"));
            Tongmoi
                = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE tb_HDBan SET TongTien =" + Tongmoi + " WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
            Functions.RunSQL(sql);
            txtTongTien.Text = Tongmoi.ToString();
            //label_thongbao_ThanhChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(txtTongTien.Text);
            //ResetValuesHang();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnExcel.Enabled = true;
        }

        private void txtMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (txtMaNV.Text == "")
                txtMaNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "select TenNhanVien from tb_NhanVien where MaNhanVien = '" + txtMaNV.Text + "'";
            txtTenNV.Text = Functions.GetFieldValues(str);
        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (txtMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi_KH.Text = "";
                txtSDT_KH.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenKhachHang from tb_KhachHang where MaKhachHang = '" + txtMaKH.Text + "'";
            txtTenKH.Text = Functions.GetFieldValues(str);
            str = "Select DiaChi from tb_KhachHang where MaKhachHang = N'" + txtMaKH.Text + "'";
            txtDiaChi_KH.Text = Functions.GetFieldValues(str);
            str = "Select SoDienThoai from tb_KhachHang where MaKhachHang= N'" + txtMaKH.Text + "'";
            txtSDT_KH.Text = Functions.GetFieldValues(str);
        }

        private void txtMaCaCanh_TextChanged(object sender, EventArgs e)
        {
            /*string str;
            if (txtMaCaCanh.Text == "")
            {
                txtTenCaCanh.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TenCa FROM tb_HangCa WHERE MaCa ='" + txtMaCaCanh.Text + "'";
            txtTenCaCanh.Text = Functions.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM tb_HangCa WHERE MaCa ='" + txtMaCaCanh.Text + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);*/
        }

        private void txtSoLuong_ValueChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - ((sl * dg * gg) / 100);
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - ((sl * dg * gg) / 100);
            txtThanhTien.Text = tt.ToString();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void comboBox_MaHD_DropDown(object sender, EventArgs e)
        {
            /*Functions.FillCombo("SELECT MaHDBan FROM tb_HDBan", comboBox_MaHD, "MaHDBan", "MaHDBan");
            comboBox_MaHD.SelectedValue = -1;*/
        }

        private void btnTinhTongTien_Click(object sender, EventArgs e)
        {
            double tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tb_HDBan WHERE MaHDBan = '" + txtMaHD_hienthi.Text + "'"));
            double tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            string sql = "update tb_HDBan set TongTien = " + tongmoi + " where MaHDBan = '" + txtMaHD_hienthi.Text + "' ";
            Functions.RunSQL(sql);
            txtTongTien.Text = tongmoi.ToString();
            label_thongbao_ThanhChu.Text =  Functions.ChuyenSoSangChu(tongmoi.ToString());
        }

        private void dgviewHoaDonBanHang_DoubleClick(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (tblCTHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgviewHoaDonBanHang.CurrentRow.Cells["MaCa"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgviewHoaDonBanHang.CurrentRow.Cells["SoLuong"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgviewHoaDonBanHang.CurrentRow.Cells["ThanhTien"].Value.ToString());
                sql = "DELETE tb_ChiTietHDBan WHERE MaHDBan=N'" + txtMaHD_hienthi.Text + "' AND MaCa = N'" + MaHangxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại số lượng cho các mặt hàng
                sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tb_HangCa WHERE MaCa = N'" + MaHangxoa + "'"));
                slcon = sl + SoLuongxoa;
                sql = "UPDATE tb_HangCa SET SoLuong =" + slcon + " WHERE MaCa= N'" + MaHangxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tb_HDBan WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE tb_HDBan SET TongTien =" + tongmoi + " WHERE MaHDBan = N'" + txtMaHD_hienthi.Text + "'";
                Functions.RunSQL(sql);
                txtTongTien.Text = tongmoi.ToString();
                //label_thongbao_ThanhChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(tongmoi.ToString());
                LoadDataGridView();

                txtMaCaCanh.Text = "";
                //txtTenCaCanh.Text = "";
                txtDonGia.Text = "";
                txtSoLuong.Text = "";
                txtGiamGia.Text = "";
                txtThanhTien.Text = "";
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtMaCaCanh.Text = "";
            //txtTenCaCanh.Text = "";
            txtDonGia.Text = "0";
            txtSoLuong.Text = "0";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
            txtMaHD_hienthi.Text = "";
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi_KH.Text = "";
            txtSDT_KH.Text = "";
            txtTongTien.Text = "0";

            btnBoQua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            
           // btnSua.Enabled = true;
            //btnSave.Enabled = false;
           // txtMaKhachHang.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaHD_hienthi.Text;

            if (txtMaHD_hienthi.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dg = MessageBox.Show("Bạn có muốn xóa thông tin này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                // XÓA 
                string query = String.Format("delete from tb_ChiTietHDBan where MaHDBan = '{0}'", a1);
                bool kt = kn.HienThi(query);
                if (kt == true)
                {
                    // getData();
                    LoadDataGridView();
                    MessageBox.Show("Bạn Đã Xóa Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // ResetValues();
                    // txtMaKhachHang.Enabled = true;
                    txtMaCaCanh.Text = "";
                    //txtTenCaCanh.Text = "";
                    txtDonGia.Text = "0";
                    txtSoLuong.Text = "0";
                    txtGiamGia.Text = "0";
                    txtThanhTien.Text = "0";
                    txtMaHD_hienthi.Text = "";
                    txtMaCaCanh.Text = "";
                    txtDonGia.Text= "0";
                    txtMaNV.Text = "";
                    txtTenNV.Text = "";
                    txtSearch.Text = "";
                    txtMaKH.Text = "";
                    txtTenKH.Text = "";
                    txtDiaChi_KH.Text = "";
                    txtSDT_KH.Text = "";
                    txtTongTien.Text = "0";

                }
                else
                {
                    MessageBox.Show("Xóa Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Doãn Ánh Trang";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Thành Phố Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 0972.138.493";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHDBan, a.NgayBan, a.TongTien, b.TenKhachHang, b.DiaChi, b.SoDienThoai, c.TenNhanVien FROM tb_HDBan AS a, tb_KhachHang AS b, tb_NhanVien AS c WHERE a.MaHDBan = N'" + txtMaHD_hienthi.Text + "' AND a.MaKhachHang = b.MaKhachHang AND a.MaNhanVien = c.MaNhanVien";
            tblThongtinHD = Functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenCa, a.SoLuong, b.DonGiaBan, a.GiamGia, a.ThanhTien " +
                  "FROM tb_ChiTietHDBan AS a , tb_HangCa AS b WHERE a.MaHDBan = N'" +
                  txtMaHD_hienthi.Text + "' AND a.MaCa = b.MaCa";
            tblThongtinHang = Functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên Cá";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
           // exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChu(tblThongtinHD.Rows[0][2].ToString());
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn bán";
            exApp.Visible = true;
        }

        private void dgviewHoaDonBanHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.RowIndex >= 0)
            {
                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = this.dgviewHoaDonBanHang.Rows[e.RowIndex];
                //Đưa dữ liệu vào textbox
                txtMaHD_hienthi.Text = row.Cells[0].Value.ToString();
                txtMaCaCanh.Text = row.Cells[1].Value.ToString();
                txtTenCaCanh.Text = row.Cells[2].Value.ToString();
                txtDonGia.Text = row.Cells[3].Value.ToString();
                txtSoLuong.Text = row.Cells[4].Value.ToString();
                txtGiamGia.Text = row.Cells[5].Value.ToString();
                txtThanhTien.Text = row.Cells[6].Value.ToString();

            }
            txtMaHD_hienthi.Enabled = false;*/
        }

        private void txtTenNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string a1 = txtSearch.Text;
            string str;
            if (a1 == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để tìm kiếm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            *//*if (txtTenCaCanh.Text == "")
            {
                txtMaCaCanh.Text = "";
                txtDonGia.Text = "";
            }*//*
            str = String.Format("SELECT MaCa FROM tb_HangCa WHERE TenCa like '%{0}%'",a1);
            //str = "SELECT MaCa FROM tb_HangCa WHERE TenCa like '" + txtSearch.Text + "'";
            txtMaCaCanh.Text = Functions.GetFieldValues(str);
            str = String.Format("SELECT DonGiaBan FROM tb_HangCa WHERE TenCa like '%{0}%'", a1);
            //str = "SELECT DonGiaBan FROM tb_HangCa WHERE TenCa like '" + txtSearch.Text + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);*/
        }

        private void txtSearch_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("select TenCa from tb_HangCa", txtSearch, "TenCa", "TenCa");
            txtSearch.SelectedIndex = -1;
        }

        private void txtTenCaCanh_TextChanged(object sender, EventArgs e)
        {
            /*string str;
            *//*if (txtTenCaCanh.Text == "")
            {
                txtMaCaCanh.Text = "";
                txtDonGia.Text = "";
            }*//*
            // Khi chọn tên cá thì các thông tin về hàng hiện ra
            str = "SELECT MaCa FROM tb_HangCa WHERE TenCa = '" + txtTenCaCanh.Text + "'";
            txtMaCaCanh.Text = Functions.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM tb_HangCa WHERE TenCa = '" + txtTenCaCanh.Text + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);*/
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string str;
            string a1 = txtSearch.Text;
            if (txtSearch.Text == "")
            {
                txtMaCaCanh.Text = " ";
                txtDonGia.Text = "0";
            }
            str = String.Format("SELECT MaCa FROM tb_HangCa WHERE TenCa like N'%{0}%'", a1);
            //str = "SELECT MaCa FROM tb_HangCa WHERE TenCa like '" + txtSearch.Text + "'";
            txtMaCaCanh.Text = Functions.GetFieldValues(str);
            str = String.Format("SELECT DonGiaBan FROM tb_HangCa WHERE TenCa like N'%{0}%'", a1);
            //str = "SELECT DonGiaBan FROM tb_HangCa WHERE TenCa like '" + txtSearch.Text + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }
    }
}
