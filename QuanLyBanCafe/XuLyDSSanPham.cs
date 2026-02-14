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
    internal class XuLyDSSanPham
    {
        private List<SPham> dsQLSP=null;
        private List<LSanPham> loaisanpham;
        public XuLyDSSanPham()
        {
            TruyCapDuLieu duLieu = TruyCapDuLieu.khoiTao();
            this.dsQLSP = duLieu.getDanhSachQuanLySP();
            this.loaisanpham = duLieu.getDanhSachQuanLyLSP();
        }
        public List<SPham> getDanhSachQuanLySP()
        {
            return this.dsQLSP;
        }
        public List<LSanPham> getDanhSachQuanLyLSP()
        {
            return this.loaisanpham;
        }
        public bool kTraMa(string ma)
        {
            foreach (SPham nv in this.dsQLSP)
            {
                if (nv.masp.Equals(ma)) return true;
            }
            return false;
        }
        public void them(SPham sp)
        {
            if (kTraMa(sp.masp))
            {
                MessageBox.Show("Trùng mã!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                this.dsQLSP.Add(sp);

            }
        }
        public void xoa(string ma)
        {
            foreach (SPham nv in this.dsQLSP)
            {
                if (nv.masp.Equals(ma))
                {
                    this.dsQLSP.Remove(nv);
                    break;
                }
            }

        }
        public void sua(SPham nv)
        {
            SPham kq = timTheoMa(nv.masp);
            if (kq != null)
            {
                kq.masp = nv.masp;
                kq.tensp = nv.tensp;
                kq.dongia = nv.dongia;
               
                kq.MaLoai = nv.MaLoai;
                kq.Tenloai = nv.Tenloai;
                MessageBox.Show("Sửa thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Mã sản phẩm không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
            }
        }
        public SPham timTheoMa(string ma)
        {
            SPham kq = new SPham();
            foreach (SPham nv in this.dsQLSP)
            {
                if (nv.masp.Equals(ma))
                {
                    kq = nv;
                }
            }
            return kq;
        }     
        public List<SPham> timTheoMaLoai(string ma)
        {
            List<SPham> kq = new List<SPham>();
            foreach (SPham nv in this.dsQLSP)
            {
                if (nv.MaLoai.Equals(ma))
                {
                    kq.Add(nv);
                }
            }
            return kq;
        }
        public List<SPham> timTheoTen(string ten)
        {
            List<SPham> kq = new List<SPham>();
            foreach (SPham nv in this.dsQLSP)
            {
                if (nv.tensp.Equals(ten))
                {
                    kq.Add(nv);
                }
            }
            return kq;
        }
        public List<SPham> timTheoDonGia(float dongia)
        {
            List<SPham> kq = new List<SPham>();
            foreach (SPham nv in this.dsQLSP)
            {
                if (nv.dongia == dongia)
                {
                    kq.Add(nv);
                }
            }
            return kq;
        }
    }
}
