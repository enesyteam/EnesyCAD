using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.ComponentModel;
using Autodesk.AutoCAD.EditorInput;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Enesy.EnesyCAD.Helper;
using Autodesk.AutoCAD.CustomizationEx;

namespace Enesy.EnesyCAD.Test
{
    public class TestCommands
    {
        [CommandMethod("TEST1")]
        public void Excute()
        {
            MainForm m = new MainForm();
            m.ShowDialog();

        }
    }
}
