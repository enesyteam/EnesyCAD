using System.Runtime.InteropServices;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace EnesyCAD.Utilities.Text
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
            Enesy.WebPageLink.EnesyCadYoutube
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
            tad.ShowModal();            
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
            ObjectIdCollection objIdColl = Utilities.SelectText(db, ed);

            // Get base point
            Point3d bPoint = new Point3d();

            PromptPointResult pPtRes;

            PromptPointOptions pPtOpts = new PromptPointOptions("");
            pPtOpts.Message = "\nSpecify aligment point: ";
            pPtRes = ed.GetPoint(pPtOpts);

            if (pPtRes.Status == PromptStatus.OK)
            {
                bPoint = pPtRes.Value;
                Utilities.AligmentText(db, objIdColl, alig, bPoint);
            }
        }
    }
}
