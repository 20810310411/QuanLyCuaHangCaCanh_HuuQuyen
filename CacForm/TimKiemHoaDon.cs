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
    public partial class TimKiemHoaDon : Form
    {
        public TimKiemHoaDon()
        {
            InitializeComponent();
        }
        KetNoi.KetNoi kn = new KetNoi.KetNoi();
        KetNoi.Function Functions = new KetNoi.Function();
        DataTable tblCTHDB; //Bảng chi tiết hoá đơn bán
        private void getData()
        {
            string query = "select * from tb_HDBan";
            DataSet ds = kn.LayDL(query, "tb_HDBan");
            dgviewHoaDon.DataSource = ds.Tables["tb_HDBan"];
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void TimKiemHoaDon_Load(object sender, EventArgs e)
        {
            getData();
            dgviewHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
            dgviewHoaDon.Columns[1].HeaderText = "Mã Nhân Viên";
            dgviewHoaDon.Columns[2].HeaderText = "Ngày Bán";
            dgviewHoaDon.Columns[3].HeaderText = "Mã Khách Hàng";
            dgviewHoaDon.Columns[4].HeaderText = "Tổng Tiền";
            dgviewHoaDon.Columns[0].Width = 150;
            dgviewHoaDon.Columns[1].Width = 120;
            dgviewHoaDon.Columns[2].Width = 100;
            dgviewHoaDon.Columns[3].Width = 150;
            dgviewHoaDon.Columns[4].Width = 150;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = "";
            txtMaNV.Text = "";
            txtMaKH.Text = "";
            txtTongTien.Text = "";           
            getData();
        }

        private void dgviewHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = this.dgviewHoaDon.Rows[e.RowIndex];
                //Đưa dữ liệu vào textbox
                txtMaHD.Text = row.Cells[0].Value.ToString();
                txtMaNV.Text = row.Cells[1].Value.ToString();
                txtNgayBan.Text = row.Cells[2].Value.ToString();
                txtMaKH.Text = row.Cells[3].Value.ToString();
                txtTongTien.Text = row.Cells[4].Value.ToString();
            }           
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaHD.Text;


            //
            // kiểm tra
            if (a1 == "")
            {
                MessageBox.Show("Bạn hãy nhập hóa đơn cần tìm kiếm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // search
            string query = String.Format("select * from tb_HDBan where MaHDBan like '%{0}%'",a1);
            DataSet ds = kn.LayDL(query, "tb_HDBan");
            dgviewHoaDon.DataSource = ds.Tables["tb_HDBan"];
        }

        private void dgviewHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgviewHoaDon_DoubleClick(object sender, EventArgs e)
        {
        }

        private void dgviewHoaDon_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dgviewHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                foreach(DataGridViewRow row in dgviewHoaDon.SelectedRows)
                {
                    dgviewHoaDon.Rows.Remove(row);
                }
            }
        }
    }
}
