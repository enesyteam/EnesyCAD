namespace Enesy.Forms
{
    partial class XFManager
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.schSearch = new Enesy.Forms.SearchBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grvMain = new System.Windows.Forms.DataGridView();
            this.rchDescription = new Enesy.Forms.RichTextBoxData();
            this.label1 = new System.Windows.Forms.Label();
            this.lldHelp = new Enesy.Forms.LinkLabelData();
            this.panel1.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.mnuMain);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 27);
            this.panel1.TabIndex = 0;
            // 
            // mnuMain
            // 
            this.mnuMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(524, 27);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.mnuiImport});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.openToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // mnuiImport
            // 
            this.mnuiImport.Name = "mnuiImport";
            this.mnuiImport.Size = new System.Drawing.Size(110, 22);
            this.mnuiImport.Text = "Import";
            this.mnuiImport.Click += new System.EventHandler(this.mnuiImport_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.schSearch);
            this.panel2.Location = new System.Drawing.Point(527, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(247, 27);
            this.panel2.TabIndex = 1;
            // 
            // schSearch
            // 
            this.schSearch.BackColor = System.Drawing.Color.White;
            this.schSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.schSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schSearch.Location = new System.Drawing.Point(0, 0);
            this.schSearch.Margin = new System.Windows.Forms.Padding(0);
            this.schSearch.MinimumSize = new System.Drawing.Size(130, 23);
            this.schSearch.Name = "schSearch";
            this.schSearch.Size = new System.Drawing.Size(247, 27);
            this.schSearch.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.grvMain);
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(774, 202);
            this.panel3.TabIndex = 2;
            // 
            // grvMain
            // 
            this.grvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvMain.Location = new System.Drawing.Point(0, 0);
            this.grvMain.Name = "grvMain";
            this.grvMain.Size = new System.Drawing.Size(774, 202);
            this.grvMain.TabIndex = 0;
            // 
            // rchDescription
            // 
            this.rchDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rchDescription.DataMember = null;
            this.rchDescription.DisplayMember = "";
            this.rchDescription.Location = new System.Drawing.Point(0, 241);
            this.rchDescription.Name = "rchDescription";
            this.rchDescription.Size = new System.Drawing.Size(774, 93);
            this.rchDescription.TabIndex = 3;
            this.rchDescription.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Help:";
            // 
            // lldHelp
            // 
            this.lldHelp.AutoSize = true;
            this.lldHelp.DataMember = null;
            this.lldHelp.DisplayMember = "";
            this.lldHelp.Location = new System.Drawing.Point(49, 339);
            this.lldHelp.Name = "lldHelp";
            this.lldHelp.Size = new System.Drawing.Size(135, 14);
            this.lldHelp.TabIndex = 5;
            this.lldHelp.TabStop = true;
            this.lldHelp.Text = "https://youtube.com/Enesy";
            // 
            // XFManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 362);
            this.Controls.Add(this.lldHelp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rchDescription);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(790, 400);
            this.Name = "XFManager";
            this.Text = "XFManager";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuiImport;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView grvMain;
        private Enesy.Forms.RichTextBoxData rchDescription;
        private System.Windows.Forms.Label label1;
        private LinkLabelData lldHelp;
        private SearchBox schSearch;
    }
}