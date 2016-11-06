using Autodesk.AutoCAD.Internal;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class CommandListView : ListView
    {
        private ToolTip mToolTip;

        private ListViewItem mCurrentListViewItem;

        private bool mbResultSelected;

        private static int ITEMHEIGHT;

        private ColorDialog mColorDialog;

        private Color mAnswerColor = Color.Red;

        private bool mbShiftOrCtrlKeyPressed;

        private int mWidthAfterResize;

        private int mItemHeight;

        public Color AnswerColor
        {
            get
            {
                return this.mAnswerColor;
            }
            set
            {
                this.mAnswerColor = value;
                IEnumerator enumerator = base.Items.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    IEnumerator enumerator1 = ((ListViewItem)enumerator.Current).SubItems.GetEnumerator();
                    for (int i = 0; enumerator1.MoveNext() && i < 2; i++)
                    {
                        if (i == 1)
                        {
                            ((ListViewItem.ListViewSubItem)enumerator1.Current).ForeColor = this.mAnswerColor;
                        }
                    }
                }
            }
        }

        public CMNControl CMNControl
        {
            get
            {
                if (base.Parent == null || base.Parent.Parent == null)
                {
                    return null;
                }
                return (CMNControl)base.Parent.Parent;
            }
        }

        public Color CommandItemColor
        {
            get
            {
                return this.ForeColor;
            }
            set
            {
                this.ForeColor = value;
            }
        }

        static CommandListView()
        {
            CommandListView.ITEMHEIGHT = 20;
        }

        public CommandListView()
        {
        }

        
        public void ClearCommandList()
        {
            base.Items.Clear();
            this.mToolTip.RemoveAll();
            this.UpdateScrollBar();
        }

        private void CreateContextMenu()
        {
            this.ContextMenu = new System.Windows.Forms.ContextMenu();
            this.ContextMenu.MenuItems.Add(0, new MenuItem(StringResources.ResourceManager.GetString("CommandExcute"),
                /*new EventHandler(this.SetExpressionFontColor)*/ new EventHandler(this.ExcuteCurrentCommand)));
            this.ContextMenu.MenuItems.Add(1, new MenuItem("ccc", new EventHandler(this.SetValueFontColor)));
            this.ContextMenu.MenuItems.Add("-");
            this.ContextMenu.MenuItems.Add(3, new MenuItem("Copy", new EventHandler(this.MenuCopy)));
            this.ContextMenu.MenuItems.Add("-");
            this.ContextMenu.MenuItems.Add("-");
            this.ContextMenu.MenuItems.Add(8, new MenuItem("Clear", new EventHandler(this.HandleClearHistory)));
            if (this.CMNControl != null && this.CMNControl.Host.GetType() == typeof(cmnESW))
            {
                this.ContextMenu.MenuItems.Add(9, new MenuItem("XXX", new EventHandler(this.PasteToCommandLine)));
            }
        }

        private void ExcuteCurrentCommand(object sender, EventArgs e)
        {
            ExcuteCurrentCommand();
        }

        public void ExpressionDoubleClicked(object sender, EventArgs e)
        {
            ExcuteCurrentCommand();
        }
        public void ExcuteCurrentCommand()
        {
            if (this.mCurrentListViewItem != null)
            {
                var row = this.mCurrentListViewItem.Tag as DataRow;
                Autodesk.AutoCAD.Internal.Utils.WriteToCommandLine(row[0].ToString() + " ");
            }
        }

        private void HandleClearHistory(object sender, EventArgs e)
        {
           // this.CalcControl.ClearHistory();
        }

        protected void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.Shift)
            {
                this.mbShiftOrCtrlKeyPressed = true;
                e.Handled = true;
            }
        }

        protected void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control || e.Shift)
            {
                this.mbShiftOrCtrlKeyPressed = false;
                e.Handled = true;
            }
        }

        public void HandleMouseDown(object sender, MouseEventArgs e)
        {
            IEnumerator enumerator;
            try
            {
                this.mCurrentListViewItem = base.GetItemAt(e.X, e.Y);
                if (this.mCurrentListViewItem == null || e.X >= (this.mCurrentListViewItem.Bounds.Right - this.mCurrentListViewItem.Bounds.Left) / 2)
                {
                    this.mbResultSelected = true;
                }
                else
                {
                    this.mbResultSelected = false;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (this.ContextMenu == null)
                    {
                        this.CreateContextMenu();
                    }
                    if (!this.mbShiftOrCtrlKeyPressed)
                    {
                        if (base.SelectedItems.Count > 0)
                        {
                            base.BeginUpdate();
                            enumerator = base.SelectedItems.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                ((ListViewItem)enumerator.Current).Selected = false;
                            }
                            base.EndUpdate();
                        }
                        if (this.mCurrentListViewItem != null)
                        {
                            this.mCurrentListViewItem.Selected = true;
                        }
                    }
                    if (base.SelectedItems.Count > 1 || base.SelectedItems.Count == 0)
                    {
                        int num = 0;
                        enumerator = this.ContextMenu.MenuItems.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            if (num == 5 || num == 6 || num == 9)
                            {
                                ((MenuItem)enumerator.Current).Enabled = false;
                            }
                            if (num == 3)
                            {
                                if (base.SelectedItems.Count != 0)
                                {
                                    ((MenuItem)enumerator.Current).Enabled = true;
                                }
                                else
                                {
                                    ((MenuItem)enumerator.Current).Enabled = false;
                                }
                            }
                            if (num == 8)
                            {
                                if (base.Items.Count <= 0)
                                {
                                    ((MenuItem)enumerator.Current).Enabled = false;
                                }
                                else
                                {
                                    ((MenuItem)enumerator.Current).Enabled = true;
                                }
                            }
                            num++;
                        }
                    }
                    else if (base.SelectedItems.Count == 1)
                    {
                        enumerator = this.ContextMenu.MenuItems.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            ((MenuItem)enumerator.Current).Enabled = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public void HandleMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem itemAt = base.GetItemAt(e.X, e.Y);
                string str = "";
                if (itemAt != null)
                {
                    if (itemAt.SubItems.Count == 2)
                    {
                        string text = itemAt.SubItems[0].Text;
                        string text1 = itemAt.SubItems[1].Text;
                        int width = base.ClientSize.Width / 2 - 1;
                        Graphics graphic = base.CreateGraphics();
                        SizeF sizeF = graphic.MeasureString(text, this.Font);
                        int num = (int)sizeF.Width;
                        sizeF = graphic.MeasureString(text1, this.Font);
                        if (num > width || (int)sizeF.Width > width)
                        {
                            str = string.Concat(text, " = ", text1);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
               // this.mToolTip.SetToolTip(this, str);
            }
            catch (Exception exception)
            {
            }
        }

        public void Initialize()
        {
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            base.FullRowSelect = true;
            base.HeaderStyle = ColumnHeaderStyle.None;
            base.LabelWrap = false;
            base.Location = new Point(16, 70);
            base.MultiSelect = true;
            base.Name = "mHistoryList";
            base.Size = new System.Drawing.Size(320, 15 * CommandListView.ITEMHEIGHT);
            base.TabIndex = 9;
            base.View = System.Windows.Forms.View.Details;
            base.GridLines = true;
            //base.Scrollable = false;
            this.ContextMenu = null;
            ColumnHeader columnHeader = new ColumnHeader()
            {
                TextAlign = HorizontalAlignment.Left,
                Text = "Commands"
            };
            base.Columns.Add(columnHeader);
            ColumnHeader columnHeader1 = new ColumnHeader()
            {
                TextAlign = HorizontalAlignment.Left,
                Text = "Description"
            };
            base.Columns.Add(columnHeader1);
            this.mToolTip = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 5000,
                ShowAlways = true
            };
            base.MouseDown += new MouseEventHandler(this.HandleMouseDown);
            base.MouseMove += new MouseEventHandler(this.HandleMouseMove);
            base.DoubleClick += new EventHandler(this.ExpressionDoubleClicked);
            base.KeyUp += new KeyEventHandler(this.HandleKeyUp);
            base.KeyDown += new KeyEventHandler(this.HandleKeyDown);
            this.mCurrentListViewItem = null;
            this.ResizeColumns();
        }

        public void MenuCopy(object sender, EventArgs e)
        {
            IEnumerator enumerator = base.SelectedItems.GetEnumerator();
            string str = "";
            while (enumerator.MoveNext())
            {
                IEnumerator enumerator1 = ((ListViewItem)enumerator.Current).SubItems.GetEnumerator();
                int num = 0;
                while (enumerator1.MoveNext())
                {
                    ListViewItem.ListViewSubItem current = (ListViewItem.ListViewSubItem)enumerator1.Current;
                    if (num == 1)
                    {
                        str = string.Concat(str, " = ");
                    }
                    str = string.Concat(str, current.Text);
                    num++;
                }
                str = string.Concat(str, "\r");
            }
            if (!str.Equals(""))
            {
                Clipboard.SetDataObject(str, true);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.SuspendLayout();
            base.OnResize(e);
            this.ResizeColumns();
            base.ResumeLayout();
        }

        public void PasteToCommandLine(object sender, EventArgs e)
        {
            if (this.mCurrentListViewItem.SubItems[1] != null)
            {
                string str = this.mCurrentListViewItem.SubItems[1].Text.Replace(" ", "-");
                Autodesk.AutoCAD.Internal.Utils.WriteToCommandLine(str);
            }
        }

        public int ResizeAmount(int newY)
        {
            int y = newY;
            int height = 0;
            int tEMHEIGHT = 0;
            IEnumerator enumerator = base.Items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Rectangle bounds = ((ListViewItem)enumerator.Current).GetBounds(ItemBoundsPortion.Entire);
                height = height + bounds.Height;
                if (tEMHEIGHT != 0)
                {
                    continue;
                }
                tEMHEIGHT = ((ListViewItem)enumerator.Current).GetBounds(ItemBoundsPortion.Entire).Height;
            }
            int num = base.Location.Y;
            if (tEMHEIGHT == 0)
            {
                tEMHEIGHT = CommandListView.ITEMHEIGHT;
            }
            if (newY >= base.Location.Y + tEMHEIGHT)
            {
                if (height < 15 * tEMHEIGHT)
                {
                    height = 15 * tEMHEIGHT;
                }
                if (newY > base.Location.Y + height + tEMHEIGHT)
                {
                    y = base.Location.Y + height + tEMHEIGHT;
                }
            }
            else
            {
                y = 0;
            }
            return y;
        }

        public void ResizeColumns()
        {
            if (base.Columns == null || base.Columns.Count < 2)
            {
                return;
            }
            int width = base.ClientRectangle.Width;
            if (this.mWidthAfterResize != width)
            {
                base.Columns[0].Width = Convert.ToInt32((double)width * 0.25);
                base.Columns[1].Width = width - base.Columns[0].Width;
                this.mWidthAfterResize = width;
            }
        }

        public void SetExpressionFontColor(object sender, EventArgs e)
        {
            if (this.mColorDialog == null)
            {
                this.mColorDialog = new ColorDialog();
            }
            this.mColorDialog.Color = this.ForeColor;
            if (this.mColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.ForeColor = this.mColorDialog.Color;
            }
        }

        public void SetValueFontColor(object sender, EventArgs e)
        {
            if (this.mColorDialog == null)
            {
                this.mColorDialog = new ColorDialog();
            }
            this.mColorDialog.Color = this.mAnswerColor;
            if (this.mColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.AnswerColor = this.mColorDialog.Color;
            }
        }

        public void UpdateScrollBar()
        {
            if ((base.Items == null || base.Items.Count <= 0) && base.Scrollable)
            {
                base.Scrollable = false;
            }
            if (base.Items.Count * this.mItemHeight > base.Height)
            {
                if (!base.Scrollable)
                {
                    base.Scrollable = true;
                    return;
                }
            }
            else if (base.Scrollable)
            {
               // base.Scrollable = false;
            }
        }

        public enum RightClickMenuItems
        {
            eNone = -1,
            eExpressionFontColor = 0,
            eValueFontColor = 1,
            eMenuCopy = 3,
            eAppendExpression = 5,
            eAppendValue = 6,
            eClearHistory = 8,
            ePasteToCommandLine = 9
        }
    }
}