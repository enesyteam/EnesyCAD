namespace Enesy.EnesyCAD.CommandManager
{
    partial class UCSuite
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butHelp = new System.Windows.Forms.Button();
            this.butClose = new System.Windows.Forms.Button();
            this.butRemove = new System.Windows.Forms.Button();
            this.butAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstvFiles = new Enesy.Forms.ListViewFilePath();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 25);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "List of user commands:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.butHelp);
            this.panel2.Controls.Add(this.butClose);
            this.panel2.Controls.Add(this.butRemove);
            this.panel2.Controls.Add(this.butAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 198);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(588, 32);
            this.panel2.TabIndex = 1;
            // 
            // butHelp
            // 
            this.butHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butHelp.Location = new System.Drawing.Point(508, 4);
            this.butHelp.Name = "butHelp";
            this.butHelp.Size = new System.Drawing.Size(75, 25);
            this.butHelp.TabIndex = 3;
            this.butHelp.Text = "Help";
            this.butHelp.UseVisualStyleBackColor = true;
            this.butHelp.Click += new System.EventHandler(this.butHelp_Click);
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(430, 4);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 25);
            this.butClose.TabIndex = 2;
            this.butClose.Text = "Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butRemove
            // 
            this.butRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRemove.Enabled = false;
            this.butRemove.Location = new System.Drawing.Point(349, 4);
            this.butRemove.Name = "butRemove";
            this.butRemove.Size = new System.Drawing.Size(75, 25);
            this.butRemove.TabIndex = 1;
            this.butRemove.Text = "Remove";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
            // 
            // butAdd
            // 
            this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAdd.Location = new System.Drawing.Point(268, 4);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(75, 25);
            this.butAdd.TabIndex = 0;
            this.butAdd.Text = "Add ...";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lstvFiles);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(588, 173);
            this.panel3.TabIndex = 2;
            // 
            // lstvFiles
            // 
            this.lstvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvFiles.FullRowSelect = true;
            this.lstvFiles.Location = new System.Drawing.Point(3, 3);
            this.lstvFiles.Name = "lstvFiles";
            this.lstvFiles.Size = new System.Drawing.Size(582, 167);
            this.lstvFiles.TabIndex = 0;
            this.lstvFiles.UseCompatibleStateImageBehavior = false;
            this.lstvFiles.View = System.Windows.Forms.View.Details;
            this.lstvFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvFiles_ItemSelectionChanged);
            // 
            // UCSuite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 230);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UCSuite";
            this.Text = "User Command Suite";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Enesy.Forms.ListViewFilePath lstvFiles;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.Button butHelp;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Button butRemove;
    }
}