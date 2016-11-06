
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class CMNBtnGroupData : cmnGroupData
    {
        [XmlAttribute("ButtonStyle")]
        public FlatStyle mBtnStyle = FlatStyle.System;
        [XmlAttribute("MaxHRatio")]
        public double mBtnMaxHRatio = 2.0;
        [XmlElement(ElementName = "ButtonSize")]
        public Size mBtnSize;
        [XmlElement(ElementName = "Button")]
        public cmnBtnData[] mButtons;
    }
}
