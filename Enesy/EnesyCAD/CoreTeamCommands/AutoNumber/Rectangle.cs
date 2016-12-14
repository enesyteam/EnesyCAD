using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;

namespace Enesy.EnesyCAD.CoreTeamCommands.AutoNumber
{
    public class Rectangle
    {
        public Rectangle()
        {
        }
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
            UpperLeft = _upperLeft;
            UpperRight = _upperRight;
            LowerLeft = _lowerLeft;
            LowerRight = _lowerRight;
        }

        public double Width { get; set; }
        public double Height { get; set; }

        private Point2d _upperLeft;
        private Point2d _upperRight;
        private Point2d _lowerLeft;
        private Point2d _lowerRight;

        public Point2d UpperLeft
        {
            get { return _upperLeft; }
            set { _upperLeft = value; }
        }
        public Point2d UpperRight
        {
            get { return _upperRight; }
            set { _upperRight = value; }
        }
        public Point2d LowerLeft
        {
            get { return _lowerLeft; }
            set { _lowerLeft = value; }
        }
        public Point2d LowerRight
        {
            get { return _lowerRight; }
            set { _lowerRight = value; }
        }
        public Point2d Center
        {
            get { return new Point2d((LowerLeft.X + UpperRight.X) / 2, (LowerLeft.Y + UpperRight.Y) / 2); }
        }

    }
}
