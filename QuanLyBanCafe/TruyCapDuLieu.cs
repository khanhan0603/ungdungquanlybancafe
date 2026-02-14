using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe
{
    [Serializable]
    internal class TruyCapDuLieu
    {
        private static TruyCapDuLieu instance = null;
        private List<LSanPham> dsLoaiSanPham;
        private List<SPham> dsSanPham;
        private List<NVien> dsNhanVien;
        private List<HDon> dsHoaDon;
        private List<TKe> dsThongKe;
        private TruyCapDuLieu()
        {
            dsLoaiSanPham = new List<LSanPham>();
            dsSanPham = new List<SPham>();
            dsNhanVien = new List<NVien>();
            dsHoaDon = new List<HDon>();
            dsThongKe=new List<TKe>();
        }
        public static TruyCapDuLieu khoiTao()
        {
            if (instance == null)
                instance = new TruyCapDuLieu();
            return instance;
        }
        public List<LSanPham> getDanhSachQuanLyLSP()
        {
            return dsLoaiSanPham;
        }
        public List<SPham> getDanhSachQuanLySP()
        {
            return dsSanPham;
        }
        public List<NVien> getDanhSachQuanLyNV()
        {
            return dsNhanVien;
        }  
        public List<HDon> getDanhSachQuanLyHoaDon()
        {
            return dsHoaDon;
        }      
        public List<TKe> getDanhSachQuanLyThongKe()
        {
            return dsThongKe;   
        }
        public static List<T> docFile<T>(List <T> data,string path)
        {
            try
            {
                FileStream fp = new FileStream(path, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                data = (List<T>)bf.Deserialize(fp);
                fp.Close();
            }
            catch (Exception error)
            {

            }
            return data;
        }
        public static bool ghiFile<T>(List<T> data,string path)
        {
            try
            {
                FileStream fp = new FileStream(path, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fp, data);
                fp.Close();
                return true;

            }

            catch (Exception error)
            {
                return false;
            }
        }

      
    }
  }
