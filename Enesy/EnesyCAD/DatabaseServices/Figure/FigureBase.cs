using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.ComponentModel;
using System.Xml;
using System.Collections.Generic;

namespace Enesy.EnesyCAD.DatabaseServices
{
    /// <summary>
    /// Base class of Figure
    /// </summary>
    public class FigureBase : IFigure
    {
        [Browsable(false)]
        public Document Drawing { get; set; }
        [Browsable(false)]
        public Transaction Transaction { get; set; }
        [Browsable(false)]
        public BlockTableRecord BlockTableRecord { get; set; }

        #region Methods
        public virtual void Append(Document drawing)
        {

        }
        #endregion

        #region xml
        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Style", "");
        }

        public virtual void ReadXml(XmlElement element)
        {

        }



        #endregion
    }
}
