using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySach
{
    public partial class frmSignUp : Form
    {
        HHContextDB context = new HHContextDB();
        public frmSignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }

        private void cbHienthi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienthi.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
                txtPass.UseSystemPasswordChar = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmSignIn form1 = new frmSignIn();
            this.Hide();
            form1.ShowDialog();
        }
        public bool check()
        {
            if (txtName.Text == "" || txtUser.Text == "" || txtEmail.Text == "" || txtSDT.Text == "" || txtPass.Text == "" || txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (txtSDT.TextLength != 10)
            {
                MessageBox.Show("Số điện thoại phải là 10 ký tự!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Account kh = context.Accounts.SingleOrDefault(n => n.Username == txtUser.Text);
            if (check())
            {
                if (kh != null)
                    MessageBox.Show("tài khoản đã có!", "Thông báo", MessageBoxButtons.OK);
                else
                {
                    Account acc = new Account();
                    acc.HoTen = txtName.Text;
                    acc.Username = txtUser.Text;
                    acc.Password = txtPass.Text;
                    acc.DiaChi = txtDiaChi.Text;
                    acc.Email = txtEmail.Text;
                    acc.SDT = txtSDT.Text;
                    context.Accounts.Add(acc);
                    context.SaveChanges();
                    MessageBox.Show("Đã đăng kí thành công!", "Thông báo", MessageBoxButtons.OK);
                    this.Hide();
                }
            }
        }

/*        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }*/

        //thiết lập thuộc tính KeyPressEventArgs.Handled là true để bỏ qua cú nhấn phím không hợp lệ

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))

                e.Handled = true;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            Regex regex;
            regex = new Regex(@"\S+@\S+\.\S+");

            Control ctrl = (Control)sender;
            if (regex.IsMatch(ctrl.Text))
            {
                errorEmail.SetError(ctrl, "");
            }
            else
            {
                errorEmail.SetError(ctrl,
                  "Địa chỉ email không hợp lệ.");
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            if(txtSDT.TextLength != 10)
            {
                errorSDT.SetError(ctrl, "Nhập số điện thoại hợp lệ.");
            }
            else
            {
                errorSDT.SetError(ctrl, "");
            }
        }
    }
}
