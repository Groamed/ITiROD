using System;
using System.Collections.Generic;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = ".\\csv\\group.csv";
            string fileType = "json";
            string outputFile = "output";

            if (args.Length == 3)
            {
                inputFile = args[0];
                fileType = args[1];
                outputFile = args[2];
            }

            List<string[]> data = CsvReader.ReadStudentsGroup(inputFile);
            Group group = new Group(data);

            Console.WriteLine(group);

            if (fileType != null && outputFile != null)
            {
                if (fileType.ToUpper() == "JSON")
                {
                    JsonOutput.SaveStudentsGroup(group, outputFile);
                    Console.WriteLine("Записано в JSON.");
                }
                else if (fileType.ToUpper() == "EXCEL")
                {
                    ExcelOutput.SaveStudentsGroup(group, outputFile);
                    Console.WriteLine("Записано в Excel.");
                }
            }

            Console.ReadKey();
        }
    }
}
