using Autodesk.AutoCAD.Geometry;
using System;

namespace Enesy.EnesyCAD.Utilities.CivilWorks.RebarArrangment
{
    public interface IRebarSection
    {
        /// <summary>
        /// Concrete Cover thickness
        /// </summary>
        double Cover { get; set; }
        /// <summary>
        /// Build Whole Section
        /// </summary>
        void BuildSection(Point3d startPoint);
    }
}
