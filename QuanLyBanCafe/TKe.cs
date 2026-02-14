using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class TKe
    {
        private DateTime thoiGianTK;
        private int soLuongHD;
        private double tongTienHD;
        private NVien nv;
        private List<HDon> dsHD;
        public TKe()
        {
            this.thoiGianTK = System.DateTime.Now;
            this.nv = null ;
            this.soLuongHD = 0;
            this.tongTienHD = 0;
            this.dsHD = null;

        }
        public TKe(DateTime thoiGianTK, NVien nv,int soLuongHD,double tongTienHD)
        {
            this.thoiGianTK = thoiGianTK;
            this.nv = nv;
            this.soLuongHD = soLuongHD;
            this.tongTienHD=tongTienHD;
        }     
        public DateTime ThoiGianTK
        {
            get { return this.thoiGianTK;}
            set {  this.thoiGianTK = value;}
        }
        public string MaNV
        {
            get { return this.nv.maNV; }
            set { this.nv.maNV = value;}
        }
        public int SL
        {
            get { return this.soLuongHD;}
            set { this.soLuongHD = value;}
        }
        public double TongTienTK
        {
            get { return this.tongTienHD; }
            set { this.tongTienHD = value; }
        }
        public List<HDon> DSHD
        {
            get { return this.dsHD; }
            set { this.DSHD = value; }
        }
       
    }
}
