using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using acApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Enesy.EnesyCAD
{
    partial class Utils
    {
        /// <summary>
        /// Calculate Polar Point
        /// </summary>
        /// <param name="pPt">Basepoint</param>
        /// <param name="dDist">Distance from basePoint to target</param>
        /// <param name="dAng">Angle from X axis</param>
        /// <returns></returns>
        public static Point3d Polar(Point3d pPt, double dDist, double dAng)
        {
            return new Point3d(pPt.X + dDist * Math.Cos(dAng),
                     pPt.Y + dDist * Math.Sin(dAng),
                     pPt.Z);
        }

        /// <summary>
        /// Get angle of pnt1-pnt2 line to horizontal axis
        /// </summary>
        /// <param name="pnt1"></param>
        /// <param name="pnt2"></param>
        /// <returns></returns>
        public static double AngleFromXAxisInXYPlane(
                                Point3d firstPoint, Point3d secondPoint)
        {
            Vector3d normal = new Vector3d(0, 0, 1);
            Plane plane = new Plane(new Point3d(0, 0, 0), normal);
            Point2d p1, p2;
            p1 = firstPoint.Convert2d(plane);
            p2 = secondPoint.Convert2d(plane);
            return p1.GetVectorTo(p2).Angle;
        }

        /// <summary>
        /// Get middle point between 2 points
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        /// <returns></returns>
        public static Point3d MiddlePoint(Point3d firstPoint, Point3d secondPoint)
        {
            LineSegment3d lineSeg3d = new LineSegment3d(firstPoint, secondPoint);
            return lineSeg3d.MidPoint;
        }      
    }
}
