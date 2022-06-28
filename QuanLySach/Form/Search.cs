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
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        HHContextDB context = new HHContextDB();
        private void frmSearch_Load(object sender, EventArgs e)
        {
            HHContextDB context = new HHContextDB();
            List<HangHoa> listHangHoas = context.HangHoas.ToList();
            List<LoaiHang> listLoaiHangHoas = context.LoaiHangs.ToList();
            BindGrid(listHangHoas);
        }

        private void BindGrid(List<HangHoa> listHangHoas)
        {
            dgvHangHoa.Rows.Clear();
            foreach (var item in listHangHoas)
            {
                int index = dgvHangHoa.Rows.Add();
                dgvHangHoa.Rows[index].Cells[0].Value = item.MaHang;
                dgvHangHoa.Rows[index].Cells[1].Value = item.TenHang;
                dgvHangHoa.Rows[index].Cells[2].Value = item.DonGia;
                dgvHangHoa.Rows[index].Cells[3].Value = item.SoLuong;
                dgvHangHoa.Rows[index].Cells[4].Value = item.LoaiHang.TenLoai;
                dgvHangHoa.Rows[index].Cells[5].Value = item.DonGia * item.SoLuong;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<HangHoa> listSearch = context.HangHoas
                    .Where(p => p.MaHang.Contains(txtFind.Text)
                                || p.TenHang.Contains(txtFind.Text)
                                || p.LoaiHang.TenLoai.ToString().Contains(txtFind.Text))
                    .ToList();
            BindGrid(listSearch);
            txtCount.Text = listSearch.Count().ToString();
        }

    }
}
