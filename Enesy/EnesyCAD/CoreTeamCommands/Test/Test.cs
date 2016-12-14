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
            MyPaletteHeader = "Test";
            MyControl.Controls.Add(new TextBox() { Text = "Some Text"});

            base.Active();
            //CMNApplication.ESWCmn.ESW.Activate(CMNApplication.ESWCmn.ESW.Count - 1);
        }
    }
    public class AnotherTestCommands : HavePaletteCommandBase
    {
        public AnotherTestCommands()
            : base()
        {
        }

        [CommandMethod("TTT")]
        public void Test1()
        {
            MyControl = new UserControl();
            MyPaletteHeader = "Another Test";
            //MyControl.Controls.Add(new TextBox() { Text = "Another Text" });

            UserControl1 uc = new UserControl1();
            MyControl = uc;

            base.Active();
            //CMNApplication.ESWCmn.ESW.Activate(CMNApplication.ESWCmn.ESW.Count - 1);
        }
    }
}
