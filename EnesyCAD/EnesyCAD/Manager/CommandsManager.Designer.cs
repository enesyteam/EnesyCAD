namespace EnesyCAD.Manager
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchBox = new Enesy.Forms.SearchBox();
            this.pnlLink = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblWebLink = new Enesy.Forms.LinkLabelData();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.dgrvCommands = new System.Windows.Forms.DataGridView();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.txtDescription = new Enesy.Forms.RichTextBoxData();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlLink.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvCommands)).BeginInit();
            this.pnlDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(584, 29);
            this.panel1.TabIndex = 0;
            // 
            // searchBox
            // 
            this.searchBox.BackColor = System.Drawing.Color.White;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.DisplayMember = "";
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchBox.Enabled = false;
            this.searchBox.Location = new System.Drawing.Point(329, 3);
            this.searchBox.Margin = new System.Windows.Forms.Padding(0);
            this.searchBox.MinimumSize = new System.Drawing.Size(130, 23);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(252, 23);
            this.searchBox.TabIndex = 0;
            this.searchBox.MouseHover += new System.EventHandler(this.searchBox_MouseHover);
            // 
            // pnlLink
            // 
            this.pnlLink.Controls.Add(this.lblStatus);
            this.pnlLink.Controls.Add(this.lblWebLink);
            this.pnlLink.Controls.Add(this.label2);
            this.pnlLink.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLink.Location = new System.Drawing.Point(0, 278);
            this.pnlLink.Name = "pnlLink";
            this.pnlLink.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLink.Size = new System.Drawing.Size(584, 22);
            this.pnlLink.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(280, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 18);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Help:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlViewer);
            this.panel3.Controls.Add(this.pnlDescription);
            this.panel3.Controls.Add(this.pnlLink);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 29);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 300);
            this.panel3.TabIndex = 2;
            // 
            // pnlViewer
            // 
            this.pnlViewer.Controls.Add(this.dgrvCommands);
            this.pnlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewer.Location = new System.Drawing.Point(0, 0);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlViewer.Size = new System.Drawing.Size(584, 190);
            this.pnlViewer.TabIndex = 3;
            // 
            // dgrvCommands
            // 
            this.dgrvCommands.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrvCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrvCommands.Location = new System.Drawing.Point(3, 0);
            this.dgrvCommands.Name = "dgrvCommands";
            this.dgrvCommands.ReadOnly = true;
            this.dgrvCommands.Size = new System.Drawing.Size(578, 190);
            this.dgrvCommands.TabIndex = 0;
            this.dgrvCommands.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrvCommands_CellDoubleClick);
            this.dgrvCommands.MouseHover += new System.EventHandler(this.dgrvCommands_MouseHover);
            // 
            // pnlDescription
            // 
            this.pnlDescription.Controls.Add(this.txtDescription);
            this.pnlDescription.Controls.Add(this.label1);
            this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDescription.Location = new System.Drawing.Point(0, 190);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.pnlDescription.Size = new System.Drawing.Size(584, 88);
            this.pnlDescription.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.DataMember = null;
            this.txtDescription.DisplayMember = "";
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(3, 20);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(578, 65);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Description";
            // 
            // CommandsManagerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 329);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 280);
            this.Name = "CommandsManagerDialog";
            this.ShowInTaskbar = false;
            this.Text = "Commands Manager";
            this.panel1.ResumeLayout(false);
            this.pnlLink.ResumeLayout(false);
            this.pnlLink.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlViewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrvCommands)).EndInit();
            this.pnlDescription.ResumeLayout(false);
            this.pnlDescription.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlLink;
        private System.Windows.Forms.Panel panel3;
        private Enesy.Forms.SearchBox searchBox;
        private System.Windows.Forms.Panel pnlDescription;
        private Enesy.Forms.RichTextBoxData txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Enesy.Forms.LinkLabelData lblWebLink;
        private System.Windows.Forms.Panel pnlViewer;
        private System.Windows.Forms.DataGridView dgrvCommands;
        private System.Windows.Forms.Label lblStatus;
    }
}