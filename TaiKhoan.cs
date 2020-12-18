using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTK
{
    public class TaiKhoan
    {
        public string SoTK { get; set; }
        public string CMND { get; set; }
        public string LoaiTK { get; set; }
        public string KyHan { get; set; }
        public float LaiSuat { get; set; }
        public double SoTien { get; set; }
        public DateTime NgayBatDau { get; set; }
        public TaiKhoan() { }
        public TaiKhoan(string sotk,string cmnd,string loaitk,string kyhan,float laisuat,double sotien, DateTime ngaybatdau)
        {
            SoTK = sotk;
            CMND = cmnd;
            LoaiTK = loaitk;
            KyHan = kyhan;
            LaiSuat = laisuat;
            SoTien = sotien;
            NgayBatDau = ngaybatdau;
        }
    }
    public class QLTaiKhoan
    {
        public List<TaiKhoan> DanhSach;
        public QLTaiKhoan()
        {
            DanhSach = new List<TaiKhoan>();
        }
        public QLTaiKhoan(List<TaiKhoan> ds)
        {
            DanhSach = ds;
        }
        public void ThemDS(TaiKhoan tk)
        {
            DanhSach.Add(tk);
        }
    }
}
