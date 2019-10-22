using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab1
{
    class CsvReader
    {
        public static List<string[]> ReadStudentsGroup(string path)
        {
            List<string[]> stringCSV = new List<string[]>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                        string[] lineArray = CSVParser.Split(line);

                        stringCSV.Add(lineArray);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Неверный формат файла или путь к нему.");
            }

            return stringCSV;
        }
    }
}
