using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class AcCalcUnitItem
    {
        [XmlAttribute("Name")]
        public string msName;
        [XmlAttribute("Label")]
        public string msLabel;
        [XmlAttribute("IsDefault")]
        public bool mIsDefault;
    }
}
