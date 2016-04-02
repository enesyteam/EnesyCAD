using System;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Enesy.EnesyCAD.Helper
{
    public class EditorHelper
    {
        public static Point3d GetSinglePoint(string message)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            PromptPointOptions po = new PromptPointOptions(message);
            return ed.GetPoint(po).Value;
        
        }
    }
}
