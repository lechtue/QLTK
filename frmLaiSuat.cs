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
    public partial class frmLaiSuat : Form
    {
        public frmQLTK frm;
        public QLLoaiTK qlLoaitk;
        public frmLaiSuat(frmQLTK frmql)
        {
            frm = frmql;
            InitializeComponent();
        }
        public void GetLoaiTK()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select * from LoaiTaiKhoan";
            read = cmd.ExecuteReader();
            qlLoaitk = new QLLoaiTK();
            while (read.Read())
            {
                LoaiTK loaitk = new LoaiTK();
                loaitk.MaLoai = read.GetValue(0).ToString(); 
                loaitk.TenLoai= read.GetValue(1).ToString();
                qlLoaitk.ThemLoaiTK(loaitk);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void ThemLoai()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "insert into LoaiTaiKhoan values(N'" + txtLoaiTK.Text + "')";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                ThemLaiSuat();
            else
                MessageBox.Show("Thêm Loại tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public string GetMaLoai()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string tenloai = GetTenLoaiTK();
            cmd.CommandText = "select MaLoai from LoaiTaiKhoan where TenLoai=N'"+txtLoaiTK.Text+"'";
            read = cmd.ExecuteReader();
            string maloai = "";
            while (read.Read())
            {
                maloai = read.GetValue(0).ToString();
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
            return maloai;
        }
        public void ThemLaiSuat()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string maloai = GetMaLoai();
            cmd.CommandText = "insert into LaiSuat values(N'1 Tháng','"+maloai+"','"+txtLS1Thang.Text+ "');insert into LaiSuat values(N'6 Tháng','" + maloai + "','" + txtLS6Thang.Text + "');insert into LaiSuat values(N'12 Tháng','" + maloai + "','" + txtLS12Thang.Text + "')";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Thêm Loại tài khoản thành công!");
            else
                MessageBox.Show("Thêm Loại tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public void GetLVLoaiTK()
        {
            for(int i=0;i<qlLoaitk.DanhSach.Count;i++)
            {
                ListViewItem lvitem = new ListViewItem(qlLoaitk.DanhSach[i].TenLoai);
                lvLoaiTK.Items.Add(lvitem);
            }
        }
        public string GetTenLoaiTK()
        {
            string TenLoai = "";
            for (int i = 0; i < lvLoaiTK.Items.Count; i++)
            {
                if (lvLoaiTK.Items[i].Selected)
                {
                    TenLoai = qlLoaitk.DanhSach[i].TenLoai;
                }
            }
            return TenLoai;
        }
        public void GetLaiSuat()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string tenloai = GetTenLoaiTK();
            cmd.CommandText = "select LaiSuat from LoaiTaiKhoan,LaiSuat where LoaiTaiKhoan.MaLoai=LaiSuat.MaLoai and TenLoai=N'"+tenloai+"'";
            read = cmd.ExecuteReader();
            double a;
            List<double> ds=new List<double>();
            while (read.Read())
            {
                a = read.GetDouble(0);
                ds.Add(a);
            }
            txtLoaiTK.Text = tenloai;
            txtLS1Thang.Text = ds[0].ToString();
            txtLS6Thang.Text = ds[1].ToString();
            txtLS12Thang.Text = ds[2].ToString();
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void SuaLoai()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string tencu=GetTenLoaiTK();
            cmd.CommandText = "update LoaiTaiKhoan set TenLoai=N'"+txtLoaiTK.Text+ "' where TenLoai=N'"+tencu+"'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                SuaLaiSuat();
            else
                MessageBox.Show("Sửa Loại tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public void SuaLaiSuat()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string maloai = GetMaLoai();
            cmd.CommandText = "update LaiSuat set LaiSuat="+txtLS1Thang.Text+" where MaLoai="+maloai+ " and KyHan=N'1 Tháng';update LaiSuat set LaiSuat=" + txtLS6Thang.Text + " where MaLoai=" + maloai + " and KyHan=N'6 Tháng';update LaiSuat set LaiSuat=" + txtLS12Thang.Text + " where MaLoai=" + maloai + " and KyHan=N'12 Tháng'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Sửa Loại tài khoản thành công!");
            else
                MessageBox.Show("Sửa Loại tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public void LoadLV()
        {
            lvLoaiTK.Items.Clear();
            GetLoaiTK();
            GetLVLoaiTK();
        }
        public void Xoa()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string tenloai = GetTenLoaiTK();
            cmd.CommandText = "delete LaiSuat from LoaiTaiKhoan,LaiSuat where LoaiTaiKhoan.MaLoai=LaiSuat.MaLoai and TenLoai=N'" + tenloai + "'; delete LoaiTaiKhoan where TenLoai=N'" + tenloai+"'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Xoá loại tài khoản thành công!");
            else
                MessageBox.Show("Xoá loại tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }

        private void frmLaiSuat_Load(object sender, EventArgs e)
        {
            LoadLV();
        }

        private void lvLoaiTK_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvLoaiTK.SelectedItems.Count != 0)
            {
                GetLaiSuat();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!qlLoaitk.KiemTra(GetTenLoaiTK()))
            {
                DialogResult kq=MessageBox.Show("Bạn có thật sự muốn xoá Loại tài khoản này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (kq == DialogResult.Yes)
                {
                    Xoa();
                    LoadLV();
                    frm.GetLoaiTK();
                }
            }
            else
            {
                MessageBox.Show("Vẫn còn tài khoản thuộc loại tài khoản này tồn tại!\nKhông thể xoá loại tài khoản này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemLoai();
            LoadLV();
            frm.GetLoaiTK();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SuaLoai();
            LoadLV();
            frm.GetLoaiTK();
        }

        private void lvLoaiTK_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lvLoaiTK.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
    }
}
