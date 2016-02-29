using Autodesk.AutoCAD.Geometry;

namespace Enesy.EnesyCAD.Plot
{
    class PlotArea
    {
        /// <summary>
        /// Properties(/field) that indicates layout own this Plotwindow
        /// </summary>
        public string LayoutName;

        /// <summary>
        /// Properties that indicates PlotWindow's position
        /// MinPoint: Left, Lower corner point
        /// MaxPoint: Right, Upper corner point
        /// </summary>
        public Point3d MinPoint, MaxPoint;

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="minPoint">Left Lower point of plot area</param>
        /// <param name="maxPoint">Right Upper point of plot area</param>
        /// <param name="layout">Layout that contains plot area</param>
        public PlotArea(Point3d minPoint, Point3d maxPoint, string layout)
        {
            LayoutName = layout;
            MinPoint = minPoint;
            MaxPoint = maxPoint;            
        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="minPoint"></param>
        /// <param name="maxPoint"></param>
        public PlotArea(Point3d minPoint, Point3d maxPoint)
        {
            MinPoint = minPoint;
            MaxPoint = maxPoint;
        }
    }
}
