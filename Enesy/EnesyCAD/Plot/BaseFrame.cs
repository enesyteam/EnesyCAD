using System.Collections.Generic;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Enesy.EnesyCAD;

namespace Enesy.EnesyCAD.Plot
{
    class BaseFrame
    {
        /// <summary>
        /// Frame
        /// </summary>
        private BlockReference frame;

        /// <summary>
        /// MinPoint and MaxPoint of plot area
        /// </summary>
        private Point3d[] points;

        /// <summary>
        /// Origin point of block/xref
        /// </summary>
        private Point3d origin;

        /// <summary>
        /// MinPoint and MaxPoint of plot area
        /// </summary>
        private Point3d minPoint, maxPoint;

        /// <summary>
        /// Distance from Origin to MinPoint and MaxPoint
        /// </summary>
        private double dMin, dMax;

        /// <summary>
        /// Angle between horizontal axis and Origin-MinPoint & MaxPoint direction
        /// </summary>
        private double aMin, aMax;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Frame"></param>
        /// <param name="Points"></param>
        public BaseFrame(BlockReference Frame, Point3d[] Points)
        {
            frame = Frame;
            // Note that position point of blockReference always is in WCS
            origin = Frame.Position;
            points = Points;
            minPoint = points[0];
            maxPoint = points[1];
            dMin = origin.DistanceTo(minPoint);
            dMax = origin.DistanceTo(maxPoint);
            aMin = Utils.AngleFromXAxisInXYPlane(origin, minPoint);
            aMax = Utils.AngleFromXAxisInXYPlane(origin, maxPoint);
        }

        /// <summary>
        /// Caculating plot area from baseFrame & list of block frame
        /// </summary>
        /// <param name="blkRefs">List of blockRef of frame</param>
        /// <param name="db">Current database</param>
        /// <returns>List of PlotArea</returns>
        public List<PlotArea> Generate(List<BlockReference> blkRefs)
        {
            List<PlotArea> areas = new List<PlotArea>();

            return areas;
        }
    }
}
