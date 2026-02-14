using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanCafe
{
    public partial class ThongKe : Form
    {
        private XuLyDSThongKe xuLyThongKe;
        private XuLyDSHoaDon xuLyHoaDon;
       
        public ThongKe()
        {
            xuLyThongKe = new XuLyDSThongKe();
            xuLyHoaDon = new XuLyDSHoaDon();
            InitializeComponent();
        }
        private void hienThiDanhSachHoaDon(DataGridView dgv, List<HDon> hd)
        {
            dgv.DataSource = hd.ToList();
        } 
        private void ThongKe_Load(object sender, EventArgs e)
        {
            dtTK.Value = System.DateTime.Now;
            dtNgayBatdau.Value = System.DateTime.Now;
            dtNgayKetThuc.Value = System.DateTime.Now;
        }   
        private void LoadCBMaNV()
        {
            string path = Application.StartupPath + "\\NhanVien.dat";
            List<NVien> dsNV = TruyCapDuLieu.docFile(xuLyHoaDon.getDanhSachQuanLyNV(), path);
            List<NVien> quanLy = new List<NVien>();
            foreach (NVien nv in dsNV)
            {
                if (nv.maNV.StartsWith("QL")) 
                {
                   quanLy.Add(nv);  
                }
            }
            cbMaNV.DataSource = quanLy;
            cbMaNV.DisplayMember = "MaNV";
            cbMaNV.ValueMember = "MaNV";
        }
        private void cbMaNV_Click(object sender, EventArgs e)
        {
            LoadCBMaNV();
        }       
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnLoc_Click_1(object sender, EventArgs e)
        {
            if (cbMaNV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DateTime ngayBatDau = dtNgayBatdau.Value.Date;
                DateTime ngayKetThuc = dtNgayKetThuc.Value.Date;
               List<HDon>ketqua=xuLyThongKe.locHoaDon(ngayBatDau, ngayKetThuc);
                if (ketqua.Count > 0)
                {
                    hienThiDanhSachHoaDon(dgvHD, ketqua);
                }
                else
                {
                    MessageBox.Show("Không có hóa đơn nào trong thời gian yêu cầu!!!", "Thông báo", MessageBoxButtons.OK);
                }
                int soLuongHD = xuLyThongKe.soLuongHDThongKe(ngayBatDau, ngayKetThuc);
                txtSLHD.Text= soLuongHD.ToString();
                double tongTienHD = xuLyThongKe.tongTienHDThongKe(ngayBatDau, ngayKetThuc);
                txtTTHD.Text= tongTienHD.ToString();
            }

          

        }
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "THỐNG KÊ";
            // Biểu diễn thông tin chung của hóa đơn bán
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Thời gian thống kê:";
            exRange.Range["C6:C6"].Value = dtTK.Value.ToString("dd/MM/yyyy");
            exRange.Range["B7:B7"].Value = "Thời gian bắt đầu:";
            exRange.Range["C7:C7"].Value = dtNgayBatdau.Value.ToString("dd/MM/yyyy");
            exRange.Range["B8:B8"].Value = "Thời gian kết thúc:";
            exRange.Range["C8:C8"].Value = dtNgayKetThuc.Value.ToString("dd/MM/yyyy");
            exRange.Range["B9:B9"].Value = "Mã nhân viên lập thống kê:";
            exRange.Range["C9:C9"].Value = cbMaNV.Text;
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Mã hóa đơn";
            exRange.Range["C11:C11"].Value = "Ngày lập hóa đơn";
            exRange.Range["D11:D11"].Value = "Tổng tiền hóa đơn";
            exRange.Range["E11:E11"].Value = "Mã nhân viên lập hóa đơn";
            int hang = 0;
            for (hang = 0; hang < dgvHD.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;

            }
            int hang1 = 0;
            foreach (DataGridViewRow dgvRow in dgvHD.Rows)
            {

                //Điền thông tin hàng từ cột thứ 2, dòng 12

                exSheet.Cells[2][hang1 + 12] = dgvRow.Cells["MaHD"].Value;
                exSheet.Cells[3][hang1 + 12] = dgvRow.Cells["NgayLapHD"].Value;
                exSheet.Cells[3][hang1 + 12].NumberFormat = "dd/mm/yyyy";
                exSheet.Cells[4][hang1 + 12] = dgvRow.Cells["TongTienHD"].Value;
                exSheet.Cells[5][hang1 + 12] = dgvRow.Cells["MaNV"].Value;
                hang1++;
            }
            exRange = exSheet.Cells[4][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Số lượng hóa đơn:";
            exRange = exSheet.Cells[4 + 1][hang + 14];
            exRange.Font.Bold = true;
            foreach (DataGridViewRow dgvRow in dgvHD.Rows)
            {
               
                exRange.Value2 = txtSLHD.Text;

            }
            exRange = exSheet.Cells[4][hang + 15];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền thống kê:";
            exRange = exSheet.Cells[4 + 1][hang + 15];
            exRange.Font.Bold = true;
            foreach (DataGridViewRow dgvRow in dgvHD.Rows)
            {

                exRange.Value2 = txtTTHD.Text;

            }
            exSheet.Name = "Thống kê";
            exApp.Visible = true;
        }
    }
}
