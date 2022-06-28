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
    public partial class frmLuaChon : Form
    {
        HHContextDB context = new HHContextDB();
        public string username;
        public frmLuaChon()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien n = new frmNhanVien();
            this.Hide();
            n.ShowDialog();
            this.Show();
        }

        private void frmLuaChon_Load(object sender, EventArgs e)
        {
            /*Account a = new Account();
            LoginNV l = new LoginNV()
            {
                Username = a.Username,
                TimeLogin = System.DateTime.Now,
                TimeLogout = System.DateTime.Now,
            };
            context.LoginNVs.Add(l);
            context.SaveChanges();*/
        }
    }
}
