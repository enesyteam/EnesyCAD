using System.Collections.Generic;
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
        /// Transform point3D coordinate from WCS to current UCS
        /// </summary>
        /// <param name="pnts"></param>
        /// <returns></returns>
        public static Point3dCollection Wcs2Ucs(Point3dCollection pnts)
        {
            // Get current ucs then inverse it
            Document doc = acApp.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Matrix3d wcs2ucs = ed.CurrentUserCoordinateSystem.Inverse();

            // Translate
            Point3dCollection ucsPnts = new Point3dCollection();
            foreach (Point3d p in pnts)
            {
                ucsPnts.Add(p.TransformBy(wcs2ucs));
            }
            return ucsPnts;
        }

        /// <summary>
        /// Transform point3D coordinate from WCS to named UCS
        /// </summary>
        /// <param name="pnts"></param>
        /// <param name="ucsName"></param>
        /// <returns></returns>
        public static Point3dCollection Wcs2Ucs(Point3dCollection pnts, string ucsName)
        {
            Document doc = acApp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Point3dCollection ucsPnts = new Point3dCollection();
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                UcsTable acUCSTbl;
                acUCSTbl = tr.GetObject(db.UcsTableId, OpenMode.ForRead) as UcsTable;
                UcsTableRecord ucs1 = tr.GetObject(acUCSTbl[ucsName], OpenMode.ForRead) 
                                                                            as UcsTableRecord;
                Matrix3d mat = Matrix3d.AlignCoordinateSystem(
                                        ucs1.Origin,
                                        ucs1.XAxis,
                                        ucs1.YAxis,
                                        ucs1.XAxis.CrossProduct(ucs1.YAxis),
                                        Point3d.Origin,
                                        Vector3d.XAxis,
                                        Vector3d.YAxis,
                                        Vector3d.XAxis.CrossProduct(ucs1.YAxis)
                                   );
                // Translate                
                foreach (Point3d p in pnts)
                {
                    ucsPnts.Add(p.TransformBy(mat));
                }
                tr.Commit();
            }
            return ucsPnts;
        }
    }
}
