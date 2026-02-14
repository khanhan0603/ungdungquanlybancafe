using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCafe
{
    public partial class LoaiSanPham : Form
    {
        private XuLyDSLoaiSanPham xuLy;
        private int vt = -1;
        public LoaiSanPham()
        {
            InitializeComponent();
            xuLy=new XuLyDSLoaiSanPham();
        }
        private void hienThiDanhSachLSP(DataGridView dgv, List<LSanPham> lsp)
        {
            dgv.DataSource = lsp.ToList();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            LSanPham nv = new LSanPham(txtML.Text, txtTL.Text);
            xuLy.them(nv);
            hienThiDanhSachLSP(dgvLSP, xuLy.getDanhSachQuanLyLSP());
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maLoai = txtML.Text;
            xuLy.xoa(maLoai);
            hienThiDanhSachLSP(dgvLSP, xuLy.getDanhSachQuanLyLSP());
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            LSanPham nv = new LSanPham(txtML.Text, txtTL.Text);
            xuLy.sua(nv);
            hienThiDanhSachLSP(dgvLSP, xuLy.getDanhSachQuanLyLSP());
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void dgvLSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                vt = e.RowIndex;
                LSanPham sp = new LSanPham();
                sp = xuLy.getDanhSachQuanLyLSP()[vt];

                txtML.Text = sp.MaLoai;
                txtTL.Text = sp.TenLoai;
            }
            catch (Exception ex) { }
        }
        private void LoaiSanPham_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\LSP.dat";

            if (System.IO.File.Exists(path))
            {
                List<LSanPham> dsLSPTuFile = TruyCapDuLieu.docFile(xuLy.getDanhSachQuanLyLSP(),path);

                // Cập nhật danh sách dsQLNV bằng dữ liệu từ file
                foreach (LSanPham nv in dsLSPTuFile)
                {
                    if (!xuLy.getDanhSachQuanLyLSP().Contains(nv))
                    {
                        xuLy.getDanhSachQuanLyLSP().Add(nv);
                    }
                }
                hienThiDanhSachLSP(dgvLSP, xuLy.getDanhSachQuanLyLSP());
            }
            else
            {
                // Trường hợp file không tồn tại, xử lý như hiện tại
                MessageBox.Show("File không tồn tại.");
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\LSP.dat";
            bool kt = TruyCapDuLieu.ghiFile(xuLy.getDanhSachQuanLyLSP(), path);
            if (kt == true)
            {
                MessageBox.Show("Đã lưu file");

            }
            else
            {
                MessageBox.Show("Lưu file không thành công");
            }
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            hienThiDanhSachLSP(dgvLSP, xuLy.getDanhSachQuanLyLSP());
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            List<LSanPham> ketqua = new List<LSanPham>();
            if (rdMa.Checked == true)
            {
                ketqua = xuLy.timTheoMaLoai(txtMa.Text);
                if (ketqua.Count > 0)
                {
                    hienThiDanhSachLSP(dgvLSP, ketqua);
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
                    hienThiDanhSachLSP(dgvLSP, ketqua);
                }
                else
                {
                    MessageBox.Show("Không thấy thông tin!!!", "Thông báo", MessageBoxButtons.OKCancel);
                }
            }
            else
            {
               
                    MessageBox.Show("Chọn loại tìm kiếm", "Thông báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                
            }
        }
    }
    
}
