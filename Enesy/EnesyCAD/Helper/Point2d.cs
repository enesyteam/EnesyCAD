using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace Enesy.EnesyCAD.Helper
{
    partial class Point2dHelper
    {
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
        /// <summary>
        /// Offset 2d Point with giving x and y distances
        /// </summary>
        /// <param name="point">Base Point</param>
        /// <param name="deltaX">Distance on X direct</param>
        /// <param name="deltaY">Distance on Y direct</param>
        /// <returns></returns>
        internal static Point2d Offset2d(Point2d point, double deltaX, double deltaY)
        {
            return new Point2d(point.X + deltaX, point.Y + deltaY);
        }
        internal static Point2d ToPoint2d(Point3d p)
        {
            return new Point2d(p.X, p.Y);
        }
    }
}
