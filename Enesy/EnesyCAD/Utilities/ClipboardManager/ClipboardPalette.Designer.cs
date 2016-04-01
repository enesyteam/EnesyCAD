namespace Enesy.EnesyCAD.Utilities.ClipboardManager
{
    partial class ClipboardPalette
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RemoveAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Split = new System.Windows.Forms.SplitContainer();
            this.clipboardDataGridView = new System.Windows.Forms.DataGridView();
            this.CbName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Img = new System.Windows.Forms.PictureBox();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteAsBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToOriginalCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.Split.Panel1.SuspendLayout();
            this.Split.Panel2.SuspendLayout();
            this.Split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Img)).BeginInit();
            this.rightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveAllToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(388, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RemoveAllToolStripButton
            // 
            this.RemoveAllToolStripButton.Image = global::Enesy.EnesyCAD.Properties.Resources.ClearClipBoard;
            this.RemoveAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveAllToolStripButton.Name = "RemoveAllToolStripButton";
            this.RemoveAllToolStripButton.Size = new System.Drawing.Size(105, 24);
            this.RemoveAllToolStripButton.Text = "Remove All";
            this.RemoveAllToolStripButton.Click += new System.EventHandler(this.RemoveAllToolStripButton_Click_1);
            // 
            // Split
            // 
            this.Split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Split.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Split.Location = new System.Drawing.Point(0, 27);
            this.Split.Name = "Split";
            this.Split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Split.Panel1
            // 
            this.Split.Panel1.Controls.Add(this.clipboardDataGridView);
            // 
            // Split.Panel2
            // 
            this.Split.Panel2.Controls.Add(this.Img);
            this.Split.Panel2MinSize = 100;
            this.Split.Size = new System.Drawing.Size(388, 536);
            this.Split.SplitterDistance = 326;
            this.Split.TabIndex = 3;
            // 
            // clipboardDataGridView
            // 
            this.clipboardDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.clipboardDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clipboardDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CbName,
            this.Time});
            this.clipboardDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clipboardDataGridView.Location = new System.Drawing.Point(0, 0);
            this.clipboardDataGridView.Name = "clipboardDataGridView";
            this.clipboardDataGridView.RowTemplate.Height = 24;
            this.clipboardDataGridView.Size = new System.Drawing.Size(388, 326);
            this.clipboardDataGridView.TabIndex = 2;
            this.clipboardDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.clipboardDataGridView_CellMouseDown_1);
            this.clipboardDataGridView.SelectionChanged += new System.EventHandler(this.clipboardDataGridView_SelectionChanged_1);
            this.clipboardDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clipboardDataGridView_MouseDown_1);
            // 
            // CbName
            // 
            this.CbName.HeaderText = "Name";
            this.CbName.Name = "CbName";
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // Img
            // 
            this.Img.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Img.Location = new System.Drawing.Point(0, 0);
            this.Img.Name = "Img";
            this.Img.Size = new System.Drawing.Size(388, 206);
            this.Img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Img.TabIndex = 3;
            this.Img.TabStop = false;
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PasteToolStripMenuItem,
            this.PasteAsBlockToolStripMenuItem,
            this.PasteToOriginalCoordinatesToolStripMenuItem,
            this.RenameToolStripMenuItem,
            this.RemoveToolStripMenuItem});
            this.rightClickMenu.Name = "rightClickMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(256, 124);
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.PasteToolStripMenuItem.Text = "Paste";
            this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // PasteAsBlockToolStripMenuItem
            // 
            this.PasteAsBlockToolStripMenuItem.Name = "PasteAsBlockToolStripMenuItem";
            this.PasteAsBlockToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.PasteAsBlockToolStripMenuItem.Text = "Paste as Block";
            this.PasteAsBlockToolStripMenuItem.Click += new System.EventHandler(this.PasteAsBlockToolStripMenuItem_Click_1);
            // 
            // PasteToOriginalCoordinatesToolStripMenuItem
            // 
            this.PasteToOriginalCoordinatesToolStripMenuItem.Name = "PasteToOriginalCoordinatesToolStripMenuItem";
            this.PasteToOriginalCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.PasteToOriginalCoordinatesToolStripMenuItem.Text = "Paste to Orgin Coordinates";
            this.PasteToOriginalCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.PasteToOriginalCoordinatesToolStripMenuItem_Click_1);
            // 
            // RenameToolStripMenuItem
            // 
            this.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem";
            this.RenameToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.RenameToolStripMenuItem.Text = "Rename";
            this.RenameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click_1);
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.RemoveToolStripMenuItem.Text = "Remove";
            this.RemoveToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click_1);
            // 
            // ClipboardPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Split);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ClipboardPalette";
            this.Size = new System.Drawing.Size(388, 563);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Split.Panel1.ResumeLayout(false);
            this.Split.Panel2.ResumeLayout(false);
            this.Split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clipboardDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Img)).EndInit();
            this.rightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RemoveAllToolStripButton;
        private System.Windows.Forms.SplitContainer Split;
        private System.Windows.Forms.DataGridView clipboardDataGridView;
        private System.Windows.Forms.PictureBox Img;
        private System.Windows.Forms.DataGridViewTextBoxColumn CbName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteAsBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToOriginalCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
    }
}
