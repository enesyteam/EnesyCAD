using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;

namespace RElXref
{
    public class Class1
    {
        [CommandMethod("RELXREF")]
        public void RelXref()
        {
            Document doc = acad.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Database db = doc.Database;
            Transaction tr = doc.TransactionManager.StartTransaction();
            try
            {
                using (tr)
                {
                    ObjectIdCollection btrCol = new ObjectIdCollection();
                    XrefGraph xrgraph = doc.Database.GetHostDwgXrefGraph(false);
                    // look at all Nodes in the XrefGraph.  Skip 0 node since it is the drawing itself.
                    for (int i = 1; i < (xrgraph.NumNodes - 1); i++)
                    {
                        XrefGraphNode xrNode = xrgraph.GetXrefNode(i);
                        if (!xrNode.IsNested)
                        {
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject
                                (xrNode.BlockTableRecordId, OpenMode.ForWrite);
                            string origPath = btr.PathName;
                            
                            string relativePath = AbstoRel(origPath);
                            ed.WriteMessage("\n Xref: " + btr.Name + " has path " + origPath + " or relative path " + relativePath);
                            db.XrefEditEnabled = true;
                            btr.PathName = relativePath;
                        }
                    }
                    tr.Commit();
                }
            }
            catch(Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage(ex.ToString());
            }
        }
        private string AbstoRel(string absolutePath)
        {
            string[] absoluteDirectories = absolutePath.Split('\\');
            string relativePath = @"..\";
            for (int i = absoluteDirectories.Length-2; i < absoluteDirectories.Length ; i++)
            {
                relativePath = string.Concat(relativePath, absoluteDirectories[i]);
                if (i < absoluteDirectories.Length - 1)
                {
                    relativePath = string.Concat(relativePath, "\\"); 
                }
            }
            return relativePath;
        }
    }
}

