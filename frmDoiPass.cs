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
    public partial class frmDoiPass : Form
    {
        private Admin admin;
        public frmDoiPass(Admin admin)
        {
            this.admin = admin;
            InitializeComponent();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (txtOld.PasswordChar == '*')
            {
                txtOld.PasswordChar = '\0';
            }
            else
            {
                txtOld.PasswordChar = '*';
            }
        }
        public void DoiPass()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "update Admin set Password ='"+txtRewrite.Text+"' where UserName ='"+admin.User+"'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Đổi mật khẩu thành công!!");
            else
                MessageBox.Show("Đổi mật khẩu thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtOld.Text==admin.Password)
            {
                if (txtNew.Text==txtRewrite.Text)
                {
                    DoiPass();
                    this.Close();
                }
                else
                    label3.Text = "Sai thông tin nhập!";
            }
            label3.Text = "Sai thông tin nhập!";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
