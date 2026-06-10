using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.App
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

    public class GroupService
    {
        public double CalculateAverage(Student student)
        {
            if (student.Grades == null || !student.Grades.Any())
                return 0;

            return student.Grades.Average();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var grades = new List<int> { 8, 9, 10, 7 };

            var student = new Student(
                "Md Samiul Alam Rifad",
                grades
            );

            var service = new GroupService();

            double avg = service.CalculateAverage(student);

            Console.WriteLine("## Lab 1 Refactored Output");
            Console.WriteLine($"Student Name: {student.Name}");
            Console.WriteLine($"Average Grade: {avg:F2}");
        }
    }
}