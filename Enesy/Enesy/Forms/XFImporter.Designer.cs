namespace Enesy.Forms
{
    partial class XFImporter
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
            this.mnuiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grvMain = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rchdDescription = new Enesy.Forms.RichTextBoxData();
            this.schFilter = new Enesy.Forms.SearchBox();
            this.panel1.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvMain)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.mnuMain);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 27);
            this.panel1.TabIndex = 0;
            // 
            // mnuMain
            // 
            this.mnuMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(528, 27);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuiOpen});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.openToolStripMenuItem.Text = "File";
            // 
            // mnuiOpen
            // 
            this.mnuiOpen.Name = "mnuiOpen";
            this.mnuiOpen.Size = new System.Drawing.Size(103, 22);
            this.mnuiOpen.Text = "Open";
            this.mnuiOpen.Click += new System.EventHandler(this.mnuiOpen_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.schFilter);
            this.panel2.Location = new System.Drawing.Point(527, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(247, 27);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.grvMain);
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(774, 205);
            this.panel3.TabIndex = 2;
            // 
            // grvMain
            // 
            this.grvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvMain.Location = new System.Drawing.Point(0, 0);
            this.grvMain.Name = "grvMain";
            this.grvMain.Size = new System.Drawing.Size(774, 205);
            this.grvMain.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 241);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 119);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rchdDescription);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(766, 92);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Description";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(766, 92);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Errors";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rchdDescription
            // 
            this.rchdDescription.DataMember = null;
            this.rchdDescription.DisplayMember = "";
            this.rchdDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchdDescription.Location = new System.Drawing.Point(3, 3);
            this.rchdDescription.Name = "rchdDescription";
            this.rchdDescription.Size = new System.Drawing.Size(760, 86);
            this.rchdDescription.TabIndex = 0;
            this.rchdDescription.Text = "";
            // 
            // schFilter
            // 
            this.schFilter.BackColor = System.Drawing.Color.White;
            this.schFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.schFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schFilter.Location = new System.Drawing.Point(0, 0);
            this.schFilter.Margin = new System.Windows.Forms.Padding(0);
            this.schFilter.MinimumSize = new System.Drawing.Size(130, 23);
            this.schFilter.Name = "schFilter";
            this.schFilter.Size = new System.Drawing.Size(247, 27);
            this.schFilter.TabIndex = 0;
            // 
            // XFImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 362);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(790, 400);
            this.Name = "XFImporter";
            this.Text = "XFImporter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvMain)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView grvMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem mnuiOpen;
        private RichTextBoxData rchdDescription;
        private SearchBox schFilter;
    }
}