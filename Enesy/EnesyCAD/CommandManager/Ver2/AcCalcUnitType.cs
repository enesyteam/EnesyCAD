using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class AcCalcUnitType
    {
        [XmlAttribute("Name")]
        public string msName;
        [XmlAttribute("Label")]
        public string msLabel;
        [XmlAttribute("IsDefault")]
        public bool mIsDefault;
        [XmlElement(ElementName = "UnitSubType")]
        public AcCalcUnitItem[] mUnitItems;
        private AcCalcUnitItem _mDefaultItem;

        public AcCalcUnitItem DefaultItem
        {
            get
            {
                return this._mDefaultItem;
            }
            set
            {
                this._mDefaultItem = value;
            }
        }
    }
}
