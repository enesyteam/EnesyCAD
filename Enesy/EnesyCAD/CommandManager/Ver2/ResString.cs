using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
  public class ResString
  {
    [XmlAttribute("name")]
    public string mName;
    [XmlAttribute("value")]
    public string mValue;
  }
}