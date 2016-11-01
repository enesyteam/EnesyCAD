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
            System.Windows.Forms.Panel scrollPanel;
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RemoveAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Split = new System.Windows.Forms.SplitContainer();
            this.clbList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.previewInfoText = new System.Windows.Forms.ToolStripLabel();
            this.btnResetZoom = new System.Windows.Forms.ToolStripButton();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteAsBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToOriginalCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            scrollPanel = new System.Windows.Forms.Panel();
            scrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.Split.Panel1.SuspendLayout();
            this.Split.Panel2.SuspendLayout();
            this.Split.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.rightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.BackColor = System.Drawing.SystemColors.Control;
            scrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            scrollPanel.Controls.Add(this.pictureBox);
            scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            scrollPanel.Location = new System.Drawing.Point(0, 0);
            scrollPanel.Margin = new System.Windows.Forms.Padding(4);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new System.Drawing.Size(388, 253);
            scrollPanel.TabIndex = 4;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(3, 4);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(250, 150);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
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
            this.RemoveAllToolStripButton.Size = new System.Drawing.Size(63, 24);
            this.RemoveAllToolStripButton.Text = "Clear";
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
            this.Split.Panel1.Controls.Add(this.clbList);
            // 
            // Split.Panel2
            // 
            this.Split.Panel2.Controls.Add(this.splitContainer1);
            this.Split.Panel2MinSize = 100;
            this.Split.Size = new System.Drawing.Size(388, 536);
            this.Split.SplitterDistance = 250;
            this.Split.TabIndex = 3;
            // 
            // clbList
            // 
            this.clbList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.clbList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbList.FullRowSelect = true;
            this.clbList.LabelEdit = true;
            this.clbList.Location = new System.Drawing.Point(0, 0);
            this.clbList.Name = "clbList";
            this.clbList.Size = new System.Drawing.Size(388, 250);
            this.clbList.TabIndex = 3;
            this.clbList.UseCompatibleStateImageBehavior = false;
            this.clbList.View = System.Windows.Forms.View.Details;
            this.clbList.SelectedIndexChanged += new System.EventHandler(this.clbList_SelectedIndexChanged);
            this.clbList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clbList_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(scrollPanel);
            this.splitContainer1.Size = new System.Drawing.Size(388, 282);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewInfoText,
            this.btnResetZoom});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(388, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // previewInfoText
            // 
            this.previewInfoText.Name = "previewInfoText";
            this.previewInfoText.Size = new System.Drawing.Size(49, 22);
            this.previewInfoText.Text = "Zoom";
            // 
            // btnResetZoom
            // 
            this.btnResetZoom.Name = "btnResetZoom";
            this.btnResetZoom.Size = new System.Drawing.Size(23, 22);
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
            scrollPanel.ResumeLayout(false);
            scrollPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Split.Panel1.ResumeLayout(false);
            this.Split.Panel2.ResumeLayout(false);
            this.Split.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.rightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RemoveAllToolStripButton;
        private System.Windows.Forms.SplitContainer Split;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteAsBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToOriginalCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
        private System.Windows.Forms.ListView clbList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel previewInfoText;
        private System.Windows.Forms.ToolStripButton btnResetZoom;
    }
}
