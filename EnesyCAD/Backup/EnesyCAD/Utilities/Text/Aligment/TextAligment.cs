using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace EnesyCAD.Utilities.Text
{
    public partial class TextAligmentDialog : Form
    {
        public TextAligmentDialog()
        {
            InitializeComponent();
        }

        private void butLeft_Click(object sender, EventArgs e)
        {
            this.Close();
            Command.TextAligmentAction(Aligment.Left);
        }

        private void butCenter_Click(object sender, EventArgs e)
        {
            this.Close();
            Command.TextAligmentAction(Aligment.Center);
        }

        private void butRight_Click(object sender, EventArgs e)
        {
            this.Close();
            Command.TextAligmentAction(Aligment.Right);
        }

        private void butBottom_Click(object sender, EventArgs e)
        {
            this.Close();
            Command.TextAligmentAction(Aligment.Bottom);
        }

        private void butMiddle_Click(object sender, EventArgs e)
        {
            this.Close();
            Command.TextAligmentAction(Aligment.Middle);
        }

        private void butTop_Click(object sender, EventArgs e)
        {
            this.Close();
            Command.TextAligmentAction(Aligment.Top);
        }
    }
}