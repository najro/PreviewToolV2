using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
    [XmlRoot(ElementName = "FigurePreview")]
    public class FigurePreview
    {
        [XmlAttribute(AttributeName = "startpath")]
        public string StartPath;

        [XmlElement(ElementName = "DynamicPathFile")]
        public DynamicPathFile DynamicPathFile;

        [XmlElement(ElementName = "TempPath")]
        public TempPath TempPath;
        
        //[XmlElement(ElementName = "PathRoule")]
        //public string PathRoule;

        [XmlElement(ElementName = "Figure")]
        public List<Figure> Figure;

        public string DynamicPathDirectory => Path.GetDirectoryName(DynamicPathFile.Text);

        public string DynamicPathFileName => Path.GetFileName(DynamicPathFile.Text);

        public PublicationDynamicPath PublicationDynamicPath;
    }
}
