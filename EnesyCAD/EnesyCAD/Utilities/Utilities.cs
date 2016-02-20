using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace EnesyCAD.Utilities
{
    class Utilities
    {
        /// <summary>
        /// Calculate Polar Point
        /// </summary>
        /// <param name="pPt">Basepoint</param>
        /// <param name="dDist">Distance from basePoint to target</param>
        /// <param name="dAng">Angle from X axis</param>
        /// <returns></returns>
        public static Point3d Polar(Point3d pPt, double dDist, double dAng)
        {
            return new Point3d(pPt.X + dDist * Math.Cos(dAng),
                     pPt.Y + dDist * Math.Sin(dAng),
                     pPt.Z);
        }

        /// <summary>
        /// Get 2 point which is 2 corner of a rectangle
        /// </summary>
        /// <param name="ed">Editor of active CAD document</param>
        /// <returns>2D Array of point 3D</returns>
        public static Point3d[] GetCorners()
        {
            Document ac = Application.DocumentManager.MdiActiveDocument;
            Editor ed = ac.Editor;

            Point3d[] result = new Point3d[2];
            PromptPointResult prPntRes1;
            PromptPointOptions prPntOpts1 = new PromptPointOptions(
                "\nSpecify the first corner: \n"
                );

            // Set attributes for selection function
            prPntOpts1.AllowArbitraryInput = true;
            prPntOpts1.AllowNone = false;
            prPntOpts1.LimitsChecked = false;
            prPntRes1 = ed.GetPoint(prPntOpts1);

            if (prPntRes1.Status != PromptStatus.Cancel)
            {
                PromptPointResult prPntRes2;
                PromptCornerOptions prCorOpts2 = new PromptCornerOptions(
                    "\nSpecify the opposite corner: \n",
                    prPntRes1.Value
                    );
                prPntRes2 = ed.GetCorner(prCorOpts2);

                if (prPntRes2.Status != PromptStatus.Cancel)
                {
                    result[0] = prPntRes1.Value;
                    result[1] = prPntRes2.Value;
                }
            }
            return result;
        }

        /// <summary>
        /// Get all layouts of active document
        /// </summary>
        /// <param name="db">Database of active document</param>
        /// <returns></returns>
        public static List<string> GetLayoutNames(Database db)
        {
            List<string> layoutNames = new List<string>();

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                DBDictionary lytDct =
                    (DBDictionary)tr.GetObject(db.LayoutDictionaryId, OpenMode.ForRead);

                foreach (DBDictionaryEntry lytDe in lytDct)
                {
                    Layout lyt = (Layout)tr.GetObject(lytDe.Value, OpenMode.ForRead);

                    layoutNames.Add(lyt.LayoutName);
                }
                tr.Commit();
            }
            return layoutNames;
        }

        /// <summary>
        /// Visit to enesy.vn
        /// </summary>
        public static void VisitWeb()
        {
            System.Diagnostics.Process.Start("http://enesy.vn/");
        }

        /// <summary>
        /// Get angle of pnt1-pnt2 line to horizontal axis
        /// </summary>
        /// <param name="pnt1"></param>
        /// <param name="pnt2"></param>
        /// <returns></returns>
        public static double AngleFromXAxisInXYPlane(
                                Point3d firstPoint, Point3d secondPoint)
        {
            Vector3d normal = new Vector3d(0, 0, 1);
            Plane plane = new Plane(new Point3d(0, 0, 0), normal);
            Point2d p1, p2;
            p1 = firstPoint.Convert2d(plane);
            p2 = secondPoint.Convert2d(plane);
            return p1.GetVectorTo(p2).Angle;
        }

        /// <summary>
        /// Get objects of specified type
        /// </summary>
        /// <param name="oType">List of type</param>
        /// <param name="db">Current database</param>
        /// <param name="ed">Current editor</param>
        /// <returns>Collection of objectID</returns>
        public static ObjectIdCollection SelectObjectsOfType(List<Type> oType, 
                Database db,
                Editor ed
            )
        {
            ObjectIdCollection objIdColl = new ObjectIdCollection();
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                PromptSelectionResult ssPrompt = ed.GetSelection();
                if (ssPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet sSet = ssPrompt.Value;
                    foreach (SelectedObject obj in sSet)
                    {
                        Entity curEnt = tr.GetObject(obj.ObjectId, OpenMode.ForRead) 
                                                                                as Entity;
                        if (curEnt != null)
                        {
                            if (oType.Contains(curEnt.GetType()))
                            {
                                objIdColl.Add(obj.ObjectId);
                            }
                        }
                    }
                }
            }
            if (objIdColl.Count == 0) return null;
            return objIdColl;
        }

        /// <summary>
        /// Select text (include MText & DBText)
        /// </summary>
        /// <param name="db">Current database</param>
        /// <param name="ed">Current edito</param>
        /// <returns>Collection of ObjectID</returns>
        public static ObjectIdCollection SelectText(Database db, Editor ed)
        {
            List<Type> oType = new List<Type>();
            oType.Add(typeof(MText));
            oType.Add(typeof(DBText));
            return SelectObjectsOfType(oType, db, ed);
        }

        /// <summary>
        /// Aligment text is specified by objectID in ObjectIdCollection
        /// </summary>
        /// <param name="db"></param>
        /// <param name="txtIdColl"></param>
        /// <param name="aligment"></param>
        /// <param name="bsPoint"></param>
        public static void AligmentText(Database db,
                                        ObjectIdCollection txtIdColl,
                                        Text.Aligment aligment,
                                        Point3d bsPoint
            )
        {
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                foreach (ObjectId objID in txtIdColl)
                {
                    Entity ent = tr.GetObject(objID, OpenMode.ForRead) as Entity;

                    // Get boundingBox of text
                    Point3dCollection boundPoints = new Point3dCollection();
                    if (ent.GetType() == typeof(MText))
                    {
                        MText txt = ent as MText;
                        boundPoints.Add(txt.GetBoundingPoints()[2]);
                        boundPoints.Add(txt.GetBoundingPoints()[1]);
                    }
                    else
                    {
                        DBText txt = ent as DBText;
                        Point3dCollection points = new Point3dCollection();
                        txt.IntersectWith(txt, Intersect.OnBothOperands, points, 0, 0);
                        Extents3d ext = txt.GeometricExtents;
                        boundPoints.Add(points[0]);
                        boundPoints.Add(ext.MaxPoint);
                    }

                    // Performing aligment
                    ObjectIdCollection ids = new ObjectIdCollection();
                    ids.Add(objID);
                    Point3d p = new Point3d();
                    switch (aligment)
                    {
                        case EnesyCAD.Utilities.Text.Aligment.Left:
                            p = boundPoints[0];
                            MoveObjects(ids, p, new Point3d(bsPoint.X, p.Y, bsPoint.Z));
                            break;
                        case EnesyCAD.Utilities.Text.Aligment.Center:
                            p = MiddlePoint(boundPoints[0], boundPoints[1]);
                            MoveObjects(ids, p, new Point3d(bsPoint.X, p.Y, bsPoint.Z));
                            break;
                        case EnesyCAD.Utilities.Text.Aligment.Right:
                            p = boundPoints[1];
                            MoveObjects(ids, p, new Point3d(bsPoint.X, p.Y, bsPoint.Z));
                            break;
                        case EnesyCAD.Utilities.Text.Aligment.Top:
                            p = boundPoints[1];
                            MoveObjects(ids, p, new Point3d(p.X, bsPoint.Y, bsPoint.Z));
                            break;
                        case EnesyCAD.Utilities.Text.Aligment.Middle:
                            p = MiddlePoint(boundPoints[0], boundPoints[1]);
                            MoveObjects(ids, p, new Point3d(p.X, bsPoint.Y, bsPoint.Z));
                            break;
                        case EnesyCAD.Utilities.Text.Aligment.Bottom:
                            p = boundPoints[0];
                            MoveObjects(ids, p, new Point3d(p.X, bsPoint.Y, bsPoint.Z));
                            break;
                    }
                }
                tr.Commit();
            }
        }

        /// <summary>
        /// Move objects in active document
        /// </summary>
        /// <param name="objIdColl">ObjectIdCollection of objects</param>
        /// <param name="basePoint">Point3D</param>
        /// <param name="targetPoint">Point3D</param>
        public static void MoveObjects(ObjectIdCollection objIdColl,
                                Point3d basePoint,
                                Point3d targetPoint
            )
        {
            Document doc = acApp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            Vector3d acVec3d = basePoint.GetVectorTo(targetPoint);

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                doc.LockDocument();

                // Convert ObjectIdCollection to Entities then move
                foreach (ObjectId id in objIdColl)
                {
                    Entity ent = tr.GetObject(id, OpenMode.ForWrite) as Entity;
                    ent.TransformBy(Matrix3d.Displacement(acVec3d));
                }
                tr.Commit();
            }
        }

        /// <summary>
        /// Get middle point between 2 points
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        /// <returns></returns>
        public static Point3d MiddlePoint(Point3d firstPoint, Point3d secondPoint)
        {
            LineSegment3d lineSeg3d = new LineSegment3d(firstPoint, secondPoint);
            return lineSeg3d.MidPoint;
        }
    }
}
