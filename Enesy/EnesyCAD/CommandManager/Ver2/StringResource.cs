using System;
using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class StringResource
    {
        [XmlElement(ElementName = "ResString")]
        public ResString[] mStrings;

        public StringResource()
        {
        }
    }
}