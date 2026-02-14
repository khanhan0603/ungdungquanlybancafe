using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCafe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            this.Hide();           
           nv.ShowDialog();
            this.Show();
        }
        private void quảnLýLoạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoaiSanPham nv = new LoaiSanPham();
            this.Hide();
            nv.ShowDialog();
            this.Show();

        }
        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SanPham nv = new SanPham();
            this.Hide();
            nv.ShowDialog();
            this.Show();

        }
        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon nv = new HoaDon();
            this.Hide();
            nv.ShowDialog();
            this.Show();
        }
        private void quảnLýThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKe nv = new ThongKe();
            this.Hide();
            nv.ShowDialog();
            this.Show();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn thoát?","Thông báo",MessageBoxButtons.YesNo);
            if (kq == DialogResult.Yes) { this.Close(); }

        }
    }
}
