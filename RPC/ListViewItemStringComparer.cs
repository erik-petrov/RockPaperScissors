using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPC
{
    class ListViewItemStringComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemStringComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public ListViewItemStringComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            
            int returnVal = -1;
            returnVal = Decimal.Compare(Convert.ToDecimal(((ListViewItem)x).SubItems[col].Text.TrimEnd('%')),
                                       Convert.ToDecimal(((ListViewItem)y).SubItems[col].Text.TrimEnd('%')));

            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;

            return returnVal;
        }
    }
}
