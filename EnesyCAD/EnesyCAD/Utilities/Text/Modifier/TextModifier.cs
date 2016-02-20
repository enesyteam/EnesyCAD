using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace EnesyCAD.Utilities.Text.Modifier
{
    public partial class TextModifierDialog : EnesyCAD.Forms.Form
    {
        public TextModifierDialog()
        {
            InitializeComponent();
        }
    }
}