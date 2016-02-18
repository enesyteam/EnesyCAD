namespace Enesy.Forms
{
    public partial class XFInformationDialog
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grvInfos = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.searchBox1 = new Enesy.Forms.SearchBox();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvInfos)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.grvInfos);
            this.pnlMain.Location = new System.Drawing.Point(2, 27);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.pnlMain.Size = new System.Drawing.Size(735, 223);
            this.pnlMain.TabIndex = 0;
            // 
            // grvInfos
            // 
            this.grvInfos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvInfos.Location = new System.Drawing.Point(3, 5);
            this.grvInfos.MultiSelect = false;
            this.grvInfos.Name = "grvInfos";
            this.grvInfos.Size = new System.Drawing.Size(729, 218);
            this.grvInfos.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 24);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.searchBox1);
            this.panel2.Location = new System.Drawing.Point(491, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 24);
            this.panel2.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(483, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // searchBox1
            // 
            this.searchBox1.BackColor = System.Drawing.Color.White;
            this.searchBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBox1.Location = new System.Drawing.Point(0, 0);
            this.searchBox1.Margin = new System.Windows.Forms.Padding(0);
            this.searchBox1.MinimumSize = new System.Drawing.Size(100, 20);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.Size = new System.Drawing.Size(244, 24);
            this.searchBox1.TabIndex = 0;
            // 
            // XFInformationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 362);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMain);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(755, 400);
            this.Name = "XFInformationDialog";
            this.Text = "XFInformationDialog";
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvInfos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.Panel pnlMain;
        protected internal System.Windows.Forms.DataGridView grvInfos;
        protected internal System.Windows.Forms.Panel panel1;
        protected internal System.Windows.Forms.Panel panel2;
        protected internal System.Windows.Forms.MenuStrip menuStrip1;
        protected internal SearchBox searchBox1;




    }
}