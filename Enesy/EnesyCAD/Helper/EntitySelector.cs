using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using System;

namespace Enesy.EnesyCAD.Helper
{
    public partial class EntitySelector
    {
        public static DBText[] SelectMultiDbText(string message, string rejectMessage = "")
        {
            DBText[] ents = null;

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDB = acDoc.Database;
            Editor acEd = acDoc.Editor;

            TypedValue[] acTypValAr = new TypedValue[1];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "TEXT"), 0);

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);
            PromptSelectionOptions po = new PromptSelectionOptions();
            po.MessageForAdding = message;
            if (!string.IsNullOrEmpty(rejectMessage))
                po.MessageForRemoval = rejectMessage;

            PromptSelectionResult pr = acEd.GetSelection(po, acSelFtr);
            
            using (Transaction tr = acDB.TransactionManager.StartTransaction())
            {
                if (pr.Status == PromptStatus.OK)
                {
                    SelectionSet selectionSet = pr.Value;
                    ents = new DBText[selectionSet.Count];
                    int icount = 0;
                    foreach (SelectedObject selectedObject in selectionSet)
                    {
                        if (selectedObject != null)
                        {
                            var selectedEntity = tr.GetObject(selectedObject.ObjectId, OpenMode.ForRead) as DBText;
                            if (selectedEntity != null)
                            {
                                ents[icount] = selectedEntity;
                                icount++;
                            }
                        }
                    }
                    return ents;
                }
                else
                    return null;
            }
        }
    }
}
