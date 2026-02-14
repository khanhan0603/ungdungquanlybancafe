using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class NVien
    {
        private string MaNV;
        private string HoTen;
        private DateTime NgaySinh;
        private string GioiTinh;
        private string SDT;
        private string CCCD;
        private DateTime NgayVaoLam;
        private string ChucVu;
        public NVien()
        {
            this.MaNV = null;
            this.HoTen = null;
            this.NgaySinh = System.DateTime.Now;
            this.GioiTinh = null;
            this.SDT = null;
            this.CCCD = null;
            this.NgayVaoLam = System.DateTime.Now;
            this.ChucVu = null;
        }
        public NVien(string maNV, string hoTen, DateTime ngaySinh, string gioiTinh, string sDT, string cCCD, DateTime ngayVaoLam, string chucVu)
        {
            MaNV = maNV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            SDT = sDT;
            CCCD = cCCD;
            NgayVaoLam = ngayVaoLam;
            ChucVu = chucVu;
        }
        public string maNV
        {
            get { return MaNV; }
            set { MaNV = value; }
        }
        public string hoTen
        {

            get { return HoTen; }
            set { HoTen = value; }
        }
        public DateTime ngaySinh
        {
            get { return NgaySinh; }
            set { NgaySinh = value; }
        }
        public string gioiTinh
        {
            get { return GioiTinh; }
            set { GioiTinh = value; }
        }
        public string sdt
        {
            get { return SDT; }

            set { SDT = value; }
        }
        public string cccd
        {
            get { return CCCD; }
            set { CCCD = value; }
        }
        public DateTime ngayVaoLam
        {
            get { return NgayVaoLam; }
            set { NgayVaoLam = value; }
        }
        public string chucVu
        {
            get { return ChucVu; }
            set { ChucVu = value; }
        }
        public string tachTen()
        {
            string ten = null;
            int viTriKhoangTrangCuoi = this.hoTen.Trim().LastIndexOf(" ");
            ten = this.hoTen.Substring(viTriKhoangTrangCuoi + 1);
            return ten;
        }
    }
}
