namespace Enesy.EnesyCAD.Manager
{
    partial class CommandsManagerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Enesy.vn");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Autodesk");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("All", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.dgrvCommands = new System.Windows.Forms.DataGridView();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.pnlData = new System.Windows.Forms.Panel();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.txtDescription = new Enesy.Forms.RichTextBoxData();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblWebLink = new Enesy.Forms.LinkLabelData();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCommandStore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisplayFavorite = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisplayDescription = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisplayCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuError = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlLinkButton = new System.Windows.Forms.Panel();
            this.butFanPage = new System.Windows.Forms.Button();
            this.butHomePage = new System.Windows.Forms.Button();
            this.butYoutube = new System.Windows.Forms.Button();
            this.searchBox = new Enesy.Forms.SearchBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trvSubCommands = new Enesy.Forms.TreeViewSheets();
            this.cmnuRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuSub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuCommand = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuExpand = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgrvCommands)).BeginInit();
            this.pnlViewer.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.pnlDescription.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.pnlLinkButton.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgrvCommands
            // 
            this.dgrvCommands.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrvCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrvCommands.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgrvCommands.Location = new System.Drawing.Point(0, 3);
            this.dgrvCommands.Name = "dgrvCommands";
            this.dgrvCommands.ReadOnly = true;
            this.dgrvCommands.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgrvCommands.RowTemplate.ReadOnly = true;
            this.dgrvCommands.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrvCommands.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrvCommands.ShowEditingIcon = false;
            this.dgrvCommands.Size = new System.Drawing.Size(515, 178);
            this.dgrvCommands.TabIndex = 0;
            this.dgrvCommands.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrvCommands_CellDoubleClick);
            this.dgrvCommands.MouseHover += new System.EventHandler(this.dgrvCommands_MouseHover);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Location = new System.Drawing.Point(3, 6);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(61, 14);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Location = new System.Drawing.Point(365, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 14);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHelp.Location = new System.Drawing.Point(3, 3);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(31, 14);
            this.lblHelp.TabIndex = 3;
            this.lblHelp.Text = "Help:";
            // 
            // pnlViewer
            // 
            this.pnlViewer.Controls.Add(this.pnlData);
            this.pnlViewer.Controls.Add(this.pnlDescription);
            this.pnlViewer.Controls.Add(this.pnlStatus);
            this.pnlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewer.Location = new System.Drawing.Point(0, 0);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(518, 293);
            this.pnlViewer.TabIndex = 4;
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.dgrvCommands);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlData.Location = new System.Drawing.Point(0, 0);
            this.pnlData.Name = "pnlData";
            this.pnlData.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlData.Size = new System.Drawing.Size(518, 184);
            this.pnlData.TabIndex = 2;
            // 
            // pnlDescription
            // 
            this.pnlDescription.Controls.Add(this.lblDescription);
            this.pnlDescription.Controls.Add(this.txtDescription);
            this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDescription.Location = new System.Drawing.Point(0, 184);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.pnlDescription.Size = new System.Drawing.Size(518, 89);
            this.pnlDescription.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.DataMember = null;
            this.txtDescription.DisplayMember = "";
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtDescription.Location = new System.Drawing.Point(3, 23);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(512, 63);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Text = "...";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Controls.Add(this.lblHelp);
            this.pnlStatus.Controls.Add(this.lblWebLink);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 273);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(3);
            this.pnlStatus.Size = new System.Drawing.Size(518, 20);
            this.pnlStatus.TabIndex = 0;
            // 
            // lblWebLink
            // 
            this.lblWebLink.AutoSize = true;
            this.lblWebLink.DataMember = null;
            this.lblWebLink.DisplayMember = "";
            this.lblWebLink.Location = new System.Drawing.Point(40, 3);
            this.lblWebLink.Name = "lblWebLink";
            this.lblWebLink.Size = new System.Drawing.Size(40, 14);
            this.lblWebLink.TabIndex = 4;
            this.lblWebLink.TabStop = true;
            this.lblWebLink.Text = "https://";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mnuMain);
            this.panel1.Controls.Add(this.pnlLinkButton);
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(694, 29);
            this.panel1.TabIndex = 7;
            // 
            // mnuMain
            // 
            this.mnuMain.BackColor = System.Drawing.Color.Transparent;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView});
            this.mnuMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mnuMain.Location = new System.Drawing.Point(3, 3);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnuMain.Size = new System.Drawing.Size(338, 24);
            this.mnuMain.TabIndex = 5;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuImport,
            this.mnuSave,
            this.mnuCommandStore,
            this.mnuClose});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(172, 22);
            this.mnuOpen.Text = "Open ...";
            // 
            // mnuImport
            // 
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.Size = new System.Drawing.Size(172, 22);
            this.mnuImport.Text = "Import ...";
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(172, 22);
            this.mnuSave.Text = "Save";
            // 
            // mnuCommandStore
            // 
            this.mnuCommandStore.Name = "mnuCommandStore";
            this.mnuCommandStore.Size = new System.Drawing.Size(172, 22);
            this.mnuCommandStore.Text = "Command store ...";
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(172, 22);
            this.mnuClose.Text = "Close";
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDisplayFavorite,
            this.mnuDisplayDescription,
            this.mnuDisplayCommand,
            this.mnuError});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "View";
            // 
            // mnuDisplayFavorite
            // 
            this.mnuDisplayFavorite.Checked = true;
            this.mnuDisplayFavorite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuDisplayFavorite.Name = "mnuDisplayFavorite";
            this.mnuDisplayFavorite.Size = new System.Drawing.Size(205, 22);
            this.mnuDisplayFavorite.Text = "Display Favorite";
            this.mnuDisplayFavorite.Click += new System.EventHandler(this.mnuDisplayFavorite_Click);
            // 
            // mnuDisplayDescription
            // 
            this.mnuDisplayDescription.Checked = true;
            this.mnuDisplayDescription.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuDisplayDescription.Name = "mnuDisplayDescription";
            this.mnuDisplayDescription.Size = new System.Drawing.Size(205, 22);
            this.mnuDisplayDescription.Text = "Display Description";
            this.mnuDisplayDescription.Click += new System.EventHandler(this.mnuDisplayDescription_Click);
            // 
            // mnuDisplayCommand
            // 
            this.mnuDisplayCommand.Checked = true;
            this.mnuDisplayCommand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuDisplayCommand.Enabled = false;
            this.mnuDisplayCommand.Name = "mnuDisplayCommand";
            this.mnuDisplayCommand.Size = new System.Drawing.Size(205, 22);
            this.mnuDisplayCommand.Text = "Display Command name";
            // 
            // mnuError
            // 
            this.mnuError.Name = "mnuError";
            this.mnuError.Size = new System.Drawing.Size(205, 22);
            this.mnuError.Text = "Error log";
            // 
            // pnlLinkButton
            // 
            this.pnlLinkButton.Controls.Add(this.butFanPage);
            this.pnlLinkButton.Controls.Add(this.butHomePage);
            this.pnlLinkButton.Controls.Add(this.butYoutube);
            this.pnlLinkButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLinkButton.Location = new System.Drawing.Point(341, 3);
            this.pnlLinkButton.Name = "pnlLinkButton";
            this.pnlLinkButton.Size = new System.Drawing.Size(98, 23);
            this.pnlLinkButton.TabIndex = 4;
            // 
            // butFanPage
            // 
            this.butFanPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butFanPage.BackgroundImage = global::Enesy.Drawing.Icons._1455795385_facebook_circle_color;
            this.butFanPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butFanPage.FlatAppearance.BorderSize = 0;
            this.butFanPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butFanPage.Location = new System.Drawing.Point(5, 0);
            this.butFanPage.Name = "butFanPage";
            this.butFanPage.Padding = new System.Windows.Forms.Padding(3);
            this.butFanPage.Size = new System.Drawing.Size(23, 23);
            this.butFanPage.TabIndex = 6;
            this.butFanPage.UseVisualStyleBackColor = true;
            this.butFanPage.Click += new System.EventHandler(this.butFanPage_Click);
            // 
            // butHomePage
            // 
            this.butHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butHomePage.BackgroundImage = global::Enesy.Drawing.Icons._1456239772_Globe;
            this.butHomePage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butHomePage.FlatAppearance.BorderSize = 0;
            this.butHomePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butHomePage.Location = new System.Drawing.Point(34, 0);
            this.butHomePage.Name = "butHomePage";
            this.butHomePage.Padding = new System.Windows.Forms.Padding(3);
            this.butHomePage.Size = new System.Drawing.Size(23, 23);
            this.butHomePage.TabIndex = 5;
            this.butHomePage.UseVisualStyleBackColor = true;
            this.butHomePage.Click += new System.EventHandler(this.butHomePage_Click);
            // 
            // butYoutube
            // 
            this.butYoutube.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butYoutube.BackgroundImage = global::Enesy.Drawing.Icons._1455795873_YouTube;
            this.butYoutube.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butYoutube.FlatAppearance.BorderSize = 0;
            this.butYoutube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butYoutube.Location = new System.Drawing.Point(63, 0);
            this.butYoutube.Name = "butYoutube";
            this.butYoutube.Size = new System.Drawing.Size(23, 23);
            this.butYoutube.TabIndex = 4;
            this.butYoutube.UseVisualStyleBackColor = true;
            this.butYoutube.Click += new System.EventHandler(this.butYoutube_Click);
            // 
            // searchBox
            // 
            this.searchBox.BackColor = System.Drawing.Color.White;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.DisplayMember = "";
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchBox.Enabled = false;
            this.searchBox.Location = new System.Drawing.Point(439, 3);
            this.searchBox.Margin = new System.Windows.Forms.Padding(0);
            this.searchBox.MinimumSize = new System.Drawing.Size(130, 23);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(252, 23);
            this.searchBox.TabIndex = 0;
            this.searchBox.MouseHover += new System.EventHandler(this.searchBox_MouseHover);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvSubCommands);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlViewer);
            this.splitContainer1.Size = new System.Drawing.Size(694, 293);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 8;
            // 
            // trvSubCommands
            // 
            this.trvSubCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSubCommands.Location = new System.Drawing.Point(3, 3);
            this.trvSubCommands.Name = "trvSubCommands";
            treeNode1.Name = "ndoEnesy";
            treeNode1.Text = "Enesy.vn";
            treeNode2.Name = "ndoAutodesk";
            treeNode2.Text = "Autodesk";
            treeNode3.Name = "ndoRoot";
            treeNode3.Text = "All";
            this.trvSubCommands.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.trvSubCommands.Size = new System.Drawing.Size(168, 287);
            this.trvSubCommands.TabIndex = 0;
            // 
            // cmnuRoot
            // 
            this.cmnuRoot.Name = "cmnuRoot";
            this.cmnuRoot.Size = new System.Drawing.Size(61, 4);
            // 
            // cmnuSub
            // 
            this.cmnuSub.Name = "cmnuSub";
            this.cmnuSub.Size = new System.Drawing.Size(61, 4);
            // 
            // cmnuCommand
            // 
            this.cmnuCommand.Name = "cmnuCommand";
            this.cmnuCommand.Size = new System.Drawing.Size(61, 4);
            // 
            // cmnuExpand
            // 
            this.cmnuExpand.Name = "cmnuExpand";
            this.cmnuExpand.Size = new System.Drawing.Size(61, 4);
            // 
            // CommandsManagerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 322);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(670, 214);
            this.Name = "CommandsManagerDialog";
            this.ShowInTaskbar = false;
            this.Text = "Commands Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dgrvCommands)).EndInit();
            this.pnlViewer.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlDescription.ResumeLayout(false);
            this.pnlDescription.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.pnlLinkButton.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Enesy.Forms.SearchBox searchBox;
        private Enesy.Forms.RichTextBoxData txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DataGridView dgrvCommands;
        private System.Windows.Forms.Label lblStatus;
        private Enesy.Forms.LinkLabelData lblWebLink;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Panel pnlViewer;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Panel pnlDescription;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Enesy.Forms.TreeViewSheets trvSubCommands;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.Panel pnlLinkButton;
        private System.Windows.Forms.Button butFanPage;
        private System.Windows.Forms.Button butHomePage;
        private System.Windows.Forms.Button butYoutube;
        private System.Windows.Forms.ToolStripMenuItem mnuDisplayFavorite;
        private System.Windows.Forms.ToolStripMenuItem mnuDisplayDescription;
        private System.Windows.Forms.ToolStripMenuItem mnuDisplayCommand;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ContextMenuStrip cmnuRoot;
        private System.Windows.Forms.ContextMenuStrip cmnuSub;
        private System.Windows.Forms.ContextMenuStrip cmnuCommand;
        private System.Windows.Forms.ContextMenuStrip cmnuExpand;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuError;
        private System.Windows.Forms.ToolStripMenuItem mnuCommandStore;
    }
}