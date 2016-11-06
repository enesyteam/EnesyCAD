using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class AcCalcUnitGroupData : cmnGroupData
    {
        [XmlElement(ElementName = "UnitType")]
        public AcCalcUnitType[] mUnitTypes;
        private AcCalcUnitType _mDefault;

        public AcCalcUnitType DefaultType
        {
            get
            {
                return this._mDefault;
            }
            set
            {
                this._mDefault = value;
            }
        }
    }
}
