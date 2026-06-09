using System;
using Lab4.App.Models;
using Lab4.App.Strategies;

namespace Lab4.App.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentFinder _finder;

        // Constructor Injection
        public StudentService(IStudentFinder finder)
        {
            _finder = finder;
        }

        public void AddGrade(Student student, int grade)
        {
            student.AddGrade(grade);
        }

        public void PrintStudentReport(Student student, IAverageStrategy strategy)
        {
            double average = strategy.Calculate(student);
            Console.WriteLine($"Student: {student.Name} | Strategy: {strategy.GetType().Name} | Average = {average:F2}");
        }

        public void SearchAndDisplayStudent(Group group, string query)
        {
            var student = _finder.Find(group, query);

            if (student != null)
            {
                Console.WriteLine($"[Success] Found student: {student.Name}");
            }
            else
            {
                Console.WriteLine($"[Error] Student '{query}' not found.");
            }
        }
    }
}