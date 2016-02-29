using Autodesk.AutoCAD.Runtime;

using System.Runtime.InteropServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Enesy.EnesyCAD.Manager
{
    public class Command
    {
        private CommandsManagerDialog cmdMngDia = null;

        [EnesyCAD.Runtime.EnesyCADCommandMethod("CE",
            "Manager",
            "Management all Enesy commands",
            "enesy.vn",
            "quandt@enesy.vn",
            Enesy.Page.CadYoutube
            )]
        public void ListUserCommandDialog()
        {
            if (cmdMngDia == null)
            {
                this.cmdMngDia = new CommandsManagerDialog();
            }
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(cmdMngDia);
        }
    }
}
