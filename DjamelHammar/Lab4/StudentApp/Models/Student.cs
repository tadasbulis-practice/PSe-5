using System.Collections.Generic;

namespace StudentApp.Models
{
    public class Student
    {
        public string Name { get; set; }
        public List<int> Grades { get; set; }

        public Student(string name, List<int> grades)
        {
            Name = name;
            Grades = grades;
        }
    }
}