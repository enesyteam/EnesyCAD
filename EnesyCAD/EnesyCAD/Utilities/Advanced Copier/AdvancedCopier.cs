using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EnesyCAD.Utilities.Advanced_Copier
{
    public partial class AdvancedCopierDialog : Form
    {
        public AdvancedCopierDialog()
        {
            InitializeComponent();
        }

        private void lnkWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://enesy.vn");
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}