using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace Enesy.EnesyCAD.Utilities
{
    partial class Utils
    {
        /// <summary>
        /// List name of all UCS of active document
        /// </summary>
        /// <param name="db">Database of active document</param>
        /// <returns></returns>
        public static List<string> GetUCSName(Database db)
        {
            List<string> ucsNames = null;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // Open the UCS table for read
                UcsTable acUCSTbl;
                acUCSTbl = tr.GetObject(db.UcsTableId, OpenMode.ForRead) as UcsTable;                

                // Listing
                foreach (ObjectId ucs in acUCSTbl)
                {
                    ucsNames = new List<string>();
                    UcsTableRecord acUCSTblRec = (UcsTableRecord)tr.GetObject(
                                                                    ucs, OpenMode.ForRead);
                    if (acUCSTblRec != null)
                    {
                        ucsNames.Add(acUCSTblRec.Name);
                    }
                }
                tr.Commit();
            }
            return ucsNames;
        }

        /// <summary>
        /// Listing vertices of polylines (LWPOLYLINE, 2D & 3D POLYLINE)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="polylines">Polylines objectID</param>
        /// <returns></returns>
        public static List<Point3d> ListVertices(Transaction tr, ObjectIdCollection polylines)
        {
            List<Point3d> coords = new List<Point3d>();

            using (tr)
            {
                foreach (ObjectId id in polylines)
                {
                    DBObject obj = tr.GetObject(id, OpenMode.ForRead);

                    // If a lwPolyline
                    Polyline lwp = obj as Polyline;
                    if (lwp != null)
                    {
                        int vn = lwp.NumberOfVertices;
                        for (int i = 0; i < vn; i++)
                        {
                            // Could also get the 2D point here
                            Point3d pt = lwp.GetPoint3dAt(i);
                            coords.Add(pt);
                        }
                    }
                    else
                    {
                        // If an old-style, 2D polyline
                        Polyline2d p2d = obj as Polyline2d;
                        if (p2d != null)
                        {
                            foreach (ObjectId vId in p2d)
                            {
                                Vertex2d v2d = (Vertex2d)tr.GetObject(vId, OpenMode.ForRead);
                                coords.Add(v2d.Position);
                            }
                        }
                        else
                        {
                            // If an old-style, 3D polyline
                            Polyline3d p3d = obj as Polyline3d;
                            if (p3d != null)
                            {
                                // Use foreach to get each contained vertex
                                foreach (ObjectId vId in p3d)
                                {
                                    PolylineVertex3d v3d = (PolylineVertex3d)tr.GetObject(
                                        vId,
                                        OpenMode.ForRead
                                      );
                                    coords.Add(v3d.Position);
                                }
                            }
                        }
                    }
                }
                tr.Commit();
            }
            return coords;
        }
    }
}
