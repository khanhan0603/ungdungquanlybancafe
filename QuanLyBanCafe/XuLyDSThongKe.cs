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
    internal class XuLyDSThongKe
    {
        private List<TKe> dsQLTK = null;
        private List<HDon> dsQLHD = null;
        private List<NVien>dsQLNV= null;
        public XuLyDSThongKe()
        {
            TruyCapDuLieu duLieu = TruyCapDuLieu.khoiTao();
            this.dsQLTK = duLieu.getDanhSachQuanLyThongKe();
            this.dsQLHD = duLieu.getDanhSachQuanLyHoaDon();
            this.dsQLNV=duLieu.getDanhSachQuanLyNV();
        }
        public List<TKe> getDanhSachQuanLyThongKe()
        {
            return this.dsQLTK;
        }
        public List<HDon> getDanhSachQuanLyHoaDon()
        {
            return this.dsQLHD; 
        }
        public List<NVien> getDanhSachQuanLyNhanVien()
        {
            return this.dsQLNV;
        }
        public List<HDon>locHoaDon(DateTime ngayBatDau,DateTime ngayKetThuc)
        {
            string path = Application.StartupPath + "\\HD.dat";
            List<HDon> dsHD = TruyCapDuLieu.docFile(this.dsQLHD, path);
            List<HDon> ketqua = new List<HDon>();
            foreach(HDon hd in dsHD)
            {
                if (hd.ngayLapHoaDon >= ngayBatDau && hd.ngayLapHoaDon <= ngayKetThuc)
                {
                    ketqua.Add(hd);
                }
            }
            return ketqua;
        }

        public int soLuongHDThongKe(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            int dem = 0;
            string path = Application.StartupPath + "\\HD.dat";
            List<HDon> dsHD = TruyCapDuLieu.docFile(this.dsQLHD, path);
            foreach (HDon hd in dsHD)
            {
                if (hd.ngayLapHoaDon >= ngayBatDau && hd.ngayLapHoaDon <= ngayKetThuc)
                {
                    dem++;
                }
            }
            return dem; 
        }

        public double tongTienHDThongKe(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            double tong = 0;
            string path = Application.StartupPath + "\\HD.dat";
            List<HDon> dsHD = TruyCapDuLieu.docFile(this.dsQLHD, path);
            foreach (HDon hd in dsHD)
            {
                if (hd.ngayLapHoaDon >= ngayBatDau && hd.ngayLapHoaDon <= ngayKetThuc)
                {
                    tong = tong + hd.tongTienHD;
                }
            }
            return tong;
        }

    }
}
