using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTK
{
    public class KhachHang
    {
        public string CMND { get; set; }
        public string TenKH { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public KhachHang() { }
        public KhachHang(string cmnd,string tenkh,DateTime ngaysinh, string diachi)
        {
            CMND = cmnd;
            TenKH = tenkh;
            NgaySinh = ngaysinh;
            DiaChi = diachi;
        }
    }
    public class QLKhachHang
    {
        public List<KhachHang> DanhSach;
        public QLKhachHang()
        {
            DanhSach = new List<KhachHang>();
        }
        public QLKhachHang(List<KhachHang> ds)
        {
            DanhSach = ds;
        }public void ThemDS(KhachHang kh)
        {
            DanhSach.Add(kh);
        }
        public bool SoSanhCMND(string cmnd)
        {
            bool kq=false;
            for(int i=0;i<DanhSach.Count;i++)
            {
                if (cmnd == DanhSach[i].CMND)
                    kq= true;
            }
            return kq;
        }
    }
}
