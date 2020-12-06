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
    public partial class frmKH : Form
    {
        private frmQLTK frm;
        public QLKhachHang qlkh;
        public frmKH(frmQLTK frmqltk,QLKhachHang ql)
        {
            frm = frmqltk;
            qlkh = ql;
            InitializeComponent();
            btnThem.Enabled = true;
        }
        public frmKH(string cmnd,frmQLTK frmqltk)
        {
            frm = frmqltk;
            InitializeComponent();
            btnSua.Enabled = true;
            mtxtCMND.Text = cmnd;
            mtxtCMND.ReadOnly = true;
        }
        public void ThemKH()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "set dateformat dmy; insert into KhachHang values('" + mtxtCMND.Text + "',N'" + txtKH.Text + "','" + dtpNgaySinh.Value.ToString("dd/MM/yyyy") + "',N'" + txtDiaChi.Text + "')";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Thêm khách hàng thành công!");
            else
                MessageBox.Show("Thêm khách hàng thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public void SuaKH()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "set dateformat dmy; update KhachHang set TenKH=N'" + txtKH.Text + "',NgaySinh='" + dtpNgaySinh.Value.ToString("dd/MM/yyyy") + "',DiaChi=N'" + txtDiaChi.Text + "' where CMND='" + mtxtCMND.Text + "'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Sửa thông tin thành công!");
            else
                MessageBox.Show("Sửa thông tin thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!qlkh.SoSanhCMND(mtxtCMND.Text))
            {
                ThemKH();
                this.Close();
                frm.GetKhachHang();
                frm.LoadDSKH();
            }
            else
            {
                MessageBox.Show("Số CMND đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SuaKH();
            this.Close();
            frm.GetKhachHang();
            frm.LoadDSKH();
        }
    }
}
