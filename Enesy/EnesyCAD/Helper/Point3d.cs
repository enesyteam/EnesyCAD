using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace Enesy.EnesyCAD.Helper
{
    partial class Point3dHelper
    {
        /// <summary>
        /// Return Point2d from 3d point
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static Point2d ToPoint2d(Point3d p)
        {
            return new Point2d(p.X, p.Y);
        }
        /// <summary>
        /// Return distance between two points
        /// </summary>
        /// <param name="point2d1">1st point</param>
        /// <param name="point2d2">2nd point</param>
        /// <returns>return distance</returns>
        internal static double Distance(Autodesk.AutoCAD.Geometry.Point2d point2d1, Autodesk.AutoCAD.Geometry.Point2d point2d2)
        {
            return Math.Sqrt((point2d1.X - point2d2.X) * (point2d1.X - point2d2.X) + (point2d1.Y - point2d2.Y) * (point2d1.Y - point2d2.Y));
        }
        public static Point3d GetPoint(string s)
        {
            DocumentCollection acDocMgr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
            Document acDoc = acDocMgr.MdiActiveDocument;
            Editor ed = acDoc.Editor;
            Database acDb = acDoc.Database;

            // Lock the new document
            using (DocumentLock acLckDoc = acDoc.LockDocument())
            {
                // Start a transaction in the new database
                using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
                {
                    // Open the Block table for read
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acDb.BlockTableId,
                                                 OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                    PromptPointOptions po = new PromptPointOptions(s);
                    PromptPointResult pr;
                    pr = ed.GetPoint(po);

                    if (pr.Status != PromptStatus.OK)
                        return new Point3d(0, 0, 0);

                    acTrans.Commit();

                    return (Point3d)pr.Value;
                }
            }
        }
        public static Point3d Offset(Point3d point, double deltaX, double deltaY, double deltaZ)
        {
            return new Point3d(point.X + deltaX, point.Y + deltaY, point.Z + deltaZ);
        }
        public static Point3d HigherPoint(Point3d p1, Point3d p2)
        {
            return p1.Y >= p2.Y ? p1 : p2;
        }
        /// <summary>
        /// Khoảng cách giữa 2 điểm
        /// </summary>
        /// <param name="point1">Điểm 1</param>
        /// <param name="point2">Điểm 2</param>
        /// <returns></returns>
        public static double DistanceBetween2Point(Point3d point1, Point3d point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y) + (point1.Z - point2.Z) * (point1.Z - point2.Z));
        }

        /// <summary>
        /// Khoảng cách đại số theo phương X của 2 điểm
        /// </summary>
        /// <param name="point1">Điểm 1</param>
        /// <param name="point2">Điểm 2</param>
        /// <returns></returns>
        public static double HorizontalDistanceBetween2Point(Point3d point1, Point3d point2)
        {
            return point2.X - point1.X;
        }

        /// <summary>
        /// Khoảng cách đại số theo phương Y của 2 điểm
        /// </summary>
        /// <param name="point1">Điểm 1</param>
        /// <param name="point2">Điểm 2</param>
        /// <returns></returns>
        public static double VerticalDistanceBetween2Point(Point3d point1, Point3d point2)
        {
            return point2.Y - point1.Y;
        }

        /// <summary>
        /// Trả về một điểm cách một điểm cho trước một khoảng
        /// </summary>
        /// <param name="basePoint">Điểm gốc</param>
        /// <param name="horizontalDistance">Khoảng cách theo phương ngang</param>
        /// <param name="verticalDistance">Khoảng cách theo phương đứng</param>
        /// <returns></returns>
        public static Point3d GetPointFromAPoint(Point3d basePoint, double horizontalDistance, double verticalDistance)
        {
            return new Point3d(basePoint.X + horizontalDistance, basePoint.Y + verticalDistance, basePoint.Z);
        }
        /// <summary>
        /// Trả về điểm giữa 2 điểm
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point3d MidPoint(Point3d p1, Point3d p2)
        {
            return new Point3d((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2, (p1.Z + p2.Z) / 2);
        }

        /// <summary>
        /// Tính diện tích tam giác biết tọa độ 3 đỉnh
        /// </summary>
        /// <param name="p1">Đỉnh 1</param>
        /// <param name="p2">Đỉnh 2</param>
        /// <param name="p3">Đỉnh 3</param>
        /// <returns></returns>
        public static double AreaOfThreePoints(Point3d p1, Point3d p2, Point3d p3)
        {
            double dA, dB, dC, dP;

            dA = Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2) + Math.Pow((p2.Z - p1.Z), 2));
            dB = Math.Sqrt(Math.Pow((p3.X - p2.X), 2) + Math.Pow((p3.Y - p2.Y), 2) + Math.Pow((p3.Z - p2.Z), 2));
            dC = Math.Sqrt(Math.Pow((p1.X - p3.X), 2) + Math.Pow((p1.Y - p3.Y), 2) + Math.Pow((p1.Z - p3.Z), 2));
            dP = (dA + dB + dC) / 2;
            double resutl = Math.Sqrt(dP * (dP - dA) * (dP - dB) * (dP - dC));
            return resutl;

        }
    }
}
