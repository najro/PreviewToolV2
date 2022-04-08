using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
	[XmlRoot(ElementName = "DynamicPathFile")]
    public class DynamicPathFile
    {

        [XmlAttribute(AttributeName = "enable")]
        public bool Enable { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

}