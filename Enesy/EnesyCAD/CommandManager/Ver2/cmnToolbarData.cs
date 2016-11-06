using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class cmnToolbarData
    {
        [XmlElement(ElementName = "ToolBarButton")]
        public cmnToolbarButtonData[] mButtons;
    }
}
