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
    public partial class frmChiTiet : Form
    {
        public TaiKhoan TK;
        public frmChiTiet(TaiKhoan tk)
        {
            TK = tk;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void GetLaiSuat()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select LaiSuat from LaiSuat,LoaiTaiKhoan where LaiSuat.MaLoai=LoaiTaiKhoan.MaLoai and TenLoai=N'" + TK.LoaiTK + "' and KyHan=N'" + TK.KyHan + "' group by LaiSuat";
            read = cmd.ExecuteReader();
            while (read.Read())
                txtLaiSuat.Text = read.GetValue(0).ToString();
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void NoLai()
        {
            double kq = TK.SoTien * double.Parse(txtLaiSuat.Text) / 100 / 12 * int.Parse(TK.KyHan.Replace(" Tháng", ""));
            txtNoLai.Text = string.Format("{0:#,###.###}", kq);
        }
        private void frmChiTiet_Load(object sender, EventArgs e)
        {
            GetLaiSuat();
            txtSoTien.Text = string.Format("{0:#,###.###}", TK.SoTien);
            txtKyHan.Text = TK.KyHan;
            txtLoaiTK.Text = TK.LoaiTK;
            txtSoTK.Text = TK.SoTK;
            txtNgayBatDau.Text = TK.NgayBatDau.ToString("dd/MM/yyyy");
            txtNgayKetThuc.Text = TK.NgayBatDau.AddMonths(int.Parse(TK.KyHan.Replace(" Tháng", ""))).ToString("dd/MM/yyyy");
            NoLai();
        }
    }
}
