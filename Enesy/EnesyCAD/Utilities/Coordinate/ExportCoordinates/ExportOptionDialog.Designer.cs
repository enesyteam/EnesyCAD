namespace Enesy.EnesyCAD.Utilities.Coordinate
{
    partial class ExportOptionDialog
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
            this.rdoClipboard = new System.Windows.Forms.RadioButton();
            this.rdoText = new System.Windows.Forms.RadioButton();
            this.rdoExcel = new System.Windows.Forms.RadioButton();
            this.butOK = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdoClipboard
            // 
            this.rdoClipboard.AutoSize = true;
            this.rdoClipboard.Checked = true;
            this.rdoClipboard.Location = new System.Drawing.Point(12, 12);
            this.rdoClipboard.Name = "rdoClipboard";
            this.rdoClipboard.Size = new System.Drawing.Size(114, 17);
            this.rdoClipboard.TabIndex = 0;
            this.rdoClipboard.TabStop = true;
            this.rdoClipboard.Text = "Export to Clipboard";
            this.rdoClipboard.UseVisualStyleBackColor = true;
            // 
            // rdoText
            // 
            this.rdoText.AutoSize = true;
            this.rdoText.Location = new System.Drawing.Point(12, 58);
            this.rdoText.Name = "rdoText";
            this.rdoText.Size = new System.Drawing.Size(107, 17);
            this.rdoText.TabIndex = 1;
            this.rdoText.TabStop = true;
            this.rdoText.Text = "Export to Text file";
            this.rdoText.UseVisualStyleBackColor = true;
            // 
            // rdoExcel
            // 
            this.rdoExcel.AutoSize = true;
            this.rdoExcel.Location = new System.Drawing.Point(12, 35);
            this.rdoExcel.Name = "rdoExcel";
            this.rdoExcel.Size = new System.Drawing.Size(96, 17);
            this.rdoExcel.TabIndex = 2;
            this.rdoExcel.TabStop = true;
            this.rdoExcel.Text = "Export to Excel";
            this.rdoExcel.UseVisualStyleBackColor = true;
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(12, 88);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 24);
            this.butOK.TabIndex = 3;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(93, 88);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 24);
            this.butCancel.TabIndex = 4;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // ExportOptionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 118);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.rdoExcel);
            this.Controls.Add(this.rdoText);
            this.Controls.Add(this.rdoClipboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportOptionDialog";
            this.Text = "Export Option";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoClipboard;
        private System.Windows.Forms.RadioButton rdoText;
        private System.Windows.Forms.RadioButton rdoExcel;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butCancel;
    }
}