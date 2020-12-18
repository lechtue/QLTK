using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTK
{
    public class Log
    {
        public int STT { get; set; }
        public string User { get; set; }
        public DateTime TimeLog { get; set; }
        public Log() { }
        public Log(int stt,string user, DateTime timelog)
        {
            STT = stt;
            User = user;
            TimeLog = timelog;
        }
    }
    public class ListLog
    {
        public List<Log> List;
        public ListLog(List<Log> list)
        {
            List = list;
        }
        public ListLog()
        {
            List = new List<Log>();
        }
    }
}
