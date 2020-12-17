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
    public partial class frmLog : Form
    {
        private ListLog List;
        public frmLog()
        {
            InitializeComponent();
        }

        public void GetLog()
        {
            string sql = @"server=DESKTOP-3PAL4OL\SQL; database=QLTK ; Integrated Security=true;";
            SqlConnection conection = new SqlConnection(sql);
            SqlDataReader read;
            SqlCommand cmd = conection.CreateCommand();
            conection.Open();
            cmd.CommandText = "select * from DangNhap";
            read = cmd.ExecuteReader();
            List = new ListLog();
            while (read.Read())
            {
                Log log = new Log();
                log.STT = int.Parse(read.GetValue(0).ToString());
                log.User = read.GetValue(1).ToString();
                log.TimeLog = read.GetDateTime(2);
                List.List.Add(log);
            }
            cmd.Dispose();
            read.Close();
            conection.Close();
        }
        public void LoadLv()
        {
            for(int i=0;i<List.List.Count();i++)
            {
                ListViewItem item = new ListViewItem(List.List[i].STT.ToString());
                item.SubItems.Add(List.List[i].User.ToString());
                item.SubItems.Add(List.List[i].TimeLog.ToString("dd/MM/yyyy hh:mm:ss tt"));
                lvLog.Items.Add(item);
            }
        }
        private void frmLog_Load(object sender, EventArgs e)
        {
            GetLog();
            LoadLv();
        }
    }
}
