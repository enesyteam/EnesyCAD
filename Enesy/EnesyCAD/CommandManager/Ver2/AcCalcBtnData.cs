using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class cmnBtnData
    {
        [XmlAttribute("Label")]
        public string msLabel;
        [XmlAttribute("Action")]
        public cmnBtnData.Action msAction;
        [XmlAttribute("Expression")]
        public string msExpression;
        [XmlAttribute("ExpressionType")]
        public ExpressionType msExpressionType;
        [XmlAttribute("HIndex")]
        public int mHIndex;
        [XmlAttribute("VIndex")]
        public int mVIndex;
        [XmlAttribute("Color")]
        public string msColor;
        [XmlAttribute("ToolTip")]
        public string msToolTip;

        public enum Action
        {
            append = 1,
            clear = 2,
            clear_history = 3,
            backspace = 4,
            evaluate = 5,
            mem_store = 6,
            mem_plus = 7,
            mem_minus = 8,
            mem_recall = 9,
            mem_clear = 10,
        }
    }
}
