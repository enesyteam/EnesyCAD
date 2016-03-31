using Autodesk.AutoCAD.Runtime;

using System.Runtime.InteropServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

using Enesy.EnesyCAD.DEMO.CE;

namespace Enesy.EnesyCAD.CE
{
    public class Command
    {
        private CEForm cmdMngDia = null;

        [EnesyCAD.Runtime.EnesyCADCommandMethod("CEF",
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
                this.cmdMngDia = new CEForm();
            }
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(cmdMngDia);
        }
    }
}
