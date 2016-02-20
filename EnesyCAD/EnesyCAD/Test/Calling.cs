using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace EnesyCAD.Test
{
    public class Calling
    {
        [CommandMethod("Tes")]
        public void Doit()
        {
            Form1 f = new Form1();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(f);
        }
    }
}
