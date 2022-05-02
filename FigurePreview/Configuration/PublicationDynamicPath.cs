using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
    [XmlRoot(ElementName = "PublicationDynamicPath")]
    public class PublicationDynamicPath
    {
        [XmlText]
        public string Text { get; set; }
    }
}