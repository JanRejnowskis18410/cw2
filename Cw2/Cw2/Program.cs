using Cw2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"\dane.csv";
            string result = @"\result.xml";
            string default_format = "xml";

            //XML
            var st = new Student
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Birthdate = "03.04.1984",
                Email = "kowalski@wp.pl",
                MothersName = "Alina",
                FathersName = "Andrzej",
                Studies = new Studies
                {
                    Name = "Computer Science",
                    Mode = "Dzienne"
                }
            };
            var st2 = new Student
            {
                FirstName = "Adam",
                LastName = "Nowak",
                Birthdate = "05.05.2000",
                Email = "nowak@wp.pl",
                MothersName = "Anna",
                FathersName = "Sebastian",
                Studies = new Studies
                {
                    Name = "New Media Art",
                    Mode = "Zaoczne"
                }
            };
            Student[] students = { st, st2 };

            Studies[] studies = { new Studies { NameAttribute = "Computer Science", NumberOfStudents = 1 }, new Studies { NameAttribute = "New Media Art", NumberOfStudents = 1 } };
            College col = new College
            {
                Students = students,
                ActiveStudies = studies,
                Author = "Jan Rejnowski",
                CreatedAt = "12.03.2020"
            };

            FileStream writer = new FileStream(@"data.xml",
            FileMode.Create);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(College));
            serializer.Serialize(writer, col, ns);
            writer.Close();
        }
    }
}
