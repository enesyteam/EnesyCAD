using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.Helper;
using System;

namespace Enesy.EnesyCAD.CoreTeamCommands.Text
{
    public class CloneSourceTextCommands : EnesyCADCommandBase
    {
        [EnesyCAD.Runtime.EnesyCADCommandMethod("CTT",
        "Text",
        "Clone content of one Text to another Text(s)",
        CommandsHelp.EnesyAuthor,
        "congnvc@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        [EnesyCAD.Runtime.CommandGroup("YYY")]
        public static void ColneToMultiText()
        {
            DBText sourText = null;
            DBText[] targetTexts = null;

            var doc = Application.DocumentManager.MdiActiveDocument;
            var peo = new PromptEntityOptions("\n" + CommandStringResources.ResourceManager.GetString("SelectSourceText", GLOBAL.CurrentCulture));
            peo.SetRejectMessage("\n" + CommandStringResources.ResourceManager.GetString("ObjectTypeIsNotSupported", GLOBAL.CurrentCulture));
            peo.AddAllowedClass(typeof(DBText), false);

            var per = doc.Editor.GetEntity(peo);
            if (per.Status != PromptStatus.OK)
                return;

            var sId = per.ObjectId;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var t = tr.GetObject(sId, OpenMode.ForRead) as DBText;
                if (t != null)
                {
                    sourText = t;
                }
            }
            //doc.Editor.WriteMessage("\nSource Text Content: " + sourText.TextString);
            targetTexts = EntitySelector.SelectMultiDbText("\nSelect Target Text(s)");
            if (targetTexts == null) return;
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                int index = 0;
                foreach (DBText t in targetTexts)
                {
                    var text = tr.GetObject(t.ObjectId, OpenMode.ForWrite) as DBText;
                    if (text != null)
                    {
                        text.TextString = sourText.TextString;
                        index++;
                    } 
                }
                tr.Commit();
                //doc.Editor.WriteMessage("\nCloned " + index + (index > 1 ? "  Texts" : " Text"));
            }
        }
        [EnesyCAD.Runtime.EnesyCADCommandMethod("TT",
        "Text",
        "TT.Desciption",
        CommandsHelp.EnesyAuthor,
        "congnvc@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        public static void ColneToText()
        {
            DBText sourText = null;
            DBText targetText = null;
            
            var doc = Application.DocumentManager.MdiActiveDocument;
            var peo = new PromptEntityOptions("\n" + CommandStringResources.ResourceManager.GetString("SelectSourceText", GLOBAL.CurrentCulture));
            peo.SetRejectMessage("\n" + CommandStringResources.ResourceManager.GetString("ObjectTypeIsNotSupported", GLOBAL.CurrentCulture));
            peo.AddAllowedClass(typeof(DBText), false);

            var per = doc.Editor.GetEntity(peo);
            if (per.Status != PromptStatus.OK)
                return;

            var sId = per.ObjectId;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var t = tr.GetObject(sId, OpenMode.ForRead) as DBText;
                if (t != null)
                {
                    sourText = t;
                }
            }
            doc.Editor.WriteMessage("\nSource Text Content: " + sourText.TextString);

            bool continueSelect = true;
            while (continueSelect)
            {
                var peo2 = new PromptEntityOptions("\nSelect Target Text");
                peo2.SetRejectMessage("\n" + CommandStringResources.ResourceManager.GetString("ObjectTypeIsNotSupported", GLOBAL.CurrentCulture));
                peo2.AddAllowedClass(typeof(DBText), false);
                var per2 = doc.Editor.GetEntity(peo2);
                if (per2.Status == PromptStatus.OK)
                {
                    var tId = per2.ObjectId;
                    using (var tr = doc.TransactionManager.StartTransaction())
                    {
                        var t = tr.GetObject(tId, OpenMode.ForWrite) as DBText;
                        if (t != null)
                        {
                            targetText = t;
                        }
                    }

                    using (Transaction tr = doc.TransactionManager.StartTransaction())
                    {
                        var text = tr.GetObject(targetText.ObjectId, OpenMode.ForWrite) as DBText;
                        if (text != null)
                        {
                            text.TextString = sourText.TextString;
                        }
                        tr.Commit();
                    }
                }
                else
                {
                    continueSelect = false;
                }

                
            }


            
        }
    }
}
