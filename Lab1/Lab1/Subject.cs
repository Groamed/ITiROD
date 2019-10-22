using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class Subject
    {
        public string name { get; set; }
        public List<double> mark { get; set; }

        public Subject(string name)
        {
            this.name = name;
            mark = new List<double>();
        }

        public void AddMark(double mark)
        {
            this.mark.Add(mark);
        }

        public override string ToString()
        {
            return name + ": " + mark.ToString();
        }
    }
}
