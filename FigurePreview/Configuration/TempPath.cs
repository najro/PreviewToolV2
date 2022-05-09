using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
	[XmlRoot(ElementName = "TempPath")]
    public class TempPath
    {
        [XmlAttribute(AttributeName = "enable")]
        public bool Enabled { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}