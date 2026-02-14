using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanCafe
{

    public partial class HoaDon : Form
    {
        private XuLyDSHoaDon xuLyHoaDon;
        private XuLyDSSanPham xuLySanPham;
        public HoaDon()
        {
            xuLyHoaDon = new XuLyDSHoaDon();
            xuLySanPham = new XuLyDSSanPham();
            InitializeComponent();
        }
        private void HienThiDanhSachHoaDon(DataGridView dgv, List<HDon> ds)
        {
            dgv.DataSource = ds.ToList();
        }
        private void HienThiDanhSachCTHD(DataGridView dgv, List<CTietHDon> ds)
        {
            dgv.DataSource = ds.ToList();
        }
        private void khoiTaoDanhSachMaLoaiVaoComBoBoxML()
        {
            string path = Application.StartupPath + "\\LSP.dat";
            List<LSanPham> dsLSPTuFile = TruyCapDuLieu.docFile(xuLySanPham.getDanhSachQuanLyLSP(), path);
            cbML.DataSource = dsLSPTuFile;
            cbML.DisplayMember = "MaLoai";
            cbML.ValueMember = "MaLoai";
        }
        private void cbML_Click(object sender, EventArgs e)
        {
            khoiTaoDanhSachMaLoaiVaoComBoBoxML();
        }
        private void cbML_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\SPham.dat";
            List<SPham> dsSPTuFile = TruyCapDuLieu.docFile(xuLySanPham.getDanhSachQuanLySP(), path);
            if (cbML.SelectedIndex == -1)
            {
                MessageBox.Show("Chọn mã loại sản phẩm!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string maLoaiDaChon = cbML.SelectedValue.ToString();
                List<SPham> danhSachSanPhamTheoLoai = dsSPTuFile.Where(sp => sp.MaLoai == maLoaiDaChon).ToList();
                cbMSP.DataSource = danhSachSanPhamTheoLoai.Select(sp => sp.masp).ToList();
            }
        }
        private void cbMSP_Click(object sender, EventArgs e)
        {
            cbML_SelectedIndexChanged(sender, e);
        }
        private void cbMSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\SPham.dat";
            List<SPham> dsSanPham = TruyCapDuLieu.docFile(xuLySanPham.getDanhSachQuanLySP(), path);
            string maSPDaChon = (string)cbMSP.SelectedValue;
            var sp = dsSanPham.FirstOrDefault(m => m.masp == maSPDaChon);
            if (sp != null)
            {
                txtSP.Text = sp.tensp;
                txtDGia.Text = sp.dongia.ToString();
            }
            else
            {
                txtSP.Text = "Không tìm thấy sản phẩm";
                txtDGia.Text = "Không tìm thấy sản phẩm";

            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMHD.Text))
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để thêm chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                
                    int dem = 0;
                    foreach (DataGridViewRow row in dgvHD.Rows)
                    {
                        if (row.Cells["MaHD"].Value == null)
                        {
                            MessageBox.Show("Vui lòng lập hóa đơn!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (row.Cells["MaHD"].Value.ToString() == txtMHD.Text)
                        {
                            dem++;

                        }
                    }
                    if (dem == 0)
                    {
                        MessageBox.Show("Vui lòng lập hóa đơn!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {

                        object tmp = cbMSP.SelectedItem;
                        //SPham sPham = new SPham(tmp.ToString(), txtSP.Text, double.Parse(txtDGia.Text),null);
                        SPham sPham = new SPham();
                        sPham.masp = tmp.ToString();
                        sPham.tensp = txtSP.Text;
                        sPham.dongia = double.Parse(txtDGia.Text);
                        double thanhTien = double.Parse(txtDGia.Text) * (int)NSLuong.Value;
                        CTietHDon sp = new CTietHDon(txtMHD.Text, sPham, (int)NSLuong.Value, thanhTien);
                        xuLyHoaDon.themCTHD(sp);
                        HienThiDanhSachCTHD(dgvCTHD, xuLyHoaDon.getDanhSachQuanLyCTHD());
                        tinhTongTien();
                    }
                
            }
        }
        private void tinhTongTien()
        {
            double tongTien = 0;
            foreach (DataGridViewRow row in dgvCTHD.Rows)
            {
                if (row.Cells["MaHoaDon"].Value.ToString() == txtMHD.Text)
                {
                    if (row.Cells["ThanhTien"].Value != null)
                    {
                        tongTien += Convert.ToDouble(row.Cells["ThanhTien"].Value);
                    }
                }
            }
            foreach (DataGridViewRow row in dgvHD.Rows)
            {

                if (row.Cells["MaHD"].Value.ToString() == txtMHD.Text)
                {
                    row.Cells["TongTienHD"].Value = tongTien;
                    break;
                }

            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {

            string ma = txtSP.Text;
            xuLyHoaDon.xoaCTHD(ma);
            HienThiDanhSachCTHD(dgvCTHD, xuLyHoaDon.getDanhSachQuanLyCTHD());
            tinhTongTien();
        }
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            object tmp = cbMSP.Text;
            SPham sPham = new SPham();
            sPham.masp = tmp.ToString();
            sPham.tensp = txtSP.Text;
            sPham.dongia = double.Parse(txtDGia.Text);
            double thanhTien = double.Parse(txtDGia.Text) * (int)NSLuong.Value;
            CTietHDon sp = new CTietHDon(txtMHD.Text, sPham, (int)NSLuong.Value, thanhTien);
            xuLyHoaDon.suaCTHD(sp);
            HienThiDanhSachCTHD(dgvCTHD, xuLyHoaDon.getDanhSachQuanLyCTHD());
            tinhTongTien();
        }
        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMHD.Text = dgvCTHD.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbMSP.Text = dgvCTHD.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSP.Text = dgvCTHD.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDGia.Text = dgvCTHD.Rows[e.RowIndex].Cells[3].Value.ToString();
            NSLuong.Value = int.Parse(dgvCTHD.Rows[e.RowIndex].Cells[4].Value.ToString());
        }
        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (cbMaNV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (dtNLHD.Value.Date < DateTime.Today)
                {
                    MessageBox.Show("Ngày lập hóa đơn không được nhỏ hơn ngày hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng thực hiện nếu không hợp lệ
                }
                
                NVien nv = new NVien();
                nv.maNV = cbMaNV.SelectedValue.ToString();
                double tongTien = 0;
                string maHD = txtMHD.Text;
                HDon hd = new HDon(txtMHD.Text, dtNLHD.Value, nv, tongTien);
                xuLyHoaDon.themHD(hd);
                HienThiDanhSachHoaDon(dgvHD, xuLyHoaDon.getDanhSachQuanLyHoaDon());// Lấy thông tin từ giao diện

            }

        }
        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMHD.Text = dgvHD.Rows[e.RowIndex].Cells[0].Value.ToString();
            dtNLHD.Text = dgvHD.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbMaNV.Text = dgvHD.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void LoadCBMaNV()
        {
            string path = Application.StartupPath + "\\NhanVien.dat";
            List<NVien> dsNhanVien = TruyCapDuLieu.docFile(xuLyHoaDon.getDanhSachQuanLyNV(), path);
            cbMaNV.DataSource = dsNhanVien;
            List<NVien> quanLy = new List<NVien>();
            foreach (NVien nv in dsNhanVien)
            {
                if (nv.maNV.StartsWith("NV"))
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
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string pathHD = Application.StartupPath + "\\HD.dat";
            string pathCTHD = Application.StartupPath + "\\CTHD.dat";
            List<HDon> dsHoaDon = TruyCapDuLieu.docFile(xuLyHoaDon.getDanhSachQuanLyHoaDon(), pathHD);
            List<CTietHDon> dsCTHDTuFile = TruyCapDuLieu.docFile(xuLyHoaDon.getDanhSachQuanLyCTHD(), pathCTHD);
            foreach (DataGridViewRow row in dgvHD.Rows)
            {
                NVien nv = new NVien();
                string MaHD = row.Cells["MaHD"].Value.ToString();
                DateTime NgayLapHoaDon = DateTime.Parse(row.Cells["NgayLapHD"].Value.ToString());
                nv.maNV = row.Cells["MaNV"].Value.ToString();
                double TongTienHD = double.Parse(row.Cells["TongTienHD"].Value.ToString());

                HDon hoaDonMoi = new HDon(MaHD, NgayLapHoaDon, nv, TongTienHD);


                // Kiểm tra xem hóa đơn có tồn tại trong danh sách không
                bool hoaDonTonTai = dsHoaDon.Any(hd => hd.maHD == hoaDonMoi.maHD);
                if (!hoaDonTonTai)
                {
                    dsHoaDon.Add(hoaDonMoi);
                }


            }
            bool ktHD = TruyCapDuLieu.ghiFile(dsHoaDon, pathHD);
            if (ktHD)
            {
                MessageBox.Show("Đã lưu hóa đơn !!!", "Thông báo", MessageBoxButtons.OK);

            }
            else
            {
                MessageBox.Show("Lưu hóa đơn không thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }            // Kiểm tra và thêm chi tiết hóa đơn mới nếu không tồn tại trong danh sách
            foreach (CTietHDon ct in xuLyHoaDon.getDanhSachQuanLyCTHD())
            {
                if (!dsCTHDTuFile.Contains(ct))
                {
                    dsCTHDTuFile.Add(ct);
                }
            }
            bool ktCTHD = TruyCapDuLieu.ghiFile(dsCTHDTuFile, pathCTHD);
            if (ktCTHD)
            {
                MessageBox.Show("Đã lưu chi tiết hóa đơn !!!", "Thông báo", MessageBoxButtons.OK);

            } else
            {
                MessageBox.Show("Lưu chi tiết hóa đơn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }


        }
        private void HoaDon_Load_1(object sender, EventArgs e)
        {
            dtNLHD.Value=System.DateTime.Now;
            txtMHD.Text = SinhMaHoaDonTuDong(); // Gán mã mới vào TextBox
        }
        private string SinhMaHoaDonTuDong()
        {
            List<string> maHoaDonList = new List<string>();
            string pathHD = Application.StartupPath + "\\HD.dat";
            if (System.IO.File.Exists(pathHD))
            {
                List<HDon> dsHDTuFile = TruyCapDuLieu.docFile(xuLyHoaDon.getDanhSachQuanLyHoaDon(), pathHD);
                foreach (HDon nv in dsHDTuFile)
                {
                    maHoaDonList.Add(nv.maHD);

                }
            }
            // Tìm mã hóa đơn lớn nhất
            int maxMaHD = 0;
            foreach (string maHD in maHoaDonList)
            {
                if (maHD.StartsWith("HD")) // Kiểm tra định dạng mã hóa đơn
                {
                    string numberPart = maHD.Substring(2); // Lấy phần số sau "HD"
                    if (int.TryParse(numberPart, out int maHDNumber)) // Chuyển đổi phần số thành int
                    {
                        if (maHDNumber > maxMaHD)
                        {
                            maxMaHD = maHDNumber; // Cập nhật mã lớn nhất
                        }
                    }
                }
            }

            // Sinh mã hóa đơn mới
            int newMaHD = maxMaHD + 1;
            return "HD" + newMaHD.ToString("D3"); // Định dạng mã, ví dụ: HD001, HD002,...

        }
        private void btnXemFile_Click(object sender, EventArgs e)
        {

            string path2 = Application.StartupPath + "\\HD.dat";
            if (System.IO.File.Exists(path2))
            {
                List<HDon> dsHDTuFile = TruyCapDuLieu.docFile(xuLyHoaDon.getDanhSachQuanLyHoaDon(), path2);
                foreach (HDon nv in dsHDTuFile)
                {
                    if (!xuLyHoaDon.getDanhSachQuanLyHoaDon().Contains(nv))
                    {
                        xuLyHoaDon.getDanhSachQuanLyHoaDon().Add(nv);
                    }

                }

                HienThiDanhSachHoaDon(dgvHD, xuLyHoaDon.getDanhSachQuanLyHoaDon());

            }
            else
            {
                // Trường hợp file không tồn tại, xử lý như hiện tại
                MessageBox.Show("File không tồn tại.");
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            txtMHD.Text = string.Empty;
            txtDGia.Text = string.Empty;
            txtSP.Text = string.Empty;
            cbMaNV.Text = string.Empty;
            cbML.Text = string.Empty;
            cbMSP.Text = string.Empty;
            NSLuong.Value = 1;
            xuLyHoaDon.xoaALLHD();
            HienThiDanhSachHoaDon(dgvHD, xuLyHoaDon.getDanhSachQuanLyHoaDon());
            xuLyHoaDon.xoaALLCTHD();
            HienThiDanhSachCTHD(dgvCTHD, xuLyHoaDon.getDanhSachQuanLyCTHD());
            txtMHD.Text = SinhMaHoaDonTuDong();
        }
        private void dgvHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string path = Application.StartupPath + "\\CTHD.dat";
            List<CTietHDon> dsCTHDTuFile = TruyCapDuLieu.docFile(xuLyHoaDon.getDanhSachQuanLyCTHD(), path);

            // Kiểm tra chỉ thực hiện khi nhấp đúp vào dòng hợp lệ
            if (e.RowIndex >= 0 && dgvHD.Rows[e.RowIndex].Cells["MaHD"].Value != null)
            {
                // Lấy mã hóa đơn từ dòng được chọn
                string maHoaDon = dgvHD.Rows[e.RowIndex].Cells["MaHD"].Value.ToString();



            } // Hiển thị chi tiết hóa đơn liên quan
            if (System.IO.File.Exists(path))
            {
                List<CTietHDon> ketqua = new List<CTietHDon>();
                foreach (CTietHDon nv in dsCTHDTuFile)
                {

                    string maHD = txtMHD.Text;
                    if (nv.maHD.Equals(maHD))
                    {
                        ketqua.Add(nv);
                    }

                }
                if (ketqua.Count > 0)
                {
                    HienThiDanhSachCTHD(dgvCTHD, ketqua);
                }
                else
                {
                    MessageBox.Show("Mã hóa đơn chưa được thêm sản phẩm", "Thông báo", MessageBoxButtons.OKCancel);
                }

            }
            else
            {
                MessageBox.Show("File không tồn tại!!!", "Thông báo", MessageBoxButtons.OKCancel);
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
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN THANH TOÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = txtMHD.Text;
            exRange.Range["B7:B7"].Value = "Ngày lập hóa đơn:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = dtNLHD.Value.ToString("dd/MM/yyyy");
            exRange.Range["B8:B8"].Value = "Mã nhân viên:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = cbMaNV.Text;
            //Lấy thông tin các mặt hàng
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên sản phẩm";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Thành Tiền";

            int hang = 0;
            for (hang = 0; hang < dgvCTHD.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
               
            }
            int hang1 = 0;
            foreach (DataGridViewRow dgvRow in dgvCTHD.Rows)
            {
                
                //Điền thông tin hàng từ cột thứ 2, dòng 12

                exSheet.Cells[2][hang1 + 12] = dgvRow.Cells["TenSP"].Value;
                exSheet.Cells[3][hang1 + 12] = dgvRow.Cells["SoLuong"].Value;
                exSheet.Cells[4][hang1 + 12] = dgvRow.Cells["DonGia"].Value;
                exSheet.Cells[5][hang1 + 12] = dgvRow.Cells["ThanhTien"].Value;
                hang1++;
            }
            exRange = exSheet.Cells[4][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[4 + 1][hang + 14];
            exRange.Font.Bold = true;
            foreach (DataGridViewRow dgvRow in dgvHD.Rows)
            {
               
                exRange.Value2 = dgvRow.Cells["TongTienHD"].Value;
               
            }
            exSheet.Name = "Hóa đơn thanh toán";
            exApp.Visible = true;
        }   
    }
}
    
    
    

