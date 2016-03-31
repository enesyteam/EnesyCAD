using System.Runtime.InteropServices;
using System.Collections.Generic;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Enesy.EnesyCAD.Runtime;
using Enesy.EnesyCAD.Utilities.Text;

namespace Enesy.EnesyCAD.Utilities
{
    public partial class Commands
    {
        //[DllImport("user32.dll")]
        //private static extern System.IntPtr SetFocus(System.IntPtr hwnd);
        //private static ObjectIdCollection objIdColl = null;

        [EnesyCAD.Runtime.EnesyCADCommandMethod("TA",
            "Text",
            "Aligment for text",
            "EnesyCAD",
            "quandt@enesy.vn",
            Enesy.Page.CadYoutube,
            CommandFlags.UsePickSet
            )]
        public void PerformTextAligment()
        {
            // Get implied Selection set
            //Document doc = Application.DocumentManager.MdiActiveDocument;
            //Editor ed = doc.Editor;
            //PromptSelectionResult prmSelRes = ed.SelectImplied();

            // Set selection set if implied Selection set exists
            //if (prmSelRes.Status == PromptStatus.OK)
            //{
                
            //    SelectionSet sSet = prmSelRes.Value;
            //    foreach (SelectedObject so in sSet)
            //    {
            //        objIdColl.Add(so.ObjectId);
            //    }
                //System.IntPtr hwnd = Autodesk.AutoCAD.ApplicationServices
                //                                    .Application.MainWindow.Handle;
                //SetFocus(hwnd);
            //}

            // Show text aligment dialog
            TextAligmentDialog tad = new TextAligmentDialog();
            //tad.ShowModal();            
            tad.ShowModeless(true);
        }

        /// <summary>
        /// Aligment text in current document
        /// </summary>
        /// <param name="alig"></param>
        public static void TextAligmentAction(Aligment alig)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            // Select DBText and MText
            List<string> oTp = new List<string>
            {
                "TEXT",
                "MTEXT"
            };
            ObjectIdCollection objIdColl = Utils.SelectionFilter(oTp, ed);

            // Get base point
            Point3d bPoint = new Point3d();
            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");
            pPtOpts.Message = "\nSpecify aligment point: ";
            pPtRes = ed.GetPoint(pPtOpts);

            // Perform aligment
            if (pPtRes.Status == PromptStatus.OK)
            {
                bPoint = pPtRes.Value;
                Utils.AligmentText(db, objIdColl, alig, bPoint);
            }
        }
    }
}
