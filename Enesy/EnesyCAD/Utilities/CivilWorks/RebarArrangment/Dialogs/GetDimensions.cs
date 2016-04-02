using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.Utilities.CivilWorks.RebarArrangment.Dialogs
{
    public partial class GetDimensions : Enesy.EnesyCAD.Forms.Form
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public GetDimensions()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWidth.Text))
                Width = Convert.ToDouble(txtWidth.Text);
            if (!string.IsNullOrEmpty(txtHeight.Text))
                Height = Convert.ToDouble(txtHeight.Text);

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
