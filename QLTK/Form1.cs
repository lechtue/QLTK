using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTK
{
    public partial class frmDangNhap : Form
    {
        private QLAdmin qladmin;
        private Admin admin;
        public frmDangNhap()
        {
            InitializeComponent();
        }
        public void GetAdmin()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select * from Admin";
            read = cmd.ExecuteReader();
            qladmin = new QLAdmin();
            while (read.Read())
            {
                admin = new Admin();
                admin.User = read.GetValue(0).ToString();
                admin.Password = read.GetValue(1).ToString();
                qladmin.ThemDS(admin);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            for(int i= 0;i<qladmin.DanhSach.Count();i++)
            if (txtUser.Text == qladmin.DanhSach[i].User.ToString())
            {
                    if (txtPassword.Text == qladmin.DanhSach[i].Password.ToString())
                    {
                        frmQLTK frm = new frmQLTK();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                        label3.Text = "Sai thông tin đăng nhập!";
            }
            label3.Text = "Sai thông tin đăng nhập!";
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
                txtPassword.PasswordChar = '\0';
            else
                txtPassword.PasswordChar = '*';
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            GetAdmin();
        }
    }
}
