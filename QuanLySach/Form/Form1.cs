using QuanLySach;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyHangHoa
{
    public partial class frmQuanLySach : Form
    {

        public frmQuanLySach()
        {
            InitializeComponent();
            
        }
        HHContextDB context = new HHContextDB();

        private void frmQuanLyHangHoa_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            cmbLoaiHang.SelectedIndex = 0;
            txtSum.ReadOnly = true;
            try
            {
                HHContextDB context = new HHContextDB();
                List<HangHoa> listHangHoas = context.HangHoas.ToList();
                List<LoaiHang> listLoaiHangHoas = context.LoaiHangs.ToList();
                FillTheLoaiCombobox(listLoaiHangHoas);
                BindGrid(listHangHoas);
                Sum();
            }
            catch (Exception)
            {

                throw;
            }
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

        private void FillTheLoaiCombobox(List<LoaiHang> listLoaiHangHoas)
        {
            this.cmbLoaiHang.DataSource = listLoaiHangHoas;
            this.cmbLoaiHang.DisplayMember = "TenLoai";
            this.cmbLoaiHang.ValueMember = "MaLoai";
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvHangHoa.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgvHangHoa.CurrentRow.Selected = true;

                    //thay vì truyền tên cột trong datagirdView, có thể truyền thứ tự của các cột để lấy giá trị
                    txtMaHang.Text = dgvHangHoa.Rows[e.RowIndex].Cells["colMaHang"].FormattedValue.ToString();
                    txtTenHang.Text = dgvHangHoa.Rows[e.RowIndex].Cells["colTenHang"].FormattedValue.ToString();
                    txtDonGia.Text = dgvHangHoa.Rows[e.RowIndex].Cells["colDonGia"].FormattedValue.ToString();
                    nbericSoLuong.Value = Convert.ToInt32(dgvHangHoa.Rows[e.RowIndex].Cells["colSoLuong"].FormattedValue.ToString());
                    cmbLoaiHang.SelectedIndex = cmbLoaiHang.FindString(dgvHangHoa.Rows[e.RowIndex].Cells["colLoaiHang"].FormattedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Check()
        { 
            if (txtMaHang.Text == "" || txtTenHang.Text == "" || txtDonGia.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!");
                //throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }


            else if (txtMaHang.TextLength < 3)
            {
                MessageBox.Show("Mã hàng phải lớn hơn 3 kí tự!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                int SelectedRow = GetSelectedRow(txtMaHang.Text);
                if (SelectedRow == -1) // Thêm TH mới?
                {

                     HangHoa s = new HangHoa()
                    {
                        MaHang = txtMaHang.Text,
                        TenHang = txtTenHang.Text,
                        DonGia = int.Parse(txtDonGia.Text),
                        SoLuong = Convert.ToInt32(nbericSoLuong.Value),
                        //SoLuong = int.Parse(nbericSoLuong.Value.ToString()),
                        MaLoai = int.Parse(cmbLoaiHang.SelectedValue.ToString())
                    };
                    context.HangHoas.Add(s);
                    context.SaveChanges();
                    reloadDGV();
                    refesh();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void refesh()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            txtDonGia.Text = "";
            nbericSoLuong.Value = 0;
            Sum();
        }

        private void reloadDGV()
        {
            List<HangHoa> listHangHoas = context.HangHoas.ToList();
            BindGrid(listHangHoas);
        }

        // Check ten
        private int GetSelectedRow(string MaHang)
        {
            for (int i = 0; i < dgvHangHoa.Rows.Count; i++)
            {
                //if (dgvHangHoa.Rows[i].Cells[0].Value != null)
                //them dong nay de check null dgv
                // or Bỏ dòng này & bỏ tích Enable Adding
                {
                    if (dgvHangHoa.Rows[i].Cells[0].Value.ToString() == MaHang)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            HangHoa a = context.HangHoas.FirstOrDefault(s => s.MaHang.CompareTo(txtMaHang.Text) == 0);
            //context.MaHang.SingleOrDefault(n => n.TenHang == txtTenHang.Text);
            if (a == null)
            {
                MessageBox.Show("Hàng cần sửa không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn sửa?", "YES/NO", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    a.MaHang = txtMaHang.Text;
                    a.TenHang = txtTenHang.Text;
                    a.DonGia = int.Parse(txtDonGia.Text);
                    a.SoLuong = Convert.ToInt32(nbericSoLuong.Value);
                    a.MaLoai = int.Parse(cmbLoaiHang.SelectedValue.ToString());
                    context.HangHoas.AddOrUpdate();
                    context.SaveChanges();
                    BindGrid(context.HangHoas.ToList());
                    reloadDGV();
                    refesh();
                    MessageBox.Show("Cập nhật mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            HangHoa dbDelete = context.HangHoas.FirstOrDefault(p => p.MaHang == txtMaHang.Text);
            if (dbDelete != null)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn xoá?", "YES/NO", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    context.HangHoas.Remove(dbDelete);
                    context.SaveChanges();
                    reloadDGV();
                    refesh();
                    MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK);
                }

            }
            else
            {
                MessageBox.Show("Sách cần xoá không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

/*        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            List<HangHoa> listSearch = context.HangHoas
                    .Where(p => p.MaHang.Contains(txtFind.Text)
                                || p.TenHang.Contains(txtFind.Text)
                                || p.LoaiHang.TenLoai.ToString().Contains(txtFind.Text))
                    .ToList();
            BindGrid(listSearch);
        }*/

/*        private void txtSum_TextChanged(object sender, EventArgs e)
        {
        }*/
        
        private void Sum()
        {
            decimal Total = 0;
            for (int i = 0; i < dgvHangHoa.Rows.Count; i++)
            {
                Total += Convert.ToDecimal(dgvHangHoa.Rows[i].Cells["colThanhTien"].Value);
            }

            txtSum.Text = Total.ToString() + " VNĐ";
        }


        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSearch form = new frmSearch();
            form.ShowDialog();
        }

        private void TaoHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTaoHoaDon form = new frmTaoHoaDon();
            form.ShowDialog();
        }


        private void thốngKêHàngHoáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKe t = new ThongKe();
            t.Show();
        }

        private void làmMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refesh();
            reloadDGV();
        }

        private void momoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMomo form = new frmMomo();
            form.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSignIn f = new frmSignIn();
            this.Visible = false;
            f.ShowDialog();
            this.Visible = true;
        }
    }
}
