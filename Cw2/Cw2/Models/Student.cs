using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{
    [Serializable]
    public class Student
    {
        [XmlAttribute(attributeName: "indexNumber")]
        public int IndexNumber { get; set; }
        [XmlElement(elementName: "fname")]
        public string FirstName { get; set; }
        [XmlElement(elementName: "lname")]
        public string LastName { get; set; }
        [XmlElement(elementName: "birthdate")]
        public string Birthdate { get; set; }
        [XmlElement(elementName: "email")]
        public string Email { get; set; }
        [XmlElement(elementName: "mothersName")]
        public string MothersName { get; set; }
        [XmlElement(elementName: "fathersName")]
        public string FathersName { get; set; }
        [XmlElement(elementName: "studies")]
        public Studies Studies { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + IndexNumber + " " + Birthdate + " " + FathersName + " " + MothersName;
        }
    }
}
