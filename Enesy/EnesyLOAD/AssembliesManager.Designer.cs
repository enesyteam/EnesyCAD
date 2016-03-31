namespace RegisterAutoCADpzo
{
    partial class AssembliesManager
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
            this.lstAssemblies = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUnload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstAssemblies
            // 
            this.lstAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAssemblies.FormattingEnabled = true;
            this.lstAssemblies.HorizontalScrollbar = true;
            this.lstAssemblies.Location = new System.Drawing.Point(10, 17);
            this.lstAssemblies.Name = "lstAssemblies";
            this.lstAssemblies.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAssemblies.Size = new System.Drawing.Size(230, 147);
            this.lstAssemblies.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label3.Location = new System.Drawing.Point(-47, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(300, 1);
            this.label3.TabIndex = 12;
            // 
            // btnUnload
            // 
            this.btnUnload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnload.Location = new System.Drawing.Point(91, 189);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(67, 23);
            this.btnUnload.TabIndex = 10;
            this.btnUnload.Text = "Unload";
            this.btnUnload.UseVisualStyleBackColor = true;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(173, 189);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AssembliesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 221);
            this.Controls.Add(this.lstAssemblies);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUnload);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AssembliesManager";
            this.ShowIcon = false;
            this.Text = "Assemblies Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAssemblies;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUnload;
        private System.Windows.Forms.Button btnClose;

    }
}