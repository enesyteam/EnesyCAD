using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class cmnToolbarButtonData
    {
        [XmlAttribute("Image")]
        public string msImage;
        [XmlAttribute("Expression")]
        public string msExpression;
        [XmlAttribute("ToolTip")]
        public string msToolTip;
        [XmlAttribute("Type")]
        public string msType;
    }
}
