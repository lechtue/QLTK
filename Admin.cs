using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTK
{
    public class Admin
    {
        public string User { get; set; }
        public string Password { get; set; }
        public Admin()
        {}
        public Admin(string user,string pass)
        {
            User = user;
            Password = pass;
        }
    }
    public class QLAdmin
    {
        public List<Admin> DanhSach;
        public QLAdmin(List<Admin> ds)
        {
            DanhSach = ds;
        }
        public QLAdmin()
        {
            DanhSach = new List<Admin>();
        }
        public void ThemDS(Admin admin)
        {
            DanhSach.Add(admin);
        }
    }
}
