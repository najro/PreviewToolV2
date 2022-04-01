using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
    public class Figure
    {
        [XmlElement(ElementName = "Name")]
        public string Name;

        [XmlElement(ElementName = "FormatPath")]
        public string FormatPath;

        [XmlElement(ElementName = "Extentions")]
        public Extentions Extentions;
    }
}
