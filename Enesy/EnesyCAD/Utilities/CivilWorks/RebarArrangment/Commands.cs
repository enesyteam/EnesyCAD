using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.ComponentModel;
using Autodesk.AutoCAD.EditorInput;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Enesy.EnesyCAD.Helper;

namespace Enesy.EnesyCAD.Utilities.CivilWorks.RebarArrangment
{
    public class Commands
    {
        [EnesyCAD.Runtime.EnesyCADCommandMethod("RAR",
        "Civil",
        "Rebar Arrangment for Basic Shapes",
        "EnesyCAD",
        "cad@enesy.vn",
        "",
        CommandFlags.UsePickSet
        )]
        public void Excute()
        { 
            // Get dimensions
            Dialogs.GetDimensions getdim = new Dialogs.GetDimensions();
            if (getdim.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Point3d point = EditorHelper.GetSinglePoint("\nPick a point to Draw");

                RectRebarSection rect = new RectRebarSection(getdim.Width, getdim.Height);
                rect.BuildSection(point);
                rect.Add(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument);
            
            }
        
        }
    }
}
