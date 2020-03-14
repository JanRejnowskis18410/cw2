using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{
    [Serializable]
    [XmlRoot(ElementName = "uczelnia")]
    public class College
    {
        [XmlArray("studenci") ,XmlArrayItem(Type = typeof(Student), ElementName = "student")]
        public Student[] Students { get; set; }
        [XmlArray("activeStudies"), XmlArrayItem(Type = typeof(Studies), ElementName = "studies")]
        public Studies[] ActiveStudies { get; set; }
        [XmlAttribute(attributeName: "createdAt")]
        public string CreatedAt { get; set; }
        [XmlAttribute(attributeName: "author")]
        public string Author { get; set; }
    }
}
