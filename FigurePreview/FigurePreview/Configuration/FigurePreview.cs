using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FigurePreview.Configuration
{

    [XmlRoot(ElementName = "FigurePreview")]
    public class FigurePreview
    {
        [XmlAttribute(AttributeName = "startpath")]
        public string StartPath;

        [XmlElement(ElementName = "PathRoule")]
        public string PathRoule;

        [XmlElement(ElementName = "Figure")]
        public List<Figure> Figure;
    }
}
