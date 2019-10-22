using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class Subject
    {
        public string name { get; set; }
        public double mark { get; set; }

        public Subject(string name, double mark)
        {
            this.name = name;
            this.mark = mark;
        }

        public override string ToString()
        {
            return name + ": " + mark.ToString();
        }
    }
}
