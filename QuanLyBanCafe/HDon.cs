using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class HDon
    {
        private string MaHD;
        private DateTime NgayLapHoaDon;
        private double TongTienHD;
        private List<CTietHDon> ct;
        private NVien nv;     
        public HDon() {
            this.MaHD = null;
            this.NgayLapHoaDon =System.DateTime.Now;
            this.TongTienHD = 0;
            this.ct = null;
            this.nv = null;
        }
        public HDon(string maHD,DateTime ngayLapHoaDon, NVien nv,double tongTienHD)
        {
            MaHD = maHD;
            NgayLapHoaDon = ngayLapHoaDon;
            this.nv=nv;
            TongTienHD = tongTienHD;
        }                 
        public string maHD
        { get { return this.MaHD; }
          set { this.MaHD = value; }
        }      
        public DateTime ngayLapHoaDon
        {
            get { return this.NgayLapHoaDon; }
            set { this.NgayLapHoaDon = value; }
        }
        public string MaNV
        {
            get { return this.nv.maNV; }
            set { this.nv.maNV = value; }
        }
        public double tongTienHD
        {
            get { return TongTienHD; }
            set { TongTienHD = value; }
        }
        public List<CTietHDon> DanhSachCTHD
        {
            get { return this.ct; }
            set { ct = value; }

        }

    }
}
