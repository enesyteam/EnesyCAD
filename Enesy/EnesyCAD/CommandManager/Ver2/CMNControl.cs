using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Autodesk.AutoCAD.ApplicationServices;
using System.Reflection;
using Autodesk.AutoCAD.Windows;
using System.Collections;
using Enesy.EnesyCAD.DatabaseServices;
using Enesy.EnesyCAD.ApplicationServices;
using System.Diagnostics;
using System.Text;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public partial class CMNControl : UserControl
    {
        [XmlElement(ElementName = "ToolBar")]
        public cmnControlData mToolbarData;
        [XmlAttribute("AllowGroupHide")]
        public bool mbAllowHide;
        public static cmnControlData mLayoutData;
        public static bool Loading;
        private static bool mUIChangeEvent;
        private object mHost;
        private PerDocData mCurDocData;
        internal SearchTextBox mSearchTextBox;
        private CommandListView mCommandList;
        private CalculatorSplitter mSplitter;
        private ToolBar mToolBar;
        private ToolTipButton mbtnExpand;
        private Label mStatusRegion;
        private GroupsPane mGroupPane;
        private bool mbCreated;
        private Panel mTopPanel;
        private Panel mBottomPanel;
        private int mPaneHeight;
        public bool mbShouldRestore;
        public bool mbHideByProlog;
        private bool bExpanded_;
        private RichTextBox CommandInfo = null;
        public DataView CommandListView = null;
        private CmdTableRecord ListCommandTable = null;
        private GroupControl InfoGroup = null;
        public object Host
        {
            get
            {
                return this.mHost;
            }
        }
        public static cmnControlData UIData
        {
            get
            {
                return CMNControl.mLayoutData;
            }
        }
        internal GroupsPane GroupPane
        {
            get
            {
                return this.mGroupPane;
            }
        }
        public CMNControl()
        {
            InitializeComponent();
        }
        public CMNControl(object host)
        {
            this.mHost = host;
            this.mCurDocData = null;
            this.CreateControls();
        }
        protected void CreateControls()
        {
            this.SuspendLayout();
            this.mBottomPanel = new Panel();
            this.mTopPanel = new Panel();
            this.mBottomPanel.Location = new Point(20, 28);
            this.mTopPanel.Location = new Point(20, 28);
            if (this.mSearchTextBox == null)
            {
                this.mSearchTextBox = new SearchTextBox();
                this.mSearchTextBox.Initialize();
                mSearchTextBox.TextChanged += mSearchTextBox_TextChanged;
                this.mSearchTextBox.InitializeText(mSearchTextBox.SearchWaterMark);
            }
            this.mBottomPanel.Width = this.mSearchTextBox.Width + 28;
            this.mBottomPanel.Height = 300;
            this.mBottomPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            if (this.mStatusRegion == null)
                this.mStatusRegion = new Label();
            this.mStatusRegion.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.mStatusRegion.Location = new Point(16, 12);
            //this.mStatusRegion.Size = new Size(200, 20);
            this.mStatusRegion.BackColor = this.BackColor;
            this.mStatusRegion.BorderStyle = BorderStyle.None;
            this.mBottomPanel.Controls.Add(this.mStatusRegion);
            this.mbtnExpand = new ToolTipButton();
            this.mbtnExpand.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.mbtnExpand.FlatStyle = FlatStyle.Flat;
            this.mbtnExpand.ForeColor = this.mbtnExpand.BackColor = this.BackColor;
            this.mbtnExpand.Location = new Point(312, 10);
            this.mbtnExpand.Size = new Size(24, 24);
            this.mbtnExpand.Name = "mbtnExpand";
            this.mbtnExpand.TabIndex = 10;
            this.mbtnExpand.Click += new EventHandler(this.ExpandBtn_Click);
            this.mbtnExpand.MouseDown += new MouseEventHandler(this.ExpandBtn_MouseDown);
            this.mbtnExpand.ImageList = new ImageList();
            this.mbtnExpand.ImageList.ImageSize = new Size(20, 20);
           this.mbtnExpand.ImageList.Images.Add((System.Drawing.Image) GlobalResource.calc_less, Color.Magenta);
           this.mbtnExpand.ImageList.Images.Add((System.Drawing.Image) GlobalResource.calc_more, Color.Magenta);
            this.mbtnExpand.ImageIndex = 0;
            this.mbtnExpand.ToolTip = StringResources.ResourceManager.GetString("Less");
            this.mBottomPanel.Controls.Add(this.mbtnExpand);
           this.mTopPanel.Width = this.mBottomPanel.Width;
           this.mTopPanel.Height = 350;
           this.mCommandList = new CommandListView();
           //this.mHistoryList.BackColor = this.BackColor;

           this.mCommandList.SelectedIndexChanged += mCommandList_SelectedIndexChange;
           
            this.mTopPanel.Controls.Add(this.mCommandList);
            this.mCommandList.Initialize();

            this.Controls.AddRange(new Control[2]
              {
                (Control) this.mTopPanel,
                (Control) this.mBottomPanel
              });
            this.Name = "cmnControl";
            this.Resize += new EventHandler(this.Control_Resize);
            this.SystemColorsChanged += new EventHandler(this.Control_SystemColorsChanged);
            if (CMNControl.UIData == null)
                CMNControl.DeserializeUiLayout();
            if (CMNControl.UIData == null)
                return;
            if (this.InitializeToolbar())
                this.mTopPanel.Controls.Add(this.mToolBar);
            this.mTopPanel.Controls.Add((Control)this.mSearchTextBox);
            this.mGroupPane = new GroupsPane(this);
            this.mGroupPane.Top = this.mbtnExpand.Bottom + GroupsPane.GROUP_VERT_SPACE;
            this.mGroupPane.Left = this.mSearchTextBox.Left;
            //this.mGroupPane.Width = this.mSearchTextBox.Width+20;

            this.mGroupPane.Height = this.mBottomPanel.Height - this.mGroupPane.Top - GroupsPane.GROUP_VERT_SPACE;
            this.mBottomPanel.Controls.Add(this.mGroupPane);
            this.mPaneHeight = this.mGroupPane.Height;

            this.mGroupPane.Populate(CMNControl.UIData);
            this.mSplitter = new CalculatorSplitter(this.mTopPanel, this.mBottomPanel);
           this.ActiveControl = this.mSearchTextBox;
            this.mbCreated = true;
            this.mSearchTextBox.Location = new Point(16, 38);
            ListCommandTable = EneApplication.EneDatabase.CmdTableRecord;
            CommandListView = new DataView(ListCommandTable);
            PopulateListView(CommandListView);
            addInfoGroup();
            ResizeControls();
            this.ResumeLayout(false);
            this.Expand = false;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.mSearchTextBox.InitializeText(string.Empty);
                msg.Msg = 0;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void PopulateCommandInfo(DataRow r)
        {
            StringBuilder sb = new StringBuilder();
            if (r == null) return;
            CommandInfo.Clear();
            sb.AppendLine("Command: " + r[0].ToString());
            sb.AppendLine("Tag: " + r[1].ToString());
            sb.AppendLine("Description: " + r[2].ToString());
            sb.AppendLine("Author: " + r[3].ToString());
            sb.AppendLine("Email: " + r[4].ToString());
           sb.AppendLine("Help Link: " + r[5].ToString());
           CommandInfo.Text = sb.ToString();

        }
        private void mSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (CommandListView == null) return;
            if (mSearchTextBox.Text != mSearchTextBox.SearchWaterMark)
            {
                CommandListView.RowFilter = string.Format("Commands like '%{0}%' OR Description like '%{0}%'", mSearchTextBox.Text);
                PopulateListView(CommandListView);
            }
        }
        public void PopulateListView(DataView dv)
        {
            mCommandList.Items.Clear();
            foreach (DataRow r in dv.ToTable().Rows)
            {
                ListViewItem lvi = new ListViewItem(new string[] { r[0].ToString(), r[2].ToString() });
                lvi.Tag = r;
                mCommandList.Items.Add(lvi);
            }
        }
        

        private void mCommandList_SelectedIndexChange(object sender, EventArgs e)
        {
            if (mCommandList.SelectedItems.Count != 1)
                return;
            if (mCommandList.SelectedItems[0].Selected)
            {
                PopulateCommandInfo(mCommandList.SelectedItems[0].Tag as DataRow);
            }
        }
        private void addInfoGroup()
        {
            InfoGroup = (GroupControl)new GroupControl(this.mGroupPane);
            InfoGroup.DrawBoundary = true;
            InfoGroup.Populate(new cmnGroupData() { msGroupName = "Information" });
            
            InfoGroup.Name = "CommandInfo";
            InfoGroup.Location = new Point(20, 38);
            //InfoGroup.Width = mGroupPane.Width;
            InfoGroup.Height = 350;// groupControl.RestoredHeight;

            this.mGroupPane.AddNewGroup(InfoGroup);
            CommandInfo = new RichTextBox()
            {
                Location = new Point(16, 28),
                //Width = this.Width - 20,
                BackColor = this.BackColor,
                ReadOnly = true,
                BorderStyle = System.Windows.Forms.BorderStyle.None
            };
            CommandInfo.LinkClicked += CommandInfo_LinkClicked;
            InfoGroup.Controls.Add(CommandInfo);
        }

        private void CommandInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public PerDocData CurrentDocData
        {
            get
            {
                return this.mCurDocData;
            }
            set
            {
                this.mCurDocData = value;
            }
        }
        public void RestoreFromCurrentData(bool updateIA)
        {
            this.mCommandList.ClearCommandList();
            if (this.mCurDocData == null)
            {
                return;
            }
            //foreach (ExpressionResultPair mCurDocDatum in this.mCurDocData.mHistoryList)
            //{
            //    if (mCurDocDatum.mExpression == null || !(mCurDocDatum.mResult != null) || mCurDocDatum.mResult.ResultString == null)
            //    {
            //        continue;
            //    }
            //    this.mHistoryList.AddExpression(mCurDocDatum.mExpression, mCurDocDatum.mResult);
            //}
            //if (updateIA && this.mHost is CalculatorESW && this.mCurDocData.mCurrentExpression != null)
            //{
            //    this.ApplyToInputField(this.mCurDocData.mCurrentExpression, ExpressionType.Result);
            //}
        }
        public void RepairToolTips()
        {
            if (this.mHost.GetType() == typeof(cmnESW) && this.mToolBar != null)
            {
                this.mToolBar.ShowToolTips = false;
                this.mToolBar.ShowToolTips = true;
                this.mbtnExpand.ToolTip = this.mbtnExpand.ToolTip;
                this.mGroupPane.RepairToolTips();
            }
        }
        public void SetStatusRegionText(string sNewText)
        {
            if (this.mStatusRegion == null)
            {
                this.mStatusRegion = new Label() { Font = new System.Drawing.Font(this.Font, FontStyle.Bold) };
            }
            this.mStatusRegion.Text = sNewText;
        }
        protected bool InitializeToolbar()
        {
            if (CMNControl.UIData == null)
                return false;
            this.mToolBar = new ToolBar();
            this.mToolBar.Appearance = ToolBarAppearance.Flat;
            this.mToolBar.ButtonSize = new Size(16, 16);
            this.mToolBar.Divider = false;
            this.mToolBar.Dock = DockStyle.None;
            this.mToolBar.DropDownArrows = true;
            this.mToolBar.Location = new Point(16, 8);
            this.mToolBar.ShowToolTips = true;
            this.mToolBar.Size = new Size(250, 17);
            this.mToolBar.TabIndex = 8;
            this.mToolBar.ImageList = new ImageList();
            this.mToolBar.ImageList.ImageSize = new Size(16, 16);
            this.mToolBar.ButtonClick += new ToolBarButtonClickEventHandler(this.OnToolBarButton_Click);
            int num = 0;
            foreach (cmnToolbarButtonData mButton in CMNControl.UIData.mToolbarData.mButtons)
            {
                ToolBarButton button = new ToolBarButton();
                if (mButton.msImage.Equals("SEPARATOR"))
                {
                    button.Style = ToolBarButtonStyle.Separator;
                }
                else
                {
                    button.Tag = mButton.msExpression;
                    button.ImageIndex = num++;
                    object obj = GlobalResource.ResourceManager.GetObject(mButton.msImage);
                    if (obj != null)
                    {
                        if (obj is Bitmap)
                            this.mToolBar.ImageList.Images.Add((System.Drawing.Image)obj, Color.Magenta);
                        else
                            this.mToolBar.ImageList.Images.Add((Icon)obj);
                        if (mButton.msToolTip != null && !mButton.msToolTip.Equals(""))
                            button.ToolTipText = "aaa";//ResHandler.GetResStringByName(mButton.msToolTip);
                    }
                    else
                        break;
                }
                if (this.mHost.GetType() == typeof(cmnESW) || mButton.msType == null || !mButton.msType.Equals("Modeless"))
                    this.mToolBar.Buttons.Add(button);
            }
            return this.mToolBar != null;
        }
        private void OnToolBarButton_Click(object sender, ToolBarButtonClickEventArgs e)
        {
            string tag = (string)e.Button.Tag;
           bool focused = this.mSearchTextBox.Focused;
            switch (tag)
            {
                case "C":
                   // this.mEquationTextBox.InitializeText("0");
                    break;
                case "CH":
                    //this.ClearHistory();
                    break;
                case "help":
                    CheckForUpdate();
                    this.SetFocusToInputArea();
                    break;
                case "paste":
                    {
                        XmlSerializer xmlSerializer = (XmlSerializer)new LayoutParser();
                        using (StringWriter sww = new StringWriter())
                        {
                            if (xmlSerializer != null)
                            {
                                xmlSerializer.Serialize(sww, CMNControl.mLayoutData);
                            }
                            System.Windows.Forms.SaveFileDialog sdlg = new System.Windows.Forms.SaveFileDialog();
                            if (sdlg.ShowDialog() == DialogResult.OK)
                            {
                                System.IO.File.WriteAllText(sdlg.FileName, sww.ToString());
                            }
                        }

                    }
                    //cmnControl.mLayoutData 

                    //if (this.mEquationTextBox == null || this.mEquationTextBox.Text == null || this.mEquationTextBox.Text.Equals(""))
                    //    break;
                    //CalcResult result1 = (CalcResult)null;
                    //string sAnswer1 = (string)null;
                    //if (!this.SendExpressionToEngine(this.mEquationTextBox.Text, ref sAnswer1, ref result1) || sAnswer1 == null || !((DisposableWrapper)result1 != (DisposableWrapper)null))
                    //    break;
                    //Utils.WriteToCommandLine(result1.ResultString.Replace(" ", "-"));
                    break;
                default:
                    //string sAnswer2 = (string)null;
                    //CalcResult result2 = (CalcResult)null;
                    //if (!this.SendExpressionToEngine(tag, ref sAnswer2, ref result2))
                    //    break;
                    //if (sAnswer2 != null && (DisposableWrapper)result2 != (DisposableWrapper)null)
                    //{
                    //    this.mEquationTextBox.ApplyExpressionChange(ExpressionType.Operand, sAnswer2);
                    //    this.SetLastExpressionType(ExpressionType.Operand);
                    //}
                    //this.EnableApply(true);
                    break;
            }
        }

        void CheckForUpdate()
        {
            CheckForUpdate checkupdate = new CheckForUpdate("https://raw.githubusercontent.com/enesyteam/EnesyCAD/master/Enesy/EnesyCAD/VersionInfo.txt");

            //get my own version to compare against latest.
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Version myVersion = new Version(fvi.ProductVersion);

            Version latestVersion = new Version(checkupdate.version.ToString());

            string message = "";

            if (latestVersion > myVersion)
            {
                message = "Please update new EnesyCAD version [" + latestVersion + "] " + " at: " + checkupdate.newdownloadlink;
            }
            else
                message = "The newest version is installed!";
            MessageBox.Show(message, "Version Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExpandBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || !CMNControl.UIData.mbAllowHide || !this.UpdateGroupMenu())
                return;
            this.mbtnExpand.ContextMenu.Show((Control)this.mbtnExpand, new Point(0, this.mbtnExpand.Height + 2));
        }
        public bool UpdateGroupMenu()
        {
            if (this.mbtnExpand.ContextMenu == null)
                this.mbtnExpand.ContextMenu = new ContextMenu();
            this.mbtnExpand.ContextMenu.MenuItems.Clear();
            foreach (GroupControl group in this.mGroupPane.Groups)
            {
                System.Windows.Forms.MenuItem menuItem = new System.Windows.Forms.MenuItem(ResHandler.GetResStringByName(group.Data.msGroupName));
                menuItem.Click += new EventHandler(this.HandleGroupSelection);
                menuItem.Checked = !group.Hidden;
                this.mbtnExpand.ContextMenu.MenuItems.Add(menuItem);
            }
            return true;
        }
        public void HandleGroupSelection(object sender, EventArgs e)
        {
            if (this.mbtnExpand.ContextMenu == null)
                return;
            this.SuspendLayout();
            System.Windows.Forms.MenuItem menuItem = (System.Windows.Forms.MenuItem)sender;
            foreach (GroupControl group in this.mGroupPane.Groups)
            {
                if (group != null && menuItem.Text.Equals(group.Title))
                {
                    group.Hidden = !group.Hidden;
                    break;
                }
            }
            this.ResumeLayout();
        }
        private void Control_SystemColorsChanged(object sender, EventArgs e)
        {
            this.SyncTheme();
        }
        private void ExpandBtn_Click(object sender, EventArgs e)
        {
           this.Expand = !this.Expand;
        }
        private bool Expand
        {
            get
            {
                return this.bExpanded_;
            }
            set
            {
                this.bExpanded_ = value;
                bool visible = this.mGroupPane.Visible;
                if (value == visible)
                    return;
                if (visible)
                    this.mPaneHeight = this.mGroupPane.Height <= GroupControl.GROUP_TITLE_HEIGHT || this.mGroupPane.Height > this.mGroupPane.RestoredHeight ? this.mGroupPane.RestoredHeight : this.mGroupPane.Height - GroupsPane.GROUP_VERT_SPACE;
                int num = visible ? -(this.mBottomPanel.Height - this.mbtnExpand.Bottom) : this.mPaneHeight;
                this.mGroupPane.Visible = Expand;
                if (!visible)
                    this.mGroupPane.AutoScrollPosition = new Point(0, 0);
                this.mbtnExpand.ImageIndex = this.mGroupPane.Visible ? 0 : 1;
                this.mbtnExpand.ToolTip = StringResources.ResourceManager.GetString(this.mGroupPane.Visible ? "Less" : "More");
                //if (this.mHost is CalculatorForm)
                //{
                //    ((CalculatorForm)this.mHost).Height += num;
                //}
                //else
                //{
                    if (!(this.mHost is cmnESW))
                        return;
                    PaletteSet esw = ((cmnESW)this.mHost).ESW;
                    //if (esw.Dock != DockSides.None)
                    //    return;
                    this.mBottomPanel.Height = 0;
                    Size size = new Size(esw.Size.Width, esw.Size.Height + num + 10);
                    //esw.MinimumSize = new Size(CMNControl.UIData.mESWMinSize.Width, visible ? size.Height : CMNControl.UIData.mESWMinSize.Height);
                    esw.Size = size;
                //}
            }
        }
        public void UpdateControlSizes()
        {
            this.mCommandList.UpdateScrollBar();
            this.mCommandList.ResizeColumns();
        }
        public int CommandListResizeAmount(int newY)
        {
            return this.mCommandList.ResizeAmount(newY);
        }
        private void Control_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }
        void ResizeControls()
        {
            bool visible = this.mGroupPane.Visible;
            int num = (visible ? -(this.mBottomPanel.Height - this.mbtnExpand.Bottom) : this.mPaneHeight);
            PaletteSet eSW = ((cmnESW)this.mHost).ESW;
            //if (eSW.Dock == DockSides.None)
            //{
                int width = eSW.Size.Width;
                System.Drawing.Size size = eSW.Size;
                System.Drawing.Size size1 = new System.Drawing.Size(width, size.Height + num);
                //eSW.MinimumSize = new System.Drawing.Size(CMNControl.UIData.mESWMinSize.Width, (visible ? size1.Height : CMNControl.UIData.mESWMinSize.Height));
                //eSW.Size = size1;

                this.mStatusRegion.Width = this.mbtnExpand.Left - this.mStatusRegion.Left - 20;
                this.mGroupPane.Width = this.mbtnExpand.Left + 26;
                this.InfoGroup.Width = mGroupPane.Width - 20;
                this.CommandInfo.Width = InfoGroup.Width - 20;
                this.CommandInfo.Height = 180;
                this.InfoGroup.Height = this.CommandInfo.Height + 35;
            //}
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        public void UpdateToolBar(bool enable)
        {
            if (this.mToolBar == null)
            {
                return;
            }
            if (this.Host is cmnESW)
            {
                foreach (ToolBarButton button in this.mToolBar.Buttons)
                {
                    if (button.ImageIndex <= 2 || button.ImageIndex >= 7)
                    {
                        continue;
                    }
                    button.Enabled = enable;
                }
            }
        }
        public void UpdateToolBar()
        {
            Document mdiActiveDocument = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (mdiActiveDocument == null)
            {
                return;
            }
            this.UpdateToolBar(mdiActiveDocument.CommandInProgress.Equals(string.Empty));
        }
        public bool SaveConfiguration(IConfigurationSection parentSection)
        {
            if (parentSection == null)
            {
                return false;
            }
            string str = "CMNUiSettings";
            if (parentSection.ContainsSubsection(str) && this.Host is cmnESW)
            {
                parentSection.DeleteSubsection(str);
            }
            IConfigurationSection configurationSection = parentSection.CreateSubsection(str);
            if (configurationSection == null)
            {
                return false;
            }
            if (this.mHost.GetType() == typeof(cmnESW))
            {
                IConfigurationSection configurationSection1 = configurationSection.CreateSubsection("CalcESWAdditionalData");
                if (configurationSection1 != null)
                {
                    configurationSection1.WriteProperty("CalcESWMinSize", ((cmnESW)this.mHost).ESW.MinimumSize);
                    configurationSection1.Close();
                }
            }
            IConfigurationSection configurationSection2 = configurationSection.CreateSubsection("HistoryColors");
            if (configurationSection2 != null)
            {
                configurationSection2.WriteProperty("HistoryColor1", this.mCommandList.CommandItemColor);
                configurationSection2.WriteProperty("HistoryColor2", this.mCommandList.AnswerColor);
                configurationSection2.Close();
            }
            configurationSection.WriteProperty("ExpandMode", this.Expand);
            if (this.mGroupPane != null)
            {
                this.mGroupPane.SaveConfiguration(configurationSection);
            }
            configurationSection.Close();
            return true;
        }
        public void SetFocusToInputArea()
        {
            this.mSearchTextBox.Focus();
        }
        public static void DeserializeUiLayout()
        {
            try
            {
                if (CMNControl.UIData != null)
                {
                    return;
                }
                FileStream fileStream = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CmnUi.xml"), FileMode.Open, FileAccess.Read, FileShare.Read);
                if (fileStream != null)
                {
                    XmlSerializer xmlSerializer = (XmlSerializer)new LayoutParser();
                    if (xmlSerializer != null)
                        CMNControl.mLayoutData = (cmnControlData)xmlSerializer.Deserialize((Stream)fileStream);
                    fileStream.Close();
                }
                //cmnControl.DeserializeVariables();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.FileName + " not found!");
            }
        }
        public bool LoadConfiguration(IConfigurationSection parentSection)
        {
            if (parentSection == null)
                return false;
            string name = "CMNUiSettings";
            if (parentSection.ContainsSubsection(name))
            {
                IConfigurationSection parentSection1 = parentSection.OpenSubsection(name);
                if (parentSection1 != null)
                {
                    if (this.mHost.GetType() == typeof(cmnESW) && parentSection1.ContainsSubsection("CalcESWAdditionalData"))
                    {
                        IConfigurationSection configurationSection = parentSection1.OpenSubsection("CalcESWAdditionalData");
                        if (configurationSection != null)
                        {
                            ((cmnESW)this.mHost).ESW.MinimumSize = (Size)configurationSection.ReadProperty("CalcESWMinSize", CMNControl.UIData.mESWMinSize);
                            configurationSection.Close();
                        }
                    }
                    if (parentSection1.ContainsSubsection("HistoryColors"))
                    {
                        IConfigurationSection configurationSection = parentSection1.OpenSubsection("HistoryColors");
                        if (configurationSection != null)
                        {
                            //this.mHistoryList.ExpressionColor = (Color)configurationSection.ReadProperty("HistoryColor1", (object)SystemColors.WindowText);
                            //this.mHistoryList.AnswerColor = (Color)configurationSection.ReadProperty("HistoryColor2", (object)Color.Red);
                            configurationSection.Close();
                        }
                    }
                    this.Expand = (bool)parentSection1.ReadProperty("ExpandMode", (object)true);
                    if (this.mGroupPane != null)
                        this.mGroupPane.LoadConfiguration(parentSection1);
                    parentSection1.Close();
                    return true;
                }
            }
            return false;
        }
        public void SyncTheme()
        {
            CMNApplication.Theme.Update();
            this.BackColor = CMNApplication.Theme.ESWBackground;
            if (this.GroupPane != null)
                this.GroupPane.ThemeChanged();
            if (this.mbtnExpand != null)
                this.mbtnExpand.ForeColor = this.mbtnExpand.BackColor = this.BackColor;
            if (this.mStatusRegion != null)
                this.mStatusRegion.BackColor = this.BackColor;
            this.Invalidate();
        }
    }
}
