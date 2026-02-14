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
    internal class XuLyDSNhanVien
    {
        private List<NVien> dsQLNV;
        public XuLyDSNhanVien()
        {
            TruyCapDuLieu duLieu = TruyCapDuLieu.khoiTao();
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
