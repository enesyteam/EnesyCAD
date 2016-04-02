using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.MNUParser;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Autodesk.AutoCAD.Customization
{
    internal class MainForm : Form
    {
        private MainMenu mainMenu;

        private Button okButton;

        private Button cancelButton;

        private Button applyButton;

        private Button helpButton;

        private TabControl tabControl;

        private ColorDialog colorDialog1;

        private ImageList tabImgList;

        private CustomizeTreeView customizeTreeView;

        private WorkspaceTreeView workspaceTreeView;

        private TransferTreeView leftTransferTreeView;

        private TransferTreeView rightTransferTreeView;

        private IContainer components;

        private float mImpPageSplitterRatio;

        private float mCustPageSplitterRatio;

        private float mCustPageLeftVerSplitterRatio;

        private TabPage transferPage;

        private TabPage customizePage;

        private Panel custPageRightPanel;

        private PropertyControl custPagePropertyControl;

        private Splitter custPageRightSplitter;

        private CollapsiblePanel custPageRightTopPanel;

        private Splitter custPageSplitter;

        private Panel custPageLeftPanel;

        private CollapsiblePanel custPageLeftBottomPanel;

        private CommandListControl custPageCmdListCtrl;

        private Splitter custPageLeftSplitter;

        private CollapsiblePanel custPageLeftTopPanel;

        private Panel custPageRightBottomPropPanel;

        private CollapsiblePanel custPageMiscControlPanel;

        private CollapsiblePanel custPagePropertyPanel;

        private ButtonControl buttonControl;

        private ToolbarPreview toolbarPreview;

        private Splitter transferPageSplitter;

        private Panel transferPageRightPanel;

        private CollapsiblePanel transferPageRightTopPanel;

        private Panel transferPageLeftPanel;

        private CollapsiblePanel transferPageLeftTopPanel;

        private Splitter custPageRightPanelPropSplitter;

        private ShortcutsGroup shortcutsGroup;

        private float mCustPageRightVerSplitterRatio;

        private QuickStartLink quickStartLink;

        private bool isCancelButton;

        internal static CustomizationHostServices customizationHostServices;

        private int mRightTopPanelHeight;

        private MainForm.DlgMode mDlgMode;

        private static string mInitialToolbarMenuGroup;

        private static string mInitialToolbarUID;

        private static bool mPaletteConfig;

        private bool _bitmapCacheIntialized;

        private bool mPopulateWorkspace;

        public static MainForm mainTab;

        private bool mToolbarWasHidden;

        private int mShortcutHeight = -1;

        private bool mResizePending;

        private bool bForceMenuReload;

        private bool mNeedToRepopulate;

        private static Workspace mWSCURRENT;

        private static Workspace mOrgWSCURRENT;

        private static bool mOrgWSCURRENTModified;

        public static string acad
        {
            get
            {
                return MainForm.ACAD.ToLower();
            }
        }

        public static string ACAD
        {
            get
            {
                if (HostApplicationServices.Current == null)
                {
                    return "ACAD";
                }
                return HostApplicationServices.Current.Program;
            }
        }

        internal static string ApplicationCaption
        {
            get
            {
                if (null == HostApplicationServices.Current)
                {
                    return "Customization Test";
                }
                return HostApplicationServices.Current.Product;
            }
        }

        public ButtonControl ButtonControl
        {
            get
            {
                return this.buttonControl;
            }
        }

        public CustomizationSection CustomizeMainCUIFile
        {
            get
            {
                return this.customizeTreeView.MainCUIFile;
            }
        }

        public CommandListControl CustPageCmdListCtrl
        {
            get
            {
                return this.custPageCmdListCtrl;
            }
        }

        public PropertyControl CustPagePropertyControl
        {
            get
            {
                return this.custPagePropertyControl;
            }
        }

        public static string EnterpriseCUIPath
        {
            get
            {
                if (HostApplicationServices.Current == null)
                {
                    string str = Path.Combine(Environment.GetEnvironmentVariable("LBIN"), "support\\enterprise.cui");
                    if (!File.Exists(str))
                    {
                        str = Path.ChangeExtension(str, ".mnu");
                    }
                    return str;
                }
                string systemVariable = null;
                try
                {
                    systemVariable = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("ENTERPRISEMENU") as string;
                }
                catch (Exception exception)
                {
                }
                string empty = string.Empty;
                if (systemVariable != null)
                {
                    if (Path.HasExtension(systemVariable))
                    {
                        empty = (systemVariable.ToLower().EndsWith(".cui") ? systemVariable : string.Concat(systemVariable.Substring(0, systemVariable.Length - 4), ".cui"));
                    }
                    else
                    {
                        empty = string.Concat(systemVariable, ".cui");
                    }
                }
                return empty;
            }
        }

        public static string MainCUIPath
        {
            get
            {
                if (HostApplicationServices.Current == null)
                {
                    string str = Path.Combine(Environment.GetEnvironmentVariable("LBIN"), "support\\acad.cui");
                    if (!File.Exists(str))
                    {
                        str = Path.ChangeExtension(str, ".mnu");
                    }
                    return str;
                }
                string systemVariable = null;
                try
                {
                    systemVariable = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("MENUNAME") as string;
                }
                catch (Exception exception)
                {
                }
                string empty = string.Empty;
                if (systemVariable != null && systemVariable.Trim().Length != 0)
                {
                    if (Path.HasExtension(systemVariable))
                    {
                        empty = (systemVariable.ToLower().EndsWith(".cui") ? systemVariable : string.Concat(systemVariable.Substring(0, systemVariable.Length - 4), ".cui"));
                    }
                    else
                    {
                        empty = string.Concat(systemVariable, ".cui");
                    }
                }
                return empty;
            }
        }

        internal static Workspace OrginalWSCURRENT
        {
            get
            {
                return MainForm.mOrgWSCURRENT;
            }
        }

        internal static string ReloadedWSCURRENT
        {
            get
            {
                if (MainForm.mWSCURRENT == null)
                {
                    return null;
                }
                return MainForm.mWSCURRENT.Name;
            }
        }

        public MainForm.TabPageIndex SelectedTab
        {
            get
            {
                return (MainForm.TabPageIndex)this.tabControl.SelectedIndex;
            }
            set
            {
                if (value == MainForm.TabPageIndex.TransferTab)
                {
                    this.tabControl.SelectedIndex = 1;
                    return;
                }
                if (value == MainForm.TabPageIndex.CustomizeTab)
                {
                    this.tabControl.SelectedIndex = 0;
                }
            }
        }

        public ShortcutsGroup ShortcutsGroup
        {
            get
            {
                return this.shortcutsGroup;
            }
        }

        public CustomizeTreeView TheCustomizeTreeView
        {
            get
            {
                return this.customizeTreeView;
            }
        }

        public ToolbarPreview ToolbarPreview
        {
            get
            {
                return this.toolbarPreview;
            }
        }

        public CustomizationSection TransferLeftMainCUIFile
        {
            get
            {
                return this.leftTransferTreeView.MainCUIFile;
            }
        }

        public bool TransferPaneOnly
        {
            get
            {
                return this.mDlgMode == MainForm.DlgMode.TransferOnly;
            }
        }

        public CustomizationSection TransferRightMainCUIFile
        {
            get
            {
                return this.rightTransferTreeView.MainCUIFile;
            }
        }

        public TreeViewControl TreeViewControl
        {
            get
            {
                return this.customizeTreeView.TreeViewControl;
            }
        }

        internal static Workspace WSCURRENT
        {
            get
            {
                return MainForm.mWSCURRENT;
            }
            set
            {
                MainForm.mWSCURRENT = value;
                MainForm.mainTab.forceMenuReload();
            }
        }

        static MainForm()
        {
            MainForm.customizationHostServices = new CustomizationHostServices();
            MainForm.mPaletteConfig = false;
        }

        public MainForm()
        {
            FindReplace.Reset();
            DefaultGroupEditor.Reset();
            this.InitializeComponent();
            this.isCancelButton = false;
            base.KeyPreview = true;
            if (HostApplicationServices.Current == null)
            {
                base.ShowInTaskbar = true;
            }
            this.quickStartLink.SetNewFeatureWorkshopTopic("CUI");
            this.tabImgList.ImageStream = (ImageListStreamer)GlobalResources.GetObject("tabImgList.ImageStream");
        }

        public static void AddMRUFile(ArrayList mruList, string fname, int max)
        {
            string lower = fname.ToLower();
            Path.ChangeExtension(fname, ".cui");
            if (mruList.Contains(lower))
            {
                return;
            }
            if (MainForm.MainCUIPath != null && lower == MainForm.MainCUIPath.ToLower())
            {
                return;
            }
            if (MainForm.EnterpriseCUIPath != null && lower == MainForm.EnterpriseCUIPath.ToLower())
            {
                return;
            }
            if (!File.Exists(fname))
            {
                return;
            }
            mruList.Insert(0, lower);
            if (max != -1)
            {
                while (mruList.Count > max)
                {
                    mruList.RemoveAt(max);
                }
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.okButton_Click(sender, e);
            MainForm mainForm = base.FindForm() as MainForm;
            if (mainForm != null)
            {
                mainForm.Activate();
            }
            int num = 0;
            bool flag = false;
            this.bForceMenuReload = false;
            this.applyButton.Enabled = flag;
        }

        private void buttonControlEventHandler(object sender, ButtonControlEventArgs ea)
        {
            string resourceId = ea.ResourceId;
            if (ea.Action != ButtonControlAction.IconSelected)
            {
                if (ButtonControlAction.EditButton == ea.Action && resourceId != null && 0 < resourceId.Length && null != HostApplicationServices.Current)
                {
                    MainForm.mainTab.Enabled = false;
                    Utils.CallButtonEditor(resourceId);
                    MainForm.mainTab.Enabled = true;
                    this.buttonControl.SetImageList(BitmapCache.ImageList, BitmapCache.ResourceIds);
                    this.buttonControl.SetCurrent(ea.Resolver, resourceId);
                }
                return;
            }
            ToolbarFlyout macro = ea.Macro as ToolbarFlyout;
            Macro macro1 = ea.Macro as Macro;
            string str = this.extractResDllName(resourceId);
            if (str != "")
            {
                str = Path.ChangeExtension(str, "");
            }
            if (macro1 != null)
            {
                macro1.prepForUpdate();
                if (str != "")
                {
                    CustomizationSection resolver = ea.Resolver;
                    if (resolver != null && str != Path.ChangeExtension(resolver.CUIFileName, "").ToLower())
                    {
                        MainForm.ShowAlert(LocalResources.GetString("DLG_Img_not_available"));
                        return;
                    }
                }
                if (ea.SmallSelected)
                {
                    macro1.SmallImage = this.stripDllNamePart(resourceId);
                }
                if (ea.LargeSelected)
                {
                    macro1.LargeImage = this.stripDllNamePart(resourceId);
                }
                this.custPageCmdListCtrl.updateEntry(macro1);
            }
            if (macro != null)
            {
                if (ea.SmallSelected)
                {
                    macro.SmallImage = this.stripDllNamePart(resourceId);
                }
                if (ea.LargeSelected)
                {
                    macro.LargeImage = this.stripDllNamePart(resourceId);
                }
            }
            this.custPagePropertyControl.Refresh2();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.isCancelButton = true;
        }

        public bool checkRightPanel(CollapsiblePanel panel)
        {
            bool flag = true;
            bool visible = this.custPageRightTopPanel.Visible;
            bool visible1 = this.custPageMiscControlPanel.Visible;
            bool flag1 = this.custPagePropertyPanel.Visible;
            if (panel == this.custPageRightTopPanel && !visible1 && !flag1 || panel == this.custPageMiscControlPanel && !visible && !flag1 || panel == this.custPagePropertyPanel && !visible && !visible1)
            {
                flag = false;
            }
            return flag;
        }

        public bool CloseCustomizeDocument(Document doc)
        {
            if (doc == null)
            {
                return true;
            }
            if (!MainForm.SaveDocument(doc, true))
            {
                return false;
            }
            this.customizeTreeView.TreeViewControl.ClearTree();
            doc.Close();
            return true;
        }

        public bool CloseTransferDocument(Document doc, bool prompt)
        {
            if (doc == null)
            {
                return true;
            }
            if (prompt && !MainForm.SaveDocument(doc, true))
            {
                return false;
            }
            if (doc == this.leftTransferTreeView.Document)
            {
                this.leftTransferTreeView.TreeViewControl.ClearTree();
            }
            else if (doc == this.rightTransferTreeView.Document)
            {
                this.rightTransferTreeView.TreeViewControl.ClearTree();
            }
            doc.Close();
            return true;
        }

        public static bool ConfirmDelete(bool plural)
        {
            string str = LocalResources.GetString((plural ? "MSG_DeletePlural" : "MSG_Delete"));
            return DialogResult.Yes == MainForm.ShowMessageBox(str, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        public static void CUICommand()
        {
            bool flag = false;
            if (!MainForm.gotMainCUIFile())
            {
                MainForm.ShowAlert(LocalResources.GetString("DLG_No_CUI_File_Msg"));
                flag = true;
            }
            MainForm mainForm = new MainForm();
            MainForm.mainTab = mainForm;
            using (mainForm)
            {
                if (!flag)
                {
                    MainForm.mainTab.InitForCustomization();
                }
                else
                {
                    MainForm.mainTab.InitForTransferOnly();
                }
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Autodesk.AutoCAD.ApplicationServices.Application.MainWindow, MainForm.mainTab);
                MainForm.InitForCustomizeToolbar(null, null);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void CUIFile_Modified(object sender, EventArgs e)
        {
            CustomizationSection customizationSection = sender as CustomizationSection;
            if (!customizationSection.ReadOnly && (customizationSection == Document.MainCuiFile || customizationSection == Document.EnterpriseCUIFile || customizationSection.ParentCUI != null))
            {
                this.applyButton.Enabled = true;
                this.mNeedToRepopulate = true;
            }
        }

        private void CustPage_TreeViewControl_ItemSelected(object sender, TVCItemSelectedArgs e)
        {
            CUITreeNode cUITreeNode;
            int count = e.TreeNodes.Count;
            if (count == 0)
            {
                return;
            }
            CUITreeNode item = e.TreeNodes[0] as CUITreeNode;
            this.restorePanelStatus();
            this.custPagePropertyControl.Clear();
            this.hideToolbarPreview();
            this.hideWorkspaceWindow(item);
            if (1 == count)
            {
                cUITreeNode = item;
            }
            else
            {
                cUITreeNode = null;
            }
            this.hideMiscPanel(cUITreeNode);
            if (1 < count)
            {
                return;
            }
            if (item == null)
            {
                return;
            }
            object tag = item.Tag;
            if (item.isShortcutNode())
            {
                this.showShortcutsControl();
                ShortcutsInterface.populateKeyboardShorcuts(this.shortcutsGroup, item);
                //if (item is CUIShortcutKeyNode || item is CUITemporaryOverrideNode)
                //{
                //    this.shortcutsGroup.ScrollToSingleUid(item.UidPair);
                //}
            }
            InformationRetriever informationRetriever = new InformationRetriever(item);
            if (informationRetriever.HasInformation)
            {
                this.custPagePropertyPanel.HeaderText = LocalResources.GetString("JT_Information");
                this.custPagePropertyControl.SetTextPane(informationRetriever.Header, informationRetriever.Body, informationRetriever.LinkText, informationRetriever.HelpfileName, informationRetriever.TopicId);
                //if (!(item is CUIPartialCUIFileNode))
                //{
                //    return;
                //}
            }
            if (item.Tag == null)
            {
                return;
            }
            if (!(tag is CustomizationElement) && !(tag is CustomizationSection))
            {
                return;
            }
            this.custPagePropertyPanel.HeaderText = LocalResources.GetString("JT_Properties");
            CustomizationElement customizationElement = tag as CustomizationElement;
            CustomizationSection customizationSection = tag as CustomizationSection;
            if (customizationElement != null)
            {
                this.custPagePropertyControl.SetProperties(customizationElement, item);
            }
            else if (customizationSection != null)
            {
                this.custPagePropertyControl.SetCustomizationSectionProperties(customizationSection, item);
            }
            if (tag is Workspace)
            {
                this.showWorkspaceWindow(item as CUIWorkspaceNode);
            }
            if (tag is PopMenuItem || tag is ToolbarButton)
            {
                ToolbarButton toolbarButton = tag as ToolbarButton;
                PopMenuItem popMenuItem = tag as PopMenuItem;
                CustomizationSection cUIFile = item.CUIFile;
                Macro macroForToolbarButton = null;
                string macroID = null;
                if (toolbarButton != null)
                {
                    if (!toolbarButton.IsSeparator)
                    {
                        macroForToolbarButton = cUIFile.getMacroForToolbarButton(toolbarButton);
                        macroID = toolbarButton.MacroID;
                    }
                    else
                    {
                        this.populateToolbarPreview(toolbarButton.Parent, item);
                    }
                }
                if (popMenuItem != null && !popMenuItem.IsSeparator)
                {
                    macroForToolbarButton = cUIFile.getMenuMacro(popMenuItem.MacroID).macro;
                    macroID = popMenuItem.MacroID;
                }
                if (macroForToolbarButton != null)
                {
                    this.showButtonForMacro(cUIFile, macroForToolbarButton);
                }
                if (macroID != null && 0 < macroID.Length)
                {
                    this.custPageCmdListCtrl.ScrollToSingleUid(new UidPair(item.CUIFile, macroID));
                }
            }
            if (customizationElement is ToolbarFlyout)
            {
                this.showButtonForFlyout(item.CUIFile, customizationElement as ToolbarFlyout);
            }
            if (customizationElement is Toolbar || customizationElement is ToolbarControl)
            {
                ToolbarControl toolbarControl = customizationElement as ToolbarControl;
                Toolbar toolbar = (toolbarControl == null ? customizationElement as Toolbar : toolbarControl.Parent);
                if (toolbar.ToolbarItems.Count > 0 && !item.MSTreeView.IsDragging)
                {
                    this.populateToolbarPreview(toolbar, item);
                }
                else if (toolbar.ToolbarItems.Count != 0)
                {
                    this.hideToolbarPreview();
                }
                else
                {
                    this.hideMiscPanel(null);
                }
            }
            bool flag = false;
            bool flag1 = false;
            if (customizationElement is ButtonItem || customizationElement is MenuAccelerator || customizationElement is DoubleClickCmd || customizationElement is ImageMenuItem)
            {
                flag = true;
                flag1 = false;
            }
            else if (customizationElement is TabletMenuItem || customizationElement is ScreenMenuItem)
            {
                flag = true;
                flag1 = true;
            }
            if (flag)
            {
                CustomizationReference customizationReference = customizationElement as CustomizationReference;
                if (customizationReference != null)
                {
                    string str = customizationReference.MacroID;
                    if (str != null && 0 < str.Length)
                    {
                        this.custPageCmdListCtrl.ScrollToSingleUid2(new UidPair(item.CUIFile, str), flag1);
                    }
                }
            }
        }

        private void custPageLeftBottomPanel_Collapsed(object sender, EventArgs args)
        {
            this.handleSplitter(this.custPageLeftSplitter, this.custPageLeftBottomPanel.Expanded);
            this.toggleBottomPanelState(this.custPageLeftTopPanel, this.custPageLeftBottomPanel);
        }

        private void custPageLeftTopPanel_Collapsed(object sender, EventArgs args)
        {
            this.handleSplitter(this.custPageLeftSplitter, this.custPageLeftTopPanel.Expanded);
            this.toggleTopPanelState(this.custPageLeftTopPanel, this.custPageLeftBottomPanel);
        }

        private void custPageMiscControlPanel_Collapsed(object sender, EventArgs args)
        {
            this.handleSplitter(this.custPageRightPanelPropSplitter, this.custPageMiscControlPanel.Expanded);
            this.toggleTopPanelState(this.custPageMiscControlPanel, this.custPagePropertyPanel);
        }

        private void CustPagePropertyControl_EventHandler(object from, PropertyEventArgs args)
        {
            if (args.Action == PropertyEventAction.PopulateWorkspace)
            {
                this.PopulateWorkspaceMode();
            }
        }

        private void custPagePropertyPanel_Collapsed(object sender, EventArgs args)
        {
            if (this.custPageMiscControlPanel.Visible)
            {
                this.handleSplitter(this.custPageRightPanelPropSplitter, this.custPagePropertyPanel.Expanded);
                this.toggleBottomPanelState(this.custPageMiscControlPanel, this.custPagePropertyPanel);
                return;
            }
            this.handleSplitter(this.custPageRightSplitter, this.custPagePropertyPanel.Expanded);
            this.toggleRightBottomPanelState(this.custPageRightTopPanel, this.custPagePropertyPanel);
        }

        private void custPageRightPanelPropSplitter_Move(object sender, EventArgs e)
        {
            int splitPosition = this.custPageRightPanelPropSplitter.SplitPosition;
            if (this.shortcutsGroup.Visible)
            {
                this.mShortcutHeight = splitPosition;
            }
        }

        private void custPageRightTopPanel_Collapsed(object sender, EventArgs args)
        {
            this.handleSplitter(this.custPageRightSplitter, this.custPageRightTopPanel.Expanded);
            if (this.custPageMiscControlPanel.Visible)
            {
                return;
            }
            this.toggleTopPanelState(this.custPageRightTopPanel, this.custPagePropertyPanel);
        }

        private void custPageSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.mCustPageSplitterRatio = (float)this.custPageSplitter.SplitPosition / (float)base.Width;
        }

        public static void DebugEntry()
        {
            Parser.Initialize();
            MainForm.mainTab = new MainForm();
            MainForm.mainTab.InitForCustomization();
            MainForm.mainTab.tabControl.SelectedIndex = 1;
            MainForm.mainTab.ShowDialog();
            Parser.Terminate();
        }

        public static string decoratedName(string imageName, CustomizationSection cui)
        {
            string lower = Path.ChangeExtension(cui.CUIFileName, "dll");
            lower = lower.ToLower();
            return string.Concat(string.Concat(lower, "|"), imageName);
        }

        protected override void Dispose(bool disposing)
        {
            MainForm.mainTab = null;
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
                if (this.mDlgMode == MainForm.DlgMode.TransferOnly && this.customizePage != null)
                {
                    this.customizePage.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void Document_DocumentOpened(object sender, EventArgs e)
        {
            Document document = sender as Document;
            if (document != null && document.CUIFile != null)
            {
                document.CUIFile.Modified += new EventHandler(this.CUIFile_Modified);
            }
        }

        private void doFindReplace(CommandListControl clc, UidPair uidPair, bool replace)
        {
            TreeViewControl treeViewControlFor = this.getTreeViewControlFor(clc);
            PropertyControl propertyControlFor = this.getPropertyControlFor(clc);
            bool flag = false;
            FindReplaceInterface findReplaceInterface = new FindReplaceInterface(clc, treeViewControlFor.TreeView, propertyControlFor, uidPair, replace, flag, true);
        }

        public void doFindReplace2(TreeView treeview, bool replace)
        {
            bool flag = false;
            PropertyControl propertyControl = null;
            if (this.customizeTreeView.TreeViewControl.TreeView == treeview)
            {
                flag = true;
                propertyControl = this.custPagePropertyControl;
            }
            FindReplaceInterface findReplaceInterface = new FindReplaceInterface(this.custPageCmdListCtrl, treeview, propertyControl, null, replace, flag, false);
        }

        private void DoResetMenuMacroToDefaults(UidPair uidPair)
        {
            CustomizationSection cui = uidPair.Cui;
            MenuMacro menuMacro = cui.getMenuMacro(uidPair.Uid);
            menuMacro.ResetToDefault();
            if (this.CustPagePropertyControl != null)
            {
                this.showButtonForMacro(cui, menuMacro.macro);
                this.CustPagePropertyControl.SetProperties(menuMacro, null);
            }
        }

        public static void ExportCUICommand()
        {
            bool flag = false;
            if (!MainForm.gotMainCUIFile())
            {
                MainForm.ShowAlert(LocalResources.GetString("DLG_No_CUI_File_Msg"));
                flag = true;
            }
            MainForm mainForm = new MainForm();
            MainForm.mainTab = mainForm;
            using (mainForm)
            {
                if (!flag)
                {
                    MainForm.mainTab.InitForExport();
                }
                else
                {
                    MainForm.mainTab.InitForTransferOnly();
                }
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Autodesk.AutoCAD.ApplicationServices.Application.MainWindow, MainForm.mainTab);
                MainForm.InitForCustomizeToolbar(null, null);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string extractResDllName(string str)
        {
            int num = str.IndexOf('|');
            if (num == -1)
            {
                return "";
            }
            return str.Substring(0, num);
        }

        internal void forceMenuReload()
        {
            this.applyButton.Enabled = true;
            this.bForceMenuReload = true;
        }

        private PropertyControl getPropertyControlFor(CommandListControl commandListControl)
        {
            if (commandListControl != this.custPageCmdListCtrl)
            {
                return null;
            }
            return this.custPagePropertyControl;
        }

        private CustomizationElement getShortcutObject(ShortcutsGroupEventArgs ea)
        {
            CustomizationElement menuObj = null;
            CustomizationSection cui = ea.Cui;
            if (cui != null)
            {
                menuObj = cui.getMenuObj(ea.Uid) as CustomizationElement;
            }
            return menuObj;
        }

        private Bitmap getStandaloneOrCustomResourceDllBitmap(CUITreeNode node, string smallImageName, string largeImageName)
        {
            if (BitmapCache.isValidResourceId(smallImageName))
            {
                return BitmapCache.GetImage(smallImageName, largeImageName, true);
            }
            string str = MainForm.decoratedName(smallImageName, node.CUIFile);
            string str1 = MainForm.decoratedName(largeImageName, node.CUIFile);
            return BitmapCache.GetImage(str, str1, true);
        }

        public TreeViewControl getTreeViewControlFor(CommandListControl commandListControl)
        {
            if (commandListControl != this.custPageCmdListCtrl)
            {
                return null;
            }
            return this.customizeTreeView.TreeViewControl;
        }

        private static bool gotMainCUIFile()
        {
            if (MainForm.MainCUIPath.Trim() == string.Empty)
            {
                return false;
            }
            return true;
        }

        public void handleCommandListEvent(object from, CommandListEventArgs args)
        {
            CommandListControl commandListControl = from as CommandListControl;
            switch (args.Action)
            {
                case EventAction.Edit:
                case EventAction.Dropped:
                    {
                        return;
                    }
                case EventAction.Reset:
                    {
                        this.DoResetMenuMacroToDefaults(args.Selected[0]);
                        return;
                    }
                case EventAction.New:
                    {
                        if (from != this.custPageCmdListCtrl)
                        {
                            return;
                        }
                        DataRowView selectedItem = this.customizeTreeView.TheFileNavList.SelectedItem as DataRowView;
                        if (selectedItem == null)
                        {
                            return;
                        }
                        string itemArray = selectedItem.Row.ItemArray[0] as string;
                        string str = selectedItem.Row.ItemArray[1] as string;
                        CustomizationSection customizeMainCUIFile = null;
                        if (itemArray == LocalResources.GetString("DLG_AllCustomizationFiles"))
                        {
                            customizeMainCUIFile = this.CustomizeMainCUIFile;
                        }
                        else if (string.Compare(str, this.CustomizeMainCUIFile.CUIFileName, true) != 0)
                        {
                            Document partialDocument = Document.GetPartialDocument(str);
                            if (partialDocument != null)
                            {
                                customizeMainCUIFile = partialDocument.CUIFile;
                            }
                        }
                        else
                        {
                            customizeMainCUIFile = this.CustomizeMainCUIFile;
                        }
                        if (customizeMainCUIFile == null)
                        {
                            customizeMainCUIFile = this.CustomizeMainCUIFile;
                        }
                        CommandListInterface.AddNew(commandListControl, customizeMainCUIFile);
                        customizeMainCUIFile.MakeDirty();
                        return;
                    }
                case EventAction.Rename:
                    {
                        UidPair item = args.Selected[0];
                        CustomizationSection cui = item.Cui;
                        MenuMacro menuMacro = cui.getMenuMacro(item.Uid);
                        menuMacro.macro.Name = commandListControl.entryForUid(item).CommandName;
                        cui.MakeDirty();
                        PropertyControl propertyControlFor = this.getPropertyControlFor(commandListControl);
                        if (propertyControlFor == null)
                        {
                            return;
                        }
                        propertyControlFor.Refresh2();
                        return;
                    }
                case EventAction.Delete:
                    {
                        TreeViewControl treeViewControlFor = this.getTreeViewControlFor(commandListControl);
                        StringCollection stringCollections = new StringCollection();
                        ArrayList arrayLists = new ArrayList();
                        if (treeViewControlFor == null)
                        {
                            return;
                        }
                        SelectedUidList selectedUidList = new SelectedUidList();
                        MultiSelectTreeview treeView = treeViewControlFor.TreeView;
                        treeView.LoadNodes();
                        TreeNodeCollection nodes = treeView.Nodes;
                        foreach (UidPair selected in args.Selected)
                        {
                            TreeNode treeNode = null;
                            CustomizationSection customizationSection = selected.Cui;
                            if (customizationSection == null)
                            {
                                continue;
                            }
                            MenuMacro menuMacro1 = customizationSection.getMenuMacro(selected.Uid);
                            ArrayList listRefMacro = customizationSection.getListRefMacro(menuMacro1.ElementID);
                            if (listRefMacro != null && 0 < listRefMacro.Count)
                            {
                                foreach (object obj in listRefMacro)
                                {
                                    treeNode = treeView.FindTreeNodeTag(obj, nodes);
                                    if (treeNode == null)
                                    {
                                        continue;
                                    }
                                    treeView.SelectedNode = treeNode;
                                    if (!arrayLists.Contains(treeNode))
                                    {
                                        arrayLists.Add(treeNode);
                                    }
                                    if (stringCollections.Contains(menuMacro1.macro.Name))
                                    {
                                        continue;
                                    }
                                    stringCollections.Add(menuMacro1.macro.Name);
                                }
                            }
                            if (treeNode != null)
                            {
                                continue;
                            }
                            MacroGroup parent = menuMacro1.Parent;
                            int num = -1;
                            int num1 = parent.MenuMacros.IndexOf(menuMacro1);
                            num = num1;
                            if (num1 != -1)
                            {
                                parent.MenuMacros.Remove(num);
                            }
                            selectedUidList.Add(selected);
                            customizationSection.MakeDirty();
                        }
                        if (stringCollections.Count != 0)
                        {
                            treeView.SelectNodes(arrayLists);
                            MainForm.ShowMessageBox(LocalResources.GetString((args.Selected.Count > 1 ? "MSG_CANT_DEL_COMMANDS" : "MSG_CANT_DEL_COMMAND")), MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        }
                        if (0 >= selectedUidList.Count)
                        {
                            return;
                        }
                        commandListControl.Remove(selectedUidList);
                        return;
                    }
                case EventAction.Selected:
                    {
                        if (from != this.custPageCmdListCtrl)
                        {
                            return;
                        }
                        if (!this.custPageCmdListCtrl.ContainsFocus)
                        {
                            return;
                        }
                        this.restorePanelStatus();
                        this.highlightCommandListItem(args);
                        return;
                    }
                case EventAction.DoubleClick:
                    {
                        if (from != this.custPageCmdListCtrl)
                        {
                            return;
                        }
                        this.highlightCommandListItem(args);
                        return;
                    }
                case EventAction.Find:
                    {
                        this.doFindReplace(commandListControl, args.SelectedSingle, false);
                        return;
                    }
                case EventAction.Replace:
                    {
                        this.doFindReplace(commandListControl, args.SelectedSingle, true);
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        public void handlePropertyEvent(object from, PropertyEventArgs args)
        {
            if (args.Action == PropertyEventAction.ResetDefaults)
            {
                this.DoResetMenuMacroToDefaults(args.UidPair);
            }
        }

        private void handleSplitter(Splitter splitter, bool bShow)
        {
            if (bShow)
            {
                splitter.Show();
                return;
            }
            splitter.Hide();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            if (HostApplicationServices.Current != null)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.InvokeHelp(null, "CustomizeUserInterfaceDialog");
            }
        }

        private void hideMiscPanel(CUITreeNode nextNode)
        {
            if (nextNode != null)
            {
                if (nextNode.isShortcutNode() && this.shortcutsGroup.Visible)
                {
                    return;
                }
                Type type = nextNode.GetType();
                //if (type == typeof(CUIToolbarNode) || type == typeof(CUIToolbarItemNode) && !(nextNode.Tag is ToolbarControl) || type == typeof(CUIMenuItemNode))
                //{
                //    return;
                //}
            }
            this.hideMiscPanelControls();
            if (this.shortcutsGroup.Visible)
            {
                this.mShortcutHeight = this.shortcutsGroup.Height;
            }
            this.hideToolbarPreview();
            this.custPageRightPanelPropSplitter.Hide();
            this.custPageMiscControlPanel.Hide();
        }

        private void hideMiscPanelControls()
        {
            foreach (Control control in this.custPageMiscControlPanel.Controls)
            {
                control.Hide();
                control.Enabled = false;
            }
            this.hideToolbarPreview();
        }

        private void hideToolbarPreview()
        {
            this.toolbarPreview.previewExit();
            this.toolbarPreview.Hide();
            this.toolbarPreview.Enabled = false;
        }

        private void hideWorkspaceWindow(CUITreeNode nextNode)
        {
            if (nextNode is CUIWorkspaceNode)
            {
                return;
            }
            this.custPageRightTopPanel.Hide();
            this.custPageRightSplitter.Hide();
            this.workspaceTreeView.TreeViewControl.ItemSelected -= new TreeViewControl.TVCItemSelectedArgsEventHandler(this.Workspace_ItemSelected);
        }

        private void highlightCommandListItem(CommandListEventArgs args)
        {
            this.custPagePropertyControl.Clear();
            if (1 == args.Selected.Count)
            {
                UidPair item = args.Selected[0];
                CustomizationSection cui = item.Cui;
                if (cui == null)
                {
                    try
                    {
                        ControlType controlType = (ControlType)Enum.Parse(typeof(ControlType), item.Uid, false);
                        ToolbarControl toolbarControl = new ToolbarControl(controlType);
                        PropertyBag propertyBagWithObject = new PropertyBagWithObject(toolbarControl, cui);
                        toolbarControl.FillReadOnlyPropertyBag(propertyBagWithObject);
                        this.custPagePropertyControl.SetProperties2(propertyBagWithObject);
                    }
                    catch (Exception exception)
                    {
                    }
                }
                else
                {
                    MenuMacro menuMacro = cui.getMenuMacro(item.Uid);
                    if (menuMacro != null)
                    {
                        this.showButtonForMacro(cui, menuMacro.macro);
                        this.custPagePropertyControl.SetProperties(menuMacro, null);
                        return;
                    }
                }
            }
        }

        public static void ImportCUICommand()
        {
            bool flag = false;
            if (!MainForm.gotMainCUIFile())
            {
                MainForm.ShowAlert(LocalResources.GetString("DLG_No_CUI_File_Msg"));
                flag = true;
            }
            MainForm mainForm = new MainForm();
            MainForm.mainTab = mainForm;
            using (mainForm)
            {
                if (!flag)
                {
                    MainForm.mainTab.InitForImport();
                }
                else
                {
                    MainForm.mainTab.InitForTransferOnly();
                }
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Autodesk.AutoCAD.ApplicationServices.Application.MainWindow, MainForm.mainTab);
                MainForm.InitForCustomizeToolbar(null, null);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void impPageSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.mImpPageSplitterRatio = (float)this.transferPageSplitter.SplitPosition / (float)base.Width;
        }

        internal void InitForCustomization()
        {
            this.mDlgMode = MainForm.DlgMode.CUI;
        }

        internal static void InitForCustomizeToolbar(string menuGrp, string tbUid)
        {
            MainForm.mInitialToolbarMenuGroup = menuGrp;
            MainForm.mInitialToolbarUID = tbUid;
        }

        internal static void InitForCustomizeToolpalettes()
        {
            MainForm.mPaletteConfig = true;
        }

        internal void InitForExport()
        {
            this.mDlgMode = MainForm.DlgMode.Export;
        }

        internal void InitForImport()
        {
            this.mDlgMode = MainForm.DlgMode.Import;
        }

        internal void InitForTransferOnly()
        {
            this.mDlgMode = MainForm.DlgMode.TransferOnly;
            this.tabControl.Controls.Remove(this.customizePage);
        }

        internal void InitializeButtonControlImages()
        {
            if (this._bitmapCacheIntialized)
            {
                return;
            }
            Icon obj = (Icon)GlobalResources.GetObject("tbsmiley_large.ico");
            BitmapCache.SetNotFound(obj.ToBitmap());
            try
            {
                Utils.CUIStartTransferBitmaps();
                bool flag = Utils.CUIIsUsingSmallIcon();
                this.requestImagesFromAllMacros(flag);
                this.requestImagesFromAllFlyouts(flag);
                if (!flag)
                {
                    BitmapCache.ImageList.ImageSize = new Size(32, 32);
                    ListViewResize.IconColumnWidth = 36;
                }
                else
                {
                    BitmapCache.ImageList.ImageSize = new Size(16, 16);
                    ListViewResize.IconColumnWidth = 18;
                }
                Utils.CUIEndTransferBitmaps();
            }
            catch (Exception exception)
            {
                for (int i = 0; i < 48; i++)
                {
                    int num = i + 1;
                    i = num;
                    int num1 = num;
                    BitmapCache.AddImage(obj.ToBitmap(), string.Concat("a", num1.ToString()));
                }
            }
            this.buttonControl.SetImageList(BitmapCache.ImageList, BitmapCache.ResourceIds);
            this._bitmapCacheIntialized = true;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ResourceManager resourceManager = new ResourceManager(typeof(MainForm));
            this.mainMenu = new MainMenu();
            this.tabControl = new TabControl();
            this.customizePage = new TabPage();
            this.custPageRightPanel = new Panel();
            this.custPageRightBottomPropPanel = new Panel();
            this.custPagePropertyPanel = new CollapsiblePanel();
            this.custPagePropertyControl = new PropertyControl();
            this.custPageRightPanelPropSplitter = new Splitter();
            this.custPageMiscControlPanel = new CollapsiblePanel();
            this.buttonControl = new ButtonControl();
            this.toolbarPreview = new ToolbarPreview();
            this.shortcutsGroup = new ShortcutsGroup();
            this.custPageRightSplitter = new Splitter();
            this.custPageRightTopPanel = new CollapsiblePanel();
            this.workspaceTreeView = new WorkspaceTreeView();
            this.custPageSplitter = new Splitter();
            this.custPageLeftPanel = new Panel();
            this.custPageLeftBottomPanel = new CollapsiblePanel();
            this.custPageCmdListCtrl = new CommandListControl();
            this.custPageLeftSplitter = new Splitter();
            this.custPageLeftTopPanel = new CollapsiblePanel();
            this.customizeTreeView = new CustomizeTreeView();
            this.transferPage = new TabPage();
            this.transferPageRightPanel = new Panel();
            this.transferPageRightTopPanel = new CollapsiblePanel();
            this.rightTransferTreeView = new TransferTreeView();
            this.transferPageSplitter = new Splitter();
            this.transferPageLeftPanel = new Panel();
            this.transferPageLeftTopPanel = new CollapsiblePanel();
            this.leftTransferTreeView = new TransferTreeView();
            this.tabImgList = new ImageList(this.components);
            this.okButton = new Button();
            this.applyButton = new Button();
            this.cancelButton = new Button();
            this.helpButton = new Button();
            this.colorDialog1 = new ColorDialog();
            this.quickStartLink = new QuickStartLink();
            this.tabControl.SuspendLayout();
            this.customizePage.SuspendLayout();
            this.custPageRightPanel.SuspendLayout();
            this.custPageRightBottomPropPanel.SuspendLayout();
            this.custPagePropertyPanel.SuspendLayout();
            this.custPageMiscControlPanel.SuspendLayout();
            this.custPageRightTopPanel.SuspendLayout();
            this.custPageLeftPanel.SuspendLayout();
            this.custPageLeftBottomPanel.SuspendLayout();
            this.custPageLeftTopPanel.SuspendLayout();
            this.transferPage.SuspendLayout();
            this.transferPageRightPanel.SuspendLayout();
            this.transferPageRightTopPanel.SuspendLayout();
            this.transferPageLeftPanel.SuspendLayout();
            this.transferPageLeftTopPanel.SuspendLayout();
            base.SuspendLayout();
            this.mainMenu.RightToLeft = (RightToLeft)resourceManager.GetObject("mainMenu.RightToLeft");
            this.tabControl.AccessibleDescription = resourceManager.GetString("tabControl.AccessibleDescription");
            this.tabControl.AccessibleName = resourceManager.GetString("tabControl.AccessibleName");
            this.tabControl.Alignment = (TabAlignment)resourceManager.GetObject("tabControl.Alignment");
            this.tabControl.Anchor = (AnchorStyles)resourceManager.GetObject("tabControl.Anchor");
            this.tabControl.Appearance = (TabAppearance)resourceManager.GetObject("tabControl.Appearance");
            this.tabControl.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("tabControl.BackgroundImage");
            this.tabControl.Controls.Add(this.customizePage);
            this.tabControl.Controls.Add(this.transferPage);
            this.tabControl.Dock = (DockStyle)resourceManager.GetObject("tabControl.Dock");
            this.tabControl.Enabled = (bool)resourceManager.GetObject("tabControl.Enabled");
            this.tabControl.Font = (Font)resourceManager.GetObject("tabControl.Font");
            this.tabControl.ImageList = this.tabImgList;
            this.tabControl.ImeMode = (ImeMode)resourceManager.GetObject("tabControl.ImeMode");
            this.tabControl.ItemSize = (Size)resourceManager.GetObject("tabControl.ItemSize");
            this.tabControl.Location = (Point)resourceManager.GetObject("tabControl.Location");
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = (Point)resourceManager.GetObject("tabControl.Padding");
            this.tabControl.RightToLeft = (RightToLeft)resourceManager.GetObject("tabControl.RightToLeft");
            this.tabControl.SelectedIndex = 0;
            this.tabControl.ShowToolTips = (bool)resourceManager.GetObject("tabControl.ShowToolTips");
            this.tabControl.Size = (Size)resourceManager.GetObject("tabControl.Size");
            this.tabControl.TabIndex = (int)resourceManager.GetObject("tabControl.TabIndex");
            this.tabControl.Text = resourceManager.GetString("tabControl.Text");
            this.tabControl.Visible = (bool)resourceManager.GetObject("tabControl.Visible");
            this.tabControl.SelectedIndexChanged += new EventHandler(this.tabControl_SelectedIndexChanged);
            this.customizePage.AccessibleDescription = resourceManager.GetString("customizePage.AccessibleDescription");
            this.customizePage.AccessibleName = resourceManager.GetString("customizePage.AccessibleName");
            this.customizePage.Anchor = (AnchorStyles)resourceManager.GetObject("customizePage.Anchor");
            this.customizePage.AutoScroll = (bool)resourceManager.GetObject("customizePage.AutoScroll");
            this.customizePage.AutoScrollMargin = (Size)resourceManager.GetObject("customizePage.AutoScrollMargin");
            this.customizePage.AutoScrollMinSize = (Size)resourceManager.GetObject("customizePage.AutoScrollMinSize");
            this.customizePage.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("customizePage.BackgroundImage");
            this.customizePage.Controls.Add(this.custPageRightPanel);
            this.customizePage.Controls.Add(this.custPageSplitter);
            this.customizePage.Controls.Add(this.custPageLeftPanel);
            this.customizePage.Dock = (DockStyle)resourceManager.GetObject("customizePage.Dock");
            this.customizePage.Enabled = (bool)resourceManager.GetObject("customizePage.Enabled");
            this.customizePage.Font = (Font)resourceManager.GetObject("customizePage.Font");
            this.customizePage.ImageIndex = (int)resourceManager.GetObject("customizePage.ImageIndex");
            this.customizePage.ImeMode = (ImeMode)resourceManager.GetObject("customizePage.ImeMode");
            this.customizePage.Location = (Point)resourceManager.GetObject("customizePage.Location");
            this.customizePage.Name = "customizePage";
            this.customizePage.RightToLeft = (RightToLeft)resourceManager.GetObject("customizePage.RightToLeft");
            this.customizePage.Size = (Size)resourceManager.GetObject("customizePage.Size");
            this.customizePage.TabIndex = (int)resourceManager.GetObject("customizePage.TabIndex");
            this.customizePage.Text = resourceManager.GetString("customizePage.Text");
            this.customizePage.ToolTipText = resourceManager.GetString("customizePage.ToolTipText");
            this.customizePage.Visible = (bool)resourceManager.GetObject("customizePage.Visible");
            this.custPageRightPanel.AccessibleDescription = resourceManager.GetString("custPageRightPanel.AccessibleDescription");
            this.custPageRightPanel.AccessibleName = resourceManager.GetString("custPageRightPanel.AccessibleName");
            this.custPageRightPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPageRightPanel.Anchor");
            this.custPageRightPanel.AutoScroll = (bool)resourceManager.GetObject("custPageRightPanel.AutoScroll");
            this.custPageRightPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPageRightPanel.AutoScrollMargin");
            this.custPageRightPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageRightPanel.AutoScrollMinSize");
            this.custPageRightPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageRightPanel.BackgroundImage");
            this.custPageRightPanel.Controls.Add(this.custPageRightBottomPropPanel);
            this.custPageRightPanel.Controls.Add(this.custPageRightSplitter);
            this.custPageRightPanel.Controls.Add(this.custPageRightTopPanel);
            this.custPageRightPanel.Dock = (DockStyle)resourceManager.GetObject("custPageRightPanel.Dock");
            this.custPageRightPanel.Enabled = (bool)resourceManager.GetObject("custPageRightPanel.Enabled");
            this.custPageRightPanel.Font = (Font)resourceManager.GetObject("custPageRightPanel.Font");
            this.custPageRightPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPageRightPanel.ImeMode");
            this.custPageRightPanel.Location = (Point)resourceManager.GetObject("custPageRightPanel.Location");
            this.custPageRightPanel.Name = "custPageRightPanel";
            this.custPageRightPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageRightPanel.RightToLeft");
            this.custPageRightPanel.Size = (Size)resourceManager.GetObject("custPageRightPanel.Size");
            this.custPageRightPanel.TabIndex = (int)resourceManager.GetObject("custPageRightPanel.TabIndex");
            this.custPageRightPanel.Text = resourceManager.GetString("custPageRightPanel.Text");
            this.custPageRightPanel.Visible = (bool)resourceManager.GetObject("custPageRightPanel.Visible");
            this.custPageRightBottomPropPanel.AccessibleDescription = resourceManager.GetString("custPageRightBottomPropPanel.AccessibleDescription");
            this.custPageRightBottomPropPanel.AccessibleName = resourceManager.GetString("custPageRightBottomPropPanel.AccessibleName");
            this.custPageRightBottomPropPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPageRightBottomPropPanel.Anchor");
            this.custPageRightBottomPropPanel.AutoScroll = (bool)resourceManager.GetObject("custPageRightBottomPropPanel.AutoScroll");
            this.custPageRightBottomPropPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPageRightBottomPropPanel.AutoScrollMargin");
            this.custPageRightBottomPropPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageRightBottomPropPanel.AutoScrollMinSize");
            this.custPageRightBottomPropPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageRightBottomPropPanel.BackgroundImage");
            this.custPageRightBottomPropPanel.Controls.Add(this.custPagePropertyPanel);
            this.custPageRightBottomPropPanel.Controls.Add(this.custPageRightPanelPropSplitter);
            this.custPageRightBottomPropPanel.Controls.Add(this.custPageMiscControlPanel);
            this.custPageRightBottomPropPanel.Dock = (DockStyle)resourceManager.GetObject("custPageRightBottomPropPanel.Dock");
            this.custPageRightBottomPropPanel.Enabled = (bool)resourceManager.GetObject("custPageRightBottomPropPanel.Enabled");
            this.custPageRightBottomPropPanel.Font = (Font)resourceManager.GetObject("custPageRightBottomPropPanel.Font");
            this.custPageRightBottomPropPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPageRightBottomPropPanel.ImeMode");
            this.custPageRightBottomPropPanel.Location = (Point)resourceManager.GetObject("custPageRightBottomPropPanel.Location");
            this.custPageRightBottomPropPanel.Name = "custPageRightBottomPropPanel";
            this.custPageRightBottomPropPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageRightBottomPropPanel.RightToLeft");
            this.custPageRightBottomPropPanel.Size = (Size)resourceManager.GetObject("custPageRightBottomPropPanel.Size");
            this.custPageRightBottomPropPanel.TabIndex = (int)resourceManager.GetObject("custPageRightBottomPropPanel.TabIndex");
            this.custPageRightBottomPropPanel.Text = resourceManager.GetString("custPageRightBottomPropPanel.Text");
            this.custPageRightBottomPropPanel.Visible = (bool)resourceManager.GetObject("custPageRightBottomPropPanel.Visible");
            this.custPagePropertyPanel.AccessibleDescription = resourceManager.GetString("custPagePropertyPanel.AccessibleDescription");
            this.custPagePropertyPanel.AccessibleName = resourceManager.GetString("custPagePropertyPanel.AccessibleName");
            this.custPagePropertyPanel.AllowDrop = true;
            this.custPagePropertyPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPagePropertyPanel.Anchor");
            this.custPagePropertyPanel.AutoScroll = (bool)resourceManager.GetObject("custPagePropertyPanel.AutoScroll");
            this.custPagePropertyPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPagePropertyPanel.AutoScrollMargin");
            this.custPagePropertyPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPagePropertyPanel.AutoScrollMinSize");
            this.custPagePropertyPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPagePropertyPanel.BackgroundImage");
            this.custPagePropertyPanel.Collapsible = true;
            this.custPagePropertyPanel.Controls.Add(this.custPagePropertyControl);
            this.custPagePropertyPanel.Dock = (DockStyle)resourceManager.GetObject("custPagePropertyPanel.Dock");
            this.custPagePropertyPanel.DockPadding.Bottom = 4;
            this.custPagePropertyPanel.DockPadding.Left = 4;
            this.custPagePropertyPanel.DockPadding.Right = 4;
            this.custPagePropertyPanel.DockPadding.Top = 44;
            this.custPagePropertyPanel.Enabled = (bool)resourceManager.GetObject("custPagePropertyPanel.Enabled");
            this.custPagePropertyPanel.EnableExpanded = true;
            this.custPagePropertyPanel.Expanded = true;
            this.custPagePropertyPanel.Font = (Font)resourceManager.GetObject("custPagePropertyPanel.Font");
            this.custPagePropertyPanel.HeaderText = resourceManager.GetString("custPagePropertyPanel.HeaderText");
            this.custPagePropertyPanel.Icon = (Icon)resourceManager.GetObject("custPagePropertyPanel.Icon");
            this.custPagePropertyPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPagePropertyPanel.ImeMode");
            this.custPagePropertyPanel.Location = (Point)resourceManager.GetObject("custPagePropertyPanel.Location");
            this.custPagePropertyPanel.MinimumHeight = 0;
            this.custPagePropertyPanel.Name = "custPagePropertyPanel";
            this.custPagePropertyPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPagePropertyPanel.RightToLeft");
            this.custPagePropertyPanel.Size = (Size)resourceManager.GetObject("custPagePropertyPanel.Size");
            this.custPagePropertyPanel.TabIndex = (int)resourceManager.GetObject("custPagePropertyPanel.TabIndex");
            this.custPagePropertyPanel.Visible = (bool)resourceManager.GetObject("custPagePropertyPanel.Visible");
            this.custPagePropertyControl.AccessibleDescription = resourceManager.GetString("custPagePropertyControl.AccessibleDescription");
            this.custPagePropertyControl.AccessibleName = resourceManager.GetString("custPagePropertyControl.AccessibleName");
            this.custPagePropertyControl.Anchor = (AnchorStyles)resourceManager.GetObject("custPagePropertyControl.Anchor");
            this.custPagePropertyControl.AutoScroll = (bool)resourceManager.GetObject("custPagePropertyControl.AutoScroll");
            this.custPagePropertyControl.AutoScrollMargin = (Size)resourceManager.GetObject("custPagePropertyControl.AutoScrollMargin");
            this.custPagePropertyControl.AutoScrollMinSize = (Size)resourceManager.GetObject("custPagePropertyControl.AutoScrollMinSize");
            this.custPagePropertyControl.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPagePropertyControl.BackgroundImage");
            this.custPagePropertyControl.Dock = (DockStyle)resourceManager.GetObject("custPagePropertyControl.Dock");
            this.custPagePropertyControl.Enabled = (bool)resourceManager.GetObject("custPagePropertyControl.Enabled");
            this.custPagePropertyControl.Font = (Font)resourceManager.GetObject("custPagePropertyControl.Font");
            this.custPagePropertyControl.ImeMode = (ImeMode)resourceManager.GetObject("custPagePropertyControl.ImeMode");
            this.custPagePropertyControl.Location = (Point)resourceManager.GetObject("custPagePropertyControl.Location");
            this.custPagePropertyControl.Name = "custPagePropertyControl";
            this.custPagePropertyControl.RightToLeft = (RightToLeft)resourceManager.GetObject("custPagePropertyControl.RightToLeft");
            this.custPagePropertyControl.Size = (Size)resourceManager.GetObject("custPagePropertyControl.Size");
            this.custPagePropertyControl.TabIndex = (int)resourceManager.GetObject("custPagePropertyControl.TabIndex");
            this.custPagePropertyControl.Visible = (bool)resourceManager.GetObject("custPagePropertyControl.Visible");
            this.custPagePropertyControl.EventHandler += new PropertyControl.EventDelegate(this.handlePropertyEvent);
            this.custPageRightPanelPropSplitter.AccessibleDescription = resourceManager.GetString("custPageRightPanelPropSplitter.AccessibleDescription");
            this.custPageRightPanelPropSplitter.AccessibleName = resourceManager.GetString("custPageRightPanelPropSplitter.AccessibleName");
            this.custPageRightPanelPropSplitter.Anchor = (AnchorStyles)resourceManager.GetObject("custPageRightPanelPropSplitter.Anchor");
            this.custPageRightPanelPropSplitter.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageRightPanelPropSplitter.BackgroundImage");
            this.custPageRightPanelPropSplitter.Dock = (DockStyle)resourceManager.GetObject("custPageRightPanelPropSplitter.Dock");
            this.custPageRightPanelPropSplitter.Enabled = (bool)resourceManager.GetObject("custPageRightPanelPropSplitter.Enabled");
            this.custPageRightPanelPropSplitter.Font = (Font)resourceManager.GetObject("custPageRightPanelPropSplitter.Font");
            this.custPageRightPanelPropSplitter.ImeMode = (ImeMode)resourceManager.GetObject("custPageRightPanelPropSplitter.ImeMode");
            this.custPageRightPanelPropSplitter.Location = (Point)resourceManager.GetObject("custPageRightPanelPropSplitter.Location");
            this.custPageRightPanelPropSplitter.MinExtra = (int)resourceManager.GetObject("custPageRightPanelPropSplitter.MinExtra");
            this.custPageRightPanelPropSplitter.MinSize = (int)resourceManager.GetObject("custPageRightPanelPropSplitter.MinSize");
            this.custPageRightPanelPropSplitter.Name = "custPageRightPanelPropSplitter";
            this.custPageRightPanelPropSplitter.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageRightPanelPropSplitter.RightToLeft");
            this.custPageRightPanelPropSplitter.Size = (Size)resourceManager.GetObject("custPageRightPanelPropSplitter.Size");
            this.custPageRightPanelPropSplitter.TabIndex = (int)resourceManager.GetObject("custPageRightPanelPropSplitter.TabIndex");
            this.custPageRightPanelPropSplitter.TabStop = false;
            this.custPageRightPanelPropSplitter.Visible = (bool)resourceManager.GetObject("custPageRightPanelPropSplitter.Visible");
            this.custPageRightPanelPropSplitter.Move += new EventHandler(this.custPageRightPanelPropSplitter_Move);
            this.custPageMiscControlPanel.AccessibleDescription = resourceManager.GetString("custPageMiscControlPanel.AccessibleDescription");
            this.custPageMiscControlPanel.AccessibleName = resourceManager.GetString("custPageMiscControlPanel.AccessibleName");
            this.custPageMiscControlPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPageMiscControlPanel.Anchor");
            this.custPageMiscControlPanel.AutoScroll = (bool)resourceManager.GetObject("custPageMiscControlPanel.AutoScroll");
            this.custPageMiscControlPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPageMiscControlPanel.AutoScrollMargin");
            this.custPageMiscControlPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageMiscControlPanel.AutoScrollMinSize");
            this.custPageMiscControlPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageMiscControlPanel.BackgroundImage");
            this.custPageMiscControlPanel.Collapsible = true;
            this.custPageMiscControlPanel.Controls.Add(this.buttonControl);
            this.custPageMiscControlPanel.Controls.Add(this.toolbarPreview);
            this.custPageMiscControlPanel.Controls.Add(this.shortcutsGroup);
            this.custPageMiscControlPanel.Dock = (DockStyle)resourceManager.GetObject("custPageMiscControlPanel.Dock");
            this.custPageMiscControlPanel.DockPadding.Bottom = 4;
            this.custPageMiscControlPanel.DockPadding.Left = 4;
            this.custPageMiscControlPanel.DockPadding.Right = 4;
            this.custPageMiscControlPanel.DockPadding.Top = 4;
            this.custPageMiscControlPanel.Enabled = (bool)resourceManager.GetObject("custPageMiscControlPanel.Enabled");
            this.custPageMiscControlPanel.EnableExpanded = true;
            this.custPageMiscControlPanel.Expanded = true;
            this.custPageMiscControlPanel.Font = (Font)resourceManager.GetObject("custPageMiscControlPanel.Font");
            this.custPageMiscControlPanel.HeaderText = resourceManager.GetString("custPageMiscControlPanel.HeaderText");
            this.custPageMiscControlPanel.Icon = (Icon)resourceManager.GetObject("custPageMiscControlPanel.Icon");
            this.custPageMiscControlPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPageMiscControlPanel.ImeMode");
            this.custPageMiscControlPanel.Location = (Point)resourceManager.GetObject("custPageMiscControlPanel.Location");
            this.custPageMiscControlPanel.MinimumHeight = 0;
            this.custPageMiscControlPanel.Name = "custPageMiscControlPanel";
            this.custPageMiscControlPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageMiscControlPanel.RightToLeft");
            this.custPageMiscControlPanel.Size = (Size)resourceManager.GetObject("custPageMiscControlPanel.Size");
            this.custPageMiscControlPanel.TabIndex = (int)resourceManager.GetObject("custPageMiscControlPanel.TabIndex");
            this.custPageMiscControlPanel.Visible = (bool)resourceManager.GetObject("custPageMiscControlPanel.Visible");
            this.buttonControl.AccessibleDescription = resourceManager.GetString("buttonControl.AccessibleDescription");
            this.buttonControl.AccessibleName = resourceManager.GetString("buttonControl.AccessibleName");
            this.buttonControl.Anchor = (AnchorStyles)resourceManager.GetObject("buttonControl.Anchor");
            this.buttonControl.AutoScroll = (bool)resourceManager.GetObject("buttonControl.AutoScroll");
            this.buttonControl.AutoScrollMargin = (Size)resourceManager.GetObject("buttonControl.AutoScrollMargin");
            this.buttonControl.AutoScrollMinSize = (Size)resourceManager.GetObject("buttonControl.AutoScrollMinSize");
            this.buttonControl.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("buttonControl.BackgroundImage");
            this.buttonControl.Dock = (DockStyle)resourceManager.GetObject("buttonControl.Dock");
            this.buttonControl.Enabled = (bool)resourceManager.GetObject("buttonControl.Enabled");
            this.buttonControl.Font = (Font)resourceManager.GetObject("buttonControl.Font");
            this.buttonControl.ImeMode = (ImeMode)resourceManager.GetObject("buttonControl.ImeMode");
            this.buttonControl.Location = (Point)resourceManager.GetObject("buttonControl.Location");
            this.buttonControl.Name = "buttonControl";
            this.buttonControl.RightToLeft = (RightToLeft)resourceManager.GetObject("buttonControl.RightToLeft");
            this.buttonControl.Size = (Size)resourceManager.GetObject("buttonControl.Size");
            this.buttonControl.TabIndex = (int)resourceManager.GetObject("buttonControl.TabIndex");
            this.buttonControl.Visible = (bool)resourceManager.GetObject("buttonControl.Visible");
            this.buttonControl.OnEvent += new ButtonControlEventHandler(this.buttonControlEventHandler);
            this.toolbarPreview.AccessibleDescription = resourceManager.GetString("toolbarPreview.AccessibleDescription");
            this.toolbarPreview.AccessibleName = resourceManager.GetString("toolbarPreview.AccessibleName");
            this.toolbarPreview.Anchor = (AnchorStyles)resourceManager.GetObject("toolbarPreview.Anchor");
            this.toolbarPreview.AutoScroll = (bool)resourceManager.GetObject("toolbarPreview.AutoScroll");
            this.toolbarPreview.AutoScrollMargin = (Size)resourceManager.GetObject("toolbarPreview.AutoScrollMargin");
            this.toolbarPreview.AutoScrollMinSize = (Size)resourceManager.GetObject("toolbarPreview.AutoScrollMinSize");
            this.toolbarPreview.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("toolbarPreview.BackgroundImage");
            this.toolbarPreview.Dock = (DockStyle)resourceManager.GetObject("toolbarPreview.Dock");
            this.toolbarPreview.Enabled = (bool)resourceManager.GetObject("toolbarPreview.Enabled");
            this.toolbarPreview.Font = (Font)resourceManager.GetObject("toolbarPreview.Font");
            this.toolbarPreview.ImeMode = (ImeMode)resourceManager.GetObject("toolbarPreview.ImeMode");
            this.toolbarPreview.Location = (Point)resourceManager.GetObject("toolbarPreview.Location");
            this.toolbarPreview.Name = "toolbarPreview";
            this.toolbarPreview.RightToLeft = (RightToLeft)resourceManager.GetObject("toolbarPreview.RightToLeft");
            this.toolbarPreview.Size = (Size)resourceManager.GetObject("toolbarPreview.Size");
            this.toolbarPreview.TabIndex = (int)resourceManager.GetObject("toolbarPreview.TabIndex");
            this.toolbarPreview.Visible = (bool)resourceManager.GetObject("toolbarPreview.Visible");
            this.shortcutsGroup.AccessibleDescription = resourceManager.GetString("shortcutsGroup.AccessibleDescription");
            this.shortcutsGroup.AccessibleName = resourceManager.GetString("shortcutsGroup.AccessibleName");
            this.shortcutsGroup.Anchor = (AnchorStyles)resourceManager.GetObject("shortcutsGroup.Anchor");
            this.shortcutsGroup.AutoScroll = (bool)resourceManager.GetObject("shortcutsGroup.AutoScroll");
            this.shortcutsGroup.AutoScrollMargin = (Size)resourceManager.GetObject("shortcutsGroup.AutoScrollMargin");
            this.shortcutsGroup.AutoScrollMinSize = (Size)resourceManager.GetObject("shortcutsGroup.AutoScrollMinSize");
            this.shortcutsGroup.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("shortcutsGroup.BackgroundImage");
            this.shortcutsGroup.Dock = (DockStyle)resourceManager.GetObject("shortcutsGroup.Dock");
            this.shortcutsGroup.Enabled = (bool)resourceManager.GetObject("shortcutsGroup.Enabled");
            this.shortcutsGroup.Font = (Font)resourceManager.GetObject("shortcutsGroup.Font");
            this.shortcutsGroup.ImeMode = (ImeMode)resourceManager.GetObject("shortcutsGroup.ImeMode");
            this.shortcutsGroup.Location = (Point)resourceManager.GetObject("shortcutsGroup.Location");
            this.shortcutsGroup.Name = "shortcutsGroup";
            this.shortcutsGroup.RightToLeft = (RightToLeft)resourceManager.GetObject("shortcutsGroup.RightToLeft");
            this.shortcutsGroup.Size = (Size)resourceManager.GetObject("shortcutsGroup.Size");
            this.shortcutsGroup.TabIndex = (int)resourceManager.GetObject("shortcutsGroup.TabIndex");
            this.shortcutsGroup.Visible = (bool)resourceManager.GetObject("shortcutsGroup.Visible");
            this.shortcutsGroup.OnEvent += new ShortcutsGroupEventHandler(this.shortcutsGroupEventHandler);
            this.custPageRightSplitter.AccessibleDescription = resourceManager.GetString("custPageRightSplitter.AccessibleDescription");
            this.custPageRightSplitter.AccessibleName = resourceManager.GetString("custPageRightSplitter.AccessibleName");
            this.custPageRightSplitter.Anchor = (AnchorStyles)resourceManager.GetObject("custPageRightSplitter.Anchor");
            this.custPageRightSplitter.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageRightSplitter.BackgroundImage");
            this.custPageRightSplitter.Dock = (DockStyle)resourceManager.GetObject("custPageRightSplitter.Dock");
            this.custPageRightSplitter.Enabled = (bool)resourceManager.GetObject("custPageRightSplitter.Enabled");
            this.custPageRightSplitter.Font = (Font)resourceManager.GetObject("custPageRightSplitter.Font");
            this.custPageRightSplitter.ImeMode = (ImeMode)resourceManager.GetObject("custPageRightSplitter.ImeMode");
            this.custPageRightSplitter.Location = (Point)resourceManager.GetObject("custPageRightSplitter.Location");
            this.custPageRightSplitter.MinExtra = (int)resourceManager.GetObject("custPageRightSplitter.MinExtra");
            this.custPageRightSplitter.MinSize = (int)resourceManager.GetObject("custPageRightSplitter.MinSize");
            this.custPageRightSplitter.Name = "custPageRightSplitter";
            this.custPageRightSplitter.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageRightSplitter.RightToLeft");
            this.custPageRightSplitter.Size = (Size)resourceManager.GetObject("custPageRightSplitter.Size");
            this.custPageRightSplitter.TabIndex = (int)resourceManager.GetObject("custPageRightSplitter.TabIndex");
            this.custPageRightSplitter.TabStop = false;
            this.custPageRightSplitter.Visible = (bool)resourceManager.GetObject("custPageRightSplitter.Visible");
            this.custPageRightTopPanel.AccessibleDescription = resourceManager.GetString("custPageRightTopPanel.AccessibleDescription");
            this.custPageRightTopPanel.AccessibleName = resourceManager.GetString("custPageRightTopPanel.AccessibleName");
            this.custPageRightTopPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPageRightTopPanel.Anchor");
            this.custPageRightTopPanel.AutoScroll = (bool)resourceManager.GetObject("custPageRightTopPanel.AutoScroll");
            this.custPageRightTopPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPageRightTopPanel.AutoScrollMargin");
            this.custPageRightTopPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageRightTopPanel.AutoScrollMinSize");
            this.custPageRightTopPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageRightTopPanel.BackgroundImage");
            this.custPageRightTopPanel.Collapsible = true;
            this.custPageRightTopPanel.Controls.Add(this.workspaceTreeView);
            this.custPageRightTopPanel.Dock = (DockStyle)resourceManager.GetObject("custPageRightTopPanel.Dock");
            this.custPageRightTopPanel.DockPadding.Bottom = 4;
            this.custPageRightTopPanel.DockPadding.Left = 4;
            this.custPageRightTopPanel.DockPadding.Right = 4;
            this.custPageRightTopPanel.DockPadding.Top = 44;
            this.custPageRightTopPanel.Enabled = (bool)resourceManager.GetObject("custPageRightTopPanel.Enabled");
            this.custPageRightTopPanel.EnableExpanded = true;
            this.custPageRightTopPanel.Expanded = true;
            this.custPageRightTopPanel.Font = (Font)resourceManager.GetObject("custPageRightTopPanel.Font");
            this.custPageRightTopPanel.HeaderText = resourceManager.GetString("custPageRightTopPanel.HeaderText");
            this.custPageRightTopPanel.Icon = (Icon)resourceManager.GetObject("custPageRightTopPanel.Icon");
            this.custPageRightTopPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPageRightTopPanel.ImeMode");
            this.custPageRightTopPanel.Location = (Point)resourceManager.GetObject("custPageRightTopPanel.Location");
            this.custPageRightTopPanel.MinimumHeight = 0;
            this.custPageRightTopPanel.Name = "custPageRightTopPanel";
            this.custPageRightTopPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageRightTopPanel.RightToLeft");
            this.custPageRightTopPanel.Size = (Size)resourceManager.GetObject("custPageRightTopPanel.Size");
            this.custPageRightTopPanel.TabIndex = (int)resourceManager.GetObject("custPageRightTopPanel.TabIndex");
            this.custPageRightTopPanel.Visible = (bool)resourceManager.GetObject("custPageRightTopPanel.Visible");
            this.workspaceTreeView.AccessibleDescription = resourceManager.GetString("workspaceTreeView.AccessibleDescription");
            this.workspaceTreeView.AccessibleName = resourceManager.GetString("workspaceTreeView.AccessibleName");
            this.workspaceTreeView.Anchor = (AnchorStyles)resourceManager.GetObject("workspaceTreeView.Anchor");
            this.workspaceTreeView.AutoScroll = (bool)resourceManager.GetObject("workspaceTreeView.AutoScroll");
            this.workspaceTreeView.AutoScrollMargin = (Size)resourceManager.GetObject("workspaceTreeView.AutoScrollMargin");
            this.workspaceTreeView.AutoScrollMinSize = (Size)resourceManager.GetObject("workspaceTreeView.AutoScrollMinSize");
            this.workspaceTreeView.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("workspaceTreeView.BackgroundImage");
            this.workspaceTreeView.Dock = (DockStyle)resourceManager.GetObject("workspaceTreeView.Dock");
            this.workspaceTreeView.Enabled = (bool)resourceManager.GetObject("workspaceTreeView.Enabled");
            this.workspaceTreeView.Font = (Font)resourceManager.GetObject("workspaceTreeView.Font");
            this.workspaceTreeView.ImeMode = (ImeMode)resourceManager.GetObject("workspaceTreeView.ImeMode");
            this.workspaceTreeView.Location = (Point)resourceManager.GetObject("workspaceTreeView.Location");
            this.workspaceTreeView.Name = "workspaceTreeView";
            this.workspaceTreeView.RightToLeft = (RightToLeft)resourceManager.GetObject("workspaceTreeView.RightToLeft");
            this.workspaceTreeView.Size = (Size)resourceManager.GetObject("workspaceTreeView.Size");
            this.workspaceTreeView.TabIndex = (int)resourceManager.GetObject("workspaceTreeView.TabIndex");
            this.workspaceTreeView.Visible = (bool)resourceManager.GetObject("workspaceTreeView.Visible");
            this.custPageSplitter.AccessibleDescription = resourceManager.GetString("custPageSplitter.AccessibleDescription");
            this.custPageSplitter.AccessibleName = resourceManager.GetString("custPageSplitter.AccessibleName");
            this.custPageSplitter.Anchor = (AnchorStyles)resourceManager.GetObject("custPageSplitter.Anchor");
            this.custPageSplitter.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageSplitter.BackgroundImage");
            this.custPageSplitter.Dock = (DockStyle)resourceManager.GetObject("custPageSplitter.Dock");
            this.custPageSplitter.Enabled = (bool)resourceManager.GetObject("custPageSplitter.Enabled");
            this.custPageSplitter.Font = (Font)resourceManager.GetObject("custPageSplitter.Font");
            this.custPageSplitter.ImeMode = (ImeMode)resourceManager.GetObject("custPageSplitter.ImeMode");
            this.custPageSplitter.Location = (Point)resourceManager.GetObject("custPageSplitter.Location");
            this.custPageSplitter.MinExtra = (int)resourceManager.GetObject("custPageSplitter.MinExtra");
            this.custPageSplitter.MinSize = (int)resourceManager.GetObject("custPageSplitter.MinSize");
            this.custPageSplitter.Name = "custPageSplitter";
            this.custPageSplitter.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageSplitter.RightToLeft");
            this.custPageSplitter.Size = (Size)resourceManager.GetObject("custPageSplitter.Size");
            this.custPageSplitter.TabIndex = (int)resourceManager.GetObject("custPageSplitter.TabIndex");
            this.custPageSplitter.TabStop = false;
            this.custPageSplitter.Visible = (bool)resourceManager.GetObject("custPageSplitter.Visible");
            this.custPageSplitter.SplitterMoved += new SplitterEventHandler(this.custPageSplitter_SplitterMoved);
            this.custPageLeftPanel.AccessibleDescription = resourceManager.GetString("custPageLeftPanel.AccessibleDescription");
            this.custPageLeftPanel.AccessibleName = resourceManager.GetString("custPageLeftPanel.AccessibleName");
            this.custPageLeftPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPageLeftPanel.Anchor");
            this.custPageLeftPanel.AutoScroll = (bool)resourceManager.GetObject("custPageLeftPanel.AutoScroll");
            this.custPageLeftPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPageLeftPanel.AutoScrollMargin");
            this.custPageLeftPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageLeftPanel.AutoScrollMinSize");
            this.custPageLeftPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageLeftPanel.BackgroundImage");
            this.custPageLeftPanel.Controls.Add(this.custPageLeftBottomPanel);
            this.custPageLeftPanel.Controls.Add(this.custPageLeftSplitter);
            this.custPageLeftPanel.Controls.Add(this.custPageLeftTopPanel);
            this.custPageLeftPanel.Dock = (DockStyle)resourceManager.GetObject("custPageLeftPanel.Dock");
            this.custPageLeftPanel.Enabled = (bool)resourceManager.GetObject("custPageLeftPanel.Enabled");
            this.custPageLeftPanel.Font = (Font)resourceManager.GetObject("custPageLeftPanel.Font");
            this.custPageLeftPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPageLeftPanel.ImeMode");
            this.custPageLeftPanel.Location = (Point)resourceManager.GetObject("custPageLeftPanel.Location");
            this.custPageLeftPanel.Name = "custPageLeftPanel";
            this.custPageLeftPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageLeftPanel.RightToLeft");
            this.custPageLeftPanel.Size = (Size)resourceManager.GetObject("custPageLeftPanel.Size");
            this.custPageLeftPanel.TabIndex = (int)resourceManager.GetObject("custPageLeftPanel.TabIndex");
            this.custPageLeftPanel.Text = resourceManager.GetString("custPageLeftPanel.Text");
            this.custPageLeftPanel.Visible = (bool)resourceManager.GetObject("custPageLeftPanel.Visible");
            this.custPageLeftBottomPanel.AccessibleDescription = resourceManager.GetString("custPageLeftBottomPanel.AccessibleDescription");
            this.custPageLeftBottomPanel.AccessibleName = resourceManager.GetString("custPageLeftBottomPanel.AccessibleName");
            this.custPageLeftBottomPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPageLeftBottomPanel.Anchor");
            this.custPageLeftBottomPanel.AutoScroll = (bool)resourceManager.GetObject("custPageLeftBottomPanel.AutoScroll");
            this.custPageLeftBottomPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPageLeftBottomPanel.AutoScrollMargin");
            this.custPageLeftBottomPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageLeftBottomPanel.AutoScrollMinSize");
            this.custPageLeftBottomPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageLeftBottomPanel.BackgroundImage");
            this.custPageLeftBottomPanel.Collapsible = true;
            this.custPageLeftBottomPanel.Controls.Add(this.custPageCmdListCtrl);
            this.custPageLeftBottomPanel.Dock = (DockStyle)resourceManager.GetObject("custPageLeftBottomPanel.Dock");
            this.custPageLeftBottomPanel.DockPadding.Bottom = 4;
            this.custPageLeftBottomPanel.DockPadding.Left = 4;
            this.custPageLeftBottomPanel.DockPadding.Right = 4;
            this.custPageLeftBottomPanel.DockPadding.Top = 26;
            this.custPageLeftBottomPanel.Enabled = (bool)resourceManager.GetObject("custPageLeftBottomPanel.Enabled");
            this.custPageLeftBottomPanel.EnableExpanded = true;
            this.custPageLeftBottomPanel.Expanded = true;
            this.custPageLeftBottomPanel.Font = (Font)resourceManager.GetObject("custPageLeftBottomPanel.Font");
            this.custPageLeftBottomPanel.HeaderText = resourceManager.GetString("custPageLeftBottomPanel.HeaderText");
            this.custPageLeftBottomPanel.Icon = (Icon)resourceManager.GetObject("custPageLeftBottomPanel.Icon");
            this.custPageLeftBottomPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPageLeftBottomPanel.ImeMode");
            this.custPageLeftBottomPanel.Location = (Point)resourceManager.GetObject("custPageLeftBottomPanel.Location");
            this.custPageLeftBottomPanel.MinimumHeight = 0;
            this.custPageLeftBottomPanel.Name = "custPageLeftBottomPanel";
            this.custPageLeftBottomPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageLeftBottomPanel.RightToLeft");
            this.custPageLeftBottomPanel.Size = (Size)resourceManager.GetObject("custPageLeftBottomPanel.Size");
            this.custPageLeftBottomPanel.TabIndex = (int)resourceManager.GetObject("custPageLeftBottomPanel.TabIndex");
            this.custPageLeftBottomPanel.Visible = (bool)resourceManager.GetObject("custPageLeftBottomPanel.Visible");
            this.custPageCmdListCtrl.AccessibleDescription = resourceManager.GetString("custPageCmdListCtrl.AccessibleDescription");
            this.custPageCmdListCtrl.AccessibleName = resourceManager.GetString("custPageCmdListCtrl.AccessibleName");
            this.custPageCmdListCtrl.Anchor = (AnchorStyles)resourceManager.GetObject("custPageCmdListCtrl.Anchor");
            this.custPageCmdListCtrl.AutoScroll = (bool)resourceManager.GetObject("custPageCmdListCtrl.AutoScroll");
            this.custPageCmdListCtrl.AutoScrollMargin = (Size)resourceManager.GetObject("custPageCmdListCtrl.AutoScrollMargin");
            this.custPageCmdListCtrl.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageCmdListCtrl.AutoScrollMinSize");
            this.custPageCmdListCtrl.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageCmdListCtrl.BackgroundImage");
            this.custPageCmdListCtrl.Dock = (DockStyle)resourceManager.GetObject("custPageCmdListCtrl.Dock");
            this.custPageCmdListCtrl.Enabled = (bool)resourceManager.GetObject("custPageCmdListCtrl.Enabled");
            this.custPageCmdListCtrl.Font = (Font)resourceManager.GetObject("custPageCmdListCtrl.Font");
            this.custPageCmdListCtrl.ImeMode = (ImeMode)resourceManager.GetObject("custPageCmdListCtrl.ImeMode");
            this.custPageCmdListCtrl.Location = (Point)resourceManager.GetObject("custPageCmdListCtrl.Location");
            this.custPageCmdListCtrl.Name = "custPageCmdListCtrl";
            this.custPageCmdListCtrl.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageCmdListCtrl.RightToLeft");
            this.custPageCmdListCtrl.Size = (Size)resourceManager.GetObject("custPageCmdListCtrl.Size");
            this.custPageCmdListCtrl.TabIndex = (int)resourceManager.GetObject("custPageCmdListCtrl.TabIndex");
            this.custPageCmdListCtrl.Visible = (bool)resourceManager.GetObject("custPageCmdListCtrl.Visible");
            this.custPageCmdListCtrl.EventHandler += new CommandListControl.EventDelegate(this.handleCommandListEvent);
            this.custPageLeftSplitter.AccessibleDescription = resourceManager.GetString("custPageLeftSplitter.AccessibleDescription");
            this.custPageLeftSplitter.AccessibleName = resourceManager.GetString("custPageLeftSplitter.AccessibleName");
            this.custPageLeftSplitter.Anchor = (AnchorStyles)resourceManager.GetObject("custPageLeftSplitter.Anchor");
            this.custPageLeftSplitter.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageLeftSplitter.BackgroundImage");
            this.custPageLeftSplitter.Cursor = Cursors.HSplit;
            this.custPageLeftSplitter.Dock = (DockStyle)resourceManager.GetObject("custPageLeftSplitter.Dock");
            this.custPageLeftSplitter.Enabled = (bool)resourceManager.GetObject("custPageLeftSplitter.Enabled");
            this.custPageLeftSplitter.Font = (Font)resourceManager.GetObject("custPageLeftSplitter.Font");
            this.custPageLeftSplitter.ImeMode = (ImeMode)resourceManager.GetObject("custPageLeftSplitter.ImeMode");
            this.custPageLeftSplitter.Location = (Point)resourceManager.GetObject("custPageLeftSplitter.Location");
            this.custPageLeftSplitter.MinExtra = (int)resourceManager.GetObject("custPageLeftSplitter.MinExtra");
            this.custPageLeftSplitter.MinSize = (int)resourceManager.GetObject("custPageLeftSplitter.MinSize");
            this.custPageLeftSplitter.Name = "custPageLeftSplitter";
            this.custPageLeftSplitter.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageLeftSplitter.RightToLeft");
            this.custPageLeftSplitter.Size = (Size)resourceManager.GetObject("custPageLeftSplitter.Size");
            this.custPageLeftSplitter.TabIndex = (int)resourceManager.GetObject("custPageLeftSplitter.TabIndex");
            this.custPageLeftSplitter.TabStop = false;
            this.custPageLeftSplitter.Visible = (bool)resourceManager.GetObject("custPageLeftSplitter.Visible");
            this.custPageLeftTopPanel.AccessibleDescription = resourceManager.GetString("custPageLeftTopPanel.AccessibleDescription");
            this.custPageLeftTopPanel.AccessibleName = resourceManager.GetString("custPageLeftTopPanel.AccessibleName");
            this.custPageLeftTopPanel.Anchor = (AnchorStyles)resourceManager.GetObject("custPageLeftTopPanel.Anchor");
            this.custPageLeftTopPanel.AutoScroll = (bool)resourceManager.GetObject("custPageLeftTopPanel.AutoScroll");
            this.custPageLeftTopPanel.AutoScrollMargin = (Size)resourceManager.GetObject("custPageLeftTopPanel.AutoScrollMargin");
            this.custPageLeftTopPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("custPageLeftTopPanel.AutoScrollMinSize");
            this.custPageLeftTopPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("custPageLeftTopPanel.BackgroundImage");
            this.custPageLeftTopPanel.Collapsible = true;
            this.custPageLeftTopPanel.Controls.Add(this.customizeTreeView);
            this.custPageLeftTopPanel.Dock = (DockStyle)resourceManager.GetObject("custPageLeftTopPanel.Dock");
            this.custPageLeftTopPanel.DockPadding.Bottom = 4;
            this.custPageLeftTopPanel.DockPadding.Left = 4;
            this.custPageLeftTopPanel.DockPadding.Right = 4;
            this.custPageLeftTopPanel.DockPadding.Top = 44;
            this.custPageLeftTopPanel.Enabled = (bool)resourceManager.GetObject("custPageLeftTopPanel.Enabled");
            this.custPageLeftTopPanel.EnableExpanded = true;
            this.custPageLeftTopPanel.Expanded = true;
            this.custPageLeftTopPanel.Font = (Font)resourceManager.GetObject("custPageLeftTopPanel.Font");
            this.custPageLeftTopPanel.HeaderText = resourceManager.GetString("custPageLeftTopPanel.HeaderText");
            this.custPageLeftTopPanel.Icon = (Icon)resourceManager.GetObject("custPageLeftTopPanel.Icon");
            this.custPageLeftTopPanel.ImeMode = (ImeMode)resourceManager.GetObject("custPageLeftTopPanel.ImeMode");
            this.custPageLeftTopPanel.Location = (Point)resourceManager.GetObject("custPageLeftTopPanel.Location");
            this.custPageLeftTopPanel.MinimumHeight = 0;
            this.custPageLeftTopPanel.Name = "custPageLeftTopPanel";
            this.custPageLeftTopPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("custPageLeftTopPanel.RightToLeft");
            this.custPageLeftTopPanel.Size = (Size)resourceManager.GetObject("custPageLeftTopPanel.Size");
            this.custPageLeftTopPanel.TabIndex = (int)resourceManager.GetObject("custPageLeftTopPanel.TabIndex");
            this.custPageLeftTopPanel.Visible = (bool)resourceManager.GetObject("custPageLeftTopPanel.Visible");
            this.customizeTreeView.AccessibleDescription = resourceManager.GetString("customizeTreeView.AccessibleDescription");
            this.customizeTreeView.AccessibleName = resourceManager.GetString("customizeTreeView.AccessibleName");
            this.customizeTreeView.Anchor = (AnchorStyles)resourceManager.GetObject("customizeTreeView.Anchor");
            this.customizeTreeView.AutoScroll = (bool)resourceManager.GetObject("customizeTreeView.AutoScroll");
            this.customizeTreeView.AutoScrollMargin = (Size)resourceManager.GetObject("customizeTreeView.AutoScrollMargin");
            this.customizeTreeView.AutoScrollMinSize = (Size)resourceManager.GetObject("customizeTreeView.AutoScrollMinSize");
            this.customizeTreeView.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("customizeTreeView.BackgroundImage");
            this.customizeTreeView.Dock = (DockStyle)resourceManager.GetObject("customizeTreeView.Dock");
            this.customizeTreeView.Enabled = (bool)resourceManager.GetObject("customizeTreeView.Enabled");
            this.customizeTreeView.Font = (Font)resourceManager.GetObject("customizeTreeView.Font");
            this.customizeTreeView.ImeMode = (ImeMode)resourceManager.GetObject("customizeTreeView.ImeMode");
            this.customizeTreeView.Location = (Point)resourceManager.GetObject("customizeTreeView.Location");
            this.customizeTreeView.Name = "customizeTreeView";
            this.customizeTreeView.RightToLeft = (RightToLeft)resourceManager.GetObject("customizeTreeView.RightToLeft");
            this.customizeTreeView.Size = (Size)resourceManager.GetObject("customizeTreeView.Size");
            this.customizeTreeView.TabIndex = (int)resourceManager.GetObject("customizeTreeView.TabIndex");
            this.customizeTreeView.Visible = (bool)resourceManager.GetObject("customizeTreeView.Visible");
            this.transferPage.AccessibleDescription = resourceManager.GetString("transferPage.AccessibleDescription");
            this.transferPage.AccessibleName = resourceManager.GetString("transferPage.AccessibleName");
            this.transferPage.Anchor = (AnchorStyles)resourceManager.GetObject("transferPage.Anchor");
            this.transferPage.AutoScroll = (bool)resourceManager.GetObject("transferPage.AutoScroll");
            this.transferPage.AutoScrollMargin = (Size)resourceManager.GetObject("transferPage.AutoScrollMargin");
            this.transferPage.AutoScrollMinSize = (Size)resourceManager.GetObject("transferPage.AutoScrollMinSize");
            this.transferPage.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("transferPage.BackgroundImage");
            this.transferPage.Controls.Add(this.transferPageRightPanel);
            this.transferPage.Controls.Add(this.transferPageSplitter);
            this.transferPage.Controls.Add(this.transferPageLeftPanel);
            this.transferPage.Dock = (DockStyle)resourceManager.GetObject("transferPage.Dock");
            this.transferPage.Enabled = (bool)resourceManager.GetObject("transferPage.Enabled");
            this.transferPage.Font = (Font)resourceManager.GetObject("transferPage.Font");
            this.transferPage.ImageIndex = (int)resourceManager.GetObject("transferPage.ImageIndex");
            this.transferPage.ImeMode = (ImeMode)resourceManager.GetObject("transferPage.ImeMode");
            this.transferPage.Location = (Point)resourceManager.GetObject("transferPage.Location");
            this.transferPage.Name = "transferPage";
            this.transferPage.RightToLeft = (RightToLeft)resourceManager.GetObject("transferPage.RightToLeft");
            this.transferPage.Size = (Size)resourceManager.GetObject("transferPage.Size");
            this.transferPage.TabIndex = (int)resourceManager.GetObject("transferPage.TabIndex");
            this.transferPage.Text = resourceManager.GetString("transferPage.Text");
            this.transferPage.ToolTipText = resourceManager.GetString("transferPage.ToolTipText");
            this.transferPage.Visible = (bool)resourceManager.GetObject("transferPage.Visible");
            this.transferPageRightPanel.AccessibleDescription = resourceManager.GetString("transferPageRightPanel.AccessibleDescription");
            this.transferPageRightPanel.AccessibleName = resourceManager.GetString("transferPageRightPanel.AccessibleName");
            this.transferPageRightPanel.Anchor = (AnchorStyles)resourceManager.GetObject("transferPageRightPanel.Anchor");
            this.transferPageRightPanel.AutoScroll = (bool)resourceManager.GetObject("transferPageRightPanel.AutoScroll");
            this.transferPageRightPanel.AutoScrollMargin = (Size)resourceManager.GetObject("transferPageRightPanel.AutoScrollMargin");
            this.transferPageRightPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("transferPageRightPanel.AutoScrollMinSize");
            this.transferPageRightPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("transferPageRightPanel.BackgroundImage");
            this.transferPageRightPanel.Controls.Add(this.transferPageRightTopPanel);
            this.transferPageRightPanel.Dock = (DockStyle)resourceManager.GetObject("transferPageRightPanel.Dock");
            this.transferPageRightPanel.Enabled = (bool)resourceManager.GetObject("transferPageRightPanel.Enabled");
            this.transferPageRightPanel.Font = (Font)resourceManager.GetObject("transferPageRightPanel.Font");
            this.transferPageRightPanel.ImeMode = (ImeMode)resourceManager.GetObject("transferPageRightPanel.ImeMode");
            this.transferPageRightPanel.Location = (Point)resourceManager.GetObject("transferPageRightPanel.Location");
            this.transferPageRightPanel.Name = "transferPageRightPanel";
            this.transferPageRightPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("transferPageRightPanel.RightToLeft");
            this.transferPageRightPanel.Size = (Size)resourceManager.GetObject("transferPageRightPanel.Size");
            this.transferPageRightPanel.TabIndex = (int)resourceManager.GetObject("transferPageRightPanel.TabIndex");
            this.transferPageRightPanel.Text = resourceManager.GetString("transferPageRightPanel.Text");
            this.transferPageRightPanel.Visible = (bool)resourceManager.GetObject("transferPageRightPanel.Visible");
            this.transferPageRightTopPanel.AccessibleDescription = resourceManager.GetString("transferPageRightTopPanel.AccessibleDescription");
            this.transferPageRightTopPanel.AccessibleName = resourceManager.GetString("transferPageRightTopPanel.AccessibleName");
            this.transferPageRightTopPanel.Anchor = (AnchorStyles)resourceManager.GetObject("transferPageRightTopPanel.Anchor");
            this.transferPageRightTopPanel.AutoScroll = (bool)resourceManager.GetObject("transferPageRightTopPanel.AutoScroll");
            this.transferPageRightTopPanel.AutoScrollMargin = (Size)resourceManager.GetObject("transferPageRightTopPanel.AutoScrollMargin");
            this.transferPageRightTopPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("transferPageRightTopPanel.AutoScrollMinSize");
            this.transferPageRightTopPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("transferPageRightTopPanel.BackgroundImage");
            this.transferPageRightTopPanel.Collapsible = true;
            this.transferPageRightTopPanel.Controls.Add(this.rightTransferTreeView);
            this.transferPageRightTopPanel.Dock = (DockStyle)resourceManager.GetObject("transferPageRightTopPanel.Dock");
            this.transferPageRightTopPanel.DockPadding.Bottom = 4;
            this.transferPageRightTopPanel.DockPadding.Left = 4;
            this.transferPageRightTopPanel.DockPadding.Right = 4;
            this.transferPageRightTopPanel.DockPadding.Top = 4;
            this.transferPageRightTopPanel.Enabled = (bool)resourceManager.GetObject("transferPageRightTopPanel.Enabled");
            this.transferPageRightTopPanel.EnableExpanded = false;
            this.transferPageRightTopPanel.Collapsible = false;
            this.transferPageRightTopPanel.Expanded = true;
            this.transferPageRightTopPanel.Font = (Font)resourceManager.GetObject("transferPageRightTopPanel.Font");
            this.transferPageRightTopPanel.HeaderText = resourceManager.GetString("transferPageRightTopPanel.HeaderText");
            this.transferPageRightTopPanel.Icon = (Icon)resourceManager.GetObject("transferPageRightTopPanel.Icon");
            this.transferPageRightTopPanel.ImeMode = (ImeMode)resourceManager.GetObject("transferPageRightTopPanel.ImeMode");
            this.transferPageRightTopPanel.Location = (Point)resourceManager.GetObject("transferPageRightTopPanel.Location");
            this.transferPageRightTopPanel.MinimumHeight = 0;
            this.transferPageRightTopPanel.Name = "transferPageRightTopPanel";
            this.transferPageRightTopPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("transferPageRightTopPanel.RightToLeft");
            this.transferPageRightTopPanel.Size = (Size)resourceManager.GetObject("transferPageRightTopPanel.Size");
            this.transferPageRightTopPanel.TabIndex = (int)resourceManager.GetObject("transferPageRightTopPanel.TabIndex");
            this.transferPageRightTopPanel.Visible = (bool)resourceManager.GetObject("transferPageRightTopPanel.Visible");
            this.rightTransferTreeView.AccessibleDescription = resourceManager.GetString("rightTransferTreeView.AccessibleDescription");
            this.rightTransferTreeView.AccessibleName = resourceManager.GetString("rightTransferTreeView.AccessibleName");
            this.rightTransferTreeView.Anchor = (AnchorStyles)resourceManager.GetObject("rightTransferTreeView.Anchor");
            this.rightTransferTreeView.AutoScroll = (bool)resourceManager.GetObject("rightTransferTreeView.AutoScroll");
            this.rightTransferTreeView.AutoScrollMargin = (Size)resourceManager.GetObject("rightTransferTreeView.AutoScrollMargin");
            this.rightTransferTreeView.AutoScrollMinSize = (Size)resourceManager.GetObject("rightTransferTreeView.AutoScrollMinSize");
            this.rightTransferTreeView.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("rightTransferTreeView.BackgroundImage");
            this.rightTransferTreeView.Dock = (DockStyle)resourceManager.GetObject("rightTransferTreeView.Dock");
            this.rightTransferTreeView.Enabled = (bool)resourceManager.GetObject("rightTransferTreeView.Enabled");
            this.rightTransferTreeView.Font = (Font)resourceManager.GetObject("rightTransferTreeView.Font");
            this.rightTransferTreeView.ImeMode = (ImeMode)resourceManager.GetObject("rightTransferTreeView.ImeMode");
            this.rightTransferTreeView.Location = (Point)resourceManager.GetObject("rightTransferTreeView.Location");
            this.rightTransferTreeView.Name = "rightTransferTreeView";
            this.rightTransferTreeView.RightToLeft = (RightToLeft)resourceManager.GetObject("rightTransferTreeView.RightToLeft");
            this.rightTransferTreeView.Size = (Size)resourceManager.GetObject("rightTransferTreeView.Size");
            this.rightTransferTreeView.TabIndex = (int)resourceManager.GetObject("rightTransferTreeView.TabIndex");
            this.rightTransferTreeView.TransferLeftToRight = false;
            this.rightTransferTreeView.Visible = (bool)resourceManager.GetObject("rightTransferTreeView.Visible");
            this.transferPageSplitter.AccessibleDescription = resourceManager.GetString("transferPageSplitter.AccessibleDescription");
            this.transferPageSplitter.AccessibleName = resourceManager.GetString("transferPageSplitter.AccessibleName");
            this.transferPageSplitter.Anchor = (AnchorStyles)resourceManager.GetObject("transferPageSplitter.Anchor");
            this.transferPageSplitter.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("transferPageSplitter.BackgroundImage");
            this.transferPageSplitter.Dock = (DockStyle)resourceManager.GetObject("transferPageSplitter.Dock");
            this.transferPageSplitter.Enabled = (bool)resourceManager.GetObject("transferPageSplitter.Enabled");
            this.transferPageSplitter.Font = (Font)resourceManager.GetObject("transferPageSplitter.Font");
            this.transferPageSplitter.ImeMode = (ImeMode)resourceManager.GetObject("transferPageSplitter.ImeMode");
            this.transferPageSplitter.Location = (Point)resourceManager.GetObject("transferPageSplitter.Location");
            this.transferPageSplitter.MinExtra = (int)resourceManager.GetObject("transferPageSplitter.MinExtra");
            this.transferPageSplitter.MinSize = (int)resourceManager.GetObject("transferPageSplitter.MinSize");
            this.transferPageSplitter.Name = "transferPageSplitter";
            this.transferPageSplitter.RightToLeft = (RightToLeft)resourceManager.GetObject("transferPageSplitter.RightToLeft");
            this.transferPageSplitter.Size = (Size)resourceManager.GetObject("transferPageSplitter.Size");
            this.transferPageSplitter.TabIndex = (int)resourceManager.GetObject("transferPageSplitter.TabIndex");
            this.transferPageSplitter.TabStop = false;
            this.transferPageSplitter.Visible = (bool)resourceManager.GetObject("transferPageSplitter.Visible");
            this.transferPageSplitter.SplitterMoved += new SplitterEventHandler(this.impPageSplitter_SplitterMoved);
            this.transferPageLeftPanel.AccessibleDescription = resourceManager.GetString("transferPageLeftPanel.AccessibleDescription");
            this.transferPageLeftPanel.AccessibleName = resourceManager.GetString("transferPageLeftPanel.AccessibleName");
            this.transferPageLeftPanel.Anchor = (AnchorStyles)resourceManager.GetObject("transferPageLeftPanel.Anchor");
            this.transferPageLeftPanel.AutoScroll = (bool)resourceManager.GetObject("transferPageLeftPanel.AutoScroll");
            this.transferPageLeftPanel.AutoScrollMargin = (Size)resourceManager.GetObject("transferPageLeftPanel.AutoScrollMargin");
            this.transferPageLeftPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("transferPageLeftPanel.AutoScrollMinSize");
            this.transferPageLeftPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("transferPageLeftPanel.BackgroundImage");
            this.transferPageLeftPanel.Controls.Add(this.transferPageLeftTopPanel);
            this.transferPageLeftPanel.Dock = (DockStyle)resourceManager.GetObject("transferPageLeftPanel.Dock");
            this.transferPageLeftPanel.Enabled = (bool)resourceManager.GetObject("transferPageLeftPanel.Enabled");
            this.transferPageLeftPanel.Font = (Font)resourceManager.GetObject("transferPageLeftPanel.Font");
            this.transferPageLeftPanel.ImeMode = (ImeMode)resourceManager.GetObject("transferPageLeftPanel.ImeMode");
            this.transferPageLeftPanel.Location = (Point)resourceManager.GetObject("transferPageLeftPanel.Location");
            this.transferPageLeftPanel.Name = "transferPageLeftPanel";
            this.transferPageLeftPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("transferPageLeftPanel.RightToLeft");
            this.transferPageLeftPanel.Size = (Size)resourceManager.GetObject("transferPageLeftPanel.Size");
            this.transferPageLeftPanel.TabIndex = (int)resourceManager.GetObject("transferPageLeftPanel.TabIndex");
            this.transferPageLeftPanel.Text = resourceManager.GetString("transferPageLeftPanel.Text");
            this.transferPageLeftPanel.Visible = (bool)resourceManager.GetObject("transferPageLeftPanel.Visible");
            this.transferPageLeftTopPanel.AccessibleDescription = resourceManager.GetString("transferPageLeftTopPanel.AccessibleDescription");
            this.transferPageLeftTopPanel.AccessibleName = resourceManager.GetString("transferPageLeftTopPanel.AccessibleName");
            this.transferPageLeftTopPanel.Anchor = (AnchorStyles)resourceManager.GetObject("transferPageLeftTopPanel.Anchor");
            this.transferPageLeftTopPanel.AutoScroll = (bool)resourceManager.GetObject("transferPageLeftTopPanel.AutoScroll");
            this.transferPageLeftTopPanel.AutoScrollMargin = (Size)resourceManager.GetObject("transferPageLeftTopPanel.AutoScrollMargin");
            this.transferPageLeftTopPanel.AutoScrollMinSize = (Size)resourceManager.GetObject("transferPageLeftTopPanel.AutoScrollMinSize");
            this.transferPageLeftTopPanel.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("transferPageLeftTopPanel.BackgroundImage");
            this.transferPageLeftTopPanel.Collapsible = true;
            this.transferPageLeftTopPanel.Controls.Add(this.leftTransferTreeView);
            this.transferPageLeftTopPanel.Dock = (DockStyle)resourceManager.GetObject("transferPageLeftTopPanel.Dock");
            this.transferPageLeftTopPanel.DockPadding.Bottom = 4;
            this.transferPageLeftTopPanel.DockPadding.Left = 4;
            this.transferPageLeftTopPanel.DockPadding.Right = 4;
            this.transferPageLeftTopPanel.DockPadding.Top = 4;
            this.transferPageLeftTopPanel.Enabled = (bool)resourceManager.GetObject("transferPageLeftTopPanel.Enabled");
            this.transferPageLeftTopPanel.EnableExpanded = false;
            this.transferPageLeftTopPanel.Collapsible = false;
            this.transferPageLeftTopPanel.Expanded = true;
            this.transferPageLeftTopPanel.Font = (Font)resourceManager.GetObject("transferPageLeftTopPanel.Font");
            this.transferPageLeftTopPanel.HeaderText = resourceManager.GetString("transferPageLeftTopPanel.HeaderText");
            this.transferPageLeftTopPanel.Icon = (Icon)resourceManager.GetObject("transferPageLeftTopPanel.Icon");
            this.transferPageLeftTopPanel.ImeMode = (ImeMode)resourceManager.GetObject("transferPageLeftTopPanel.ImeMode");
            this.transferPageLeftTopPanel.Location = (Point)resourceManager.GetObject("transferPageLeftTopPanel.Location");
            this.transferPageLeftTopPanel.MinimumHeight = 0;
            this.transferPageLeftTopPanel.Name = "transferPageLeftTopPanel";
            this.transferPageLeftTopPanel.RightToLeft = (RightToLeft)resourceManager.GetObject("transferPageLeftTopPanel.RightToLeft");
            this.transferPageLeftTopPanel.Size = (Size)resourceManager.GetObject("transferPageLeftTopPanel.Size");
            this.transferPageLeftTopPanel.TabIndex = (int)resourceManager.GetObject("transferPageLeftTopPanel.TabIndex");
            this.transferPageLeftTopPanel.Visible = (bool)resourceManager.GetObject("transferPageLeftTopPanel.Visible");
            this.leftTransferTreeView.AccessibleDescription = resourceManager.GetString("leftTransferTreeView.AccessibleDescription");
            this.leftTransferTreeView.AccessibleName = resourceManager.GetString("leftTransferTreeView.AccessibleName");
            this.leftTransferTreeView.Anchor = (AnchorStyles)resourceManager.GetObject("leftTransferTreeView.Anchor");
            this.leftTransferTreeView.AutoScroll = (bool)resourceManager.GetObject("leftTransferTreeView.AutoScroll");
            this.leftTransferTreeView.AutoScrollMargin = (Size)resourceManager.GetObject("leftTransferTreeView.AutoScrollMargin");
            this.leftTransferTreeView.AutoScrollMinSize = (Size)resourceManager.GetObject("leftTransferTreeView.AutoScrollMinSize");
            this.leftTransferTreeView.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("leftTransferTreeView.BackgroundImage");
            this.leftTransferTreeView.Dock = (DockStyle)resourceManager.GetObject("leftTransferTreeView.Dock");
            this.leftTransferTreeView.Enabled = (bool)resourceManager.GetObject("leftTransferTreeView.Enabled");
            this.leftTransferTreeView.Font = (Font)resourceManager.GetObject("leftTransferTreeView.Font");
            this.leftTransferTreeView.ImeMode = (ImeMode)resourceManager.GetObject("leftTransferTreeView.ImeMode");
            this.leftTransferTreeView.Location = (Point)resourceManager.GetObject("leftTransferTreeView.Location");
            this.leftTransferTreeView.Name = "leftTransferTreeView";
            this.leftTransferTreeView.RightToLeft = (RightToLeft)resourceManager.GetObject("leftTransferTreeView.RightToLeft");
            this.leftTransferTreeView.Size = (Size)resourceManager.GetObject("leftTransferTreeView.Size");
            this.leftTransferTreeView.TabIndex = (int)resourceManager.GetObject("leftTransferTreeView.TabIndex");
            this.leftTransferTreeView.Visible = (bool)resourceManager.GetObject("leftTransferTreeView.Visible");
            this.tabImgList.ImageSize = (Size)resourceManager.GetObject("tabImgList.ImageSize");
            this.tabImgList.TransparentColor = Color.Transparent;
            this.okButton.AccessibleDescription = resourceManager.GetString("okButton.AccessibleDescription");
            this.okButton.AccessibleName = resourceManager.GetString("okButton.AccessibleName");
            this.okButton.Anchor = (AnchorStyles)resourceManager.GetObject("okButton.Anchor");
            this.okButton.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("okButton.BackgroundImage");
            this.okButton.DialogResult = DialogResult.OK;
            this.okButton.Dock = (DockStyle)resourceManager.GetObject("okButton.Dock");
            this.okButton.Enabled = (bool)resourceManager.GetObject("okButton.Enabled");
            this.okButton.FlatStyle = (FlatStyle)resourceManager.GetObject("okButton.FlatStyle");
            this.okButton.Font = (Font)resourceManager.GetObject("okButton.Font");
            this.okButton.Image = (System.Drawing.Image)resourceManager.GetObject("okButton.Image");
            this.okButton.ImageAlign = (ContentAlignment)resourceManager.GetObject("okButton.ImageAlign");
            this.okButton.ImageIndex = (int)resourceManager.GetObject("okButton.ImageIndex");
            this.okButton.ImeMode = (ImeMode)resourceManager.GetObject("okButton.ImeMode");
            this.okButton.Location = (Point)resourceManager.GetObject("okButton.Location");
            this.okButton.Name = "okButton";
            this.okButton.RightToLeft = (RightToLeft)resourceManager.GetObject("okButton.RightToLeft");
            this.okButton.Size = (Size)resourceManager.GetObject("okButton.Size");
            this.okButton.TabIndex = (int)resourceManager.GetObject("okButton.TabIndex");
            this.okButton.Text = resourceManager.GetString("okButton.Text");
            this.okButton.TextAlign = (ContentAlignment)resourceManager.GetObject("okButton.TextAlign");
            this.okButton.Visible = (bool)resourceManager.GetObject("okButton.Visible");
            this.okButton.Click += new EventHandler(this.okButton_Click);
            this.applyButton.AccessibleDescription = resourceManager.GetString("applyButton.AccessibleDescription");
            this.applyButton.AccessibleName = resourceManager.GetString("applyButton.AccessibleName");
            this.applyButton.Anchor = (AnchorStyles)resourceManager.GetObject("applyButton.Anchor");
            this.applyButton.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("applyButton.BackgroundImage");
            this.applyButton.Dock = (DockStyle)resourceManager.GetObject("applyButton.Dock");
            this.applyButton.Enabled = (bool)resourceManager.GetObject("applyButton.Enabled");
            this.applyButton.FlatStyle = (FlatStyle)resourceManager.GetObject("applyButton.FlatStyle");
            this.applyButton.Font = (Font)resourceManager.GetObject("applyButton.Font");
            this.applyButton.Image = (System.Drawing.Image)resourceManager.GetObject("applyButton.Image");
            this.applyButton.ImageAlign = (ContentAlignment)resourceManager.GetObject("applyButton.ImageAlign");
            this.applyButton.ImageIndex = (int)resourceManager.GetObject("applyButton.ImageIndex");
            this.applyButton.ImeMode = (ImeMode)resourceManager.GetObject("applyButton.ImeMode");
            this.applyButton.Location = (Point)resourceManager.GetObject("applyButton.Location");
            this.applyButton.Name = "applyButton";
            this.applyButton.RightToLeft = (RightToLeft)resourceManager.GetObject("applyButton.RightToLeft");
            this.applyButton.Size = (Size)resourceManager.GetObject("applyButton.Size");
            this.applyButton.TabIndex = (int)resourceManager.GetObject("applyButton.TabIndex");
            this.applyButton.Text = resourceManager.GetString("applyButton.Text");
            this.applyButton.TextAlign = (ContentAlignment)resourceManager.GetObject("applyButton.TextAlign");
            this.applyButton.Visible = (bool)resourceManager.GetObject("applyButton.Visible");
            this.applyButton.Click += new EventHandler(this.applyButton_Click);
            this.cancelButton.AccessibleDescription = resourceManager.GetString("cancelButton.AccessibleDescription");
            this.cancelButton.AccessibleName = resourceManager.GetString("cancelButton.AccessibleName");
            this.cancelButton.Anchor = (AnchorStyles)resourceManager.GetObject("cancelButton.Anchor");
            this.cancelButton.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("cancelButton.BackgroundImage");
            this.cancelButton.DialogResult = DialogResult.Cancel;
            this.cancelButton.Dock = (DockStyle)resourceManager.GetObject("cancelButton.Dock");
            this.cancelButton.Enabled = (bool)resourceManager.GetObject("cancelButton.Enabled");
            this.cancelButton.FlatStyle = (FlatStyle)resourceManager.GetObject("cancelButton.FlatStyle");
            this.cancelButton.Font = (Font)resourceManager.GetObject("cancelButton.Font");
            this.cancelButton.Image = (System.Drawing.Image)resourceManager.GetObject("cancelButton.Image");
            this.cancelButton.ImageAlign = (ContentAlignment)resourceManager.GetObject("cancelButton.ImageAlign");
            this.cancelButton.ImageIndex = (int)resourceManager.GetObject("cancelButton.ImageIndex");
            this.cancelButton.ImeMode = (ImeMode)resourceManager.GetObject("cancelButton.ImeMode");
            this.cancelButton.Location = (Point)resourceManager.GetObject("cancelButton.Location");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RightToLeft = (RightToLeft)resourceManager.GetObject("cancelButton.RightToLeft");
            this.cancelButton.Size = (Size)resourceManager.GetObject("cancelButton.Size");
            this.cancelButton.TabIndex = (int)resourceManager.GetObject("cancelButton.TabIndex");
            this.cancelButton.Text = resourceManager.GetString("cancelButton.Text");
            this.cancelButton.TextAlign = (ContentAlignment)resourceManager.GetObject("cancelButton.TextAlign");
            this.cancelButton.Visible = (bool)resourceManager.GetObject("cancelButton.Visible");
            this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
            this.helpButton.AccessibleDescription = resourceManager.GetString("helpButton.AccessibleDescription");
            this.helpButton.AccessibleName = resourceManager.GetString("helpButton.AccessibleName");
            this.helpButton.Anchor = (AnchorStyles)resourceManager.GetObject("helpButton.Anchor");
            this.helpButton.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("helpButton.BackgroundImage");
            this.helpButton.Dock = (DockStyle)resourceManager.GetObject("helpButton.Dock");
            this.helpButton.Enabled = (bool)resourceManager.GetObject("helpButton.Enabled");
            this.helpButton.FlatStyle = (FlatStyle)resourceManager.GetObject("helpButton.FlatStyle");
            this.helpButton.Font = (Font)resourceManager.GetObject("helpButton.Font");
            this.helpButton.Image = (System.Drawing.Image)resourceManager.GetObject("helpButton.Image");
            this.helpButton.ImageAlign = (ContentAlignment)resourceManager.GetObject("helpButton.ImageAlign");
            this.helpButton.ImageIndex = (int)resourceManager.GetObject("helpButton.ImageIndex");
            this.helpButton.ImeMode = (ImeMode)resourceManager.GetObject("helpButton.ImeMode");
            this.helpButton.Location = (Point)resourceManager.GetObject("helpButton.Location");
            this.helpButton.Name = "helpButton";
            this.helpButton.RightToLeft = (RightToLeft)resourceManager.GetObject("helpButton.RightToLeft");
            this.helpButton.Size = (Size)resourceManager.GetObject("helpButton.Size");
            this.helpButton.TabIndex = (int)resourceManager.GetObject("helpButton.TabIndex");
            this.helpButton.Text = resourceManager.GetString("helpButton.Text");
            this.helpButton.TextAlign = (ContentAlignment)resourceManager.GetObject("helpButton.TextAlign");
            this.helpButton.Visible = (bool)resourceManager.GetObject("helpButton.Visible");
            this.helpButton.Click += new EventHandler(this.helpButton_Click);
            this.quickStartLink.AccessibleDescription = resourceManager.GetString("quickStartLink.AccessibleDescription");
            this.quickStartLink.AccessibleName = resourceManager.GetString("quickStartLink.AccessibleName");
            this.quickStartLink.Anchor = (AnchorStyles)resourceManager.GetObject("quickStartLink.Anchor");
            this.quickStartLink.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("quickStartLink.BackgroundImage");
            this.quickStartLink.Dock = (DockStyle)resourceManager.GetObject("quickStartLink.Dock");
            this.quickStartLink.Enabled = (bool)resourceManager.GetObject("quickStartLink.Enabled");
            this.quickStartLink.Font = (Font)resourceManager.GetObject("quickStartLink.Font");
            this.quickStartLink.ImeMode = (ImeMode)resourceManager.GetObject("quickStartLink.ImeMode");
            this.quickStartLink.Location = (Point)resourceManager.GetObject("quickStartLink.Location");
            this.quickStartLink.Name = "quickStartLink";
            this.quickStartLink.RightToLeft = (RightToLeft)resourceManager.GetObject("quickStartLink.RightToLeft");
            this.quickStartLink.Size = (Size)resourceManager.GetObject("quickStartLink.Size");
            this.quickStartLink.TabIndex = (int)resourceManager.GetObject("quickStartLink.TabIndex");
            this.quickStartLink.Text = resourceManager.GetString("quickStartLink.Text");
            this.quickStartLink.Visible = (bool)resourceManager.GetObject("quickStartLink.Visible");
            base.AccessibleDescription = resourceManager.GetString("$this.AccessibleDescription");
            base.AccessibleName = resourceManager.GetString("$this.AccessibleName");
            this.AutoScaleBaseSize = (Size)resourceManager.GetObject("$this.AutoScaleBaseSize");
            this.AutoScroll = (bool)resourceManager.GetObject("$this.AutoScroll");
            base.AutoScrollMargin = (Size)resourceManager.GetObject("$this.AutoScrollMargin");
            base.AutoScrollMinSize = (Size)resourceManager.GetObject("$this.AutoScrollMinSize");
            this.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("$this.BackgroundImage");
            base.ClientSize = (Size)resourceManager.GetObject("$this.ClientSize");
            base.Controls.Add(this.quickStartLink);
            base.Controls.Add(this.tabControl);
            base.Controls.Add(this.helpButton);
            base.Controls.Add(this.cancelButton);
            base.Controls.Add(this.okButton);
            base.Controls.Add(this.applyButton);
            base.Enabled = (bool)resourceManager.GetObject("$this.Enabled");
            this.Font = (Font)resourceManager.GetObject("$this.Font");
            base.Icon = (Icon)resourceManager.GetObject("$this.Icon");
            base.ImeMode = (ImeMode)resourceManager.GetObject("$this.ImeMode");
            base.Location = (Point)resourceManager.GetObject("$this.Location");
            this.MaximumSize = (Size)resourceManager.GetObject("$this.MaximumSize");
            base.Menu = this.mainMenu;
            base.MinimizeBox = false;
            this.MinimumSize = (Size)resourceManager.GetObject("$this.MinimumSize");
            base.Name = "MainForm";
            this.RightToLeft = (RightToLeft)resourceManager.GetObject("$this.RightToLeft");
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = (FormStartPosition)resourceManager.GetObject("$this.StartPosition");
            this.Text = resourceManager.GetString("$this.Text");
            base.Resize += new EventHandler(this.MainForm_Resize);
            this.tabControl.ResumeLayout(false);
            this.customizePage.ResumeLayout(false);
            this.custPageRightPanel.ResumeLayout(false);
            this.custPageRightBottomPropPanel.ResumeLayout(false);
            this.custPagePropertyPanel.ResumeLayout(false);
            this.custPageMiscControlPanel.ResumeLayout(false);
            this.custPageRightTopPanel.ResumeLayout(false);
            this.custPageLeftPanel.ResumeLayout(false);
            this.custPageLeftBottomPanel.ResumeLayout(false);
            this.custPageLeftTopPanel.ResumeLayout(false);
            this.transferPage.ResumeLayout(false);
            this.transferPageRightPanel.ResumeLayout(false);
            this.transferPageRightTopPanel.ResumeLayout(false);
            this.transferPageLeftPanel.ResumeLayout(false);
            this.transferPageLeftTopPanel.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public static void InitMRUDropDownList(SeparatorComboBox combo, bool showNewCmd, bool showSaveAsCmd, ArrayList mrulist, bool allMenus, bool newFile)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add(new System.Data.DataColumn("Display", typeof(string)));
            dataTable.Columns.Add(new System.Data.DataColumn("Path", typeof(string)));
            combo.ClearSeparators();
            int num = 0;
            string str = null;
            string fileName = null;
            if (allMenus)
            {
                dataTable.Rows.Add(dataTable.NewRow());
                str = LocalResources.GetString("DLG_AllCustomizationFiles");
                fileName = Path.GetFileName(MainForm.MainCUIPath);
                str = string.Format(str, fileName);
                dataTable.Rows[num][0] = str;
                dataTable.Rows[num][1] = MainForm.MainCUIPath;
                num++;
            }
            if (!MainForm.mainTab.TransferPaneOnly)
            {
                dataTable.Rows.Add(dataTable.NewRow());
                str = LocalResources.GetString("DLG_MainCUIFile");
                fileName = Path.GetFileName(MainForm.MainCUIPath);
                str = string.Format(str, fileName);
                dataTable.Rows[num][0] = str;
                dataTable.Rows[num][1] = MainForm.MainCUIPath;
                num++;
            }
            if (MainForm.EnterpriseCUIPath != null && MainForm.EnterpriseCUIPath != string.Empty && File.Exists(MainForm.EnterpriseCUIPath))
            {
                dataTable.Rows.Add(dataTable.NewRow());
                str = LocalResources.GetString("DLG_EnterpriseCUIFiles");
                fileName = Path.GetFileName(MainForm.EnterpriseCUIPath);
                str = string.Format(str, fileName);
                dataTable.Rows[num][0] = str;
                dataTable.Rows[num][1] = MainForm.EnterpriseCUIPath;
                num++;
            }
            combo.SetSeparator(num - 1);
            if (mrulist.Count > 0)
            {
                foreach (string str1 in mrulist)
                {
                    dataTable.Rows.Add(dataTable.NewRow());
                    fileName = Path.GetFileName(str1);
                    dataTable.Rows[num][0] = fileName;
                    dataTable.Rows[num][1] = str1;
                    num++;
                }
                combo.SetSeparator(num - 1);
            }
            if (showNewCmd)
            {
                dataTable.Rows.Add(dataTable.NewRow());
                dataTable.Rows[num][0] = LocalResources.GetString("DLG_New");
                dataTable.Rows[num][1] = null;
                num++;
            }
            dataTable.Rows.Add(dataTable.NewRow());
            dataTable.Rows[num][0] = LocalResources.GetString("DLG_Open");
            dataTable.Rows[num][1] = null;
            num++;
            if (showSaveAsCmd)
            {
                dataTable.Rows.Add(dataTable.NewRow());
                dataTable.Rows[num][0] = LocalResources.GetString("DLG_SaveAs");
                dataTable.Rows[num][1] = null;
                num++;
            }
            if (newFile)
            {
                dataTable.Rows.Add(dataTable.NewRow());
                str = LocalResources.GetString("NewCuiFIle");
                dataTable.Rows[num][0] = str;
                dataTable.Rows[num][1] = string.Empty;
                num++;
            }
            combo.DataSource = dataTable;
            combo.DisplayMember = "Display";
            combo.ValueMember = "Path";
        }

        internal static void initWSCURRENT()
        {
            if (HostApplicationServices.Current != null)
            {
                string systemVariable = (string)Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("WSCURRENT");
                if (systemVariable == null)
                {
                    return;
                }
                Workspace item = null;
                int num = -1;
                if (Document.EnterpriseCUIFile != null)
                {
                    num = Document.EnterpriseCUIFile.Workspaces.IndexOfWorkspaceName(systemVariable);
                }
                if (num <= -1)
                {
                    if (Document.MainCuiFile != null)
                    {
                        num = Document.MainCuiFile.Workspaces.IndexOfWorkspaceName(systemVariable);
                    }
                    if (num > -1)
                    {
                        item = Document.MainCuiFile.Workspaces[num];
                    }
                }
                else
                {
                    item = Document.EnterpriseCUIFile.Workspaces[num];
                }
                if (item == null)
                {
                    return;
                }
                if (MainForm.mOrgWSCURRENT != null)
                {
                    MainForm.mOrgWSCURRENT.Modified -= new EventHandler(MainForm.WSCURRENT_Modified);
                }
                Workspace workspace = item;
                MainForm.mOrgWSCURRENT = workspace;
                MainForm.mWSCURRENT = workspace;
                MainForm.mOrgWSCURRENT.FireModifiedEvent();
                if (MainForm.mOrgWSCURRENT != null)
                {
                    MainForm.mOrgWSCURRENT.Modified += new EventHandler(MainForm.WSCURRENT_Modified);
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.mResizePending = true;
            this.ResizeHandler();
        }

        public Document NewTransferDocument(bool leftPanel)
        {
            if (!leftPanel)
            {
                Document document = this.rightTransferTreeView.Document;
            }
            else
            {
                Document document1 = this.leftTransferTreeView.Document;
            }
            Document document2 = new Document();
            document2.CUIFile.EnsureDefaultDrawing();
            if (!leftPanel)
            {
                this.PopulateTransferRightPanelFromCUI(document2);
            }
            else
            {
                this.PopulateTransferLeftPanelFromCUI(document2);
            }
            return document2;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (Document document in Document.Documents)
            {
                if (!document.IsModified)
                {
                    continue;
                }
                if (!document.CUIFile.ReadOnly)
                {
                    if (document == Document.MainCuiDoc || document == Document.EnterpriseCUIDoc || Document.IsPartialDoc(document))
                    {
                        flag = true;
                    }
                    if (document.Save() || MainForm.ShowMessageBox(string.Format(LocalResources.GetString("DLG_Save_Failed_Msg"), document.Filename), MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        continue;
                    }
                    base.DialogResult = DialogResult.None;
                }
                else
                {
                    string str = LocalResources.GetString("MSG_UnableToSaveFileWriteProtected");
                    str = string.Format(str, Path.GetFileName(document.Filename));
                    MainForm.ShowAlert(str);
                }
            }
            if ((flag || this.bForceMenuReload) && HostApplicationServices.Current != null)
            {
                Utils.EnableToolPaletteDragDrop(false);
                Utils.ReloadMenus(MainForm.ReloadedWSCURRENT, (MainForm.mOrgWSCURRENTModified ? true : this.bForceMenuReload));
                Utils.EnableToolPaletteDragDrop(true);
                MainForm.reinitWSCURRENT();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (base.DialogResult == DialogResult.Cancel && !this.isCancelButton && MainForm.somethingIsDirty())
            {
                using (IConfigurationSection configurationSection = Autodesk.AutoCAD.ApplicationServices.Application.UserConfigurationManager.OpenDialogSection(this))
                {
                    bool flag = (bool)configurationSection.ReadProperty("ShowCUIFileNeedsSaveMsg", true);
                    DialogResult dialogResult = DialogResult.OK;
                    if (flag)
                    {
                        bool flag1 = flag;
                        dialogResult = SuppressableMessageBox.Show(LocalResources.GetString("CUI_Needs_Save_Msg"), LocalResources.GetString("CUI_Needs_Save_Title"), ref flag);
                        if (flag1 != flag)
                        {
                            configurationSection.WriteProperty("ShowCUIFileNeedsSaveMsg", flag);
                        }
                    }
                    if (dialogResult == DialogResult.OK)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                        base.OnClosing(e);
                        return;
                    }
                }
            }
            Document.CloseAll();
            Document.DocumentOpened -= new EventHandler(this.Document_DocumentOpened);
            TransferTreeView.StoreMRUToRegistry();
            this.custPageCmdListCtrl.StoreToRegistry();
            this.shortcutsGroup.StoreToRegistry();
            this.UninitializeButtonControlImages();
            MainForm.mPaletteConfig = false;
            Utils.EnableToolPaletteDragDrop(false);
            base.OnClosing(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.F1)
            {
                this.helpButton_Click(this, e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Document.CloseAll();
            MainForm.resetWSCURRENT();
            Document.DocumentOpened += new EventHandler(this.Document_DocumentOpened);
            this.tabControl.SelectedIndex = -1;
            if (this.mDlgMode == MainForm.DlgMode.CUI || this.mDlgMode == MainForm.DlgMode.TransferOnly)
            {
                this.tabControl.SelectedIndex = 0;
            }
            else
            {
                this.tabControl.SelectedIndex = 1;
            }
            if (this.custPageCmdListCtrl.Entries == null || this.custPageCmdListCtrl.Entries.Count == 0)
            {
                this.custPageCmdListCtrl.EnterEmptyMode();
            }
            this.custPageMiscControlPanel.Hide();
            this.custPageRightSplitter.Hide();
            this.custPageRightPanelPropSplitter.Hide();
            this.mImpPageSplitterRatio = (float)this.transferPageSplitter.SplitPosition / (float)base.Width;
            this.mCustPageSplitterRatio = (float)this.custPageSplitter.SplitPosition / (float)base.Width;
            this.mCustPageLeftVerSplitterRatio = (float)this.custPageLeftSplitter.SplitPosition / (float)this.custPageLeftPanel.Height;
            this.mCustPageRightVerSplitterRatio = (float)this.custPageRightSplitter.SplitPosition / (float)this.custPageRightPanel.Height;
            this.custPagePropertyControl.EventHandler += new PropertyControl.EventDelegate(this.CustPagePropertyControl_EventHandler);
            this.custPageRightTopPanel.Hide();
            TransferTreeView.LoadMRUFromRegistry();
            if (MainForm.mInitialToolbarMenuGroup != null && MainForm.mInitialToolbarUID != null)
            {
                //this.customizeTreeView.SelectInitialToolbarNode(MainForm.mInitialToolbarMenuGroup, MainForm.mInitialToolbarUID);
            }
            this.custPageLeftTopPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageLeftTopPanel_Collapsed);
            this.custPageLeftBottomPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageLeftBottomPanel_Collapsed);
            if (MainForm.mPaletteConfig)
            {
                this.custPageLeftTopPanel.Collapse();
            }
            this.custPageRightTopPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageRightTopPanel_Collapsed);
            this.custPageMiscControlPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageMiscControlPanel_Collapsed);
            this.custPagePropertyPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPagePropertyPanel_Collapsed);
            this.customizeTreeView.TreeViewControl.ItemSelected += new TreeViewControl.TVCItemSelectedArgsEventHandler(this.CustPage_TreeViewControl_ItemSelected);
            Utils.EnableToolPaletteDragDrop(true);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            this.toolbarPreview.displayToolbar(false);
        }

        public void PopulateCustomizeTabCommandList(Document doc)
        {
            CommandListInterface.PopulateFromCUI(this.custPageCmdListCtrl, doc.CUIFile);
        }

        public void PopulateCustomizeTabCommandListForAllPartialCuis()
        {
            CommandListInterface.PopulateFromMainCuiAndAllPartialCuis(this.custPageCmdListCtrl);
        }

        private void populateToolbarPreview(Toolbar toolbar, CUITreeNode node)
        {
            this.showToolbarPreview();
            if (toolbar != null)
            {
                this.toolbarPreview.previewInit(toolbar, null);
                foreach (object toolbarItem in toolbar.ToolbarItems)
                {
                    if (toolbarItem is ToolbarButton)
                    {
                        ToolbarButton toolbarButton = (ToolbarButton)toolbarItem;
                        if (!toolbarButton.IsSeparator)
                        {
                            Macro macroForToolbarButton = node.CUIFile.getMacroForToolbarButton(toolbarButton);
                            this.toolbarPreview.buttonAdd(this.getStandaloneOrCustomResourceDllBitmap(node, macroForToolbarButton.SmallImage, macroForToolbarButton.LargeImage));
                        }
                        else
                        {
                            this.toolbarPreview.separatorAdd();
                        }
                    }
                    else if (toolbarItem is ToolbarControl)
                    {
                        this.toolbarPreview.comboAdd();
                    }
                    else if (!(toolbarItem is ToolbarFlyout))
                    {
                        this.toolbarPreview.buttonAdd(null);
                    }
                    else
                    {
                        ToolbarFlyout toolbarFlyout = toolbarItem as ToolbarFlyout;
                        if (toolbarFlyout.SmallImage == null || toolbarFlyout.LargeImage == null)
                        {
                            continue;
                        }
                        this.toolbarPreview.buttonAdd(this.getStandaloneOrCustomResourceDllBitmap(node, toolbarFlyout.SmallImage, toolbarFlyout.LargeImage));
                    }
                }
                this.toolbarPreview.displayToolbar(true);
            }
        }

        public void PopulateTransferLeftPanelFromCUI(Document doc)
        {
            this.leftTransferTreeView.TreeViewControl.PopulateAsStandAloneCUIFile(doc.CUIFile);
        }

        public void PopulateTransferRightPanelFromCUI(Document doc)
        {
            this.rightTransferTreeView.TreeViewControl.PopulateAsStandAloneCUIFile(doc.CUIFile);
        }

        public void PopulateWorkspaceMode()
        {
            MultiSelectTreeview treeView = this.customizeTreeView.TreeViewControl.TreeView;
            if (treeView.SelectedNodes.Count > 0)
            {
                CUIWorkspaceNode selectedNode = (CUIWorkspaceNode)treeView.SelectedNode;
                if (selectedNode != null)
                {
                    this.PopulateWorkspaceMode(selectedNode);
                }
            }
        }

        public void PopulateWorkspaceMode(CUIWorkspaceNode fromWsNode)
        {
            this.mPopulateWorkspace = true;
            this.custPageRightTopPanel.Show();
            this.custPageRightSplitter.Show();
            this.customizeTreeView.EnterPopulateWorkspaceMode(fromWsNode);
            this.workspaceTreeView.EnterPopulateWorkspaceMode(fromWsNode, this.customizeTreeView.FilterSettings);
            this.customizeTreeView.TreeViewControl.ItemSelected -= new TreeViewControl.TVCItemSelectedArgsEventHandler(this.CustPage_TreeViewControl_ItemSelected);
        }

        public void PopuplateWorkspaceModeDone()
        {
            if (this.mPopulateWorkspace)
            {
                this.customizeTreeView.TreeViewControl.ItemSelected += new TreeViewControl.TVCItemSelectedArgsEventHandler(this.CustPage_TreeViewControl_ItemSelected);
                this.customizeTreeView.ExitPopulateWorkspaceMode();
                this.workspaceTreeView.ExitPopulateWorkspaceMode();
                this.mPopulateWorkspace = false;
            }
        }

        internal static void reinitWSCURRENT()
        {
            MainForm.initWSCURRENT();
            MainForm.mOrgWSCURRENTModified = false;
        }

        public static void RemoveMRUEmptyDoc(ComboBox combo)
        {
            DataRowView dataRowView = null;
            string str = LocalResources.GetString("NewCuiFIle");
            foreach (DataRowView item in combo.Items)
            {
                if ((int)item.Row.ItemArray.Length < 2 || item.Row.ItemArray[1] == DBNull.Value || string.Compare((string)item.Row.ItemArray[1], string.Empty) != 0 || string.Compare(item.Row.ItemArray[0] as string, str) != 0)
                {
                    continue;
                }
                dataRowView = item;
                break;
            }
            if (dataRowView != null)
            {
                (combo.DataSource as System.Data.DataTable).Rows.Remove(dataRowView.Row);
            }
        }

        public void RePopulateWorkspace(CUIWorkspaceNode wsNode, ArrayList checkedTagNodes)
        {
            this.workspaceTreeView.RePopulateWorkspace(wsNode, checkedTagNodes, this.customizeTreeView.FilterSettings);
        }

        internal static void ReportException(Exception e)
        {
            string message = e.Message;
            if (e is ArgumentOutOfRangeException)
            {
                int num = message.IndexOf(Environment.NewLine) + 1;
                if (num > 0)
                {
                    message = message.Substring(0, num);
                }
            }
            MainForm.ShowAlert(message);
        }

        private void requestImage(string CUIFilenameWoExt, bool useSmallImage, string smallImage, string largeImage)
        {
            string str;
            if (!useSmallImage)
            {
                str = (largeImage == null || largeImage.Length <= 0 ? smallImage : largeImage);
            }
            else
            {
                str = (smallImage == null || smallImage.Length <= 0 ? largeImage : smallImage);
            }
            if (str != null && str.Length > 0 && !BitmapCache.HasResourceId(str))
            {
                Utils.CUIRequestBitmap(string.Concat(CUIFilenameWoExt, "|", str));
                BitmapCache.AddResourceId(str);
            }
        }

        private void requestImagesFromAllFlyouts(ToolbarCollection tbs, bool useSmallImage)
        {
            if (tbs == null)
            {
                return;
            }
            foreach (Toolbar tb in tbs)
            {
                foreach (ToolbarItemBase toolbarItem in tb.ToolbarItems)
                {
                    ToolbarFlyout toolbarFlyout = toolbarItem as ToolbarFlyout;
                    if (toolbarFlyout == null || !toolbarFlyout.UseOwnIcon)
                    {
                        continue;
                    }
                    this.requestImage(toolbarItem.CustomizationSection.CUIFileBaseName, useSmallImage, toolbarFlyout.SmallImage, toolbarFlyout.LargeImage);
                }
            }
        }

        private void requestImagesFromAllFlyouts(bool useSmallImage)
        {
            CustomizationSection[] mainCuiFile = new CustomizationSection[] { Document.MainCuiFile, Document.EnterpriseCUIFile };
            for (int i = 0; i < (int)mainCuiFile.Length; i++)
            {
                CustomizationSection customizationSection = mainCuiFile[i];
                if (customizationSection != null)
                {
                    this.requestImagesFromAllFlyouts(customizationSection.MenuGroup.Toolbars, useSmallImage);
                    foreach (CustomizationSection partialCUI in customizationSection.getPartialCUIs())
                    {
                        this.requestImagesFromAllFlyouts(partialCUI.MenuGroup.Toolbars, useSmallImage);
                    }
                }
            }
        }

        private void requestImagesFromAllMacros(bool useSmallImage)
        {
            CustomizationSection[] mainCuiFile = new CustomizationSection[] { Document.MainCuiFile, Document.EnterpriseCUIFile };
            for (int i = 0; i < (int)mainCuiFile.Length; i++)
            {
                CustomizationSection customizationSection = mainCuiFile[i];
                if (customizationSection != null)
                {
                    this.requestImagesFromGroups(customizationSection.MenuGroup.MacroGroups, useSmallImage);
                    foreach (CustomizationSection partialCUI in customizationSection.getPartialCUIs())
                    {
                        this.requestImagesFromGroups(partialCUI.MenuGroup.MacroGroups, useSmallImage);
                    }
                }
            }
        }

        private void requestImagesFromGroups(MacroGroupCollection macroGrps, bool useSmallImage)
        {
            if (macroGrps == null)
            {
                return;
            }
            foreach (MacroGroup macroGrp in macroGrps)
            {
                foreach (MenuMacro menuMacro in macroGrp.MenuMacros)
                {
                    Macro macro = menuMacro.macro;
                    this.requestImage(menuMacro.Parent.CustomizationSection.CUIFileBaseName, useSmallImage, macro.SmallImage, macro.LargeImage);
                }
            }
        }

        private static void resetWSCURRENT()
        {
            object obj = null;
            MainForm.mWSCURRENT = (Workspace)obj;
            MainForm.mOrgWSCURRENT = (Workspace)obj;
        }

        private void ResizeHandler()
        {
            if ((double)this.mCustPageSplitterRatio != 0)
            {
                this.custPageSplitter.SplitPosition = Convert.ToInt32((float)base.Width * this.mCustPageSplitterRatio);
                this.transferPageSplitter.SplitPosition = Convert.ToInt32((float)base.Width * this.mImpPageSplitterRatio);
                if (!this.custPageLeftTopPanel.Expanded)
                {
                    this.custPageLeftSplitter.SplitPosition = 40;
                }
                else
                {
                    this.custPageLeftSplitter.SplitPosition = Convert.ToInt32((float)this.custPageLeftPanel.Height * this.mCustPageLeftVerSplitterRatio);
                }
                if (this.custPageRightTopPanel.Expanded)
                {
                    bool visible = this.custPageMiscControlPanel.Visible;
                    bool flag = this.custPagePropertyPanel.Visible;
                    if ((!visible || flag || this.custPageMiscControlPanel.Expanded) && (visible || !flag || this.custPagePropertyPanel.Expanded))
                    {
                        this.custPageRightSplitter.SplitPosition = Convert.ToInt32((float)this.custPageRightPanel.Height * this.mCustPageRightVerSplitterRatio);
                        return;
                    }
                    this.mRightTopPanelHeight = Convert.ToInt32((float)this.custPageRightPanel.Height * this.mCustPageRightVerSplitterRatio);
                    this.custPageRightTopPanel.Height = this.custPageRightPanel.Height - 40;
                    return;
                }
                this.custPageRightSplitter.SplitPosition = 40;
            }
        }

        private void restorePanelStatus()
        {
            if (this.custPageRightTopPanel.Visible)
            {
                if (!this.custPageRightTopPanel.Expanded)
                {
                    this.custPageRightTopPanel.Expanded = true;
                    if (!this.custPageMiscControlPanel.Visible)
                    {
                        this.custPagePropertyPanel.EnableExpanded = true;
                    }
                }
                else if (this.custPageRightTopPanel.Height == this.custPageRightPanel.Height - 40)
                {
                    this.custPageRightTopPanel.DockPadding.Bottom = 4;
                    this.custPageRightTopPanel.Height = this.mRightTopPanelHeight;
                    this.custPageRightTopPanel.EnableExpanded = true;
                    this.custPagePropertyPanel.Expanded = true;
                }
            }
            if (this.custPageMiscControlPanel.Visible && !this.custPageMiscControlPanel.Expanded)
            {
                this.custPageMiscControlPanel.Expanded = true;
                this.custPagePropertyPanel.EnableExpanded = true;
            }
            if (this.custPagePropertyPanel.Dock == DockStyle.Bottom)
            {
                this.custPageMiscControlPanel.DockPadding.Bottom = 4;
                this.custPageMiscControlPanel.EnableExpanded = true;
                this.custPageMiscControlPanel.Dock = DockStyle.Top;
                this.custPagePropertyPanel.Expanded = true;
                this.custPagePropertyPanel.EnableExpanded = true;
                this.custPagePropertyPanel.Dock = DockStyle.Fill;
            }
        }

        public static bool SaveDocument(Document doc, bool confirm)
        {
            if (doc == null)
            {
                return false;
            }
            if (doc.IsModified)
            {
                if (confirm)
                {
                    DialogResult dialogResult = MainForm.ShowMessageBox(LocalResources.GetString("MSG_ConfirmSaveChanges"), MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        return false;
                    }
                    if (dialogResult == DialogResult.No)
                    {
                        return true;
                    }
                    if (dialogResult == DialogResult.Yes && doc.IsOldFormatFile)
                    {
                        dialogResult = MainForm.ShowMessageBox(LocalResources.GetString("MSG_ConfirmSaveChanges"), MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
                if (doc.IsNewFile)
                {
                    return MainForm.SaveDocumentAs(doc);
                }
                if (doc.Save() && (doc == Document.MainCuiDoc || doc == Document.EnterpriseCUIDoc || Document.IsPartialDoc(doc)))
                {
                    MainForm.mainTab.forceMenuReload();
                }
            }
            return true;
        }

        public static bool SaveDocumentAs(Document doc)
        {
            bool flag;
            string str = LocalResources.GetString("SaveFileDialog.FilterString");
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = str
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            try
            {
                if (doc.IsNewFile)
                {
                    string upper = Path.GetFileNameWithoutExtension(saveFileDialog.FileName).ToUpper();
                    upper = upper.Trim().Replace(" ", "_");
                    doc.CUIFile.CustomizationSection.MenuGroup.Name = upper;
                }
                doc.SaveAs(saveFileDialog.FileName);
                flag = true;
            }
            catch (FileSaveException fileSaveException)
            {
                MainForm.ShowAlert(fileSaveException.Message);
                return false;
            }
            return flag;
        }

        public static void SelectMRU(ComboBox combo, string fn)
        {
            MainForm.SelectMRU(combo, fn, true);
        }

        private static void SelectMRU(ComboBox combo, string fn, bool firstMatch)
        {
            string str = (fn == null ? string.Empty : fn);
            IEnumerator enumerator = combo.Items.GetEnumerator();
            try
            {
                do
                {
                Label0:
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    DataRowView current = (DataRowView)enumerator.Current;
                    if ((int)current.Row.ItemArray.Length >= 2 && current.Row.ItemArray[1] != DBNull.Value && string.Compare(current.Row.ItemArray[1] as string, str, true) == 0)
                    {
                        combo.SelectedItem = current;
                    }
                    else
                    {
                        goto Label0;
                    }
                }
                while (!firstMatch);
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            string str1 = (combo.SelectedItem == null ? "" : ((DataRowView)combo.SelectedItem).Row.ItemArray[0].ToString());
            if (string.Compare(str1, LocalResources.GetString("DLG_AllCustomizationFiles")) != 0)
            {
                TransferTreeView parent = combo.Parent as TransferTreeView;
                if (parent == null)
                {
                    CustomizeTreeView customizeTreeView = combo.Parent as CustomizeTreeView;
                    if (customizeTreeView != null)
                    {
                        CustomizationSection currentCUIFile = customizeTreeView.TreeViewControl.CurrentCUIFile;
                        if (currentCUIFile != null)
                        {
                            if (currentCUIFile == Document.MainCuiFile)
                            {
                                str1 = LocalResources.GetString("DLG_Main_CUI");
                            }
                            else if (currentCUIFile == Document.EnterpriseCUIFile)
                            {
                                str1 = LocalResources.GetString("DLG_Enterprise_CUI");
                            }
                        }
                    }
                }
                else
                {
                    Document document = parent.Document;
                    if (document != null)
                    {
                        if (document == Document.MainCuiDoc)
                        {
                            str1 = LocalResources.GetString("DLG_Main_CUI");
                        }
                        else if (document == Document.EnterpriseCUIDoc)
                        {
                            str1 = LocalResources.GetString("DLG_Enterprise_CUI");
                        }
                    }
                }
            }
            else
            {
                str1 = LocalResources.GetString("DLG_All_CUI_Files");
            }
            ((CollapsiblePanel)combo.Parent.Parent).HeaderText = string.Format(LocalResources.GetString("DLG_Tree_Panel_Title"), str1);
        }

        public static void SelectMRUMainCUI(ComboBox combo, string fn)
        {
            MainForm.SelectMRU(combo, fn, false);
        }

        private void shortcutsGroupEventHandler(object sender, ShortcutsGroupEventArgs ea)
        {
            if (ea.Action != ShortcutsGroupAction.Select)
            {
                if (ShortcutsGroupAction.Update == ea.Action)
                {
                    this.custPagePropertyControl.Refresh2();
                    return;
                }
                if (ShortcutsGroupAction.Delete == ea.Action)
                {
                    CustomizationElement shortcutObject = this.getShortcutObject(ea);
                    if (shortcutObject != null)
                    {
                        CUITreeNode cUITreeNode = this.customizeTreeView.TreeViewControl.FindTreeNodeTag(shortcutObject);
                        if (cUITreeNode != null)
                        {
                            cUITreeNode.Delete();
                            return;
                        }
                    }
                }
                else if (ShortcutsGroupAction.New == ea.Action)
                {
                    CustomizationSection cui = ea.Cui;
                   // CUIKeyboardCommonNode.CreateTemporaryOverride(cui, this.customizeTreeView.TreeViewControl.FindTemporaryOverrideKeysNode());
                }
            }
            else
            {
                this.custPagePropertyControl.Clear();
                string uid = ea.Uid;
                if (uid != null)
                {
                    if (uid.Length == 0)
                    {
                        return;
                    }
                    if (ea.Cui != null)
                    {
                        CustomizationElement customizationElement = this.getShortcutObject(ea);
                        if (customizationElement != null)
                        {
                            KeysEditor.ShortcutName = this.shortcutsGroup.currentShortcutName();
                            this.custPagePropertyControl.SetProperties(customizationElement, null);
                            return;
                        }
                    }
                }
            }
        }

        private bool shouldRepopulateCustomizeTab()
        {
            if (this.shouldRepopulateTransferTab(this.leftTransferTreeView.Document))
            {
                return true;
            }
            return this.shouldRepopulateTransferTab(this.rightTransferTreeView.Document);
        }

        private bool shouldRepopulateTransferTab(Document doc)
        {
            if (doc == null || doc.CUIFile == null || doc.CUIFile.MenuGroupName == null)
            {
                return false;
            }
            CustomizationSection custTabCUIFileFor = Document.GetCustTabCUIFileFor(doc.CUIFile.MenuGroupName);
            if (custTabCUIFileFor != doc.CUIFile)
            {
                return false;
            }
            return custTabCUIFileFor.IsModified;
        }

        internal static void ShowAlert(string message)
        {
            if (HostApplicationServices.Current == null)
            {
                MessageBox.Show(message, "Customization Test");
                return;
            }
            if (Form.ActiveForm == null)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(message);
                return;
            }
            MessageBox.Show(Form.ActiveForm, message, HostApplicationServices.Current.Product, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void showButtonFor(CustomizationSection cui, object macroOrFlyout, string resourceIdSmall, string resourceIdLarge)
        {
            this.custPageRightPanelPropSplitter.Show();
            this.hideToolbarPreview();
            this.shortcutsGroup.Hide();
            this.shortcutsGroup.Enabled = false;
            this.buttonControl.Show();
            this.buttonControl.Enabled = true;
            this.buttonControl.SetCurrent(cui, macroOrFlyout, resourceIdSmall, resourceIdLarge);
            this.custPageMiscControlPanel.HeaderText = LocalResources.GetString("DLG_ButtonImage");
            this.custPageMiscControlPanel.Show();
        }

        private void showButtonForFlyout(CustomizationSection cui, ToolbarFlyout flyout)
        {
            this.showButtonFor(cui, flyout, flyout.SmallImage, flyout.LargeImage);
        }

        private void showButtonForMacro(CustomizationSection cui, Macro macro)
        {
            this.showButtonFor(cui, macro, macro.SmallImage, macro.LargeImage);
        }

        internal static DialogResult ShowMessageBox(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(text, MainForm.ApplicationCaption, buttons, icon, defaultButton);
        }

        internal static DialogResult ShowMessageBox(string text, MessageBoxButtons buttons)
        {
            return MessageBox.Show(text, MainForm.ApplicationCaption, buttons);
        }

        private void showShortcutsControl()
        {
            this.custPageRightPanelPropSplitter.Show();
            this.custPageMiscControlPanel.Collapsible = true;
            if (this.mShortcutHeight != -1)
            {
                this.custPageMiscControlPanel.Height = this.mShortcutHeight + 24;
                this.shortcutsGroup.Height = this.mShortcutHeight;
            }
            else
            {
                this.custPageMiscControlPanel.Height = 250;
                this.shortcutsGroup.Height = 226;
                this.mShortcutHeight = this.shortcutsGroup.Height;
            }
            this.shortcutsGroup.Show();
            this.shortcutsGroup.Enabled = true;
            this.custPageMiscControlPanel.HeaderText = LocalResources.GetString("DLG_Shortcuts");
            this.custPageMiscControlPanel.Show();
        }

        private void showToolbarPreview()
        {
            this.custPageMiscControlPanel.Collapsible = false;
            this.custPageMiscControlPanel.MinimumHeight = 142;
            this.custPageRightPanelPropSplitter.Show();
            this.toolbarPreview.Show();
            this.toolbarPreview.Enabled = true;
            this.buttonControl.Hide();
            this.shortcutsGroup.Hide();
            this.buttonControl.Enabled = false;
            this.shortcutsGroup.Enabled = false;
            this.custPageMiscControlPanel.HeaderText = LocalResources.GetString("DLG_Preview");
            this.custPageMiscControlPanel.Show();
        }

        private void showWorkspaceWindow(CUIWorkspaceNode w)
        {
            this.custPageRightTopPanel.Show();
            this.custPageRightSplitter.Show();
            this.workspaceTreeView.PopulateWorkspace(w, this.customizeTreeView.FilterSettings);
            this.workspaceTreeView.TreeViewControl.ItemSelected += new TreeViewControl.TVCItemSelectedArgsEventHandler(this.Workspace_ItemSelected);
            foreach (WorkspaceToolbar workspaceToolbar in (w.Tag as Workspace).WorkspaceToolbars)
            {
                workspaceToolbar.WSToolbarChanged += new WSToolbarChangedEventHandler(this.CustPagePropertyControl.WSToolbarChangedRefresh);
            }
        }

        private static bool somethingIsDirty()
        {
            bool flag;
            IEnumerator enumerator = Document.Documents.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    if (!((Document)enumerator.Current).IsModified)
                    {
                        continue;
                    }
                    flag = true;
                    return flag;
                }
                return false;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return flag;
        }

        private string stripDllNamePart(string str)
        {
            int num = str.IndexOf('|');
            if (num == -1 || num >= str.Length - 1)
            {
                return str;
            }
            return str.Substring(num + 1);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedTab < MainForm.TabPageIndex.CustomizeTab)
            {
                return;
            }
            if (this.mResizePending)
            {
                this.ResizeHandler();
            }
            if (this.mDlgMode == MainForm.DlgMode.TransferOnly)
            {
                //this.rightTransferTreeView.OnNewFile();
                //this.leftTransferTreeView.OnNewFile();
            }
            else if (this.SelectedTab != MainForm.TabPageIndex.TransferTab)
            {
                if (this.mToolbarWasHidden)
                {
                    this.toolbarPreview.displayToolbar(true);
                    this.mToolbarWasHidden = false;
                }
                if (this.customizeTreeView.MainCUIFile == null)
                {
                    //this.customizeTreeView.OnOpenAllCustomizationFiles();
                }
                else if (this.mNeedToRepopulate)
                {
                    //this.customizeTreeView.OnRepopulate();
                }
                this.customizeTreeView.TreeViewControl.TreeView.Select();
            }
            else
            {
                this.PopuplateWorkspaceModeDone();
                if (this.mDlgMode == MainForm.DlgMode.CUI || this.mDlgMode == MainForm.DlgMode.Export)
                {
                    if (this.leftTransferTreeView.Document == null)
                    {
                        //this.leftTransferTreeView.OnOpenMainCUI();
                    }
                    else if (this.mNeedToRepopulate)
                    {
                        //this.leftTransferTreeView.OnRepopulate();
                    }
                    if (this.rightTransferTreeView.Document == null)
                    {
                        //this.rightTransferTreeView.OnNewFile();
                    }
                    else if (this.mNeedToRepopulate)
                    {
                       // this.rightTransferTreeView.OnRepopulate();
                    }
                }
                else
                {
                    if (this.rightTransferTreeView.Document == null)
                    {
                        //this.rightTransferTreeView.OnOpenMainCUI();
                    }
                    else if (this.mNeedToRepopulate)
                    {
                        //this.rightTransferTreeView.OnRepopulate();
                    }
                    if (this.leftTransferTreeView.Document == null)
                    {
                        //this.leftTransferTreeView.OnNewFile();
                    }
                    else if (this.mNeedToRepopulate)
                    {
                        //this.leftTransferTreeView.OnRepopulate();
                    }
                }
                if (this.toolbarPreview.PreviewShow)
                {
                    this.toolbarPreview.previewHide();
                    this.mToolbarWasHidden = true;
                }
            }
            this.mResizePending = false;
            this.mNeedToRepopulate = false;
        }

        private void toggleBottomPanelState(CollapsiblePanel top, CollapsiblePanel bottom)
        {
            if (!bottom.Expanded)
            {
                bottom.Dock = DockStyle.Bottom;
                top.DockPadding.Bottom = 44;
                top.Dock = DockStyle.Fill;
                top.EnableExpanded = false;
                return;
            }
            top.DockPadding.Bottom = 4;
            top.Dock = DockStyle.Top;
            top.EnableExpanded = true;
            bottom.EnableExpanded = true;
            bottom.Dock = DockStyle.Fill;
        }

        private void toggleRightBottomPanelState(CollapsiblePanel top, CollapsiblePanel bottom)
        {
            if (bottom.Expanded)
            {
                top.DockPadding.Bottom = 4;
                top.Height = this.mRightTopPanelHeight;
                top.EnableExpanded = true;
                return;
            }
            this.mRightTopPanelHeight = top.Height;
            top.Height = this.custPageRightPanel.Height - 40;
            top.DockPadding.Bottom = 0;
            top.EnableExpanded = false;
        }

        private void toggleTopPanelState(CollapsiblePanel top, CollapsiblePanel bottom)
        {
            if (!top.Expanded)
            {
                bottom.EnableExpanded = false;
                return;
            }
            top.EnableExpanded = true;
            bottom.EnableExpanded = true;
        }

        internal void UninitializeButtonControlImages()
        {
            BitmapCache.Clear();
            this._bitmapCacheIntialized = false;
        }

        internal void UpdateSelectedNodeInCustomizeTree()
        {
            CUITreeNode selectedNode = this.customizeTreeView.TreeViewControl.TreeView.SelectedNode as CUITreeNode;
            if (selectedNode != null)
            {
                //selectedNode.UpdateLabel();
            }
        }

        private void Workspace_ItemSelected(object sender, TVCItemSelectedArgs e)
        {
            if (e.TreeNodes.Count == 0)
            {
                return;
            }
            this.custPagePropertyControl.Clear();
            CUITreeNode item = e.TreeNodes[0] as CUITreeNode;
            if (item == null || item.Tag == null)
            {
                return;
            }
            object tag = item.Tag;
            if (tag is CustomizationElement)
            {
                this.custPagePropertyPanel.HeaderText = LocalResources.GetString("JT_Properties");
                this.custPagePropertyControl.SetProperties(tag as CustomizationElement, item);
            }
        }

        private static void WSCURRENT_Modified(object sender, EventArgs e)
        {
            MainForm.mOrgWSCURRENTModified = true;
        }

        private enum DlgMode
        {
            CUI,
            Import,
            Export,
            TransferOnly
        }

        public enum TabPageIndex
        {
            CustomizeTab,
            TransferTab
        }
    }
}