using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyBanCafe.TruyCapDuLieu;

namespace QuanLyBanCafe
{
    internal class XuLyDuLieu
    {
        internal class XuLyDSachNhanVien
        {
            private List<NVien> dsQLNV;


            public XuLyDSachNhanVien()
            {
                NhanVienDAL duLieu = NhanVienDAL.khoiTao();
                this.dsQLNV = duLieu.getDanhSachQuanLyNV();

            }

            public List<NVien> getDanhSachQuanLyNV()
            {
                return this.dsQLNV;
            }

            public bool kTraMa(string ma)
            {
                foreach (NVien nv in this.dsQLNV)
                {
                    if (nv.maNV.Equals(ma)) return true;
                }
                return false;
            }

            public void them(NVien nv)
            {
                if (kTraMa(nv.maNV))
                {
                    MessageBox.Show("Trùng Mã!!!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    this.dsQLNV.Add(nv);

                }
            }

            public void xoa(string MaNV)
            {
                foreach (NVien nv in this.dsQLNV)
                {
                    if (nv.maNV.Equals(MaNV))
                    {
                        this.dsQLNV.Remove(nv);
                        break;
                    }
                }

            }

            public void sua(NVien nv)
            {
                NVien kq = timTheoMa(nv.maNV);
                if (kq != null)
                {
                    kq.maNV = nv.maNV;
                    kq.hoTen = nv.hoTen;
                    kq.ngaySinh = nv.ngaySinh;
                    kq.gioiTinh = nv.gioiTinh;
                    kq.sdt = nv.sdt;
                    kq.cccd = nv.cccd;
                    kq.ngayVaoLam = nv.ngayVaoLam;
                    kq.chucVu = nv.chucVu;
                    MessageBox.Show("Sửa thành công!!!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Mã nhân viên không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            public bool LuuDanhSachNhanVien(List<NVien> danhSachNhanVien, string filePath)
            {
                return NhanVienDAL.ghiFile(danhSachNhanVien, filePath);
            }

            public List<NVien> LayDanhSachNhanVien(string filePath)
            {
                return NhanVienDAL.docFile(filePath);
            }

            public NVien timTheoMa(string ma)
            {
                NVien kq = new NVien();
                foreach (NVien nv in dsQLNV)
                {
                    if (nv.maNV.Equals(ma))
                    {
                        kq = nv;
                    }
                }
                return kq;
            }
            public List<NVien> timTheoMaNV(string ma)
            {
                List<NVien> kq = new List<NVien>();
                foreach (NVien nv in this.dsQLNV)
                {
                    if (nv.maNV.Equals(ma))
                    {
                        kq.Add(nv);
                    }
                }
                return kq;
            }
            public List<NVien> timTheoTen(string ten)
            {
                List<NVien> kq = new List<NVien>();
                foreach (NVien nv in dsQLNV)
                {
                    if (nv.tachTen().Equals(ten))
                    {
                        kq.Add(nv);
                    }
                }
                return kq;
            }
            public List<NVien> timTheoChucVu(string chucvu)
            {
                List<NVien> kq = new List<NVien>();
                foreach (NVien nv in dsQLNV)
                {
                    if (nv.chucVu.Equals(chucvu))
                    {
                        kq.Add(nv);
                    }
                }
                return kq;
            }



        }
    }
    internal class XuLyDSachLoaiSanPham
    {
        private List<LSanPham> dsQLLSP;

        public XuLyDSachLoaiSanPham()
        {
            LoaiSanPhamDAL duLieu = LoaiSanPhamDAL.khoiTao();
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
                MessageBox.Show("Mã nhân viên không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
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
        public bool LuuDanhSachLoaiSanPham(List<LSanPham> danhSachQuanLyLoaiSanPham, string filePath)
        {
            return LoaiSanPhamDAL.ghiFile(danhSachQuanLyLoaiSanPham, filePath);
        }
        public List<LSanPham> LayDanhSachLoaiSanPham(string filePath)
        {
            return LoaiSanPhamDAL.docFile(filePath);
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
    internal class XuLyDanhSachSanPham
    {
        private List<SPham> dsQLSP;
        public XuLyDanhSachSanPham()
        {
            SanPhamDAL duLieu = SanPhamDAL.khoiTao();
            this.dsQLSP = duLieu.getDanhSachQuanLySP();

        }
        public List<SPham> getDanhSachQuanLySP()
        {
            return this.dsQLSP;
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
                MessageBox.Show("Trùng Mã!!!", "Thông báo", MessageBoxButtons.OK);
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
                //kq.tenloai = nv.tenloai;    
                kq.maloai = nv.maloai;
                MessageBox.Show("Sửa thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Mã nhân viên không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
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
        public bool LuuDanhSachSanPham(List<SPham> danhSachSanPham, string filePath)
        {
            return SanPhamDAL.ghiFile(danhSachSanPham, filePath);
        }
        public List<SPham> LayDanhSachSanPham(string filePath)
        {
            return SanPhamDAL.docFile(filePath);
        }
        public List<SPham> timTheoMaLoai(string ma)
        {
            List<SPham> kq = new List<SPham>();
            foreach (SPham nv in this.dsQLSP)
            {
                if (nv.masp.Equals(ma))
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

    internal class XuLyDanhSachHoaDon
    {
        private List<HDon> dsQLHD;
        public XuLyDanhSachHoaDon()
        {
            HoaDonDAL duLieu = HoaDonDAL.khoiTao();
            this.dsQLHD = duLieu.getDanhSachQuanLyHoaDon();

        }
        public List<HDon> getDanhSachQuanLyHoaDon()
        {
            return this.dsQLHD;
        }

      
       

    }

    internal class XuLyDanhSachCTHD
    {
        private List<CTietHDon> dsQLCTHD;
        public XuLyDanhSachCTHD()
        {
            ChiTietHoaDonDAL duLieu = ChiTietHoaDonDAL.khoiTao();
            this.dsQLCTHD = duLieu.getDanhSachQuanLyCTHD();

        }
        public List<CTietHDon> getDanhSachQuanLyCTHD()
        {
            return this.dsQLCTHD;
        }

        public void them(CTietHDon hd)
        {

            this.dsQLCTHD.Add(hd);
        }


        public void xoa(string ma)
        {
            foreach (CTietHDon nv in this.dsQLCTHD)
            {
                if (nv.maHD.Equals(ma))
                {
                    this.dsQLCTHD.Remove(nv);
                    break;
                }
            }

        }
        public void sua(CTietHDon nv)
        {
            CTietHDon kq = timTheoMa(nv.maHD);
            if (kq != null)
            {
                kq.maHD = nv.maHD; 
                kq.ngayLapHoaDon = nv.ngayLapHoaDon;
                kq.maNV = nv.maNV;
                kq.maSP = nv.maSP;
                kq.tenSP = nv.tenSP;
                kq.soLuong=nv.soLuong;
                kq.donGia=nv.donGia;
                kq.thanhTien=nv.thanhTien;
                MessageBox.Show("Sửa thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Mã hóa đơn không tồn tại!!!", "Thông báo", MessageBoxButtons.OK);
            }
        }
        public CTietHDon timTheoMa(string ma)
        {
            CTietHDon kq = new CTietHDon();
            foreach (CTietHDon nv in this.dsQLCTHD)
            {
                if (nv.maHD.Equals(ma))
                {
                    kq = nv;
                }
            }
            return kq;
        }
        public bool LuuDanhSachChiTietHoaDon(List<CTietHDon> danhSachSanPham, string filePath)
        {
            return ChiTietHoaDonDAL.ghiFile(danhSachSanPham, filePath);
        }
        public List<CTietHDon> LayDanhSachChiTietHoaDon(string filePath)
        {
            return ChiTietHoaDonDAL.docFile(filePath);
        }
        public List<CTietHDon> timTheoMaHD(string ma)
        {
            List<CTietHDon> kq = new List<CTietHDon>();
            foreach (CTietHDon nv in this.dsQLCTHD)
            {
                if (nv.maHD.Equals(ma))
                {
                    kq.Add(nv);
                }
            }
            return kq;
        }
        public List<CTietHDon> timTheoMaNV(string ma)
        {
            List<CTietHDon> kq = new List<CTietHDon>();
            foreach (CTietHDon nv in this.dsQLCTHD)
            {
                if (nv.maNV.Equals(ma))
                {
                    kq.Add(nv);
                }
            }
            return kq;
        }
    }

}
