using QuanLyHangHoa;
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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            frmQuanLySach f = new frmQuanLySach();
            this.Visible = false;
            f.ShowDialog();
            this.Visible = true;
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            frmTaoHoaDon f = new frmTaoHoaDon();
            this.Visible = false;
            f.ShowDialog();
            this.Visible = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearch f = new frmSearch();
            this.Visible = false;
            f.ShowDialog();
            this.Visible = true;
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            frmQLNhanVien n = new frmQLNhanVien();
            this.Visible = false;
            n.ShowDialog();
            this.Visible = true;
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
