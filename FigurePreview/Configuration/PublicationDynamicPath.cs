using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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