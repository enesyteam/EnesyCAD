using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.CommandManager.Ver2;
using Enesy.EnesyCAD.CoreTeamCommands;
using Enesy.EnesyCAD.StringResources;
using System;
using System.Globalization;

namespace Enesy.EnesyCAD
{
    public class GLOBAL
    {
        public static Document CurrentDocument
        {
            get { return Application.DocumentManager.MdiActiveDocument; }
        }
        public static Database CurrentDatabase
        {
            get { return Application.DocumentManager.MdiActiveDocument.Database; }
        }
        public static Editor CurrentEditor
        {
            get { return Application.DocumentManager.MdiActiveDocument.Editor; }
        }
        public static CultureInfo CurrentCulture
        {
            get {
                switch (Language)
                { 
                    case EnesyCAD.Language.English:
                        return CultureInfo.CreateSpecificCulture("en-US");
                    case EnesyCAD.Language.Vietnamese:
                        return CultureInfo.CreateSpecificCulture("vi-VN");
                    default: return CultureInfo.CreateSpecificCulture("en-US");
                }
            }
        }
        private static Language mLanguage = EnesyCAD.Language.English;
        internal static Language Language {
            get { return mLanguage; }
            set { mLanguage = value; }
        }
        [EnesyCAD.Runtime.EnesyCADCommandMethod("CLANG",
        "GENERAL",
        "CLANG.Description",
        CommandsHelp.EnesyAuthor,
        "enesyteam@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        public static void Changelanguage()
        {
            Document activeDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = activeDoc.Database;
            Editor ed = activeDoc.Editor;

            PromptKeywordOptions pko = new PromptKeywordOptions("\n" + CommandStringResources.ResourceManager.GetString("ChangeLanguageMessage", GLOBAL.CurrentCulture));
            pko.Keywords.Add("English");
            pko.Keywords.Add("Vietnamese");
            pko.Keywords.Default = "English";

            PromptResult pr = ed.GetKeywords(pko);
            if (pr.Status == PromptStatus.OK)
            {
                switch (pr.StringResult)
                {
                    case "English":
                        Language = EnesyCAD.Language.English;
                        break;
                    case "Vietnamese":
                        Language = EnesyCAD.Language.Vietnamese;
                        break;
                    default:
                        break;
                }
                // reload UI for command list
                // no need to reload Command Manager UI
                if (CMNApplication.ESWCmn != null)
                {
                    if (CMNApplication.ESWCmn.ESW.Visible)
                    {
                        CMNApplication.ShowESWCmn();
                        CMNApplication.ESWCmn.CmnControl.ReloadUILanguage();
                    }
                }

            }
        }
        public static void WriteMessage(string message)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            doc.Editor.WriteMessage(message);
        }
    }
    public enum Language
    { 
        English=0,
        Vietnamese=1
    }
}
