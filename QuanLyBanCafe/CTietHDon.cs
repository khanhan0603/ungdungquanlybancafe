using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyBanCafe
{
    [Serializable]
    internal class CTietHDon
    {
        private string MaHD;
        private int SoLuong;
        private double ThanhTien;
        private SPham sp ;    
        public CTietHDon()
        {
            this.maHD = "";
            this.SoLuong = 0;
            this.ThanhTien = 0;
            sp=new SPham() ;
        }
        public CTietHDon(string maHD, SPham sp,int soLuong, double thanhTien)
        {
            this.maHD = maHD;
            this.sp = sp;
            this.SoLuong = soLuong;
            this.ThanhTien = thanhTien;
          
        }
        public string maHD
        {
            get { return this.MaHD; }
            set { this.MaHD = value; }
        }
        private string MaSP;
        private string TenSP;
        public string maSP
        {
            get { return MaSP = this.sp.masp; }
            set { this.MaSP = value; }
        }
        public string tenSP
        {
            get { return  this.TenSP = sp.tensp; }
            set { this.TenSP = value; }
        }
        public double DonGia
        {
            get { return this.sp.dongia; }
            set { this.sp.dongia = value; }
        }
        public int soLuong
        {
            get { return this.SoLuong; }
            set { this.SoLuong = value; }
        }
        public double thanhTien
        {
            get { return this.ThanhTien; }
            set { this.ThanhTien = value; }
        }
        public SPham Sp
        {
            get { return this.sp; }
            set { this.sp = value; }
        }

    }
}
