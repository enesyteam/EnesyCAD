using Autodesk.AutoCAD.Geometry;
using Enesy.EnesyCAD.DatabaseServices;
using System;
using Point3dHelper = Enesy.EnesyCAD.Helper.Point3dHelper;
using EditorHelper = Enesy.EnesyCAD.Helper.EditorHelper;
using Autodesk.AutoCAD.DatabaseServices;

namespace Enesy.EnesyCAD.Utilities.CivilWorks.RebarArrangment
{
    public class RectRebarSection : CompositeFigure, IRebarSection
    {
        public double Cover { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public RectRebarSection(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void BuildSection(Point3d startPoint)
        { 
            // Geometry
            Point3d[] points = new Point3d[4];
            points[0] = startPoint;
            points[1] = Point3dHelper.Offset(points[0], Width, 0, 0);
            points[2] = Point3dHelper.Offset(points[1], 0, Height, 0);
            points[3] = Point3dHelper.Offset(points[2], -Width, 0, 0);

            Polyline bound = new Polyline();
            bound.AddVertexAt(bound.NumberOfVertices, Helper.Point3dHelper.ToPoint2d(points[0]), 0, 0, 0);
            bound.AddVertexAt(bound.NumberOfVertices, Helper.Point3dHelper.ToPoint2d(points[1]), 0, 0, 0);
            bound.AddVertexAt(bound.NumberOfVertices, Helper.Point3dHelper.ToPoint2d(points[2]), 0, 0, 0);
            bound.AddVertexAt(bound.NumberOfVertices, Helper.Point3dHelper.ToPoint2d(points[3]), 0, 0, 0);
            bound.Closed = true;

            this.Children.Add(bound);
        }
    }
}
