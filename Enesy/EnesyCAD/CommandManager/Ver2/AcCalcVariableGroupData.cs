using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class AcCalcVariableGroupData : cmnGroupData
    {
        [XmlAttribute("MinHeight")]
        public int mMinHeight;
        //[XmlElement(ElementName = "AcCalcVariablesData")]
        //public AcCalcVariablesData mVarData;
    }
}
