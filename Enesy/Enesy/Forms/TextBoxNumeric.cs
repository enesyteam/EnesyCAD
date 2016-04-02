using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class TextBoxNumeric : System.Windows.Forms.TextBox
    {
        private string lastText = "";

        public TextBoxNumeric()
        {
            InitializeComponent();
            this.TextChanged += new EventHandler(TextBoxNumeric_TextChanged);
        }

        void TextBoxNumeric_TextChanged(object sender, EventArgs e)
        {
            if (this.Text == "")
            {
                lastText = "";
                return;
            }
            if (Utilities.IsNumeric(this.Text))
            {
                lastText = this.Text;                
            }
            else
            {
                int po = this.SelectionStart;
                this.Text = lastText;
                this.Select(po - 1, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }


    }
}
