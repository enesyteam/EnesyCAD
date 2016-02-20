namespace EnesyCAD.Utilities.Text
{
    partial class TextParagraphDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.txtSpace = new Enesy.Forms.TextBoxNumericLargerZero();
            this.butHelp = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOK = new System.Windows.Forms.Button();
            this.butSelectText = new System.Windows.Forms.Button();
            this.butApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line spacing:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(155, 7);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(123, 18);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "% bottom text height";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(155, 31);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(61, 18);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "% units";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // txtSpace
            // 
            this.txtSpace.Location = new System.Drawing.Point(89, 6);
            this.txtSpace.Name = "txtSpace";
            this.txtSpace.Size = new System.Drawing.Size(60, 20);
            this.txtSpace.TabIndex = 1;
            // 
            // butHelp
            // 
            this.butHelp.Location = new System.Drawing.Point(224, 63);
            this.butHelp.Name = "butHelp";
            this.butHelp.Size = new System.Drawing.Size(54, 23);
            this.butHelp.TabIndex = 4;
            this.butHelp.Text = "Help";
            this.butHelp.UseVisualStyleBackColor = true;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(155, 63);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(63, 23);
            this.butCancel.TabIndex = 5;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(86, 63);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(63, 23);
            this.butOK.TabIndex = 6;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            // 
            // butSelectText
            // 
            this.butSelectText.Location = new System.Drawing.Point(15, 29);
            this.butSelectText.Name = "butSelectText";
            this.butSelectText.Size = new System.Drawing.Size(134, 23);
            this.butSelectText.TabIndex = 7;
            this.butSelectText.Text = "Select text ...";
            this.butSelectText.UseVisualStyleBackColor = true;
            this.butSelectText.Click += new System.EventHandler(this.butSelectText_Click);
            // 
            // butApply
            // 
            this.butApply.Location = new System.Drawing.Point(15, 63);
            this.butApply.Name = "butApply";
            this.butApply.Size = new System.Drawing.Size(65, 23);
            this.butApply.TabIndex = 8;
            this.butApply.Text = "Apply";
            this.butApply.UseVisualStyleBackColor = true;
            // 
            // TextParagraphDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 93);
            this.Controls.Add(this.butApply);
            this.Controls.Add(this.butSelectText);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butHelp);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.txtSpace);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextParagraphDialog";
            this.ShowInTaskbar = false;
            this.Text = "Text Spacing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private Enesy.Forms.TextBoxNumericLargerZero txtSpace;
        private System.Windows.Forms.Button butHelp;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butSelectText;
        private System.Windows.Forms.Button butApply;
    }
}