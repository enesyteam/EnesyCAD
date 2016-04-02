namespace Enesy.EnesyCAD.Utilities
{
    partial class CoordinatePickerDialog
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
            this.butClose = new System.Windows.Forms.Button();
            this.butExport = new System.Windows.Forms.Button();
            this.rdoWorld = new System.Windows.Forms.RadioButton();
            this.rdoCurrent = new System.Windows.Forms.RadioButton();
            this.lblLine2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.butPick = new System.Windows.Forms.Button();
            this.butSelect = new System.Windows.Forms.Button();
            this.lblLine1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.butBlock = new System.Windows.Forms.Button();
            this.cboUcs = new System.Windows.Forms.ComboBox();
            this.rdoUcsName = new System.Windows.Forms.RadioButton();
            this.chkZcoord = new System.Windows.Forms.CheckBox();
            this.cboOption = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(307, 144);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(83, 26);
            this.butClose.TabIndex = 1;
            this.butClose.Text = "Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butExport
            // 
            this.butExport.Location = new System.Drawing.Point(205, 144);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(96, 26);
            this.butExport.TabIndex = 2;
            this.butExport.Text = "Export";
            this.butExport.UseVisualStyleBackColor = true;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // rdoWorld
            // 
            this.rdoWorld.AutoSize = true;
            this.rdoWorld.Checked = true;
            this.rdoWorld.Location = new System.Drawing.Point(125, 83);
            this.rdoWorld.Name = "rdoWorld";
            this.rdoWorld.Size = new System.Drawing.Size(78, 17);
            this.rdoWorld.TabIndex = 19;
            this.rdoWorld.TabStop = true;
            this.rdoWorld.Text = "World UCS";
            this.rdoWorld.UseVisualStyleBackColor = true;
            // 
            // rdoCurrent
            // 
            this.rdoCurrent.AutoSize = true;
            this.rdoCurrent.Location = new System.Drawing.Point(12, 83);
            this.rdoCurrent.Name = "rdoCurrent";
            this.rdoCurrent.Size = new System.Drawing.Size(84, 17);
            this.rdoCurrent.TabIndex = 18;
            this.rdoCurrent.Text = "Current UCS";
            this.rdoCurrent.UseVisualStyleBackColor = true;
            // 
            // lblLine2
            // 
            this.lblLine2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine2.Location = new System.Drawing.Point(109, 70);
            this.lblLine2.Name = "lblLine2";
            this.lblLine2.Size = new System.Drawing.Size(279, 2);
            this.lblLine2.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Coordinate system";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 154);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(88, 13);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Press F1 for Help";
            // 
            // butPick
            // 
            this.butPick.Enabled = false;
            this.butPick.Location = new System.Drawing.Point(146, 23);
            this.butPick.Name = "butPick";
            this.butPick.Size = new System.Drawing.Size(107, 25);
            this.butPick.TabIndex = 13;
            this.butPick.Text = "Pick point <";
            this.butPick.UseVisualStyleBackColor = true;
            this.butPick.Click += new System.EventHandler(this.butPick_Click);
            // 
            // butSelect
            // 
            this.butSelect.Location = new System.Drawing.Point(12, 23);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(107, 25);
            this.butSelect.TabIndex = 12;
            this.butSelect.Text = "Select polyline ...";
            this.butSelect.UseVisualStyleBackColor = true;
            this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
            // 
            // lblLine1
            // 
            this.lblLine1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine1.Location = new System.Drawing.Point(44, 13);
            this.lblLine1.Name = "lblLine1";
            this.lblLine1.Size = new System.Drawing.Size(345, 2);
            this.lblLine1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select";
            // 
            // butBlock
            // 
            this.butBlock.Enabled = false;
            this.butBlock.Location = new System.Drawing.Point(278, 23);
            this.butBlock.Name = "butBlock";
            this.butBlock.Size = new System.Drawing.Size(107, 25);
            this.butBlock.TabIndex = 20;
            this.butBlock.Text = "Select blocks ...";
            this.butBlock.UseVisualStyleBackColor = true;
            this.butBlock.Click += new System.EventHandler(this.butBlock_Click);
            // 
            // cboUcs
            // 
            this.cboUcs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUcs.Enabled = false;
            this.cboUcs.FormattingEnabled = true;
            this.cboUcs.Location = new System.Drawing.Point(241, 82);
            this.cboUcs.Name = "cboUcs";
            this.cboUcs.Size = new System.Drawing.Size(144, 21);
            this.cboUcs.TabIndex = 21;
            // 
            // rdoUcsName
            // 
            this.rdoUcsName.AutoSize = true;
            this.rdoUcsName.Location = new System.Drawing.Point(221, 85);
            this.rdoUcsName.Name = "rdoUcsName";
            this.rdoUcsName.Size = new System.Drawing.Size(14, 13);
            this.rdoUcsName.TabIndex = 22;
            this.rdoUcsName.UseVisualStyleBackColor = true;
            this.rdoUcsName.CheckedChanged += new System.EventHandler(this.rdoUcsName_CheckedChanged);
            // 
            // chkZcoord
            // 
            this.chkZcoord.AutoSize = true;
            this.chkZcoord.Enabled = false;
            this.chkZcoord.Location = new System.Drawing.Point(12, 113);
            this.chkZcoord.Name = "chkZcoord";
            this.chkZcoord.Size = new System.Drawing.Size(124, 17);
            this.chkZcoord.TabIndex = 23;
            this.chkZcoord.Text = "Include Z coordinate";
            this.chkZcoord.UseVisualStyleBackColor = true;
            // 
            // cboOption
            // 
            this.cboOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOption.FormattingEnabled = true;
            this.cboOption.Items.AddRange(new object[] {
            "Clipboard"});
            this.cboOption.Location = new System.Drawing.Point(241, 111);
            this.cboOption.Name = "cboOption";
            this.cboOption.Size = new System.Drawing.Size(144, 21);
            this.cboOption.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Export to:";
            // 
            // CoordinatePickerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 177);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboOption);
            this.Controls.Add(this.chkZcoord);
            this.Controls.Add(this.rdoUcsName);
            this.Controls.Add(this.cboUcs);
            this.Controls.Add(this.butBlock);
            this.Controls.Add(this.rdoWorld);
            this.Controls.Add(this.rdoCurrent);
            this.Controls.Add(this.lblLine2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.butPick);
            this.Controls.Add(this.butSelect);
            this.Controls.Add(this.lblLine1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butExport);
            this.Controls.Add(this.butClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoordinatePickerDialog";
            this.Text = "Coordinate Picker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Button butExport;
        private System.Windows.Forms.RadioButton rdoWorld;
        private System.Windows.Forms.RadioButton rdoCurrent;
        private System.Windows.Forms.Label lblLine2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button butPick;
        private System.Windows.Forms.Button butSelect;
        private System.Windows.Forms.Label lblLine1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butBlock;
        private System.Windows.Forms.ComboBox cboUcs;
        private System.Windows.Forms.RadioButton rdoUcsName;
        private System.Windows.Forms.CheckBox chkZcoord;
        private System.Windows.Forms.ComboBox cboOption;
        private System.Windows.Forms.Label label2;
    }
}