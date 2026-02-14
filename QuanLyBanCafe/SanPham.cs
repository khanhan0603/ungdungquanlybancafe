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

    public partial class SanPham : Form
    {
        private XuLyDSSanPham xuLy;
        private int vt = -1;
        public SanPham()
        {
            InitializeComponent();
            xuLy = new XuLyDSSanPham();
        }
        private void hienThiDanhSachSanPham(DataGridView dgv, List<SPham> sp)
        {
            dgv.DataSource = sp.ToList();
        }      
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CBTL.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tên loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string path = Application.StartupPath + "\\LSP.dat";
                var loai = TruyCapDuLieu.docFile(xuLy.getDanhSachQuanLyLSP(), path).FirstOrDefault(l => l.MaLoai == CBTL.Text);
                object tmp = CBTL.SelectedValue;
                LSanPham lsp=new LSanPham(tmp.ToString(),loai.TenLoai);
                SPham sp = new SPham(txtMSP.Text, txtTSP.Text, double.Parse(txtDG.Text),lsp);
                if (loai != null)
                {
                    xuLy.them(sp);
                    hienThiDanhSachSanPham(dgvSP, xuLy.getDanhSachQuanLySP());
                }
                else
                {
                    MessageBox.Show("Tên loại không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
                }
            }
          
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSanPham = txtMSP.Text;
            xuLy.xoa(maSanPham);
            hienThiDanhSachSanPham(dgvSP, xuLy.getDanhSachQuanLySP());
        }
        private void SanPham_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\SPham.dat";

            if (System.IO.File.Exists(path))
            {
                List<SPham> dsSPTuFile = TruyCapDuLieu.docFile(xuLy.getDanhSachQuanLySP(),path);

                // Cập nhật danh sách dsQLNV bằng dữ liệu từ file
                foreach (SPham nv in dsSPTuFile)
                {
                    if (!xuLy.getDanhSachQuanLySP().Contains(nv))
                    {
                        xuLy.getDanhSachQuanLySP().Add(nv);
                    }
                }

                hienThiDanhSachSanPham(dgvSP, xuLy.getDanhSachQuanLySP());
            }
            else
            {
                // Trường hợp file không tồn tại, xử lý như hiện tại
                MessageBox.Show("File không tồn tại.");
            }
        }
        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
                txtMSP.Text = dgvSP.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTSP.Text = dgvSP.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDG.Text = dgvSP.Rows[e.RowIndex].Cells[2].Value.ToString();
                CBTL.Text = dgvSP.Rows[e.RowIndex].Cells[3].Value.ToString();
            
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            LSanPham loaisp = new LSanPham();
            loaisp.TenLoai = CBTL.SelectedValue.ToString();
            string path = Application.StartupPath + "\\LSP.dat";
            var loai = TruyCapDuLieu.docFile(xuLy.getDanhSachQuanLyLSP(), path).FirstOrDefault(l => l.MaLoai == CBTL.Text);
            object tmp = CBTL.SelectedValue;
            LSanPham lsp = new LSanPham(tmp.ToString(),loai.TenLoai);
            if (loai != null)
            {

                SPham sp = new SPham(txtMSP.Text, txtTSP.Text, double.Parse(txtDG.Text),lsp);

                xuLy.sua(sp);
                hienThiDanhSachSanPham(dgvSP, xuLy.getDanhSachQuanLySP());
            }

            else
            {
                MessageBox.Show("Tên loại không tồn tại.","Thông báo",MessageBoxButtons.OK);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\SPham.dat";
            bool kt = TruyCapDuLieu.ghiFile(xuLy.getDanhSachQuanLySP(), path);
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
            List<SPham> ketqua = new List<SPham>();
            if (rdMa.Checked == true)
            {
                ketqua = xuLy.timTheoMaLoai(txtMa.Text);
                if (ketqua.Count > 0)
                {
                    hienThiDanhSachSanPham(dgvSP, ketqua);
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
                    hienThiDanhSachSanPham(dgvSP, ketqua);
                }
                else
                {
                    MessageBox.Show("Không thấy thông tin!!!", "Thông báo", MessageBoxButtons.OKCancel);
                }
            }
            else if(rdDGia.Checked==true)
            {
                ketqua = xuLy.timTheoDonGia(float.Parse(txtDGia.Text));
                if (ketqua.Count > 0)
                {
                    hienThiDanhSachSanPham(dgvSP, ketqua);
                }
                else
                {
                    MessageBox.Show("Không thấy thông tin!!!", "Thông báo", MessageBoxButtons.OKCancel);
                }
            }
            else
            {
                MessageBox.Show("Chọn loại tìm kiếm!!!", "Thông báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            }
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            hienThiDanhSachSanPham(dgvSP, xuLy.getDanhSachQuanLySP());
        }
        private void CBTL_Click(object sender, EventArgs e)
        {
            LoadCBTL();
        }
        private void LoadCBTL()
        {
            string path = Application.StartupPath + "\\LSP.dat";
            List<LSanPham> loai =TruyCapDuLieu.docFile(xuLy.getDanhSachQuanLyLSP(), path);
            CBTL.DataSource = loai;
            CBTL.DisplayMember = "MaLoai";
            CBTL.ValueMember = "MaLoai";
          
        }
    }
}
