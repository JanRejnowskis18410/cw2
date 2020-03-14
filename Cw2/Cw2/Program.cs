using Cw2.Models;
using System;
using System.Collections;
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

            var fi = new FileInfo(path);
            ArrayList studentsList = new ArrayList();
            Dictionary<string, ArrayList> studiesInfo = new Dictionary<string, ArrayList>();
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] kolumny = line.Split(',');
                    bool check = true;
                    for (int j = 0; j < kolumny.Length && check; j++)
                    {
                        if (String.IsNullOrEmpty(kolumny[j]))
                        {
                            check = false;
                        }
                    }
                    if (check)
                    {
                        string[] birthInfo = kolumny[5].Split("-");
                        string birthDateString = birthInfo[2] + "." + birthInfo[1] + "." + birthInfo[0];
                        bool checkList = true;
                        foreach (Student s in studentsList)
                        {
                            if (s.IndexNumber == Int32.Parse(kolumny[4]))
                            {
                                checkList = false;
                                break;
                            }
                        }
                        if (checkList)
                        {
                            Student newStudent = new Student
                            {
                                FirstName = kolumny[0],
                                LastName = kolumny[1],
                                Studies = new Studies
                                {
                                    Name = kolumny[2],
                                    Mode = kolumny[3]
                                },
                                IndexNumber = Int32.Parse(kolumny[4]),
                                Birthdate = birthDateString,
                                Email = kolumny[6],
                                MothersName = kolumny[7],
                                FathersName = kolumny[8]
                            };
                            studentsList.Add(newStudent);
                            if (studiesInfo.ContainsKey(newStudent.Studies.Name))
                            {
                                    studiesInfo[newStudent.Studies.Name].Add(newStudent.IndexNumber);
                            }
                            else
                            {
                                studiesInfo.Add(newStudent.Studies.Name, new ArrayList());
                                studiesInfo[newStudent.Studies.Name].Add(newStudent.IndexNumber);
                            }
                        }
                    }
                }
            }
            //XML
            Student[] students = new Student[studentsList.Count];
            int i = 0;
            foreach (Student s in studentsList)
            {
                students[i++] = s;
            }
            for (int j = 0; j < students.Length; j++)
                Console.WriteLine(students[j]);

            i = 0;
            Studies[] studies = new Studies[studiesInfo.Count];
            foreach (KeyValuePair<string, ArrayList> keyValues in studiesInfo)
            {
                studies[i++] = new Studies { NameAttribute = keyValues.Key, NumberOfStudents = keyValues.Value.Count };
            }
            for (int j = 0; j < studies.Length; j++)
                Console.WriteLine(studies[j]);

            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("d"));

            College col = new College
            {
                Students = students,
                ActiveStudies = studies,
                Author = "Jan Rejnowski",
                CreatedAt = now.ToString("d")
            };

            FileStream writer = new FileStream(@"data.xml",
            FileMode.Create);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(College));
            serializer.Serialize(writer, col, ns);
            writer.Close();
        }

        public static string DictionaryToString(Dictionary<string, ArrayList> dictionary)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<string, ArrayList> keyValues in dictionary)
            {
                dictionaryString += keyValues.Key + " : " + String.Join(",", keyValues.Value.ToArray()) + ", ";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }
    }
}
