using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyBanCafe.TruyCapDuLieu;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class XuLyDSHoaDon
    {
        private List<HDon> dsQLHD=null ;
        private List<NVien> dsNhanVien=null ;
        private List<CTietHDon> dsCTHD =new List<CTietHDon>() ;
        public XuLyDSHoaDon()
        {
            TruyCapDuLieu duLieu = TruyCapDuLieu.khoiTao();
            this.dsQLHD = duLieu.getDanhSachQuanLyHoaDon();
            this.dsNhanVien = duLieu.getDanhSachQuanLyNV();
        }
        public List<HDon> getDanhSachQuanLyHoaDon()
        {
            return this.dsQLHD;
        }
        public List<NVien> getDanhSachQuanLyNV()
        {
            return this.dsNhanVien;
        }
        public List<CTietHDon> getDanhSachQuanLyCTHD()
        {
            return this.dsCTHD;
        }
        public bool kTraMa(string ma)
        {
            foreach (HDon nv in this.dsQLHD)
            {
                if (nv.maHD.Equals(ma)) return true;
            }
            return false;
        }
        public void themHD(HDon hd)
        {
            if (kTraMa(hd.maHD))
            {
                MessageBox.Show("Trùng mã!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                //CTietHDon ct = new CTietHDon();
                //themCTHD(ct);
                this.dsQLHD.Add(hd);
                MessageBox.Show("Hóa đơn được tạo thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            
        }
        public void themCTHD(CTietHDon ct)
        {
            if (ct.maHD != "")
            {
                this.dsCTHD.Add(ct);
                MessageBox.Show("Thêm sản phẩm vào hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK);

            }


        }
        public void xoaALLHD()
        {

            if (this.dsQLHD != null)
            {
                this.dsQLHD.Clear();
            }

        }
        public void xoaCTHD(string ma)
        {
            foreach (CTietHDon nv in this.dsCTHD)
            {
                if (nv.tenSP.Equals(ma))
                {
                    this.dsCTHD.Remove(nv);
                    break;
                }
            }

        }
        public void xoaALLCTHD()
        {
            this.dsCTHD.Clear();
        }
        public void suaCTHD(CTietHDon nv)
        {
            CTietHDon kq = timTheoMa(nv.maHD,nv.maSP);
            
            if (kq != null)
            {
                kq.tenSP = nv.tenSP;
                kq.DonGia = nv.DonGia;
                kq.soLuong = nv.soLuong;
                kq.thanhTien = nv.thanhTien;
                MessageBox.Show("Sửa thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Mã hóa đơn không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
            }
        }
        public CTietHDon timTheoMa(string ma,string maSP)
        {
            CTietHDon kq = new CTietHDon();
            foreach (CTietHDon nv in this.dsCTHD)
            {
                if (nv.maHD.Equals(ma)&&nv.maSP.Equals(maSP))
                {
                    kq = nv;
                }
            }
            return kq;
        }
       
    }
}
