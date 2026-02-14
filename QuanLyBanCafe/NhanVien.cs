using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyBanCafe.XuLyDSNhanVien;

namespace QuanLyBanCafe
{
    public partial class NhanVien : Form
    {
        private XuLyDSNhanVien xuLy;
        private int vt = -1;
        public NhanVien()
        {
            InitializeComponent(); 
            xuLy = new XuLyDSNhanVien();
        }
        private void hienThiDanhSachQLNV(DataGridView dgv, List<NVien> ds)
        {
            dgv.DataSource = ds.ToList();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            NVien nv = new NVien(txtMSNV.Text, txtHT.Text, dtNS.Value.Date, cbGT.Text, txtSDT.Text, txtCCCD.Text, dtNVL.Value.Date, cbCV.Text);
            xuLy.them(nv);
            hienThiDanhSachQLNV(dgvQLNV, xuLy.getDanhSachQuanLyNV());
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void dgvQLNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                vt = e.RowIndex;
                NVien nv = new NVien();
                nv = xuLy.getDanhSachQuanLyNV()[vt];

                txtMSNV.Text = nv.maNV;
                txtHT.Text = nv.hoTen;
                dtNS.Value = nv.ngaySinh.Date;
                cbGT.Text = nv.gioiTinh;
                txtSDT.Text = nv.sdt;
                txtCCCD.Text = nv.cccd;
                dtNVL.Value = nv.ngayVaoLam.Date;
                cbCV.Text = nv.chucVu;
            }
            catch (Exception ex) { }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMSNV.Text;
            xuLy.xoa(maNV);
            hienThiDanhSachQLNV(dgvQLNV, xuLy.getDanhSachQuanLyNV());
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            NVien nv = new NVien(txtMSNV.Text, txtHT.Text, dtNS.Value.Date, cbGT.Text, txtSDT.Text, txtCCCD.Text, dtNVL.Value.Date, cbCV.Text);
            xuLy.sua(nv);
            hienThiDanhSachQLNV(dgvQLNV, xuLy.getDanhSachQuanLyNV());
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
           
           
            string path = Application.StartupPath + "\\NhanVien.dat";

            if (System.IO.File.Exists(path))
            {
                List<NVien> dsNhanVienTuFile = TruyCapDuLieu.docFile(xuLy.getDanhSachQuanLyNV(),path);

                // Cập nhật danh sách dsQLNV bằng dữ liệu từ file
                foreach (NVien nv in dsNhanVienTuFile)
                {
                    if (!xuLy.getDanhSachQuanLyNV().Contains(nv))
                    {
                        xuLy.getDanhSachQuanLyNV().Add(nv);
                    }
                }

                hienThiDanhSachQLNV(dgvQLNV, xuLy.getDanhSachQuanLyNV());
            }
            else
            {
                // Trường hợp file không tồn tại, xử lý như hiện tại
                MessageBox.Show("File không tồn tại.");
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\NhanVien.dat";
            bool kt = TruyCapDuLieu.ghiFile(xuLy.getDanhSachQuanLyNV(), path);
            if (kt == true)
            {
                MessageBox.Show("Đã lưu file");

            }
            else
            {
                MessageBox.Show("Lưu file không thành công");
            }
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            List<NVien> ketqua = new List<NVien>();
            if (rdMa.Checked == true)
            {
                ketqua = xuLy.timTheoMaNV(txtMa.Text);
                if (ketqua.Count > 0)
                {
                    hienThiDanhSachQLNV(dgvQLNV, ketqua);
                }
                else
                {
                    MessageBox.Show("Không thấy thông tin!!!", "Thông báo", MessageBoxButtons.OKCancel);
                }
            }
            else if (rdTen.Checked == true)
            {
                ketqua = xuLy.timTheoTen(txtTen.Text);
                if (ketqua.Count > 0)
                {
                    hienThiDanhSachQLNV(dgvQLNV, ketqua);
                }
                else
                {
                    MessageBox.Show("Không thấy thông tin!!!", "Thông báo", MessageBoxButtons.OKCancel);
                }
            }
            else
            {
                ketqua = xuLy.timTheoChucVu(txtCV.Text);
                if (ketqua.Count > 0)
                {
                    hienThiDanhSachQLNV(dgvQLNV, ketqua);
                }
                else
                {
                    MessageBox.Show("Không thấy thông tin!!!", "Thông báo", MessageBoxButtons.OKCancel);
                }
            }
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            hienThiDanhSachQLNV(dgvQLNV, xuLy.getDanhSachQuanLyNV());
        }
        private void cbCV_Click(object sender, EventArgs e)
        {
            LoadcbCV();
        }
        private void LoadcbCV()
        {
            string[] chucvu = { "Quản Lý", "Nhân Viên Thu Ngân", "Nhân Viên Pha Chế", "Nhân Viên Phục Vụ", "Bảo Vệ" };
            cbCV.Items.Clear();
            cbCV.Items.AddRange(chucvu);
        }
        private void cbGT_Click(object sender, EventArgs e)
        {
            LoadcbGT();
        }
        private void LoadcbGT()
        {
            string[] gioitinh = { "Nam", "Nữ" };
            cbGT.Items.Clear();
            cbGT.Items.AddRange(gioitinh);
        }
    }
    
}
