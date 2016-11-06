using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Enesy.EnesyCAD.ApplicationServices;

namespace Enesy.EnesyCAD.CommandManager
{
    public class Command
    {
        [EnesyCAD.Runtime.EnesyCADCommandMethod("CEE",
            "Manager",
            "Management all Enesy commands",
            CommandsHelp.EnesyAuthor,
            "quandt@enesy.vn",
            EnesyCAD.CommandsHelp.CommandsManager
            )]
        public void ListUserCommandDialog()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(
                EneApplication.CmdManager);
        }
    }
}
