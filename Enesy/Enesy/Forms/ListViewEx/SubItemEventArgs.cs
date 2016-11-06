using System;
using System.Windows.Forms;

namespace Enesy.Form
{
    public class SubItemEventArgs : EventArgs
    {
        private int _subItemIndex = -1;

        private ListViewItem _item;

        public ListViewItem Item
        {
            get
            {
                return this._item;
            }
        }

        public int SubItem
        {
            get
            {
                return this._subItemIndex;
            }
        }

        public SubItemEventArgs(ListViewItem item, int subItem)
        {
            this._subItemIndex = subItem;
            this._item = item;
        }
    }
}