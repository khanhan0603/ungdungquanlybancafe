using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class LSanPham
    {
        private string maLoai;
        private string tenLoai;
        public LSanPham()
        {
            this.maLoai = null;
            this.tenLoai = null;
        }

        public LSanPham(string maLoai, string tenLoai)
        {
            this.maLoai = maLoai;
            this.tenLoai = tenLoai;
        }

        public string TenLoai
        {
            get { return tenLoai; }
            set { this.tenLoai = value; }
        }

        public string MaLoai
        {
            get { return maLoai; }
            set { this.maLoai = value; }
        }
    }
}
