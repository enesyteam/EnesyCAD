using Autodesk.AutoCAD.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    internal class SearchTextBox : TextBoxEx
    {
        private string m_SearchWaterMark = "Search Command...";

        public string SearchWaterMark
        {
            get { return m_SearchWaterMark; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    m_SearchWaterMark = value;
                    this.Text = SearchWaterMark;
                }
            }
        }

        private bool mbShowingResult;

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
        public bool KeepFocus
        {
            get
            {
                if (!this.Focused || !base.Capture)
                {
                    return false;
                }
                return !this.ShowingResult;
            }
        }
        public bool ShowingResult
        {
            get
            {
                return this.mbShowingResult;
            }
            set
            {
                this.mbShowingResult = value;
            }
        }
        public SearchTextBox()
        {
        }
        private void HandleKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (!e.KeyChar.Equals('*') && !e.KeyChar.Equals('+') && !e.KeyChar.Equals('-') && !e.KeyChar.Equals('/') && !e.KeyChar.Equals('<'))
            {
                if (e.KeyChar != '\r' && e.KeyChar != '\u001B')
                {
                    this.ShowingResult = false;
                }
                return;
            }
            e.Handled = true;
        }
        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            this.ShowingResult = false;
        }
        public void Initialize()
        {
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            base.Name = "mSearchTextBox";
            
            base.Size = new System.Drawing.Size(320, 30);
            base.TabIndex = 4;
            base.AcceptsReturn = true;
            base.KeyPress += new KeyPressEventHandler(this.HandleKeyPress);
            base.MouseUp += new MouseEventHandler(this.HandleMouseUp);
            this.Enter += SearchTextBox_Enter;
            this.Leave += SearchTextBox_Leave;
            this.KeyDown += SearchTextBox_KeyDown;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Text == SearchWaterMark)
            {
                this.Text = "";
            }
        }


        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            SelectAll();
            if (this.Text == SearchWaterMark)
            {
                this.Text = "";
            }
            else
            {
                this.ForeColor = Color.Gray;
            }
        }

        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (this.Text.Trim() == "")
            {
                this.Text = SearchWaterMark;
                this.ForeColor = Color.Gray;
            }
        }
        public void InitializeText(string sNewText)
        {
            this.Text = sNewText;
            base.SelectionStart = this.Text.Length;
            this.mbShowingResult = true;
            base.Focus();
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            Control focusedControl = W32Util.GetFocusedControl();
        }
        protected override void WndProc(ref Message msg)
        {
            if (this.CMNControl != null)
            {
                if (msg.Msg == 258)
                {
                    if (msg.WParam.ToInt32().Equals(Keys.Return))
                    {
                        msg.Msg = 0;
                    }
                }
                else if (msg.Msg == 256 || msg.Msg == 260)
                {
                    Keys num = (Keys)msg.WParam.ToInt32();
                    if (num.Equals(Keys.Down) || num.Equals(Keys.Up) || num.Equals(Keys.Left) || num.Equals(Keys.Right))
                    {
                        this.ShowingResult = false;
                    }
                    else if (num.Equals(Keys.Escape))
                    {
                        this.InitializeText(SearchWaterMark);
                        msg.Msg = 0;
                    }
                }
                else if ((msg.Msg == 8 || msg.Msg == 7) && this.CMNControl != null && this.CMNControl.Host != null && this.CMNControl.Host is cmnESW)
                {
                    ((cmnESW)this.CMNControl.Host).ESW.KeepFocus = msg.Msg == 7;
                }
            }
            base.WndProc(ref msg);
        }
    }
}