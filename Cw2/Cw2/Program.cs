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
            string path = @"Data\dane.csv";
            Console.WriteLine("hello world");

            //Wczytywanie pliku
            var fi = new FileInfo(path);
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            // stream.Dispose();

            //XML
            var list = new List<Student>();
            var st = new Student
            {
                Imie="Jan",
                Nazwisko="Kowalski",
                Email="kowalski@wp.pl"
            };
            list.Add(st);

            FileStream writer = new FileStream(@"data.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>),
                                       new XmlRootAttribute("uczelnia"));
            serializer.Serialize(writer, list);
            serializer.Serialize(writer, list);

            // ----------------------------------------- zadanie -----------------------------------------

            string defaultCSV = "data.csv";
            string defaultResult = "result.xml";
            string defaultFiletype = "xml";


        }
    }
}
