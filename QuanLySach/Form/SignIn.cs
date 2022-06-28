using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyHangHoa;

namespace QuanLySach
{
    public partial class frmSignIn : Form
    {
        HHContextDB context = new HHContextDB();
        public string username;
        public string password;

        private bool check()
        {
            if (txtPass.Text == "" || txtUser.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đủ thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public frmSignIn()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
            cbSave.Checked = Enabled;
            txtUser.Text = Properties.Settings.Default.users;
            txtPass.Text = Properties.Settings.Default.password;

        }
            
        private void btnLSignIn_Click(object sender, EventArgs e)
        {
            /*            frmQuanLySach form = new frmQuanLySach();
                        this.Visible = false;
                        form.ShowDialog();
                        this.Visible = true;*/
            Account kh = context.Accounts.SingleOrDefault(n => n.Username == txtUser.Text && n.Password == txtPass.Text);
            if (check())
            {
                if (kh != null)
                {
                    
                    if (kh.LoaiTK == "Admin")
                    {
                        MessageBox.Show($"Đăng nhập thành công!\n{DateTime.Now}", "Admin", MessageBoxButtons.OK);
                        
                        frmLuaChon admin = new frmLuaChon();
                        this.Hide();
                        admin.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show($"Đăng nhập thành công!\n{DateTime.Now}", "Nhân viên", MessageBoxButtons.OK);
                        frmNhanVien nv = new frmNhanVien();
                        this.Hide();
                        nv.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
                }
            }
        }

        private void cbHienThi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienthi.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
                txtPass.UseSystemPasswordChar = true;
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if(txtUser.Text == "Tên tài khoản")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Red;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "Tên tài khoản";
                txtUser.ForeColor = Color.Black;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if(txtPass.Text == "Mật khẩu")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Red;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Mật khẩu";
                txtPass.ForeColor = Color.Black;
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            frmSignUp form = new frmSignUp();
            this.Visible = false;
            form.ShowDialog();
            this.Visible = true;
        }

        private void frmSignIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cbSave_CheckedChanged(object sender, EventArgs e)
        {
                if(cbSave.Checked)
            {
                Properties.Settings.Default.users = txtUser.Text;
                Properties.Settings.Default.password = txtPass.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.users = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.Save();
            }
        }
    }
}
