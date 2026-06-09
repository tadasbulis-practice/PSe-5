using System.Collections.Generic;

namespace Lab5.App.Models
{
    public class Student
    {
        public string Name { get; }
        // Changed to public so the Strategy classes can access the grades
        public List<int> Grades { get; } 

        public Student(string name)
        {
            Name = name;
            Grades = new List<int>();
        }

        public void AddGrade(int grade)
        {
            Grades.Add(grade);
        }
    }
}