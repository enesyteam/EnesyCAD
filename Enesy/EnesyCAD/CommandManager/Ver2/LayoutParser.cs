using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    internal class LayoutParser : XmlSerializer
    {
        protected override object Deserialize(XmlSerializationReader reader)
        {
            return ((XmlSerializationReader1)reader).Read23_AcCalcControlData();
        }

        protected override XmlSerializationReader CreateReader()
        {
            return (XmlSerializationReader)new XmlSerializationReader1();
        }

        protected override void Serialize(object value, XmlSerializationWriter writer)
        {
            ((XmlSerializationWriter1)writer).Write22_AcCalcControlData(value);
        }

        protected override XmlSerializationWriter CreateWriter()
        {
            return (XmlSerializationWriter)new XmlSerializationWriter1();
        }
    }
}