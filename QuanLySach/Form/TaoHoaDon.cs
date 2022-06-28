using QuanLySach;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHangHoa
{
    public partial class frmTaoHoaDon : Form
    {
        public frmTaoHoaDon()
        {
            InitializeComponent();
        }
        HHContextDB context = new HHContextDB();
        private void TaoHoaDon_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            List<HangHoa> listHangHoas = context.HangHoas.ToList();
            List<LoaiHang> listLoaiHangHoas = context.LoaiHangs.ToList();
            List<HoaDon> listHoaDons = context.HoaDons.ToList();
            BindGrid(listHoaDons);

        }

        private void BindGrid(List<HoaDon> listHoaDons)
        {
            dgvHoaDon.Rows.Clear();
            foreach (var item in listHoaDons)
            {
                int index = dgvHoaDon.Rows.Add();
                dgvHoaDon.Rows[index].Cells[0].Value = item.MaHD;
                dgvHoaDon.Rows[index].Cells[1].Value = item.TenKhachHang;
                dgvHoaDon.Rows[index].Cells[2].Value = item.NgayLap;
                dgvHoaDon.Rows[index].Cells[3].Value = item.NgayGiao;
                dgvHoaDon.Rows[index].Cells[4].Value = item.NhanVien;
                dgvHoaDon.Rows[index].Cells[5].Value = item.TinhTrang;
                dgvHoaDon.Rows[index].Cells[6].Value = item.TenHang;
                dgvHoaDon.Rows[index].Cells[7].Value = item.SoLuong;
            }
        }

        private void refesh()
        {
            txtmahoadon.Text = "";
            txtKhachHang.Text = "";
            txtNhanVien.Text = "";
/*            datetao.Value = DateTime.Today;
            dategiao.Value = DateTime.Today;*/
        }

        private void reloadDGV()
        {
            List<HoaDon> listHoaDons = context.HoaDons.ToList();
            BindGrid(listHoaDons);
        }
        private bool Check()
        {
            if (txtmahoadon.Text == "" || txtKhachHang.Text == "" || txtNhanVien.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!", "Thông báo", MessageBoxButtons.OK);
                //throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }


            else if (txtmahoadon.TextLength < 3)
            {
                MessageBox.Show("Mã hóa đơn phải lớn hơn 4 kí tự!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                int SelectedRow = GetSelectedRow(txtmahoadon.Text);
                {
                    if (SelectedRow == -1)
                    {
                        HoaDon h = new HoaDon()
                        {
                            MaHD = txtmahoadon.Text,
                            TenKhachHang = txtKhachHang.Text,
                            NgayLap = Convert.ToDateTime(datetao.Value.ToString()),
                            NgayGiao = Convert.ToDateTime(dategiao.Value.ToString()),
                            NhanVien = txtNhanVien.Text,
                            TinhTrang = rdoDa.Checked ? "Đã hoàn thành" : "Chưa hoàn thành",
                            TenHang = txtTenSach.Text,
                            SoLuong = Convert.ToInt32(nbrSoLuong.Value)

                        };
                        context.HoaDons.AddOrUpdate(h);
                        context.SaveChanges();
                        reloadDGV();
                        refesh();
                        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
        }


        private int GetSelectedRow(string MaSach)
        {
            for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
            {
                if (dgvHoaDon.Rows[i].Cells[0].Value != null)
                //them dong nay de check null dgv
                // or Bỏ dòng này & bỏ tích Enable Adding
                {
                    if (dgvHoaDon.Rows[i].Cells[0].Value.ToString() == MaSach)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            List<HoaDon> listSearch = context.HoaDons
        .Where(p => p.MaHD.Contains(txtSearch.Text)
                    || p.TenKhachHang.Contains(txtSearch.Text)
                    || p.NhanVien.Contains(txtSearch.Text)
                    || p.TinhTrang.Contains(txtSearch.Text))
        .ToList();
            BindGrid(listSearch);
            txtCount.Text = listSearch.Count().ToString();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            MoMoForm f = new MoMoForm();
            f.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void copyAlltoClipboard()
        {
            //to remove the first blank column from datagridview
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.SelectAll();
            DataObject dataObj = dgvHoaDon.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InHoaHonRP hd = new InHoaHonRP();
            hd.ShowDialog();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvHoaDon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgvHoaDon.CurrentRow.Selected = true;

                    txtmahoadon.Text = dgvHoaDon.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    txtKhachHang.Text = dgvHoaDon.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    datetao.Text = dgvHoaDon.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    dategiao.Text = dgvHoaDon.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                    txtNhanVien.Text = dgvHoaDon.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                    if (dgvHoaDon.Rows[e.RowIndex].Cells[5].FormattedValue.ToString() == "Đã thanh toán")
                    {
                        rdoDa.Checked = true;
                    }
                    else
                    {
                        rdChua.Checked = true;
                    }
                    txtTenSach.Text = dgvHoaDon.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                    nbrSoLuong.Value = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells[7].FormattedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*        private void btnMomo_Click(object sender, EventArgs e)
                {
                    frmMomo form = new frmMomo();
                    form.ShowDialog();
                }*/
    }
}
