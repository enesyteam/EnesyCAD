using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Enesy.Form
{
    public class ListViewEx : ListView
    {
        private const int LVM_FIRST = 4096;

        private const int LVM_GETCOLUMNORDERARRAY = 4155;

        protected const int WM_HSCROLL = 276;

        protected const int WM_VSCROLL = 277;

        protected const int WM_SIZE = 5;

        protected const int WM_NOTIFY = 78;

        private const int HDN_FIRST = -300;

        private const int HDN_BEGINDRAG = -310;

        private const int HDN_ITEMCHANGINGA = -300;

        private const int HDN_ITEMCHANGINGW = -320;

        private Container components;

        private bool _doubleClickActivation;

        protected Control _editingControl;

        protected ListViewItem _editItem;

        protected int _editSubItem;

        public bool DoubleClickActivation
        {
            get
            {
                return this._doubleClickActivation;
            }
            set
            {
                this._doubleClickActivation = value;
            }
        }

        public ListViewEx()
        {
            this.InitializeComponent();
            base.FullRowSelect = true;
            base.View = View.Details;
            base.AllowColumnReorder = true;
        }

        protected void _editControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (keyChar == '\r')
            {
                this.EndEditing(true);
                return;
            }
            if (keyChar != '\u001B')
            {
                return;
            }
            this.EndEditing(false);
        }

        protected void _editControl_Leave(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EditSubitemAt(Point p)
        {
            ListViewItem listViewItem;
            int subItemAt = this.GetSubItemAt(p.X, p.Y, out listViewItem);
            if (subItemAt >= 0)
            {
                this.OnSubItemClicked(new SubItemEventArgs(listViewItem, subItemAt));
            }
        }

        public virtual void EndEditing(bool AcceptChanges)
        {
            if (this._editingControl == null)
            {
                return;
            }
            SubItemEndEditingEventArgs subItemEndEditingEventArg = new SubItemEndEditingEventArgs(this._editItem, this._editSubItem, (AcceptChanges ? this._editingControl.Text : this._editItem.SubItems[this._editSubItem].Text), !AcceptChanges);
            this.OnSubItemEndEditing(subItemEndEditingEventArg);
            if (subItemEndEditingEventArg.RetainEditor)
            {
                this._editingControl.Focus();
                return;
            }
            this._editItem.SubItems[this._editSubItem].Text = subItemEndEditingEventArg.DisplayText;
            this._editingControl.Leave -= new EventHandler(this._editControl_Leave);
            this._editingControl.KeyPress -= new KeyPressEventHandler(this._editControl_KeyPress);
            this._editingControl.SendToBack();
            this._editingControl.Visible = false;
            this._editingControl = null;
            this._editItem = null;
            this._editSubItem = -1;
        }

        public int[] GetColumnOrder()
        {
            IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * base.Columns.Count);
            if (ListViewEx.SendMessage(base.Handle, 4155, new IntPtr(base.Columns.Count), intPtr).ToInt32() == 0)
            {
                Marshal.FreeHGlobal(intPtr);
                return null;
            }
            int[] numArray = new int[base.Columns.Count];
            Marshal.Copy(intPtr, numArray, 0, base.Columns.Count);
            Marshal.FreeHGlobal(intPtr);
            return numArray;
        }

        public int GetSubItemAt(int x, int y, out ListViewItem item)
        {
            item = base.GetItemAt(x, y);
            if (item != null)
            {
                int[] columnOrder = this.GetColumnOrder();
                int left = item.GetBounds(ItemBoundsPortion.Entire).Left;
                for (int i = 0; i < (int)columnOrder.Length; i++)
                {
                    ColumnHeader columnHeader = base.Columns[columnOrder[i]];
                    if (x < left + columnHeader.Width)
                    {
                        return columnHeader.Index;
                    }
                    left = left + columnHeader.Width;
                }
            }
            return -1;
        }

        public Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
        {
            int i;
            int[] columnOrder = this.GetColumnOrder();
            Rectangle empty = Rectangle.Empty;
            if (SubItem >= (int)columnOrder.Length)
            {
                throw new IndexOutOfRangeException(string.Concat("SubItem ", SubItem, " out of range"));
            }
            if (Item == null)
            {
                throw new ArgumentNullException("Item");
            }
            Rectangle bounds = Item.GetBounds(ItemBoundsPortion.Entire);
            int left = bounds.Left;
            for (i = 0; i < (int)columnOrder.Length; i++)
            {
                ColumnHeader item = base.Columns[columnOrder[i]];
                if (item.Index == SubItem)
                {
                    break;
                }
                left = left + item.Width;
            }
            empty = new Rectangle(left, bounds.Top, base.Columns[columnOrder[i]].Width, bounds.Height);
            return empty;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (!this.DoubleClickActivation)
            {
                return;
            }
            this.EditSubitemAt(base.PointToClient(Cursor.Position));
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (base.GetItemAt(e.X, e.Y) == this._editItem && this._editingControl != null)
            {
                this._editingControl.Focus();
                if (this._editingControl is TextBox)
                {
                    ((TextBox)this._editingControl).SelectionStart = this._editingControl.Text.Length;
                    ((TextBox)this._editingControl).SelectionLength = this._editingControl.Text.Length;
                }
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            this.EndEditing(true);
            if (base.SelectedIndices.Count == 0)
            {
                return;
            }
            ListViewItem item = base.SelectedItems[0];
            if (item == null)
            {
                return;
            }
            this.OnSubItemClicked(new SubItemEventArgs(item, 1));
        }

        protected void OnSubItemBeginEditing(SubItemEventArgs e)
        {
            if (this.SubItemBeginEditing != null)
            {
                this.SubItemBeginEditing(this, e);
            }
        }

        protected void OnSubItemClicked(SubItemEventArgs e)
        {
            if (this.SubItemClicked != null)
            {
                this.SubItemClicked(this, e);
            }
        }

        protected void OnSubItemEndEditing(SubItemEndEditingEventArgs e)
        {
            if (this.SubItemEndEditing != null)
            {
                this.SubItemEndEditing(this, e);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wPar, IntPtr lPar);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int len, ref int[] order);

        public virtual void StartEditing(Control c, ListViewItem Item, int SubItem)
        {
            this.OnSubItemBeginEditing(new SubItemEventArgs(Item, SubItem));
            if (this._editingControl != null && this._editingControl != c)
            {
                this._editingControl.Leave -= new EventHandler(this._editControl_Leave);
                this._editingControl.KeyPress -= new KeyPressEventHandler(this._editControl_KeyPress);
                this._editingControl.SendToBack();
                this._editingControl.Visible = false;
                this._editingControl = null;
                this._editItem = null;
                this._editSubItem = -1;
            }
            Rectangle subItemBounds = this.GetSubItemBounds(Item, SubItem);
            if (subItemBounds.X < 0)
            {
                subItemBounds.Width = subItemBounds.Width + subItemBounds.X;
                subItemBounds.X = 0;
            }
            if (subItemBounds.X + subItemBounds.Width > base.Width)
            {
                subItemBounds.Width = base.Width - subItemBounds.Left;
            }
            subItemBounds.Offset(base.Left, base.Top);
            Point point = new Point(0, 0);
            Point screen = base.Parent.PointToScreen(point);
            Point screen1 = c.Parent.PointToScreen(point);
            subItemBounds.Offset(screen.X - screen1.X, screen.Y - screen1.Y);
            if (c is TextBox)
            {
                subItemBounds.Offset(1, 0);
                subItemBounds.Width = subItemBounds.Width - 1;
                subItemBounds.Height = subItemBounds.Height - 1;
            }
            else if (c is ComboBox)
            {
                subItemBounds.Offset(0, -1);
            }
            c.Bounds = subItemBounds;
            if (!(c is Button))
            {
                c.Text = Item.SubItems[SubItem].Text;
            }
            else
            {
                //c.Left = c.Right - GroupControl.GROUP_BUTTON_SIZE;
                //c.Width = GroupControl.GROUP_BUTTON_SIZE;
                //Control height = c;
                //height.Height = height.Height - 1;
            }
            c.Visible = true;
            c.BringToFront();
            c.Focus();
            this._editingControl = c;
            this._editingControl.Leave += new EventHandler(this._editControl_Leave);
            this._editingControl.KeyPress += new KeyPressEventHandler(this._editControl_KeyPress);
            this._editItem = Item;
            this._editSubItem = SubItem;
        }

        protected override void WndProc(ref Message msg)
        {
            int num = msg.Msg;
            if (num != 5)
            {
                if (num != 78)
                {
                    switch (num)
                    {
                        case 276:
                        case 277:
                            {
                                break;
                            }
                        default:
                            {
                                base.WndProc(ref msg);
                                return;
                            }
                    }
                }
                else
                {
                    base.WndProc(ref msg);
                    return;
                }
            }
            this.EndEditing(false);
            base.WndProc(ref msg);
        }

        public event SubItemEventHandler SubItemBeginEditing;

        public event SubItemEventHandler SubItemClicked;

        public event SubItemEndEditingEventHandler SubItemEndEditing;

        private struct NMHDR
        {
            public IntPtr hwndFrom;

            public int idFrom;

            public int code;
        }
    }
}