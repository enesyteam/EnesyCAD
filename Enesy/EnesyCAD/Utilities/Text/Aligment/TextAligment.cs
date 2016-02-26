using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Enesy.EnesyCAD.Utilities.Text
{
    public partial class TextAligmentDialog : Enesy.EnesyCAD.Forms.Form
    {
        public TextAligmentDialog()
        {
            InitializeComponent();
            this.Help = CommandsHelp.TextAligment;
        }

        private void butLeft_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commands.TextAligmentAction(Aligment.Left);
            this.Close();
        }

        private void butCenter_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commands.TextAligmentAction(Aligment.Center);
            this.Close();
        }

        private void butRight_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commands.TextAligmentAction(Aligment.Right);
            this.Close();
        }

        private void butBottom_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commands.TextAligmentAction(Aligment.Bottom);
            this.Close();
        }

        private void butMiddle_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commands.TextAligmentAction(Aligment.Middle);
            this.Close();
        }

        private void butTop_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commands.TextAligmentAction(Aligment.Top);
            this.Close();
        }
    }
}