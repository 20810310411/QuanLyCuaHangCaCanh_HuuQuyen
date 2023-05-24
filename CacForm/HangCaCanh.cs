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

namespace QuanLyCuaHangCaCanh_HuuQuyen.CacForm
{
    public partial class HangCaCanh : Form
    {
        public HangCaCanh() => InitializeComponent();
        KetNoi.KetNoi kn = new KetNoi.KetNoi();
        private void getData()
        {
            string query = "select MaCa,TenCa,SoLuong,DonGiaNhap,DonGiaBan,Img,GhiChu from tb_HangCa";
            DataSet ds = kn.LayDL(query, "tb_HangCa");
            dgviewHangCaCanh.DataSource = ds.Tables["tb_HangCa"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
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
            txtMaCa.Text = "";
            txtTenCa.Text = "";
            txtSoLuong.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtAnh.Text = "";
            txtGhiChu.Text = "";
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            //btnSave.Enabled = false;
            txtMaCa.Enabled = true;
            getData();
        }

        private void HangCaCanh_Load(object sender, EventArgs e)
        {
            getData();
            txtSoLuong.Text = "";
            dgviewHangCaCanh.Columns[0].HeaderText = "Mã Cá";
            dgviewHangCaCanh.Columns[1].HeaderText = "Tên Cá";
            dgviewHangCaCanh.Columns[2].HeaderText = "Số Lượng";
            dgviewHangCaCanh.Columns[3].HeaderText = "Đơn Giá Nhập";
            dgviewHangCaCanh.Columns[4].HeaderText = "Đơn Giá Bán";
            dgviewHangCaCanh.Columns[5].HeaderText = "Ảnh Cá";
            dgviewHangCaCanh.Columns[6].HeaderText = "Ghi Chú";
            dgviewHangCaCanh.Columns[0].Width = 80;
            dgviewHangCaCanh.Columns[1].Width = 120;
            dgviewHangCaCanh.Columns[2].Width = 100;
            dgviewHangCaCanh.Columns[3].Width = 130;
            dgviewHangCaCanh.Columns[4].Width = 130;
            dgviewHangCaCanh.Columns[5].Width = 110;
            dgviewHangCaCanh.Columns[6].Width = 170;
        }

        // EXPORT PDF
        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (dgviewHangCaCanh.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể ghi dữ liệu tới ổ đĩa. Mô tả lỗi:" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dgviewHangCaCanh.Columns.Count);
                            pdfTable.DefaultCell.Padding = 4;
                            pdfTable.WidthPercentage = 95;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dgviewHangCaCanh.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgviewHangCaCanh.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 20f, 8f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Dữ liệu Export thành công!!!", "Chúc Mừng!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Mô tả lỗi :" + ex.Message);
                        }
                    }
                }
            }
        }

        private void dgviewHangCaCanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = this.dgviewHangCaCanh.Rows[e.RowIndex];
                //Đưa dữ liệu vào textbox
                txtMaCa.Text = row.Cells[0].Value.ToString();
                txtTenCa.Text = row.Cells[1].Value.ToString();
                txtSoLuong.Text = row.Cells[2].Value.ToString();
                txtGiaNhap.Text = row.Cells[3].Value.ToString();
                txtGiaBan.Text = row.Cells[4].Value.ToString();
                txtAnh.Text = row.Cells[5].Value.ToString();
                txtGhiChu.Text = row.Cells[6].Value.ToString();

            }
            txtMaCa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaCa.Text;
            string a2 = txtTenCa.Text;
            string a3 = txtSoLuong.Text;
            string a4 = txtGiaNhap.Text;
            string a5 = txtGiaBan.Text;
            string a6 = txtAnh.Text;
            string a7 = txtGhiChu.Text;
            
            //  KIỂM TRA 
            if (txtMaCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã của cá cảnh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaCa.Focus();
                return;
            }
            if (txtTenCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên cá cảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCa.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            if (txtGiaNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá nhập của cá cảnh này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return;
            }
            if (txtGiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá bán của cá cảnh này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }
            if (txtAnh.Text == "")
            {
                MessageBox.Show("Bạn phải OPEN ảnh của cá cảnh này lên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnh.Focus();
                return;
            }
            if (txtGhiChu.Text == "")
            {
                MessageBox.Show("Bạn phải điền ghi chú cá cảnh này nha!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGhiChu.Focus();
                return;
            }

            // Thêm thông tin
            string query = String.Format("insert into tb_HangCa (MaCa, TenCa, SoLuong, DonGiaNhap, DonGiaBan, Img, GhiChu)" +
                " values('{0}', N'{1}', '{2}', '{3}', '{4}', '{5}', N'{6}')", a1,a2,a3,a4,a5,a6, a7);
            bool kt = kn.HienThi(query);
            if (kt == true)
            {
                getData();
                
                txtMaCa.Enabled = true;
                MessageBox.Show("Bạn Đã Thêm Mới Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetValues();

            }
            else
            {
                MessageBox.Show("Thêm Mới Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaCa.Text;
            string a2 = txtTenCa.Text;
            string a3 = txtSoLuong.Text;
            string a4 = txtGiaNhap.Text;
            string a5 = txtGiaBan.Text;
            string a6 = txtAnh.Text;
            string a7 = txtGhiChu.Text;

            //  KIỂM TRA 
            if (txtMaCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaCa.Focus();
                return;
            }
            if (txtTenCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên cá cảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCa.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            if (txtGiaNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá nhập của cá cảnh này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return;
            }
            if (txtGiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá bán của cá cảnh này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }
            if (txtAnh.Text == "")
            {
                MessageBox.Show("Bạn phải OPEN ảnh của cá cảnh này lên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnh.Focus();
                return;
            }
            if (txtGhiChu.Text == "")
            {
                MessageBox.Show("Bạn phải điền ghi chú cá cảnh này nha!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGhiChu.Focus();
                return;
            }

            // Thêm thông tin
            string query = String.Format("update tb_HangCa set TenCa = N'{0}', SoLuong = '{1}', DonGiaNhap = '{2}', " +
                " DonGiaBan = '{3}', Img = '{4}', GhiChu = N'{5}' where MaCa = '{6}'",a2,a3,a4,a5,a6,a7, a1 );
            bool kt = kn.HienThi(query);
            if (kt == true)
            {
                getData();
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnBoQua.Enabled = true;
                //btnSave.Enabled = false;
                txtMaCa.Enabled = false;
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
            string a1 = txtMaCa.Text;
            

            //  KIỂM TRA 
            if (txtMaCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào để xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaCa.Focus();
                return;
            }
            DialogResult dg = MessageBox.Show("Bạn có muốn xóa thông tin này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                // xóa thông tin
                string query = String.Format("delete tb_HangCa where MaCa = '{0}'", a1);
                bool kt = kn.HienThi(query);
                if (kt == true)
                {
                    getData();
                    MessageBox.Show("Bạn Đã Xóa Thông Tin Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    txtMaCa.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Bạn Đã Xóa Thông Tin Thất Bại. Vui Lòng Thử Lại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //
            string a1 = txtMaCa.Text;
            string a2 = txtTenCa.Text;
            string a3 = txtSoLuong.Text;
            string a4 = txtGiaNhap.Text;
            string a5 = txtGiaBan.Text;
            string a6 = txtAnh.Text;
            string a7 = txtGhiChu.Text;

            //
            // kiểm tra
            if (a1 == "" && a2 == "" && a3 == "")
            {
                MessageBox.Show("Bạn hãy nhập thông tin cần tìm kiếm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // search
            string query = String.Format("select MaCa,TenCa,SoLuong,DonGiaNhap,DonGiaBan,Img,GhiChu from tb_HangCa " +
                " where MaCa like '%{0}%' and TenCa like N'%{1}%' and SoLuong like '%{2}%'", a1,a2,a3);
            DataSet ds = kn.LayDL(query, "tb_HangCa");
            dgviewHangCaCanh.DataSource = ds.Tables["tb_HangCa"];
        }

        private void btnOpenAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho cá cảnh";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = System.Drawing.Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }
    }
}
