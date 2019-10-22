using System.Collections.Generic;

namespace Lab1
{
    class Student
    {
        public string name { get; set; }
        public string surname { get; set; }
        public double averageMark { get; set; }
        public List<Subject> subjects { get; set; }

        public Student(string name, string surname, string patronymic)
        {
            this.name = name;
            this.surname = surname;
            subjects = new List<Subject>();
        }

        public double AverageMark()
        {
            double total = 0;

            foreach (Subject subj in subjects)
            {
                total += subj.mark;
            }

            total /= subjects.Count;

            return total;
        }

        public void AddSubj(Subject subj)
        {
            subjects.Add(subj);
            averageMark = AverageMark();
        }
        public override string ToString()
        {
            string toOutput = "";

            toOutput += string.Format("{0,-20} {1,-20}", surname, name);

            foreach (Subject subj in subjects)
            {
                toOutput += string.Format("{0,-15}", subj.mark);
            }

            toOutput += string.Format("{0,-10}", AverageMark());

            return toOutput;
        }
    }
}