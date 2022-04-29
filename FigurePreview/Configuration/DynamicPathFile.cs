using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
	[XmlRoot(ElementName = "DynamicPathFile")]
    public class DynamicPathFile
    {

        [XmlAttribute(AttributeName = "enable")]
        public bool Enabled { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

}