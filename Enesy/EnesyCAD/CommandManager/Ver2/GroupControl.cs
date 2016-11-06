using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class GroupControl : ContainerControl
    {
        public static int GROUP_TITLE_HEIGHT = 25;
        public static int GROUP_TITLE_BEVELSIZE = 5;
        public static int GROUP_TITLE_TEXTLEFT = 10;
        public static int GROUP_TITLE_TEXTTOP = 1;
        public static int GROUP_BUTTON_SIZE = 14;
        public static int GROUP_BUTTONS_RIGHT = 28;
        public static int GROUP_BUTTON_TOPPADDING = 2;
        public static int GROUP_BUTTON_BOTTOMPADDING = 2;
        public static int GROUP_BUTTON_SPACE = 2;
        public static int GROUP_CHEVRON_RIGHT = 10;
        public static int GROUP_CHILD_VERT_SPACE = 12;
        protected static Color GROUP_HEADER_COLOR = Color.DarkGray;
        protected static Color GROUP_HEADER_TEXT_COLOR = Color.White;
        protected cmnGroupData mData;
        private string mTitle = "Group Name";
        private bool m_bHideGroupHeader;
        private bool mbIsMinimized;
        private bool mbIsHidden;
        private Button mChevronButton;
        protected int mHeight;
        protected ArrayList mTitleBtnList;
        protected GroupsPane mParent;
        protected string msMemoryString;
        private bool drawBoundary_;
        private Container components;

        public cmnGroupData Data
        {
            get
            {
                return this.mData;
            }
        }

        public CMNControl CMNControl
        {
            get
            {
                if (this.mParent == null || this.mParent.mParent == null)
                    return null;
                return this.mParent.mParent;
            }
        }

        public string Title
        {
            get
            {
                return this.mTitle;
            }
            set
            {
                this.mTitle = value;
            }
        }

        public string ConfigurationTitle
        {
            get
            {
                return this.mTitle.Replace(" ", "_");
            }
        }

        public bool Minimized
        {
            get
            {
                return this.mbIsMinimized;
            }
            set
            {
                if (value == this.mbIsMinimized)
                    return;
                Size size = this.Size;
                if (value && size.Height > GroupControl.GROUP_TITLE_HEIGHT)
                {
                    this.mHeight = this.Height;
                    this.Height = GroupControl.GROUP_TITLE_HEIGHT;
                }
                else if (!value && size.Height <= GroupControl.GROUP_TITLE_HEIGHT)
                    this.Height = this.mHeight;
                this.mbIsMinimized = value;
                this.mParent.OneGroupResized(this);
                this.UpdateTitleBar();
            }
        }

        public bool Hidden
        {
            get
            {
                return this.mbIsHidden;
            }
            set
            {
                if (value == this.mbIsHidden)
                    return;
                if (value)
                    this.mParent.RemoveGroup(this);
                else
                    this.mParent.AddNewGroup(this);
                this.mbIsHidden = value;
            }
        }

        public int RestoredHeight
        {
            get
            {
                return this.mHeight;
            }
            set
            {
                this.mHeight = value;
            }
        }

        public bool DrawBoundary
        {
            get
            {
                return this.drawBoundary_;
            }
            set
            {
                this.drawBoundary_ = value;
            }
        }

        public GroupControl(GroupsPane parent)
        {
            this.mTitleBtnList = new ArrayList();
            this.mParent = parent;
            if (CMNApplication.Theme != null)
            {
                GroupControl.GROUP_HEADER_COLOR = CMNApplication.Theme.GroupHeaderBack;
                GroupControl.GROUP_HEADER_TEXT_COLOR = CMNApplication.Theme.GroupHeaderText;
            }
            this.InitializeComponent();
            this.msMemoryString = ResHandler.GetResStringByName("#memory");
        }

        public virtual void RepairToolTips()
        {
            foreach (Control control in (ArrangedElementCollection)this.Controls)
            {
                if (control.GetType() == typeof(ToolTipButton))
                    ((ToolTipButton)control).ToolTip = ((ToolTipButton)control).ToolTip;
            }
        }

        public void AddTitleButton(ref ToolTipButton btn, Bitmap img)
        {
            if (btn != null)
                btn.Dispose();
            btn = new ToolTipButton();
            btn.FlatStyle = FlatStyle.Popup;
            btn.Size = new Size(GroupControl.GROUP_BUTTON_SIZE, GroupControl.GROUP_BUTTON_SIZE);
            img.MakeTransparent(Color.Magenta);
            btn.Image = (Image)img;
            this.mTitleBtnList.Add(btn);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.BackColor = this.BackColor;
            this.mChevronButton = new Button();
            this.mChevronButton.Size = new Size(GroupControl.GROUP_BUTTON_SIZE, GroupControl.GROUP_TITLE_HEIGHT - GroupControl.GROUP_BUTTON_TOPPADDING - GroupControl.GROUP_BUTTON_BOTTOMPADDING);
            this.mChevronButton.Location = new Point(this.ClientRectangle.Right - GroupControl.GROUP_CHEVRON_RIGHT - GroupControl.GROUP_BUTTON_SIZE, GroupControl.GROUP_BUTTON_TOPPADDING);
            this.mChevronButton.FlatStyle = FlatStyle.Flat;
            this.mChevronButton.ForeColor = GroupControl.GROUP_HEADER_COLOR;
            this.mChevronButton.BackColor = GroupControl.GROUP_HEADER_COLOR;
            this.mChevronButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.mChevronButton.Click += new EventHandler(this.ChevronBtn_Click);
            this.mChevronButton.Paint += new PaintEventHandler(this.ChevronBtn_Paint);
            this.Controls.Add((Control)this.mChevronButton);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        public bool LoadConfiguration(IConfigurationSection parentSection)
        {
            if (parentSection == null)
                return false;
            string configurationTitle = this.ConfigurationTitle;
            if (parentSection.ContainsSubsection(configurationTitle))
            {
                IConfigurationSection configurationSection = parentSection.OpenSubsection(configurationTitle);
                if (configurationSection != null)
                {
                    this.Minimized = (bool)configurationSection.ReadProperty("IsCollapsed", (object)true);
                    this.Hidden = (bool)configurationSection.ReadProperty("IsHidden", (object)false);
                    configurationSection.Close();
                    return true;
                }
            }
            return false;
        }

        public bool SaveConfiguration(IConfigurationSection parentSection)
        {
            if (parentSection == null)
                return false;
            string configurationTitle = this.ConfigurationTitle;
            if (parentSection.ContainsSubsection(configurationTitle) && this.CMNControl.Host is cmnESW)
                parentSection.DeleteSubsection(configurationTitle);
            IConfigurationSection subsection = parentSection.CreateSubsection(configurationTitle);
            if (subsection == null)
                return false;
            subsection.WriteProperty("IsCollapsed", this.Minimized);
            subsection.WriteProperty("IsHidden", this.Hidden);
            subsection.Close();
            return true;
        }

        public virtual void ThemeChanged()
        {
            GroupControl.GROUP_HEADER_COLOR = CMNApplication.Theme.GroupHeaderBack;
            GroupControl.GROUP_HEADER_TEXT_COLOR = CMNApplication.Theme.GroupHeaderText;
            this.BackColor = this.CMNControl.BackColor;
            this.mChevronButton.ForeColor = GroupControl.GROUP_HEADER_COLOR;
            this.mChevronButton.BackColor = GroupControl.GROUP_HEADER_COLOR;
        }

        private void ChevronBtn_Paint(object sender, PaintEventArgs e)
        {
            this.DrawChevron(e.Graphics);
        }

        private void DrawChevron(Graphics graphics)
        {
            if (graphics == null)
                return;
            Pen pen = new Pen(Color.White);
            Point point = new Point(this.mChevronButton.ClientRectangle.Left + (int)(0.5 * (double)this.mChevronButton.ClientRectangle.Width), this.mChevronButton.ClientRectangle.Top + (int)(0.5 * (double)this.mChevronButton.ClientRectangle.Height));
            Rectangle rectangle = new Rectangle(point.X - 4, point.Y - 4, 8, 9);
            int left = rectangle.Left;
            int num1 = rectangle.Top - 1;
            if (this.mbIsMinimized)
            {
                graphics.DrawLine(pen, left + 1, num1 + 1, left + 3, num1 + 3);
                graphics.DrawLine(pen, left + 4, num1 + 3, left + 6, num1 + 1);
                int num2 = num1 + 1;
                graphics.DrawLine(pen, left, num2, left + 3, num2 + 3);
                graphics.DrawLine(pen, left + 4, num2 + 3, left + 7, num2);
                int num3 = num2 + 4;
                graphics.DrawLine(pen, left, num3, left + 3, num3 + 3);
                graphics.DrawLine(pen, left + 4, num3 + 3, left + 7, num3);
                int num4 = num3 + 1;
                graphics.DrawLine(pen, left, num4, left + 3, num4 + 3);
                graphics.DrawLine(pen, left + 4, num4 + 3, left + 7, num4);
            }
            else
            {
                graphics.DrawLine(pen, left, num1 + 4, left + 3, num1 + 1);
                graphics.DrawLine(pen, left + 4, num1 + 1, left + 7, num1 + 4);
                int num2 = num1 + 1;
                graphics.DrawLine(pen, left, num2 + 4, left + 3, num2 + 1);
                graphics.DrawLine(pen, left + 4, num2 + 1, left + 7, num2 + 4);
                int num3 = num2 + 4;
                graphics.DrawLine(pen, left, num3 + 4, left + 3, num3 + 1);
                graphics.DrawLine(pen, left + 4, num3 + 1, left + 7, num3 + 4);
                int num4 = num3 + 1;
                graphics.DrawLine(pen, left + 1, num4 + 3, left + 3, num4 + 1);
                graphics.DrawLine(pen, left + 4, num4 + 1, left + 6, num4 + 3);
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (this.PointToClient(Cursor.Position).Y > GroupControl.GROUP_TITLE_HEIGHT)
                return;
            this.ToggleChevronBtn();
            this.mParent.OneGroupResized(this);
        }

        private void ChevronBtn_Click(object sender, EventArgs e)
        {
            this.ToggleChevronBtn();
            this.mParent.OneGroupResized(this);
        }

        public virtual void ToggleChevronBtn()
        {
            Size size = this.Size;
            if (size.Height > GroupControl.GROUP_TITLE_HEIGHT && !this.mbIsMinimized)
            {
                this.mHeight = this.Height;
                this.Height = GroupControl.GROUP_TITLE_HEIGHT;
                this.mbIsMinimized = true;
            }
            else if (size.Height <= GroupControl.GROUP_TITLE_HEIGHT && this.mbIsMinimized)
            {
                this.Height = this.mHeight;
                this.mbIsMinimized = false;
            }
            this.UpdateTitleBar();
        }

        protected virtual void UpdateTitleBar()
        {
            if (this.mChevronButton == null)
                return;
            this.mChevronButton.Invalidate();
        }

        public int GetCurrentHeight()
        {
            if (!this.mbIsMinimized)
                return this.mHeight;
            return GroupControl.GROUP_TITLE_HEIGHT;
        }

        public int GetHeightDelta()
        {
            if (!this.mbIsMinimized)
                return this.mHeight - GroupControl.GROUP_TITLE_HEIGHT;
            return GroupControl.GROUP_TITLE_HEIGHT - this.mHeight;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.DrawGroupHeader(pe);
        }

        protected virtual void DrawGroupHeader(PaintEventArgs pe)
        {
            if (this.m_bHideGroupHeader)
                return;
            Rectangle rect1 = new Rectangle(this.ClientRectangle.Location, this.ClientRectangle.Size);
            rect1.Height = GroupControl.GROUP_TITLE_HEIGHT;
            SolidBrush solidBrush1 = new SolidBrush(this.Parent.BackColor);
            pe.Graphics.FillRectangle((Brush)solidBrush1, rect1);
            Point[] points = new Point[6];
            points[0].X = rect1.Left;
            points[0].Y = rect1.Bottom;
            points[1].X = rect1.Left;
            points[1].Y = rect1.Top + GroupControl.GROUP_TITLE_BEVELSIZE;
            points[2].X = rect1.Left + GroupControl.GROUP_TITLE_BEVELSIZE;
            points[2].Y = rect1.Top;
            points[3].X = rect1.Right - GroupControl.GROUP_TITLE_BEVELSIZE - 2;
            points[3].Y = rect1.Top;
            points[4].X = rect1.Right;
            points[4].Y = rect1.Top + GroupControl.GROUP_TITLE_BEVELSIZE + 2;
            points[5].X = rect1.Right;
            points[5].Y = rect1.Bottom;
            SolidBrush solidBrush2 = new SolidBrush(GroupControl.GROUP_HEADER_COLOR);
            pe.Graphics.FillPolygon((Brush)solidBrush2, points);
            pe.Graphics.DrawString(this.mTitle, new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold), (Brush)new SolidBrush(GroupControl.GROUP_HEADER_TEXT_COLOR), (float)(rect1.Left + GroupControl.GROUP_TITLE_TEXTLEFT), (float)(rect1.Top + GroupControl.GROUP_TITLE_TEXTTOP));
            if (!this.DrawBoundary)
                return;
            Rectangle rect2 = new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top + GroupControl.GROUP_TITLE_HEIGHT - 1, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - GroupControl.GROUP_TITLE_HEIGHT - 1);
            pe.Graphics.DrawRectangle(new Pen(GroupControl.GROUP_HEADER_COLOR), rect2);
        }

        public virtual bool Populate(cmnGroupData data)
        {
            if (data == null)
                return false;
            this.mData = data;
            this.mTitle = data.msGroupName;// "Group Name"; //ResHandler.GetResStringByName(data.msGroupName);
            this.mHeight = 240;
            this.mbIsMinimized = data.mbCollapsed;
            return true;
        }

        protected void RePositionTitleButtons()
        {
            int count = this.mTitleBtnList.Count;
            if (count <= 0)
                return;
            int num = this.Width - (GroupControl.GROUP_BUTTONS_RIGHT + count * GroupControl.GROUP_BUTTON_SIZE + (count - 1) * GroupControl.GROUP_BUTTON_SPACE);
            for (int index = 0; index < this.mTitleBtnList.Count; ++index)
            {
                object mTitleBtn = this.mTitleBtnList[index];
                if (mTitleBtn.GetType().Equals(typeof(ToolTipButton)))
                {
                    ((Control)mTitleBtn).Left = num;
                    ((Control)mTitleBtn).Top = GroupControl.GROUP_BUTTON_TOPPADDING;
                    num += GroupControl.GROUP_BUTTON_SIZE + GroupControl.GROUP_BUTTON_SPACE;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.RePositionTitleButtons();
        }

        public virtual bool KeepFocus()
        {
            return false;
        }
    }
}
