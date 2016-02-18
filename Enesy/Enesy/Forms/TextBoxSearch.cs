using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class TextBoxSearch : Enesy.Forms.TextBoxFilter
    {
        public TextBoxSearch()
        {
            InitializeComponent();
            this.DataSourceChanged += 
                new OnDataSourceChangedHandler(TextBoxSearch_DataSourceChanged);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        private Color inactiveColor = Color.DarkGray;
        public Color InactiveColor
        {
            set { this.inactiveColor = value; }
        }

        private Color activeColor = Color.Black;
        public Color ActiveColor
        {
            set { this.activeColor = value; }
        }


        void TextBoxSearch_DataSourceChanged(object sender, FilterEventArgs e)
        {
            this.GotFocus -= new EventHandler(TextBoxSearch_GotFocus);
            this.GotFocus += new EventHandler(TextBoxSearch_GotFocus);

            this.LostFocus -= new EventHandler(TextBoxSearch_LostFocus);
            this.LostFocus += new EventHandler(TextBoxSearch_LostFocus);
        }

        /// <summary>
        /// If text is "Search .....", set it is ""
        /// of select all.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TextBoxSearch_GotFocus(object sender, EventArgs e)
        {
            string s = "Search " + this.DisplayMember + "...";
            if (this.Text == s)
            {
                FormatRegular();
                this.Text = "";
            }
        }

        /// <summary>
        /// This event is activated after this control is lost focus
        /// If text is nothing (state is false), set text is "Search + column"...
        /// If state is true (textBox is activating), do nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TextBoxSearch_LostFocus(object sender, EventArgs e)
        {

        }

        private void FormatItalic()
        {
            this.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Italic);
            this.ForeColor = inactiveColor;
        }

        void FormatRegular()
        {
            this.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Regular);
            this.ForeColor = activeColor;
        }
    }
}
