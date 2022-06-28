using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySach
{
    public partial class frmQLNhanVien : Form
    {
        HHContextDB context = new HHContextDB();
        public frmQLNhanVien()
        {
            InitializeComponent();
        }

        public bool check()
        {
            if (txtUser.Text == "" || txtPass.Text == "" || txtName.Text == "" || txtSDT.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else if (txtSDT.TextLength != 10)
            {
                MessageBox.Show("Số điện thoại phải đúng 10 ký tự!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            if (check())
            {
                if (getselectedRow(txtUser.Text) == -2)
                {
                    Account acc = new Account();

                    acc.Username = txtUser.Text;
                    acc.Password = txtPass.Text;
                    //acc.LoaiTK = txtLoai.Text;
                    if (cbLoai.SelectedIndex == 0)
                    {
                        acc.LoaiTK = "ADMIN";
                    }
                    else
                    {
                        acc.LoaiTK = "NhanVien";
                    }
                    acc.Email = txtEmail.Text;
                    acc.SDT = txtSDT.Text;
                    acc.HoTen = txtName.Text;
                    acc.DiaChi = txtDiaChi.Text;
                    context.Accounts.Add(acc);
                    context.SaveChanges();
                    ReloadDgvQLNV();
                    Reload();

                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Tài Khoản đã có, mời bạn nhập lại!", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void frmQLNhanVien_Load(object sender, EventArgs e)
        {
            List<Account> listAccount = context.Accounts.ToList();
            BindGrid(listAccount);
            cbLoai.SelectedIndex = 0;
        }

        private void BindGrid(List<Account> acc)
        {
            dgvQLNV.Rows.Clear();
            foreach (var item in acc)
            {
                int index = dgvQLNV.Rows.Add();
                dgvQLNV.Rows[index].Cells[0].Value = item.Username;
                dgvQLNV.Rows[index].Cells[1].Value = item.Password;
                dgvQLNV.Rows[index].Cells[2].Value = item.HoTen;
                dgvQLNV.Rows[index].Cells[3].Value = item.SDT;
                dgvQLNV.Rows[index].Cells[4].Value = item.Email;
                dgvQLNV.Rows[index].Cells[5].Value = item.DiaChi;
                dgvQLNV.Rows[index].Cells[6].Value = item.LoaiTK;
            }
        }

        private int getselectedRow(string id)
        {
            for (int i = 0; i < dgvQLNV.Rows.Count; i++)
            {
                if (dgvQLNV.Rows[i].Cells[0].Value != null)
                {
                    if (dgvQLNV.Rows[i].Cells[0].Value.ToString() == id)
                    {
                        return i;
                    }
                }
            }
            return -2;
        }

        private void dgvQLNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvQLNV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgvQLNV.CurrentRow.Selected = true;

                    txtUser.Text = dgvQLNV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    txtPass.Text = dgvQLNV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    txtName.Text = dgvQLNV.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    txtSDT.Text = dgvQLNV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                    txtEmail.Text = dgvQLNV.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                    txtDiaChi.Text = dgvQLNV.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                    cbLoai.SelectedIndex = cbLoai.FindString(dgvQLNV.Rows[e.RowIndex].Cells[6].FormattedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reload()
        {
            txtUser.Text = "";
            txtName.Text = "";
            txtPass.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
        }

        private void ReloadDgvQLNV()
        {
            List<Account> n = context.Accounts.ToList();
            BindGrid(n);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Account n = context.Accounts.FirstOrDefault(p => p.Username == txtUser.Text);


            if (n != null)
            {
                DialogResult ac = MessageBox.Show($"Xóa tài khoản '{n.Username}'", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ac == DialogResult.Yes)
                {
                    context.Accounts.Remove(n);

                    context.SaveChanges();
                    ReloadDgvQLNV();
                    Reload();
                    MessageBox.Show("Bạn đã xóa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Tài khoản cần xóa không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (check())
            {
                Account n = context.Accounts.FirstOrDefault(p => p.Username == txtUser.Text);
                if (n != null)
                {
                    n.Username = txtUser.Text;
                    n.Password = txtPass.Text;
                    if (cbLoai.SelectedIndex == 0)
                    {
                        n.LoaiTK = "Admin";
                    }
                    else
                    {
                        n.LoaiTK = "NhanVien";
                    }
                    n.Email = txtEmail.Text;
                    n.SDT = txtSDT.Text;
                    n.HoTen = txtName.Text;
                    n.DiaChi = txtDiaChi.Text;
                    context.Accounts.AddOrUpdate(n);
                    context.SaveChanges();
                    ReloadDgvQLNV();
                    MessageBox.Show("Chỉnh sửa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Account cần sửa!", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            List<Account> listSearch = context.Accounts
        .Where(p => p.Username.Contains(txtSearch.Text)
                    || p.HoTen.Contains(txtSearch.Text)
                    || p.SDT.Contains(txtSearch.Text)
                    || p.Email.Contains(txtSearch.Text))
        .ToList();
            BindGrid(listSearch);
        }
    }
}
