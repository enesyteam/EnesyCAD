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
using Autodesk.AutoCAD.Customization;

namespace Autodesk.AutoCAD.CustomizationEx
{
    public partial class MainForm : Form
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
            //this.quickStartLink.SetNewFeatureWorkshopTopic("CUI");
            //this.tabImgList.ImageStream = (ImageListStreamer)GlobalResources.GetObject("tabImgList.ImageStream");
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

        public bool CloseCustomizeDocument(Autodesk.AutoCAD.Customization.Document doc)
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

        public bool CloseTransferDocument(Autodesk.AutoCAD.Customization.Document doc, bool prompt)
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
            if (!customizationSection.ReadOnly && (customizationSection == Autodesk.AutoCAD.Customization.Document.MainCuiFile || customizationSection == Autodesk.AutoCAD.Customization.Document.EnterpriseCUIFile || customizationSection.ParentCUI != null))
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


        private void Document_DocumentOpened(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Customization.Document document = sender as Autodesk.AutoCAD.Customization.Document;
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
                            Autodesk.AutoCAD.Customization.Document partialDocument = Autodesk.AutoCAD.Customization.Document.GetPartialDocument(str);
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
                if (Autodesk.AutoCAD.Customization.Document.EnterpriseCUIFile != null)
                {
                    num = Autodesk.AutoCAD.Customization.Document.EnterpriseCUIFile.Workspaces.IndexOfWorkspaceName(systemVariable);
                }
                if (num <= -1)
                {
                    if (Autodesk.AutoCAD.Customization.Document.MainCuiFile != null)
                    {
                        num = Autodesk.AutoCAD.Customization.Document.MainCuiFile.Workspaces.IndexOfWorkspaceName(systemVariable);
                    }
                    if (num > -1)
                    {
                        item = Autodesk.AutoCAD.Customization.Document.MainCuiFile.Workspaces[num];
                    }
                }
                else
                {
                    item = Autodesk.AutoCAD.Customization.Document.EnterpriseCUIFile.Workspaces[num];
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

        public Autodesk.AutoCAD.Customization.Document NewTransferDocument(bool leftPanel)
        {
            if (!leftPanel)
            {
                Autodesk.AutoCAD.Customization.Document document = this.rightTransferTreeView.Document;
            }
            else
            {
                Autodesk.AutoCAD.Customization.Document document1 = this.leftTransferTreeView.Document;
            }
            Autodesk.AutoCAD.Customization.Document document2 = new Autodesk.AutoCAD.Customization.Document();
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
            foreach (Autodesk.AutoCAD.Customization.Document document in Autodesk.AutoCAD.Customization.Document.Documents)
            {
                if (!document.IsModified)
                {
                    continue;
                }
                if (!document.CUIFile.ReadOnly)
                {
                    if (document == Autodesk.AutoCAD.Customization.Document.MainCuiDoc || document == Autodesk.AutoCAD.Customization.Document.EnterpriseCUIDoc || Autodesk.AutoCAD.Customization.Document.IsPartialDoc(document))
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
            Autodesk.AutoCAD.Customization.Document.CloseAll();
            Autodesk.AutoCAD.Customization.Document.DocumentOpened -= new EventHandler(this.Document_DocumentOpened);
            TransferTreeView.StoreMRUToRegistry();
            //this.custPageCmdListCtrl.StoreToRegistry();
            //this.shortcutsGroup.StoreToRegistry();
            //this.UninitializeButtonControlImages();
            //MainForm.mPaletteConfig = false;
            //Utils.EnableToolPaletteDragDrop(false);
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
            //base.OnLoad(e);
            //Autodesk.AutoCAD.Customization.Document.CloseAll();
            //MainForm.resetWSCURRENT();
            //Autodesk.AutoCAD.Customization.Document.DocumentOpened += new EventHandler(this.Document_DocumentOpened);
            ////this.tabControl.SelectedIndex = -1;
            //if (this.mDlgMode == MainForm.DlgMode.CUI || this.mDlgMode == MainForm.DlgMode.TransferOnly)
            //{
            //    this.tabControl.SelectedIndex = 0;
            //}
            //else
            //{
            //    this.tabControl.SelectedIndex = 1;
            //}
            //if (this.custPageCmdListCtrl.Entries == null || this.custPageCmdListCtrl.Entries.Count == 0)
            //{
            //    this.custPageCmdListCtrl.EnterEmptyMode();
            //}
            //this.custPageMiscControlPanel.Hide();
            //this.custPageRightSplitter.Hide();
            //this.custPageRightPanelPropSplitter.Hide();
            //this.mImpPageSplitterRatio = (float)this.transferPageSplitter.SplitPosition / (float)base.Width;
            //this.mCustPageSplitterRatio = (float)this.custPageSplitter.SplitPosition / (float)base.Width;
            //this.mCustPageLeftVerSplitterRatio = (float)this.custPageLeftSplitter.SplitPosition / (float)this.custPageLeftPanel.Height;
            //this.mCustPageRightVerSplitterRatio = (float)this.custPageRightSplitter.SplitPosition / (float)this.custPageRightPanel.Height;
            //this.custPagePropertyControl.EventHandler += new PropertyControl.EventDelegate(this.CustPagePropertyControl_EventHandler);
            //this.custPageRightTopPanel.Hide();
            //TransferTreeView.LoadMRUFromRegistry();
            //if (MainForm.mInitialToolbarMenuGroup != null && MainForm.mInitialToolbarUID != null)
            //{
            //    //this.customizeTreeView.SelectInitialToolbarNode(MainForm.mInitialToolbarMenuGroup, MainForm.mInitialToolbarUID);
            //}
            //this.custPageLeftTopPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageLeftTopPanel_Collapsed);
            //this.custPageLeftBottomPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageLeftBottomPanel_Collapsed);
            //if (MainForm.mPaletteConfig)
            //{
            //    this.custPageLeftTopPanel.Collapse();
            //}
            //this.custPageRightTopPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageRightTopPanel_Collapsed);
            //this.custPageMiscControlPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPageMiscControlPanel_Collapsed);
            //this.custPagePropertyPanel.Collapsed += new CollapsiblePanel.ExpandedEventHandler(this.custPagePropertyPanel_Collapsed);
            //this.customizeTreeView.TreeViewControl.ItemSelected += new TreeViewControl.TVCItemSelectedArgsEventHandler(this.CustPage_TreeViewControl_ItemSelected);
            //Utils.EnableToolPaletteDragDrop(true);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            //this.toolbarPreview.displayToolbar(false);
        }

        public void PopulateCustomizeTabCommandList(Autodesk.AutoCAD.Customization.Document doc)
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

        public void PopulateTransferLeftPanelFromCUI(Autodesk.AutoCAD.Customization.Document doc)
        {
            this.leftTransferTreeView.TreeViewControl.PopulateAsStandAloneCUIFile(doc.CUIFile);
        }

        public void PopulateTransferRightPanelFromCUI(Autodesk.AutoCAD.Customization.Document doc)
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
            CustomizationSection[] mainCuiFile = new CustomizationSection[] { Autodesk.AutoCAD.Customization.Document.MainCuiFile, Autodesk.AutoCAD.Customization.Document.EnterpriseCUIFile };
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
            CustomizationSection[] mainCuiFile = new CustomizationSection[] { Autodesk.AutoCAD.Customization.Document.MainCuiFile, Autodesk.AutoCAD.Customization.Document.EnterpriseCUIFile };
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

        public static bool SaveDocument(Autodesk.AutoCAD.Customization.Document doc, bool confirm)
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
                if (doc.Save() && (doc == Autodesk.AutoCAD.Customization.Document.MainCuiDoc || doc == Autodesk.AutoCAD.Customization.Document.EnterpriseCUIDoc || Autodesk.AutoCAD.Customization.Document.IsPartialDoc(doc)))
                {
                    MainForm.mainTab.forceMenuReload();
                }
            }
            return true;
        }

        public static bool SaveDocumentAs(Autodesk.AutoCAD.Customization.Document doc)
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
                            if (currentCUIFile == Autodesk.AutoCAD.Customization.Document.MainCuiFile)
                            {
                                str1 = LocalResources.GetString("DLG_Main_CUI");
                            }
                            else if (currentCUIFile == Autodesk.AutoCAD.Customization.Document.EnterpriseCUIFile)
                            {
                                str1 = LocalResources.GetString("DLG_Enterprise_CUI");
                            }
                        }
                    }
                }
                else
                {
                    Autodesk.AutoCAD.Customization.Document document = parent.Document;
                    if (document != null)
                    {
                        if (document == Autodesk.AutoCAD.Customization.Document.MainCuiDoc)
                        {
                            str1 = LocalResources.GetString("DLG_Main_CUI");
                        }
                        else if (document == Autodesk.AutoCAD.Customization.Document.EnterpriseCUIDoc)
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

        private bool shouldRepopulateTransferTab(Autodesk.AutoCAD.Customization.Document doc)
        {
            if (doc == null || doc.CUIFile == null || doc.CUIFile.MenuGroupName == null)
            {
                return false;
            }
            CustomizationSection custTabCUIFileFor = Autodesk.AutoCAD.Customization.Document.GetCustTabCUIFileFor(doc.CUIFile.MenuGroupName);
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
            IEnumerator enumerator = Autodesk.AutoCAD.Customization.Document.Documents.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    if (!((Autodesk.AutoCAD.Customization.Document)enumerator.Current).IsModified)
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

        private void collapsiblePanel1_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Collap");
        }

        private void collapsiblePanel1_Collapsed(object sender, EventArgs args)
        {
            this.handleSplitter(this.custPageLeftSplitter, this.collapsiblePanel1.Expanded);
            this.toggleBottomPanelState(this.custPageLeftTopPanel, this.collapsiblePanel1);
        }
    }
}