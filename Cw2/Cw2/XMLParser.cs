using Cw2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Cw2
{
    public class XMLParser
    {
        public static void createXmlFile(ArrayList studentsList, Dictionary<string, ArrayList> studiesInfo, string path)
        {
            Student[] students = new Student[studentsList.Count];
            int i = 0;
            foreach (Student s in studentsList)
            {
                students[i++] = s;
            }


            i = 0;
            Studies[] studies = new Studies[studiesInfo.Count];
            foreach (KeyValuePair<string, ArrayList> keyValues in studiesInfo)
            {
                studies[i++] = new Studies { NameAttribute = keyValues.Key, NumberOfStudents = keyValues.Value.Count };
            }

            DateTime now = DateTime.Now;

            College col = new College
            {
                Students = students,
                ActiveStudies = studies,
                Author = "Jan Rejnowski",
                CreatedAt = now.ToString("d")
            };

            makeXmlFile(path, col);
        }

        private static void makeXmlFile(string path, College col)
        {
            FileStream writer = new FileStream(path,
            FileMode.Create);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(College));
            serializer.Serialize(writer, col, ns);
            writer.Close();
        }
    }
}
