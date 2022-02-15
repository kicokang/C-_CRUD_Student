using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class Student
    {
        public string grade { get; set; }
        public string cclass { get; set; }
        public string no { get; set; }
        public string name { get; set; }
        public string score { get; set; }
        public Student()
        {

        }
        public Student(string grade, string cclass, string no, string name, string score)
        {
            this.grade = grade;
            this.cclass = cclass;
            this.no = no;
            this.name = name;
            this.score = score;
        }
    }
}
