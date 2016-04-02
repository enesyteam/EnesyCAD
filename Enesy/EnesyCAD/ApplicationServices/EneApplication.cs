using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using System;
using EDS = Enesy.EnesyCAD.DatabaseServices;


using Autodesk.AutoCAD.EditorInput;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Enesy.EnesyCAD.ApplicationServices
{
    public class EneApplication : IExtensionApplication
    {
        internal static ECRegistry EneCadRegistry = new ECRegistry();

        internal static EDS.Database EneDatabase = new EDS.Database();

        internal static CommandManager.CommandsManagerDialog CmdManager =
            new CommandManager.CommandsManagerDialog();
        
        public void Initialize()
        {
            //throw new NotImplementedException();
            DocumentCollection dwgCol = AcadApp.DocumentManager;
            Document dwg = AcadApp.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            try
            {
                ed.WriteMessage("\nĐang tạo {0}...", this.GetType().Name);

                ed.WriteMessage("\nHoàn tất\n");
                //dwg.SendStringToExecute("CE ", true, false, true);
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nKhông thành công:\n{0}", ex.ToString());
            }
        }

        public void Terminate()
        {
            //throw new NotImplementedException();
        }
    }
}
