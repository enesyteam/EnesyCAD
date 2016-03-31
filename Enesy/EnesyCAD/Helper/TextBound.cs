using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Enesy.EnesyCAD.Geometry;

namespace Enesy.EnesyCAD.Helper
{
    public static partial class TextBounds
    {
        public enum BoundType
        {
            Rectangle = 0,
            Circle = 1,
            Triangle = 2,
            None = 3
        }

        //  -----------------------------
        //  |                           |
        //  |     TEXT                  |
        //  |                           |
        //  |         <----bufer------->|
        //  -----------------------------

        /// <summary>
        /// Tạo một hình bao quanh text
        /// </summary>
        /// <param name="text">Text cần bound</param>
        /// <param name="boundType">Kiểu bao </param>
        /// <param name="bufer">Khoảng bufer</param>
        public static void CreateTextBound(DBText text, BoundType boundType, double bufer = 0)
        {

            RectangleEx rec = GetTextBounds(text);

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;
            Editor acEd = acDoc.Editor;

            switch (boundType)
            {
                case BoundType.Rectangle:
                    {
                        using (Transaction tr = acDoc.TransactionManager.StartTransaction())
                        {
                            Polyline pl = new Polyline();
                            pl.AddVertexAt(0, rec.LowerLeft, 0, 0, 0);
                            pl.AddVertexAt(1, rec.UpperLeft, 0, 0, 0);
                            pl.AddVertexAt(2, rec.UpperRight, 0, 0, 0);
                            pl.AddVertexAt(3, rec.LowerRight, 0, 0, 0);
                            pl.Closed = true;
                            //pl.AddVertexAt(4, rec.LowerLeft, 0, 0, 0);

                            BlockTableRecord bRec = tr.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                            bRec.AppendEntity(pl);
                            tr.AddNewlyCreatedDBObject(pl, true);
                            tr.Commit();
                        }
                        break;
                    }
                case BoundType.Circle:
                    {
                        using (Transaction tr = acDoc.TransactionManager.StartTransaction())
                        {

                            Circle c = new Circle();

                            c.Center = new Point3d(rec.Center.X, rec.Center.Y, 0);
                            double radius = Enesy.EnesyCAD.Helper.Point2dHelper.Distance(rec.LowerLeft, rec.Center);
                            c.Radius = radius;

                            BlockTableRecord bRec = tr.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                            bRec.AppendEntity(c);
                            tr.AddNewlyCreatedDBObject(c, true);
                            tr.Commit();
                        }
                        break;
                    }
                case BoundType.Triangle:
                    {
                        using (Transaction tr = acDoc.TransactionManager.StartTransaction())
                        {

                            Circle c = new Circle();

                            c.Center = new Point3d(rec.Center.X, rec.Center.Y, 0);
                            double radius = Enesy.EnesyCAD.Helper.Point2dHelper.Distance(rec.LowerLeft, rec.Center);
                            c.Radius = radius;
                            double cos30 = Math.Sqrt(3) / 2;
                            double tang30 = 0.5 / cos30;

                            //
                            Point2d p1 = Enesy.EnesyCAD.Helper.Point2dHelper.Offset2d(Enesy.EnesyCAD.Helper.Point3dHelper.ToPoint2d(c.Center), 0, 2 * radius);
                            Point2d p2 = Enesy.EnesyCAD.Helper.Point2dHelper.Offset2d(Enesy.EnesyCAD.Helper.Point3dHelper.ToPoint2d(c.Center), -radius / tang30, -radius);
                            Point2d p3 = Enesy.EnesyCAD.Helper.Point2dHelper.Offset2d(Enesy.EnesyCAD.Helper.Point3dHelper.ToPoint2d(c.Center), radius / tang30, -radius);

                            Polyline pl = new Polyline();
                            pl.AddVertexAt(0, p1, 0, 0, 0);
                            pl.AddVertexAt(1, p2, 0, 0, 0);
                            pl.AddVertexAt(2, p3, 0, 0, 0);
                            pl.Closed = true;

                            BlockTableRecord bRec = tr.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                            bRec.AppendEntity(pl);
                            tr.AddNewlyCreatedDBObject(pl, true);
                            tr.Commit();
                        }
                        break;
                    }
                case BoundType.None:
                    {
                        break;
                    }

                default: break;
            }

        }
        /// <summary>
        /// Trả về một Rectangle bao quanh một chuỗi
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static RectangleEx GetTextBounds(DBText s, double bufer = 0.1)
        {
            //bufer *= s.Height;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Autodesk.AutoCAD.GraphicsInterface.TextStyle style = new Autodesk.AutoCAD.GraphicsInterface.TextStyle();
            byte n;

            Transaction tr = db.TransactionManager.StartTransaction();
            try
            {
                using (tr)
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                    string text = s.TextString;
                    TextStyleTable textStyleTable = tr.GetObject
                                                (
                                                    db.TextStyleTableId,
                                                    OpenMode.ForRead
                                                ) as TextStyleTable;

                    string currentTextStyle = Application.GetSystemVariable("TEXTSTYLE").ToString();

                    ObjectId textStyleId = ObjectId.Null;
                    textStyleId = textStyleTable[currentTextStyle];
                    Autodesk.AutoCAD.GraphicsInterface.TextStyle iStyle
                        = new Autodesk.AutoCAD.GraphicsInterface.TextStyle();

                    // get textstyle of newly created text 
                    TextStyleTableRecord txtbtr = (TextStyleTableRecord)tr.GetObject(textStyleId, OpenMode.ForRead);
                    // copy properties from TextStyleTableRecord and dbtext to temp AcGi.TextStyle (just very limited one for the future calculation)
                    style.FileName = txtbtr.FileName;
                    // then copy properties from existing text
                    style.TextSize = s.Height;  // txtbtr.TextSize;
                    style.ObliquingAngle = s.Oblique;
                    style.XScale = s.WidthFactor;
                    // load temp style record
                    try
                    {
                        n = style.LoadStyleRec;
                    }
                    catch { return null; }// something wrong then exit on error

                    // find out the extents 
                    Point2d minpt, maxpt;
                    // get extends of text
                    Extents2d ex = style.ExtentsBox(text, true, true, null);

                    minpt = ex.MinPoint;
                    maxpt = ex.MaxPoint;



                    tr.Commit();
                    RectangleEx rec = new RectangleEx(Math.Abs(minpt.X - maxpt.X),
                        Math.Abs(minpt.Y - maxpt.Y));

                    Point2d textPos = new Point2d(s.Position.X, s.Position.Y);
                    bufer = s.Height * 0.3;

                    rec.LowerLeft = new Point2d(textPos.X - bufer, textPos.Y - bufer);
                    rec.UpperLeft = new Point2d(rec.LowerLeft.X, rec.LowerLeft.Y + rec.Height + 2 * bufer);
                    rec.UpperRight = new Point2d(rec.UpperLeft.X + rec.Width + 2 * bufer, rec.UpperLeft.Y);
                    rec.LowerRight = new Point2d(rec.UpperRight.X, rec.LowerLeft.Y);

                    return rec;

                }
            }
            catch (System.Exception exc)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(exc.Message + "\n" + exc.StackTrace);
                return null;
            }
            finally { }
        }
    }
}
