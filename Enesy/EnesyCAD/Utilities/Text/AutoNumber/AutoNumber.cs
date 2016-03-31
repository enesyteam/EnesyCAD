using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Enesy.EnesyCAD.Helper;

namespace Enesy.EnesyCAD.Utilities
{
    public enum AutoType { Number, Letter }
    public static class AutoNumber
    {
        private static AutoType AutoType = AutoType.Number;
        private static char startChar = 'A';
        private static double start = 0;
        private static double step = 1;
        private static string prefix = "";
        private static string surfix = "";
        private static int precision = 0;
        private static TextBounds.BoundType boundType = TextBounds.BoundType.None;
        private static double textHeight = Convert.ToDouble(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("TEXTSIZE"));

        private static DBText t = null;

        [EnesyCAD.Runtime.EnesyCADCommandMethod("ANU",
        "Text",
        "Đánh số tự động trong AutoCAD",
        "EnesyCAD",
        "cad@enesy.vn",
        "https://www.youtube.com/watch?v=ma6t7cuxvNw",
        CommandFlags.UsePickSet
        )]
        public static void AutoNums()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;
            Editor acEd = acDoc.Editor;

            string currentFormat = "0";

            bool continuePick = true;

            Point3d basePoint;
            bool isFirstPick = true;

            while (continuePick)
            {
                string s = (AutoType == AutoType.Number) ? start.ToString("F" + precision, CultureInfo.InvariantCulture)
                    : startChar.ToString();
                string incrementString = (AutoType == AutoType.Number) ? ", Increment: " + step + ", Current Format: " + currentFormat : "";
                PromptPointOptions po2 = new PromptPointOptions("\nPick a point to insert text (Next: " + prefix
                    + s
                    + surfix
                    + incrementString + ", Text height: " + textHeight + ")" + " or ");

                if (!isFirstPick)
                {
                    po2.UseBasePoint = true;
                    po2.BasePoint = basePoint;
                }

                po2.Keywords.Add("Start");
                if (AutoType == AutoType.Number)
                {
                    po2.Keywords.Add("Increment");
                }
                po2.Keywords.Add("Options");
                PromptPointResult pr;
                pr = acEd.GetPoint(po2);

                if (pr.Status == PromptStatus.OK)
                {
                    Point3d thisPoint = pr.Value;
                    if (isFirstPick)
                    {
                        basePoint = thisPoint;
                        isFirstPick = false;
                    }

                    using (Transaction tr = acDoc.TransactionManager.StartTransaction())
                    {
                        DBText thisText = new DBText();
                        if (AutoType == AutoType.Number)
                            thisText.TextString = prefix + start.ToString("F" + precision, CultureInfo.InvariantCulture) + surfix;
                        else
                            thisText.TextString = prefix + startChar + surfix;

                        thisText.Position = pr.Value;
                        thisText.Height = textHeight;

                        BlockTable acBlkTbl;
                        acBlkTbl = tr.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                        BlockTableRecord acBlkTblRec;
                        acBlkTblRec = tr.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                        acBlkTblRec.AppendEntity(thisText);
                        tr.AddNewlyCreatedDBObject(thisText, true);
                        t = thisText;

                        TextBounds.CreateTextBound(thisText, boundType);
                        tr.Commit();
                        if (AutoType == AutoType.Number) start += step;
                        else
                            startChar++;
                    }
                }
                else if (pr.Status == PromptStatus.Keyword)
                {
                    switch (pr.StringResult)
                    {
                        case "Start":
                            if (AutoType == AutoType.Number)
                            {
                                PromptDoubleOptions pdo = new PromptDoubleOptions("\nSpecify starting number [" + start + "]");
                                PromptDoubleResult pdr = acEd.GetDouble(pdo);
                                if (pdr.Status == PromptStatus.OK) { start = pdr.Value; }
                            }
                            else
                            {
                                PromptStringOptions pso = new PromptStringOptions("\nSpecify Start Letter (" + startChar + ")");
                                pso.AllowSpaces = false;
                                PromptResult psrs = acEd.GetString(pso);
                                if (psrs.Status == PromptStatus.OK)
                                    startChar = psrs.StringResult[0];
                            }
                            break;
                        case "Increment":
                            PromptDoubleOptions pdo2 = new PromptDoubleOptions("\nSpecify increment number [" + step + "]");
                            PromptDoubleResult pdr2 = acEd.GetDouble(pdo2);
                            if (pdr2.Status == PromptStatus.OK) { step = pdr2.Value; }
                            break;
                        case "Options":
                            PromptKeywordOptions pko = new PromptKeywordOptions("\nSelect Options: ");
                            pko.Keywords.Add("Prefix");
                            pko.Keywords.Add("Surfix");
                            pko.Keywords.Add("Format");
                            pko.Keywords.Add("Bounds");
                            pko.Keywords.Add("TextHeight");
                            pko.Keywords.Add("NumberOrLetter");
                            pko.Keywords.Default = "Format";
                            PromptResult pkr = acEd.GetKeywords(pko);

                            if (pkr.Status == PromptStatus.OK)
                            {
                                string resultKeyword = pkr.StringResult;
                                switch (resultKeyword)
                                {
                                    case "Prefix":
                                        PromptStringOptions pso = new PromptStringOptions("\nSpecify prefix (" + prefix + ")");
                                        pso.AllowSpaces = true;
                                        PromptResult psrs = acEd.GetString(pso);
                                        if (psrs.Status == PromptStatus.OK)
                                            prefix = psrs.StringResult;
                                        break;
                                    case "Surfix":
                                        PromptStringOptions pso2 = new PromptStringOptions("\nSpecify Surfix (" + surfix + ")");
                                        pso2.AllowSpaces = true;
                                        PromptResult psrs2 = acEd.GetString(pso2);
                                        if (psrs2.Status == PromptStatus.OK)
                                            surfix = psrs2.StringResult;
                                        break;
                                    case "Format":
                                        PromptIntegerOptions piio = new PromptIntegerOptions("\nSpecify NUMBER symbol after decimal symbol [" + " | Current Format:" + currentFormat + "]");
                                        piio.AllowNone = true;
                                        piio.AllowZero = true;
                                        piio.AllowNegative = false;

                                        PromptIntegerResult piir = acEd.GetInteger(piio);
                                        if (piir.Status == PromptStatus.OK)
                                            precision = piir.Value;
                                        if (precision > 0)
                                        {
                                            currentFormat = "0";
                                            currentFormat += ".";
                                            for (int i = 1; i <= precision; i++)
                                                currentFormat += "0";
                                        }
                                        break;
                                    case "Bounds":
                                        PromptKeywordOptions pkob = new PromptKeywordOptions("\nSpecify Text bound type (" + boundType.ToString() + ")");
                                        pkob.AllowNone = true;

                                        pkob.Keywords.Add("None");
                                        pkob.Keywords.Add("Rectangle");
                                        pkob.Keywords.Add("Circle");
                                        pkob.Keywords.Add("Triangle");
                                        pkob.Keywords.Default = "None";
                                        PromptResult pkrb = acEd.GetKeywords(pkob);

                                        if (pkrb.Status == PromptStatus.OK)
                                        {
                                            string result = pkrb.StringResult;
                                            switch (result)
                                            {
                                                case "None":
                                                    boundType = TextBounds.BoundType.None;
                                                    break;
                                                case "Rectangle":
                                                    boundType = TextBounds.BoundType.Rectangle;
                                                    break;
                                                case "Circle":
                                                    boundType = TextBounds.BoundType.Circle;
                                                    break;
                                                case "Triangle":
                                                    boundType = TextBounds.BoundType.Triangle;
                                                    break;

                                                default: break;
                                            }
                                        }

                                        break;
                                    case "TextHeight":
                                        PromptDoubleOptions pdo3 = new PromptDoubleOptions("\nSpecify height for new text (" + textHeight + ")");

                                        PromptDoubleResult pdr3 = acEd.GetDouble(pdo3);
                                        if (pdr3.Status == PromptStatus.OK)
                                            textHeight = pdr3.Value;
                                        break;
                                    case "NumberOrLetter":
                                        PromptKeywordOptions pkob2 = new PromptKeywordOptions("\nUsing Number or Letter? (" + AutoType.ToString() + ")");
                                        pkob2.AllowNone = true;

                                        pkob2.Keywords.Add("Number");
                                        pkob2.Keywords.Add("Letter");

                                        PromptResult pkrb2 = acEd.GetKeywords(pkob2);

                                        if (pkrb2.Status == PromptStatus.OK)
                                        {
                                            string result = pkrb2.StringResult;
                                            switch (result)
                                            {
                                                case "Number":
                                                    AutoType = AutoType.Number;
                                                    break;
                                                case "Letter":
                                                    AutoType = AutoType.Letter;
                                                    break;

                                                default: break;
                                            }
                                        }

                                        break;
                                    default: break;
                                }
                            }

                            break;

                        default: break;

                    }
                }
                else
                {
                    continuePick = false;
                }
            }
        }
    }
}
