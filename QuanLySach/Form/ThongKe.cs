using Microsoft.Reporting.WinForms;
using QuanLySach;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHangHoa
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            HHContextDB context = new HHContextDB();
            List<HangHoa> listHangHoas = context.HangHoas.ToList();
            List<SachReport> listReport = new List<SachReport>();
            foreach (HangHoa h in listHangHoas)
            {
                SachReport temp = new SachReport();
                temp.MaHang = h.MaHang.ToString();
                temp.TenHang = h.TenHang;
                temp.SoLuong = h.SoLuong;
                temp.DonGia = h.DonGia;
                temp.MaLoai = h.LoaiHang.TenLoai;
                temp.ThanhTien = h.SoLuong * h.DonGia;
                listReport.Add(temp);
            }
            reportViewer1.LocalReport.ReportPath = "Sachreport.rdlc";
            var source = new ReportDataSource("HangHoaDataSet", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
