using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTK
{
    public class LoaiTK
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public LoaiTK() { }
        public LoaiTK(string maloai,string tenloai)
        {
            MaLoai = maloai;
            TenLoai = tenloai;
        }
    }
    public class QLLoaiTK
    {
        public List<LoaiTK> DanhSach;
        public QLLoaiTK()
        {
            DanhSach = new List<LoaiTK>();
        }
        public QLLoaiTK(List<LoaiTK> ds)
        {
            DanhSach = ds;
        }
        public void ThemLoaiTK(LoaiTK loaitk)
        {
            DanhSach.Add(loaitk);
        }
        public bool KiemTra(string tenloai)
        {
            bool kq = false;
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select count(SoTK) from TaiKhoan,LaiSuat,LoaiTaiKhoan where TaiKhoan.MaLaiSuat=LaiSuat.MaLaiSuat and LaiSuat.MaLoai=LoaiTaiKhoan.MaLoai and TenLoai = N'" + tenloai + "'";
            read = cmd.ExecuteReader();
            int a = 0;
            while (read.Read())
                a =int.Parse(read.GetValue(0).ToString());
            if (a != 0)
                kq = true;
            cmd.Dispose();
            read.Close();
            conection.Close();
            return kq;
        }
    }
}
