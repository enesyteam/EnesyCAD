namespace EnesyCAD.Utilities.Advanced_Copier
{
    partial class AdvancedCopierDialog
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
            this.lnkWeb = new System.Windows.Forms.LinkLabel();
            this.butCopy = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTargetType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.butType = new System.Windows.Forms.Button();
            this.cboTargetPoint = new System.Windows.Forms.ComboBox();
            this.butObjects = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOriginType = new System.Windows.Forms.Label();
            this.cboOriginPoint = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butOrigin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lnkWeb);
            this.panel1.Controls.Add(this.butCopy);
            this.panel1.Controls.Add(this.butCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 73);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 6, 0);
            this.panel1.Size = new System.Drawing.Size(534, 24);
            this.panel1.TabIndex = 1;
            // 
            // lnkWeb
            // 
            this.lnkWeb.AutoSize = true;
            this.lnkWeb.Location = new System.Drawing.Point(8, 5);
            this.lnkWeb.Name = "lnkWeb";
            this.lnkWeb.Size = new System.Drawing.Size(159, 14);
            this.lnkWeb.TabIndex = 2;
            this.lnkWeb.TabStop = true;
            this.lnkWeb.Text = "Copyright (c) 2016 by enesy.vn";
            this.lnkWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWeb_LinkClicked);
            // 
            // butCopy
            // 
            this.butCopy.Dock = System.Windows.Forms.DockStyle.Right;
            this.butCopy.Location = new System.Drawing.Point(378, 0);
            this.butCopy.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.butCopy.Name = "butCopy";
            this.butCopy.Size = new System.Drawing.Size(75, 24);
            this.butCopy.TabIndex = 1;
            this.butCopy.Text = "Copy";
            this.butCopy.UseVisualStyleBackColor = true;
            // 
            // butCancel
            // 
            this.butCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.butCancel.Location = new System.Drawing.Point(453, 0);
            this.butCancel.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 24);
            this.butCancel.TabIndex = 0;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(534, 73);
            this.panel2.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTargetType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.butType);
            this.groupBox2.Controls.Add(this.butObjects);
            this.groupBox2.Controls.Add(this.cboTargetPoint);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(223, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 73);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target";
            // 
            // lblTargetType
            // 
            this.lblTargetType.AutoSize = true;
            this.lblTargetType.Location = new System.Drawing.Point(48, 51);
            this.lblTargetType.Name = "lblTargetType";
            this.lblTargetType.Size = new System.Drawing.Size(32, 14);
            this.lblTargetType.TabIndex = 8;
            this.lblTargetType.Text = "None";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Type:";
            // 
            // butType
            // 
            this.butType.Location = new System.Drawing.Point(6, 19);
            this.butType.Name = "butType";
            this.butType.Size = new System.Drawing.Size(51, 23);
            this.butType.TabIndex = 6;
            this.butType.Text = "Type...";
            this.butType.UseVisualStyleBackColor = true;
            // 
            // cboTargetPoint
            // 
            this.cboTargetPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetPoint.FormattingEnabled = true;
            this.cboTargetPoint.Location = new System.Drawing.Point(63, 20);
            this.cboTargetPoint.Name = "cboTargetPoint";
            this.cboTargetPoint.Size = new System.Drawing.Size(135, 22);
            this.cboTargetPoint.TabIndex = 5;
            // 
            // butObjects
            // 
            this.butObjects.Location = new System.Drawing.Point(204, 19);
            this.butObjects.Name = "butObjects";
            this.butObjects.Size = new System.Drawing.Size(100, 23);
            this.butObjects.TabIndex = 4;
            this.butObjects.Text = "Select objects...";
            this.butObjects.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butOrigin);
            this.groupBox1.Controls.Add(this.lblOriginType);
            this.groupBox1.Controls.Add(this.cboOriginPoint);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Origin";
            // 
            // lblOriginType
            // 
            this.lblOriginType.AutoSize = true;
            this.lblOriginType.Location = new System.Drawing.Point(47, 51);
            this.lblOriginType.Name = "lblOriginType";
            this.lblOriginType.Size = new System.Drawing.Size(32, 14);
            this.lblOriginType.TabIndex = 4;
            this.lblOriginType.Text = "None";
            // 
            // cboOriginPoint
            // 
            this.cboOriginPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOriginPoint.FormattingEnabled = true;
            this.cboOriginPoint.Location = new System.Drawing.Point(73, 20);
            this.cboOriginPoint.Name = "cboOriginPoint";
            this.cboOriginPoint.Size = new System.Drawing.Size(135, 22);
            this.cboOriginPoint.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type:";
            // 
            // butOrigin
            // 
            this.butOrigin.Location = new System.Drawing.Point(6, 19);
            this.butOrigin.Name = "butOrigin";
            this.butOrigin.Size = new System.Drawing.Size(61, 23);
            this.butOrigin.TabIndex = 7;
            this.butOrigin.Text = "Object...";
            this.butOrigin.UseVisualStyleBackColor = true;
            // 
            // AdvancedCopierDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 97);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AdvancedCopierDialog";
            this.Text = "Advanced Copier";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboTargetPoint;
        private System.Windows.Forms.Button butObjects;
        private System.Windows.Forms.ComboBox cboOriginPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butType;
        private System.Windows.Forms.Label lblTargetType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblOriginType;
        private System.Windows.Forms.Button butCopy;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.LinkLabel lnkWeb;
        private System.Windows.Forms.Button butOrigin;
    }
}