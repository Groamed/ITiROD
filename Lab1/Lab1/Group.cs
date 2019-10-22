using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class Group
    {
        public string name { get; set; }
        public string[] header { get; set; }
        public List<Student> students { get; set; }
        public Dictionary<string, double> subjects { get; set; }
        public Dictionary<string, double> averageMarks { get; set; }


        public Group(List<string[]> data, string name = "ИП-41")
        {
            this.name = name;
            header = data[0];
            data.Remove(data[0]);

            subjects = new Dictionary<string, double>();
            averageMarks = new Dictionary<string, double>();
            students = new List<Student>();


            for (int i = 2; i < header.Length; i++)
            {
                if(!subjects.ContainsKey(header[i]))
                {
                    subjects.Add(header[i], 0);
                    averageMarks.Add(header[i], 0);
                }
            }

            subjects.Add("Средний", 0);
            averageMarks.Add("Средний", 0);

            foreach (string[] line in data)
            {
                Student student = new Student(line[0], line[1]);
                int count = 1;

                for (int i = 2; i < header.Length; i++)
                {
                    try
                    {
                        Subject subj;
                        if((subj = student.subjects.Find(item => item.name == header[i])) != null)
                        {
                            subj.AddMark(double.Parse(line[i]));
                            count++;
                        } else
                        {
                            subj = new Subject(header[i]);
                            subj.AddMark(double.Parse(line[i]));
                            student.AddSubj(subj);
                        }
                        
                        subjects[header[i]] += double.Parse(line[i]);
                        averageMarks[header[i]] = subjects[header[i]] / (students.Count * count + 1);
                    } 
                    catch
                    {
                        Console.WriteLine("Отсутствует оценка");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

                subjects["Средний"] += student.averageMark;
                averageMarks["Средний"] = subjects["Средний"] / (students.Count + 1);

                students.Add(student);
            }
        }

        public override string ToString()
        {
            string toOutput = "Группа: " + name + '\n';

            toOutput += string.Format("{0,-20} {1,-20}", header[0], header[1]);

            foreach (string head in subjects.Keys)
            {
                toOutput += string.Format("{0,-15}", head);
            }

            toOutput += string.Format("\n");

            foreach (Student person in students)
            {
                toOutput += person.ToString() + '\n';
            }

            toOutput += string.Format("{0,21}{1,-20}", "", "Средний по группе:");

            foreach (KeyValuePair<string, double> subj in averageMarks)
            {
                toOutput += string.Format("{0,-15}", Math.Round(subj.Value,2));
            }

            return toOutput;
        }
    }
}
