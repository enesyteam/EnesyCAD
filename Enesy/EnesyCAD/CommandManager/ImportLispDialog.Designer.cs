namespace Enesy.EnesyCAD.CommandManager
{
    partial class ImportLispDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportLispDialog));
            this.searchBox = new Enesy.Forms.SearchBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butOpen = new System.Windows.Forms.Button();
            this.tabMain = new Enesy.Forms.TabWizardHeaderControl();
            this.tabFunction = new System.Windows.Forms.TabPage();
            this.dgrvFunction = new System.Windows.Forms.DataGridView();
            this.tabError = new System.Windows.Forms.TabPage();
            this.dgrvError = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.prgImportFiles = new System.Windows.Forms.ProgressBar();
            this.butError = new System.Windows.Forms.Button();
            this.butFunction = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvFunction)).BeginInit();
            this.tabError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvError)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.BackColor = System.Drawing.Color.White;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.DisplayMember = "";
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchBox.Location = new System.Drawing.Point(363, 3);
            this.searchBox.Margin = new System.Windows.Forms.Padding(0);
            this.searchBox.MinimumSize = new System.Drawing.Size(130, 23);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(252, 23);
            this.searchBox.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butOpen);
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Size = new System.Drawing.Size(618, 29);
            this.panel1.TabIndex = 5;
            // 
            // butOpen
            // 
            this.butOpen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butOpen.BackgroundImage")));
            this.butOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.butOpen.FlatAppearance.BorderSize = 0;
            this.butOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butOpen.Location = new System.Drawing.Point(12, 3);
            this.butOpen.Name = "butOpen";
            this.butOpen.Padding = new System.Windows.Forms.Padding(3);
            this.butOpen.Size = new System.Drawing.Size(23, 23);
            this.butOpen.TabIndex = 4;
            this.butOpen.UseVisualStyleBackColor = true;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabFunction);
            this.tabMain.Controls.Add(this.tabError);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 29);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(618, 245);
            this.tabMain.TabIndex = 7;
            this.tabMain.TabsVisible = false;
            // 
            // tabFunction
            // 
            this.tabFunction.Controls.Add(this.dgrvFunction);
            this.tabFunction.Location = new System.Drawing.Point(4, 4);
            this.tabFunction.Name = "tabFunction";
            this.tabFunction.Padding = new System.Windows.Forms.Padding(3);
            this.tabFunction.Size = new System.Drawing.Size(610, 218);
            this.tabFunction.TabIndex = 0;
            this.tabFunction.Text = "Function";
            this.tabFunction.UseVisualStyleBackColor = true;
            // 
            // dgrvFunction
            // 
            this.dgrvFunction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrvFunction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrvFunction.Location = new System.Drawing.Point(3, 3);
            this.dgrvFunction.Name = "dgrvFunction";
            this.dgrvFunction.Size = new System.Drawing.Size(604, 212);
            this.dgrvFunction.TabIndex = 0;
            // 
            // tabError
            // 
            this.tabError.Controls.Add(this.dgrvError);
            this.tabError.Location = new System.Drawing.Point(4, 4);
            this.tabError.Name = "tabError";
            this.tabError.Padding = new System.Windows.Forms.Padding(3);
            this.tabError.Size = new System.Drawing.Size(610, 218);
            this.tabError.TabIndex = 1;
            this.tabError.Text = "Error(s)";
            this.tabError.UseVisualStyleBackColor = true;
            // 
            // dgrvError
            // 
            this.dgrvError.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrvError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrvError.Location = new System.Drawing.Point(3, 3);
            this.dgrvError.Name = "dgrvError";
            this.dgrvError.Size = new System.Drawing.Size(604, 212);
            this.dgrvError.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pnlProgress);
            this.panel2.Controls.Add(this.butError);
            this.panel2.Controls.Add(this.butFunction);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 274);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(618, 29);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(150, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 7, 3, 8);
            this.panel3.Size = new System.Drawing.Size(213, 29);
            this.panel3.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Location = new System.Drawing.Point(-51, 7);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(261, 14);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Click Open button or press Ctrl + O to import .lsp files";
            // 
            // pnlProgress
            // 
            this.pnlProgress.Controls.Add(this.prgImportFiles);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlProgress.Location = new System.Drawing.Point(363, 0);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Padding = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.pnlProgress.Size = new System.Drawing.Size(255, 29);
            this.pnlProgress.TabIndex = 2;
            this.pnlProgress.Visible = false;
            // 
            // prgImportFiles
            // 
            this.prgImportFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgImportFiles.Location = new System.Drawing.Point(3, 7);
            this.prgImportFiles.Name = "prgImportFiles";
            this.prgImportFiles.Size = new System.Drawing.Size(249, 15);
            this.prgImportFiles.TabIndex = 0;
            // 
            // butError
            // 
            this.butError.Dock = System.Windows.Forms.DockStyle.Left;
            this.butError.FlatAppearance.BorderSize = 0;
            this.butError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butError.Location = new System.Drawing.Point(75, 0);
            this.butError.Name = "butError";
            this.butError.Size = new System.Drawing.Size(75, 29);
            this.butError.TabIndex = 1;
            this.butError.Text = "Error(s)";
            this.butError.UseVisualStyleBackColor = true;
            this.butError.Click += new System.EventHandler(this.butError_Click);
            // 
            // butFunction
            // 
            this.butFunction.Dock = System.Windows.Forms.DockStyle.Left;
            this.butFunction.FlatAppearance.BorderSize = 0;
            this.butFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butFunction.Location = new System.Drawing.Point(0, 0);
            this.butFunction.Name = "butFunction";
            this.butFunction.Size = new System.Drawing.Size(75, 29);
            this.butFunction.TabIndex = 0;
            this.butFunction.Text = "Function";
            this.butFunction.UseVisualStyleBackColor = true;
            this.butFunction.Click += new System.EventHandler(this.butFunction_Click);
            // 
            // ImportLispDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 303);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ImportLispDialog";
            this.ShowInTaskbar = false;
            this.Text = "Import Lisp Funtion";
            this.panel1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrvFunction)).EndInit();
            this.tabError.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrvError)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Enesy.Forms.SearchBox searchBox;
        private System.Windows.Forms.Panel panel1;
        private Enesy.Forms.TabWizardHeaderControl tabMain;
        private System.Windows.Forms.TabPage tabFunction;
        private System.Windows.Forms.TabPage tabError;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butFunction;
        private System.Windows.Forms.Button butError;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.ProgressBar prgImportFiles;
        private System.Windows.Forms.DataGridView dgrvFunction;
        private System.Windows.Forms.DataGridView dgrvError;
        private System.Windows.Forms.Button butOpen;
    }
}