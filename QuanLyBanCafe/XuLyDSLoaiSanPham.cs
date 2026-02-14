using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyBanCafe.TruyCapDuLieu;
using System.Windows.Forms;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class XuLyDSLoaiSanPham
    {
        private List<LSanPham> dsQLLSP;
        public XuLyDSLoaiSanPham()
        {
            TruyCapDuLieu duLieu = TruyCapDuLieu.khoiTao();
            this.dsQLLSP = duLieu.getDanhSachQuanLyLSP();

        }
        public List<LSanPham> getDanhSachQuanLyLSP()
        {
            return this.dsQLLSP;
        }
        public bool kTraMa(string ma)
        {
            foreach (LSanPham nv in this.dsQLLSP)
            {
                if (nv.MaLoai.Equals(ma)) return true;
            }
            return false;
        }
        public void them(LSanPham sp)
        {
            if (kTraMa(sp.MaLoai))
            {
                MessageBox.Show("Trùng Mã!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                this.dsQLLSP.Add(sp);

            }
        }
        public void xoa(string MaLoai)
        {
            foreach (LSanPham nv in this.dsQLLSP)
            {
                if (nv.MaLoai.Equals(MaLoai))
                {
                    this.dsQLLSP.Remove(nv);
                    break;
                }
            }

        }
        public void sua(LSanPham nv)
        {
            LSanPham kq = timTheoMa(nv.MaLoai);
            if (kq != null)
            {
                kq.MaLoai = nv.MaLoai;
                kq.TenLoai = nv.TenLoai;
                MessageBox.Show("Sửa thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Mã loại sản phẩm không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
            }
        }
        public LSanPham timTheoMa(string ma)
        {
            LSanPham kq = new LSanPham();
            foreach (LSanPham nv in this.dsQLLSP)
            {
                if (nv.MaLoai.Equals(ma))
                {
                    kq = nv;
                }
            }
            return kq;
        }     
        public List<LSanPham> timTheoMaLoai(string ma)
        {
            List<LSanPham> kq = new List<LSanPham>();
            foreach (LSanPham nv in this.dsQLLSP)
            {
                if (nv.MaLoai.Equals(ma))
                {
                    kq.Add(nv);
                }
            }
            return kq;
        }
        public List<LSanPham> timTheoTen(string ten)
        {
            List<LSanPham> kq = new List<LSanPham>();
            foreach (LSanPham nv in this.dsQLLSP)
            {
                if (nv.TenLoai.Equals(ten))
                {
                    kq.Add(nv);
                }
            }
            return kq;
        }
    }
}
