using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.DatabaseServices;

namespace Enesy.EnesyCAD.CoreTeamCommands.XLine
{
    public class CreateAngleXLine
    {
        [CommandMethod("XXA")]
        public void CreateAngleXLineCommand()
        {
            PromptPointOptions ppo = new PromptPointOptions("\nPick Point");
            PromptPointResult ppr = GLOBAL.CurrentEditor.GetPoint(ppo);
            if (ppr.Status != PromptStatus.OK) return;

            PromptDoubleOptions pdo = new PromptDoubleOptions("\nEnter Grade (%)");
            PromptDoubleResult pdr = GLOBAL.CurrentEditor.GetDouble(pdo);
            if (pdr.Status != PromptStatus.OK) return;
            // calculate second point
            Point3d secondPoint = Helper.Point3dHelper.Offset(ppr.Value, 100, pdr.Value, 0);
            Xline xline = new Xline() { BasePoint = ppr.Value, UnitDir = new Vector3d(secondPoint.X - ppr.Value.X, secondPoint.Y - ppr.Value.Y, secondPoint.Z - ppr.Value.Z) };
            CompositeFigure cg = new CompositeFigure();
            cg.Children.Add(xline);
            cg.Append(GLOBAL.CurrentDocument);

        }
    }
}
