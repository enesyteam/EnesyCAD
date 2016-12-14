using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace Enesy.EnesyCAD.CoreTeamCommands.AutoNumber
{
    public enum PlaceTextOn { Left, Center, Right, Top, Bottom}
    public enum AutoType { Number, Letter }
    public enum TextAlign { Left, Center}
    public static class AutoNumber
    {
        private static PlaceTextOn PlaceTextOn = CoreTeamCommands.AutoNumber.PlaceTextOn.Center;
        private static AutoType AutoType = AutoType.Number;
        private static char startChar = 'A';
        private static double start = 0;
        private static double step = 1;
        private static string prefix = "";
        private static string surfix = "";
        private static int precision = 0;
        private static TextAlign mTextAlign = TextAlign.Left;
        private static TextBounds.BoundType boundType = TextBounds.BoundType.None;
        private static double textHeight = Convert.ToDouble(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("TEXTSIZE"));

        private static DBText t = null;
        [EnesyCAD.Runtime.EnesyCADCommandMethod("AN",
        "Text",
        "AN.Description",
        CommandsHelp.EnesyAuthor,
        "congnvc@gmail.com",
        CommandsHelp.TextAligment,
        CommandFlags.UsePickSet
        )]
        public static void AutoNums()
        {
            string currentFormat = "0";
            bool continuePick = true;
            Point3d basePoint;
            bool isFirstPick = true;
            while (continuePick)
            {
                string s = (AutoType == AutoType.Number) ? start.ToString("F" + precision, CultureInfo.InvariantCulture)
                    : startChar.ToString();
                string incrementString = (AutoType == AutoType.Number) ? ", Increment:" + step + ", Current Format:" + currentFormat :"";
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
                pr = GLOBAL.CurrentEditor.GetPoint(po2);

                if (pr.Status == PromptStatus.OK)
                {
                    Point3d thisPoint = pr.Value;
                    if (isFirstPick)
                    {
                        basePoint = thisPoint;
                        isFirstPick = false;
                    }

                    using (Transaction tr = GLOBAL.CurrentDocument.TransactionManager.StartTransaction())
                    {
                        DBText thisText = new DBText();
                        if (AutoType == AutoType.Number)
                            thisText.TextString = prefix + start.ToString("F" + precision, CultureInfo.InvariantCulture) + surfix;
                        else
                            thisText.TextString = prefix + startChar + surfix;

                        thisText.Height = textHeight;
                        // Determine Text Position
                        switch (mTextAlign)
                        { 
                            case TextAlign.Left:
                                Rectangle rec = TextBounds.GetTextBounds(thisText);
                                switch (PlaceTextOn)
                                { 
                                    case CoreTeamCommands.AutoNumber.PlaceTextOn.Bottom:
                                        thisText.Position = Helper.Point3dHelper.Offset(pr.Value, 0, -rec.Height, 0);
                                        break;
                                    case CoreTeamCommands.AutoNumber.PlaceTextOn.Center:
                                        thisText.Position = pr.Value;
                                        break;
                                    case CoreTeamCommands.AutoNumber.PlaceTextOn.Left:
                                        thisText.Position = Helper.Point3dHelper.Offset(pr.Value, -1.5*rec.Width, 0, 0);
                                        break;
                                    case CoreTeamCommands.AutoNumber.PlaceTextOn.Right:
                                        thisText.Position = Helper.Point3dHelper.Offset(pr.Value, rec.Width, 0, 0);
                                        break;
                                    case CoreTeamCommands.AutoNumber.PlaceTextOn.Top:
                                        thisText.Position = Helper.Point3dHelper.Offset(pr.Value, 0, rec.Height, 0);
                                        break;
                                }
                                break;
                            case TextAlign.Center:
                                {
                                    thisText.Position = pr.Value;
                                    Rectangle rec2 = TextBounds.GetTextBounds(thisText, 0);
                                    thisText.Position = new Point3d(rec2.Center.X, rec2.Center.Y, 0);
                                }
                                break;
                        }
                        

                        BlockTable acBlkTbl;
                        acBlkTbl = tr.GetObject(GLOBAL.CurrentDatabase.BlockTableId, OpenMode.ForRead) as BlockTable;

                        BlockTableRecord acBlkTblRec;
                        acBlkTblRec = tr.GetObject(GLOBAL.CurrentDatabase.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

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
                                PromptDoubleResult pdr = GLOBAL.CurrentEditor.GetDouble(pdo);
                                if (pdr.Status == PromptStatus.OK) { start = pdr.Value; }
                            }
                            else
                            {
                                PromptStringOptions pso = new PromptStringOptions("\nSpecify Start Letter (" + startChar + ")");
                                pso.AllowSpaces = false;
                                PromptResult psrs = GLOBAL.CurrentEditor.GetString(pso);
                                if (psrs.Status == PromptStatus.OK)
                                    startChar = psrs.StringResult[0];
                            }
                            break;
                        case "Increment":
                            PromptDoubleOptions pdo2 = new PromptDoubleOptions("\nSpecify increment number [" + step + "]");
                            PromptDoubleResult pdr2 = GLOBAL.CurrentEditor.GetDouble(pdo2);
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
                            pko.Keywords.Add("PLace");
                            pko.Keywords.Default = "Format";
                            PromptResult pkr = GLOBAL.CurrentEditor.GetKeywords(pko);

                            if (pkr.Status == PromptStatus.OK)
                            {
                                string resultKeyword = pkr.StringResult;
                                switch (resultKeyword)
                                {
                                    case "Prefix":
                                        PromptStringOptions pso = new PromptStringOptions("\nSpecify prefix (" + prefix + ")");
                                        pso.AllowSpaces = true;
                                        PromptResult psrs = GLOBAL.CurrentEditor.GetString(pso);
                                        if (psrs.Status == PromptStatus.OK)
                                            prefix = psrs.StringResult;
                                        break;
                                    case "Surfix":
                                        PromptStringOptions pso2 = new PromptStringOptions("\nSpecify Surfix (" + surfix + ")");
                                        pso2.AllowSpaces = true;
                                        PromptResult psrs2 = GLOBAL.CurrentEditor.GetString(pso2);
                                        if (psrs2.Status == PromptStatus.OK)
                                            surfix = psrs2.StringResult;
                                        break;
                                    case "Format":
                                        PromptIntegerOptions piio = new PromptIntegerOptions("\nSpecify NUMBER symbol after decimal symbol [" + " | Current Format:" + currentFormat + "]");
                                        piio.AllowNone = true;
                                        piio.AllowZero = true;
                                        piio.AllowNegative = false;

                                        PromptIntegerResult piir = GLOBAL.CurrentEditor.GetInteger(piio);
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
                                        PromptResult pkrb = GLOBAL.CurrentEditor.GetKeywords(pkob);

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

                                        PromptDoubleResult pdr3 = GLOBAL.CurrentEditor.GetDouble(pdo3);
                                        if (pdr3.Status == PromptStatus.OK)
                                            textHeight = pdr3.Value;
                                        break;
                                    case "NumberOrLetter":
                                        PromptKeywordOptions pkob2 = new PromptKeywordOptions("\nUsing Number or Letter? (" + AutoType.ToString() + ")");
                                        pkob2.AllowNone = true;

                                        pkob2.Keywords.Add("Number");
                                        pkob2.Keywords.Add("Letter");

                                        PromptResult pkrb2 = GLOBAL.CurrentEditor.GetKeywords(pkob2);

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
                                    case "PLace":
                                        PromptKeywordOptions pkob3 = new PromptKeywordOptions("\nPlace text On? (" + AutoType.ToString() + ")");
                                        pkob3.AllowNone = true;

                                        pkob3.Keywords.Add("Left");
                                        pkob3.Keywords.Add("Center");
                                        pkob3.Keywords.Add("Right");
                                        pkob3.Keywords.Add("Top");
                                        pkob3.Keywords.Add("Bottom");

                                        PromptResult pkrb3 = GLOBAL.CurrentEditor.GetKeywords(pkob3);

                                        if (pkrb3.Status == PromptStatus.OK)
                                        {
                                            string result = pkrb3.StringResult;
                                            switch (result)
                                            {
                                                case "Left":
                                                    PlaceTextOn = CoreTeamCommands.AutoNumber.PlaceTextOn.Left;
                                                    break;
                                                case "Center":
                                                    PlaceTextOn = CoreTeamCommands.AutoNumber.PlaceTextOn.Center;
                                                    break;
                                                case "Right":
                                                    PlaceTextOn = CoreTeamCommands.AutoNumber.PlaceTextOn.Right;
                                                    break;
                                                case "Top":
                                                    PlaceTextOn = CoreTeamCommands.AutoNumber.PlaceTextOn.Top;
                                                    break;
                                                case "Bottom":
                                                    PlaceTextOn = CoreTeamCommands.AutoNumber.PlaceTextOn.Bottom;
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
