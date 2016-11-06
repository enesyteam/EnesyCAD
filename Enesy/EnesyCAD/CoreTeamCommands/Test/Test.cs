using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.CommandManager.Ver2;
using System;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CoreTeamCommands.Test
{
    public class TestCommands : HavePaletteCommandBase
    {
        public TestCommands() : base()
        {
        }

        [CommandMethod("YYY")]
        public void Test1()
        {
            MyControl = new UserControl();
            MyPaletteHeader = "Test1";
            MyControl.Controls.Add(new Button() { Text = "My button"});

            base.DoCommand();
            CMNApplication.ESWCmn.ESW.Activate(CMNApplication.ESWCmn.ESW.Count - 1);
        }
    }
}
