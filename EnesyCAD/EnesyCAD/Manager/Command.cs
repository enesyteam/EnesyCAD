using Autodesk.AutoCAD.Runtime;

namespace EnesyCAD.Manager
{
    public class Command
    {
        private CommandsManagerDialog cmdMngDia = null;

        [EnesyCAD.Runtime.EnesyCADCommandMethod("CE",
            "Manager",
            "Management all Enesy commands",
            "EnesyCAD",
            "quandt@enesy.vn",
            Enesy.WebPageLink.EnesyCadYoutube
            )]
        public void ListUserCommandDialog()
        {
            if (cmdMngDia == null)
            {
                this.cmdMngDia = new CommandsManagerDialog();
            }
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(cmdMngDia);
            cmdMngDia.ShowModal();
        }
    }
}
