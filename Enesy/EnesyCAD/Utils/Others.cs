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
    }
}
