using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace Enesy.EnesyCAD.DatabaseServices
{
    public interface IFigure
    {
        Document Drawing { get; set; }
        Transaction Transaction { get; set; }
        void Append(Document drawing);
        void WriteXml(System.Xml.XmlWriter writer);
    }
}
