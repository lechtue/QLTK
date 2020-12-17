using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace QLTK
{
    public partial class frmQLTK : Form
    {
        private QLKhachHang qlkh;
        private QLTaiKhoan qltk;
        private string user;
        public frmQLTK(string user)
        {
            this.user = user;
            InitializeComponent();
        }
        public void GetLoaiTK()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select TenLoai from LoaiTaiKhoan";
            read = cmd.ExecuteReader();
            cbbTimLoai.Items.Clear();
            cbbType.Items.Clear();
            cbbTimLoai.Items.Add("Loại tài khoản");
            while (read.Read())
            {
                cbbType.Items.Add(read.GetValue(0).ToString());
                cbbTimLoai.Items.Add(read.GetValue(0).ToString());
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void GetLaiSuat()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select LaiSuat from LaiSuat,LoaiTaiKhoan where LaiSuat.MaLoai=LoaiTaiKhoan.MaLoai and TenLoai=N'" + cbbType.Text + "' and KyHan=N'" + cbbKyHan.Text + "' group by LaiSuat";
            read = cmd.ExecuteReader();
            while(read.Read())
                txtLaiSuat.Text = read.GetValue(0).ToString();
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void GetKhachHang()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select * from KhachHang";
            read = cmd.ExecuteReader();
            qlkh = new QLKhachHang();
            while (read.Read())
            {
                KhachHang kh = new KhachHang();
                kh.CMND = read.GetValue(0).ToString();
                kh.TenKH = read.GetValue(1).ToString();
                kh.NgaySinh = read.GetDateTime(2);
                kh.DiaChi = read.GetValue(3).ToString();
                qlkh.ThemDS(kh);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void GetTaiKhoan()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string cmnd = GetCMND();
            cmd.CommandText = "select SoTK,CMND,TenLoai,KyHan,LaiSuat,Sotien,NgayBatDau from TaiKhoan,LaiSuat,LoaiTaiKhoan where TaiKhoan.MaLaiSuat=LaiSuat.MaLaiSuat and LaiSuat.MaLoai=LoaiTaiKhoan.MaLoai and CMND='"+cmnd+"' group by SoTK,CMND,TenLoai,KyHan,LaiSuat,Sotien,NgayBatDau";
            read = cmd.ExecuteReader();
            qltk = new QLTaiKhoan();
            while (read.Read())
            {
                TaiKhoan tk = new TaiKhoan();
                tk.SoTK = read.GetValue(0).ToString();
                tk.CMND = read.GetValue(1).ToString();
                tk.LoaiTK = read.GetValue(2).ToString();
                tk.KyHan = read.GetValue(3).ToString();
                tk.LaiSuat = (float) read.GetDouble(4);
                tk.SoTien = read.GetDouble(5);
                tk.NgayBatDau = read.GetDateTime(6);
                qltk.ThemDS(tk);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void GetLVKH()
        {
            for (int i = 0; i < qlkh.DanhSach.Count; i++)
            {
                ListViewItem lvitem = new ListViewItem(qlkh.DanhSach[i].CMND);
                lvitem.SubItems.Add(qlkh.DanhSach[i].TenKH);
                lvitem.SubItems.Add(qlkh.DanhSach[i].NgaySinh.ToString("dd/MM/yyyy"));
                lvitem.SubItems.Add(qlkh.DanhSach[i].DiaChi);
                lvKH.Items.Add(lvitem);
            }
        }
        public void GetLVTK()
        {
            for (int i = 0; i < qltk.DanhSach.Count; i++)
            {
                ListViewItem lvitem = new ListViewItem(qltk.DanhSach[i].SoTK);
                lvitem.SubItems.Add(qltk.DanhSach[i].LoaiTK);
                lvitem.SubItems.Add(qltk.DanhSach[i].LaiSuat.ToString());
                lvitem.SubItems.Add(qltk.DanhSach[i].KyHan);
                lvTK.Items.Add(lvitem);
            }
        }
        public void MacDinh()
        {
            cbbTimLoai.Text = cbbTimLoai.Items[0].ToString();
            cbbType.Text = cbbType.Items[0].ToString();
            cbbKyHan.Text = cbbKyHan.Items[0].ToString();
            cbbTimKH.Text = cbbTimKH.Items[0].ToString();
            txtTien.Text = "";
            dtpNgayBatDau.Value = DateTime.Today;
        }
        public void LoadDSKH()
        {
            lvKH.Items.Clear();
            GetLVKH();
            toolStripStatusLabel3.Text = "Có " + lvKH.Items.Count.ToString() + " khách hàng";
        }
        public void LoadDSTK()
        {
            lvTK.Items.Clear();
            GetLVTK();
        }
        public string GetCMND()
        {
            string cmnd = "";
            for (int i = 0; i < qlkh.DanhSach.Count; i++)
            {
                if (lvKH.Items[i].Selected)
                {
                    cmnd=qlkh.DanhSach[i].CMND;
                }
            }
            return cmnd;
        }
        public void XoaKH()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string cmnd = GetCMND();
            cmd.CommandText = "delete TaiKhoan where CMND='" + cmnd + "'; delete KhachHang where CMND='" + cmnd + "'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Xoá khách hàng thành công!");
            else
                MessageBox.Show("Xoá khách hàng thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public void SearchTenKH()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select * from KhachHang where TenKH like N'%" + txtTimKH.Text + "%'";
            read = cmd.ExecuteReader();
            qlkh = new QLKhachHang();
            while (read.Read())
            {
                KhachHang kh = new KhachHang();
                kh.CMND = read.GetValue(0).ToString();
                kh.TenKH = read.GetValue(1).ToString();
                kh.NgaySinh = read.GetDateTime(2);
                kh.DiaChi = read.GetValue(3).ToString();
                qlkh.ThemDS(kh);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void SearchCMND()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select * from KhachHang where CMND like N'%" + txtTimKH.Text + "%'";
            read = cmd.ExecuteReader();
            qlkh = new QLKhachHang();
            while (read.Read())
            {
                KhachHang kh = new KhachHang();
                kh.CMND = read.GetValue(0).ToString();
                kh.TenKH = read.GetValue(1).ToString();
                kh.NgaySinh = read.GetDateTime(2);
                kh.DiaChi = read.GetValue(3).ToString();
                qlkh.ThemDS(kh);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void GetThongTinTK()
        {
            for (int i = 0; i < qltk.DanhSach.Count; i++)
            {
                if (lvTK.Items[i].Selected)
                {

                    int j;
                    txtCMND.Text = qltk.DanhSach[i].CMND;
                    txtTien.Text = qltk.DanhSach[i].SoTien.ToString();
                    txtLaiSuat.Text = qltk.DanhSach[i].LaiSuat.ToString();
                    for (j = 0; j < cbbType.Items.Count; j++)
                    {
                        if (cbbType.Items[j].ToString() == qltk.DanhSach[i].LoaiTK)
                            cbbType.Text = cbbType.Items[j].ToString();
                    }
                    for (j = 0; j < cbbKyHan.Items.Count; j++)
                    {
                        if (cbbKyHan.Items[j].ToString() == qltk.DanhSach[i].KyHan)
                            cbbKyHan.Text = cbbKyHan.Items[j].ToString();
                    }
                    dtpNgayBatDau.Value = qltk.DanhSach[i].NgayBatDau;
                }
            }
        }
        public void ThemTK()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string malaisuat = GetMaLaiSuat(), sotien = GetSoTien();
            cmd.CommandText = "set dateformat dmy; insert into TaiKhoan values('" + malaisuat + "','"+txtCMND.Text+"','"+sotien+"','"+dtpNgayBatDau.Value.ToString("dd/MM/yyyy")+"')";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Thêm tài khoản thành công!");
            else
                MessageBox.Show("Thêm tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public string GetMaLaiSuat()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select MaLaiSuat from LaiSuat,LoaiTaiKhoan where LaiSuat.MaLoai=LoaiTaiKhoan.MaLoai and TenLoai=N'" + cbbType.Text + "' and KyHan=N'" + cbbKyHan.Text + "' group by MaLaiSuat";
            read = cmd.ExecuteReader();
            string malaisuat = "";
            while (read.Read())
                malaisuat = read.GetValue(0).ToString();
            cmd.Dispose();
            conection.Close();
            return malaisuat;
        }
        public string GetSoTien()
        {
            string sotien = "";
            sotien = txtTien.Text.Replace(",", "");
            return sotien;
        }
        public void CapNhatTK()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string malaisuat = GetMaLaiSuat(), sotien = GetSoTien(), sotk = GetSoTK();
            cmd.CommandText = "set dateformat dmy; update TaiKhoan set MaLaiSuat='"+malaisuat+"',Sotien='"+sotien+"',NgayBatDau='"+dtpNgayBatDau.Value.ToString("dd/MM/yyyy")+"' where SoTK='"+sotk+"'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Cập nhật tài khoản thành công!");
            else
                MessageBox.Show("Cập nhật tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }
        public void XoaTK()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string sotk = GetSoTK();
            cmd.CommandText = "delete from TaiKhoan  where SoTK='" + sotk + "'";
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                MessageBox.Show("Xoá tài khoản thành công!");
            else
                MessageBox.Show("Xoá tài khoản thất bại!");
            cmd.Dispose();
            conection.Close();
        }

        public string GetSoTK()
        {
            string stk="";
            for(int i=0;i<qltk.DanhSach.Count;i++)
            {
                if(lvTK.Items[i].Selected)
                {
                    stk = qltk.DanhSach[i].SoTK;
                }
            }
            return stk;
        }
        public void FilterLoaiTaiKhoan()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            string cmnd = GetCMND();
            cmd.CommandText = "select SoTK,CMND,TenLoai,KyHan,LaiSuat,Sotien,NgayBatDau from TaiKhoan,LaiSuat,LoaiTaiKhoan where TaiKhoan.MaLaiSuat=LaiSuat.MaLaiSuat and LaiSuat.MaLoai=LoaiTaiKhoan.MaLoai and CMND='" + cmnd + "' and TenLoai=N'"+cbbTimLoai.Text+"' group by SoTK,CMND,TenLoai,KyHan,LaiSuat,Sotien,NgayBatDau";
            read = cmd.ExecuteReader();
            qltk = new QLTaiKhoan();
            while (read.Read())
            {
                TaiKhoan tk = new TaiKhoan();
                tk.SoTK = read.GetValue(0).ToString();
                tk.CMND = read.GetValue(1).ToString();
                tk.LoaiTK = read.GetValue(2).ToString();
                tk.KyHan = read.GetValue(3).ToString();
                tk.LaiSuat = (float)read.GetDouble(4);
                tk.SoTien = read.GetDouble(5);
                tk.NgayBatDau = read.GetDateTime(6);
                qltk.ThemDS(tk);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        private void frmQLTK_Load(object sender, EventArgs e)
        {
            GetLoaiTK();
            GetKhachHang();
            LoadDSKH();
            MacDinh();
            toolStripStatusLabel2.Text = "Có " + lvTK.Items.Count.ToString() + " tài khoản";
            this.Text = "Xin chào: " + user;
        }

        private void bảngLãiSuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLaiSuat laisuat = new frmLaiSuat(this);
            laisuat.ShowDialog();
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            frmKH khachhang = new frmKH(this,qlkh);
            khachhang.ShowDialog();
        }

        private void cbbTimKH_TextChanged(object sender, EventArgs e)
        {
            if (cbbTimKH.Text != "-----")
            {
                btnTimKH.Enabled = true;
                txtTimKH.Enabled = true;
            }
            else
            {
                btnTimKH.Enabled = false;
                txtTimKH.Enabled = false;
                GetKhachHang();
                LoadDSKH();
            }
        }

        private void thoátToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var kq = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
                Application.Exit();
        }

        private void btnChiTietTK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < qltk.DanhSach.Count; i++)
            {
                if (lvTK.Items[i].Selected)
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk = qltk.DanhSach[i];
                    frmChiTiet chitiet = new frmChiTiet(tk);
                    chitiet.ShowDialog();
                }
            }
        }

        private void txtTien_TextChanged(object sender, EventArgs e)
        {
            if (txtTien.Text != "")
            {
                txtTien.Text = string.Format("{0:#,###}", double.Parse(txtTien.Text));
            }
        }

        private void lvKH_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != 2)
                this.lvKH.ListViewItemSorter = new ListViewItemComparer(e.Column);
            else
                this.lvKH.ListViewItemSorter = new ListViewItemDateTimeComparer(2);
        }

        private void lvTK_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lvTK.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void lvKH_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvKH.SelectedItems.Count != 0)
            {
                btnSuaKH.Enabled = true;
                btnXoaKH.Enabled = true;
                btnThemTK.Enabled = true;
                groupBox1.Enabled = true;
                if (cbbTimLoai.Text != cbbTimLoai.Items[0].ToString())
                    FilterLoaiTaiKhoan();
                else
                    GetTaiKhoan();
                LoadDSTK();
                LoadDSTK();
                MacDinh();
                txtCMND.Text = GetCMND();
            }
            else
            {
                btnSuaKH.Enabled = false;
                btnXoaKH.Enabled = false;
                btnThemTK.Enabled = false;
                groupBox1.Enabled = false;
                txtCMND.Text = "";
                MacDinh();
                lvTK.Items.Clear();
            }
            toolStripStatusLabel2.Text = "Có " + lvTK.Items.Count.ToString() + " tài khoản";
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < qlkh.DanhSach.Count; i++)
            {
                if (lvKH.Items[i].Selected)
                {
                    frmKH khachhang = new frmKH(qlkh.DanhSach[i].CMND,this);
                    khachhang.ShowDialog();
                }
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Thao tác sẽ xoá hết thông tin khách hàng và tài khoản của khách hàng.\nBạn có muốn xoá khách hàng này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (kq == DialogResult.Yes)
            {
                XoaKH();
                GetKhachHang();
                LoadDSKH();
                lvTK.Items.Clear();
            }
        }
        private void btnTimKH_Click(object sender, EventArgs e)
        {
            if(cbbTimKH.Text==cbbTimKH.Items[1].ToString())
            {
                SearchTenKH();
                LoadDSKH();
            }
            else
            {
                SearchCMND();
                LoadDSKH();
            }
        }
        private void thêmToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            btnThemKH_Click(sender, e);
        }
        private void sửaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            btnSuaKH_Click(sender, e);
        }

        private void xoáToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            btnXoaKH_Click(sender, e);
        }
        private void lvTK_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvTK.SelectedItems.Count != 0)
            {
                btnChiTietTK.Enabled = true;
                btnSuaTK.Enabled = true;
                btnXoaTK.Enabled = true;
                GetThongTinTK();
            }
            else
            {
                btnChiTietTK.Enabled = false;
                btnSuaTK.Enabled = false;
                btnXoaTK.Enabled = false;
                MacDinh();
            }
        }
        private void cbbKyHan_TextChanged(object sender, EventArgs e)
        {
            GetLaiSuat();
        }

        private void cbbType_TextChanged(object sender, EventArgs e)
        {
            GetLaiSuat();
        }
        private void btnThemTK_Click(object sender, EventArgs e)
        {
            ThemTK();
            GetTaiKhoan();
            LoadDSTK();
            MacDinh();
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            CapNhatTK();
            GetTaiKhoan();
            LoadDSTK();
            MacDinh();
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Thao tác sẽ xoá tài khoản của khách hàng.\nBạn có muốn xoá tài khoản này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (kq == DialogResult.Yes)
            {
                XoaTK();
                GetTaiKhoan();
                LoadDSTK();
                MacDinh();
            }
        }

        private void cbbTimLoai_TextChanged(object sender, EventArgs e)
        {
            if (cbbTimLoai.Text != cbbTimLoai.Items[0].ToString())
                FilterLoaiTaiKhoan();
            else
                GetTaiKhoan();
            LoadDSTK();
        }

        private void thêmToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            btnThemTK_Click(sender, e);
        }

        private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSuaTK_Click(sender, e);
        }

        private void xoáToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            btnXoaTK_Click(sender, e);
        }

        private void bàngLãiSuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bảngLãiSuấtToolStripMenuItem_Click(sender, e);
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLog frm = new frmLog();
            frm.ShowDialog();
        }
    }
}
