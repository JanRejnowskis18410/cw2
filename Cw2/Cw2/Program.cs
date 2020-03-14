using Cw2.Exceptions;
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
            string pathToData;
            string pathToResult;
            string type;

            string logPath = @"log.txt";
            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }

            if (args.Length == 3)
            {
                try
                {
                    checkIfFileExists(args[0]);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
                    writeMessageToLog(logPath, "Plik " + args[0] + " nie istnieje!");
                    Environment.Exit(-1);
                };
                pathToData = args[0];
                pathToResult = args[1];
                type = args[2];
            }
            else
            {
                pathToData = @"dane.csv";
                pathToResult = @"result.xml";
                type = "xml";
            }

            var fi = new FileInfo(pathToData);
            ArrayList studentsList = new ArrayList();
            Dictionary<string, ArrayList> studiesInfo = new Dictionary<string, ArrayList>();

            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                int recordCounter = 1;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] kolumny = line.Split(',');

                    try
                    {
                        if (kolumny.Length != 9)
                        {
                            throw new WrongNumberOfColumnsException("Kolumna " + recordCounter++ + ": Niewłaściwa liczba kolumn!");
                        }
                    }
                    catch (WrongNumberOfColumnsException e)
                    {
                        Console.WriteLine(e);
                        writeMessageToLog(logPath, e.Message);
                        continue;
                    }

                    try
                    {
                        for (int j = 0; j < kolumny.Length; j++)
                        {
                            if (String.IsNullOrEmpty(kolumny[j]))
                            {
                                throw new EmptyColumnException("Kolumna " + recordCounter++ + ": Błąd w danych (puste pole)!");
                            }
                        }
                    }
                    catch (EmptyColumnException e)
                    {
                        Console.WriteLine(e);
                        writeMessageToLog(logPath, e.Message);
                        continue;
                    }

                    try
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
                            recordCounter++;
                        } else
                        {
                            throw new StudentAlreadyInFileException("Kolumna " + recordCounter++ + ": Student o podanym indeksie już znajduje się w pliku!");
                        }
                    } catch (StudentAlreadyInFileException e)
                    {
                        Console.WriteLine(e);
                        writeMessageToLog(logPath, e.Message);
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

            FileStream writer = new FileStream(pathToResult,
            FileMode.Create);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(College));
            serializer.Serialize(writer, col, ns);
            writer.Close();

            Console.WriteLine("Zakończono tworzenie pliku XML!");
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

        private static void checkIfFileExists(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("Plik " + path + " nie istnieje!");
        }

        private static void writeMessageToLog(string logPath, String message)
        {
            StreamWriter sw = File.AppendText(logPath);
            using (sw)
            {
                sw.WriteLine(message);
            }
            sw.Close();
        }
    }
};
