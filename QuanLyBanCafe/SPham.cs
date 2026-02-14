using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class SPham
    {
        private string MaSP;
        private string TenSP;
        private double DonGia;
        private LSanPham lsp;

        public SPham()
        {
            this.MaSP = "";
            this.TenSP = "";
            this.DonGia = 0;
            this.lsp = null;
            
        }
        public SPham(string MaSP, string TenSP, double DonGia, LSanPham lsp)
        {
            this.MaSP = MaSP;
            this.TenSP = TenSP;
            this.DonGia = DonGia;
            this.lsp = lsp;
        }
        public string masp
        {
            get { return this.MaSP; }
            set { this.MaSP = value; }
        }
        public string tensp
        {
            get { return this.TenSP; }
            set { this.TenSP = value; }
        }
        public double dongia
        {
            get { return this.DonGia; }
            set { this.DonGia = value; }
        }
        public string MaLoai
        {
            get { return this.lsp.MaLoai; }
            set { this.lsp.MaLoai = value; }
        } 
        public string Tenloai
        {
            get { return this.lsp.TenLoai; }
            set { this.lsp.TenLoai = value; }
        }
    }
}
