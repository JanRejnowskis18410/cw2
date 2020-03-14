using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{
    [Serializable]
    public class Studies
    {
        [XmlElement(elementName: "name")]
        public string Name { get; set; }
        [XmlElement(elementName: "mode")]
        public string Mode { get; set; }
        [XmlAttribute(attributeName: "name")]
        public string NameAttribute { get; set; }
        [XmlAttribute(attributeName: "numberOfStudents")]
        public int NumberOfStudents { get; set; }

        public bool ShouldSerializeName()
        {
            return !String.IsNullOrEmpty(Name);
        }

        public bool ShouldSerializeNameAttribute()
        {
            return !String.IsNullOrEmpty(NameAttribute);
        }

        public bool ShouldSerializeNumberOfStudents()
        {
            return NumberOfStudents > 0;
        }

        public bool ShouldSerializeMode()
        {
            return !String.IsNullOrEmpty(Mode);
        }

        public override string ToString()
        {
            return Name + " " + NameAttribute + " " + Mode + " " + NumberOfStudents;
        }
    }
}
