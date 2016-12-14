using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using System;
using Autodesk.AutoCAD.DatabaseServices;

namespace Enesy.EnesyCAD.CoreTeamCommands.Distance
{
    public class DistanceOnCurveCommands
    {
        public Point3d BasePoint { get; set; }
        public Point3d FindPoint { get; set; }
        private static double mScale = 1;
        public static double Scale
        {
            get { return mScale; }
            set {
                if (value > 0)
                {
                    Scale = value;
                }
            }
        }
        [EnesyCAD.Runtime.EnesyCADCommandMethod("DISC",
        "Distance",
        "DIS.Desciption",
        CommandsHelp.EnesyAuthor,
        "congnvc@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        [EnesyCAD.Runtime.CommandGroup("DIS")]
        public static void GetCurveLength()
        {
            bool continuePick = true;

            while (continuePick)
            {
                PromptEntityOptions peo = new PromptEntityOptions("\nSelect Entity" + "<Scale " + Scale + ">");
                peo.Keywords.Add("Scale");
                peo.SetRejectMessage("\nObject type not supported");
                peo.AddAllowedClass(typeof(Curve), false);

                PromptEntityResult per = GLOBAL.CurrentEditor.GetEntity(peo);
                if (per.Status == PromptStatus.Keyword)
                {
                    switch (per.StringResult)
                    {
                        case "Scale":
                            PromptDoubleOptions pdo = new PromptDoubleOptions("\nEnter Scale " + "<" + Scale + ">");
                            PromptDoubleResult pdr = GLOBAL.CurrentEditor.GetDouble(pdo);
                            if (pdr.Status == PromptStatus.OK)
                            {
                                mScale = pdr.Value;
                            }
                            break;
                    }
                }
                else if (per.Status == PromptStatus.OK)
                {
                    ObjectId cId = per.ObjectId;

                    using (Transaction tr = GLOBAL.CurrentDocument.TransactionManager.StartTransaction())
                    {
                        Curve curve = tr.GetObject(cId, OpenMode.ForRead) as Curve;
                        if (curve != null)
                        {
                            PromptPointOptions ppo = new PromptPointOptions("\nPick 1st Point");
                            PromptPointOptions ppo2 = new PromptPointOptions("\nPick 2nd Point");
                            PromptPointResult ppr = GLOBAL.CurrentEditor.GetPoint(ppo);
                            PromptPointResult ppr2 = GLOBAL.CurrentEditor.GetPoint(ppo2);
                            if (ppr.Status == PromptStatus.OK && ppr2.Status == PromptStatus.OK)
                            {
                                // Transform from UCS to WCS
                                Matrix3d ucs = GLOBAL.CurrentEditor.CurrentUserCoordinateSystem;
                                CoordinateSystem3d cs = ucs.CoordinateSystem3d;
                                Matrix3d mat =
                                    Matrix3d.AlignCoordinateSystem(
                                    Point3d.Origin,
                                    Vector3d.XAxis,
                                    Vector3d.YAxis,
                                    Vector3d.ZAxis,
                                    cs.Origin,
                                    cs.Xaxis,
                                    cs.Yaxis,
                                    cs.Zaxis
                                    );

                                double dis = Math.Abs(curve.GetDistAtPoint(curve.GetClosestPointTo(ppr.Value.TransformBy(mat), false))
                                            - curve.GetDistAtPoint(curve.GetClosestPointTo(ppr2.Value.TransformBy(mat), false)));
                                GLOBAL.WriteMessage(dis.ToString());

                                PromptEntityOptions peo2 = new PromptEntityOptions("\nSelect Text");
                                peo2.SetRejectMessage("\nObject type not supported");
                                peo2.AddAllowedClass(typeof(DBText), false);

                                PromptEntityResult per2 = GLOBAL.CurrentEditor.GetEntity(peo2);
                                if (per2.Status != PromptStatus.OK) return;
                                ObjectId tId = per2.ObjectId;

                                using (Transaction tr2 = GLOBAL.CurrentDocument.TransactionManager.StartTransaction())
                                {
                                    DBText text = tr2.GetObject(tId, OpenMode.ForWrite) as DBText;
                                    if (text != null)
                                    {
                                        text.TextString = (Math.Round(dis * Scale, 0)).ToString();
                                    }
                                    tr2.Commit();
                                }
                            }
                        }
                        tr.Commit();
                    }
                }
                else
                {
                    continuePick = false;
                }
            }

        }
    }
}
