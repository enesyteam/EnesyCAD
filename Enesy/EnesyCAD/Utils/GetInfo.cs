using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;
using System;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Text;
using Microsoft.Win32;

namespace Enesy.EnesyCAD
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
        public static Point3dCollection ListVertices(Transaction tr, ObjectIdCollection polylines)
        {
            Point3dCollection coords = new Point3dCollection();

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

        /// <summary>
        /// Get the AutoCAD current version registry key
        /// </summary>
        /// <returns></returns>
        public static string GetAcadCurVerKey()
        {
            StringBuilder sb = new StringBuilder(@"Software\Autodesk\AutoCAD\");
            using (RegistryKey acad = Registry.CurrentUser.OpenSubKey(sb.ToString()))
            {
                sb.Append(acad.GetValue("CurVer")).Append(@"\");
                using (RegistryKey curVer = Registry.CurrentUser.OpenSubKey(sb.ToString()))
                {
                    return sb.Append(curVer.GetValue("CurVer")).ToString();
                }
            }
        }

        /// <summary>
        /// Get the acad.exe location for the AutoCAD current version
        /// </summary>
        /// <returns></returns>
        public static string GetAcadLocation()
        {
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(GetAcadCurVerKey()))
            {
                return (string)rk.GetValue("AcadLocation");
            }
        }
    }
}
