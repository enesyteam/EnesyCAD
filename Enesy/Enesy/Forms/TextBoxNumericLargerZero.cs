using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class TextBoxNumericLargerZero : System.Windows.Forms.TextBox
    {
        private string lastText = "";

        public TextBoxNumericLargerZero()
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
            try
            {
                if (Utilities.IsNumericLargerZero(this.Text))
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
            catch (Exception ex)
            {
                int po = this.SelectionStart;
                this.Text = lastText;
                this.Select(po - 1, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
