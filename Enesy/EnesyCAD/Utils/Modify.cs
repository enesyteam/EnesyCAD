using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Enesy.EnesyCAD
{
    partial class Utils
    {
        /// <summary>
        /// Aligment text is specified by objectID in ObjectIdCollection
        /// </summary>
        /// <param name="db"></param>
        /// <param name="txtIdColl"></param>
        /// <param name="aligment"></param>
        /// <param name="bsPoint"></param>
        public static void AligmentText(Database db,
                                        ObjectIdCollection txtIdColl,
                                        Enesy.EnesyCAD.Utilities.Text.Aligment aligment,
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
    }
}
