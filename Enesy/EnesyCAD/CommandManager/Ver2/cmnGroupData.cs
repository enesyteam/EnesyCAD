using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class cmnGroupData
    {
        [XmlAttribute("GroupName")]
        public string msGroupName;
        [XmlAttribute("GroupNumber")]
        public int miGroupNumber;
        [XmlAttribute("IsCollapsed")]
        public bool mbCollapsed;
        private bool _mHide;

        public bool Hide
        {
            get
            {
                return this._mHide;
            }
            set
            {
                this._mHide = value;
            }
        }
    }
}
