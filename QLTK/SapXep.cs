using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTK
{
    class ListViewItemComparer : IComparer
    {
        private int col = 0;

        public ListViewItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }
    class ListViewItemDateTimeComparer : IComparer
    {
        private int col = 0;
        public ListViewItemDateTimeComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            int kq;
            try
            {
                System.DateTime firstDate =
                        DateTime.ParseExact(((ListViewItem)x).SubItems[col].Text, "dd/MM/yyyy", null);
                System.DateTime secondDate =
                        DateTime.ParseExact(((ListViewItem)y).SubItems[col].Text, "dd/MM/yyyy", null);
                kq = DateTime.Compare(firstDate, secondDate);
            }
            catch
            {
                kq = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                           ((ListViewItem)y).SubItems[col].Text);
            }
            return kq;
        }
    }
}

