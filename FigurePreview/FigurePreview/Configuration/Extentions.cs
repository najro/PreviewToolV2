using System.Collections.Generic;
using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
    public class Extentions
    {
        [XmlElement(ElementName = "Ext")]
        public List<string> Ext;
    }
}