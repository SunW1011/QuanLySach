using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySach
{
    public partial class InHoaHonRP : Form
    {
        public InHoaHonRP()
        {
            InitializeComponent();
        }

        private void InHoaHonRP_Load(object sender, EventArgs e)
        {
            HHContextDB context = new HHContextDB();
            List<HoaDon> listHoadon = context.HoaDons.ToList();
            // gán trên xuốnggggggg
            List<HoaDonReport> listReport = new List<HoaDonReport>();
            foreach (HoaDon item in listHoadon)
            {
                HoaDonReport temp = new HoaDonReport();
                temp.MaHD = item.MaHD;
                temp.NgayGiao = item.NgayGiao;
                temp.NgayLap = item.NgayLap;
                temp.NhanVien = item.NhanVien;
                temp.TenKhachHang = item.TenKhachHang;
                temp.TinhTrang = item.TinhTrang;
                temp.TenHang = item.TenHang;
                temp.SoLuong = item.SoLuong;
                listReport.Add(temp);
            }

            reportViewer1.LocalReport.ReportPath = "HoaDonReport.rdlc";
            var source = new ReportDataSource("HoaDonDataset", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
