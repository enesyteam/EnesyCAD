using System.Collections;
using System.Drawing;
using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{

    public class cmnControlData
    {
        [XmlElement(ElementName = "ToolBar")]
        public cmnToolbarData mToolbarData;
        [XmlAttribute("AllowGroupHide")]
        public bool mbAllowHide;
        [XmlElement(ElementName = "ButtonGroup")]
        public CMNBtnGroupData[] mBtnGroups;
        [XmlElement(ElementName = "UnitsGroup")]
        public AcCalcUnitGroupData mUnitGroup;
        [XmlElement(ElementName = "VariablesGroup")]
        public AcCalcVariableGroupData mVariableGroup;
        [XmlElement(ElementName = "ESWMinSize")]
        public Size mESWMinSize;
        [XmlElement(ElementName = "ESWDefaultSize")]
        public Size mESWDefaultSize;
        [XmlElement(ElementName = "StringResource")]
        public StringResource mStringResource;

        public ArrayList GroupList
        {
            get
            {
                ArrayList arrayList = new ArrayList();
                if (this.mBtnGroups != null)
                    arrayList.AddRange((ICollection)this.mBtnGroups);
                //if (this.mUnitGroup != null)
                //    arrayList.Add((object)this.mUnitGroup);
                //if (this.mVariableGroup != null)
                //    arrayList.Add((object)this.mVariableGroup);
                return arrayList;
            }
        }
    }
}
