using System;
using Lab5.App.Models;
using Lab5.App.Strategies;

namespace Lab5.App.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentFinder _finder;
        private readonly IAverageStrategy _averageStrategy;

        // Constructor Injection for both dependencies
        public StudentService(IStudentFinder finder, IAverageStrategy averageStrategy)
        {
            _finder = finder;
            _averageStrategy = averageStrategy;
        }

        public void AddGrade(Student student, int grade)
        {
            student.AddGrade(grade);
        }

        public void PrintStudentReport(Student student)
        {
            double average = _averageStrategy.Calculate(student);
            Console.WriteLine($"Student: {student.Name} | Strategy: {_averageStrategy.GetType().Name} | Average = {average:F2}");
        }

        public void SearchAndDisplayStudent(Group group, string query)
        {
            var result = _finder.Find(group, query);
            if (result != null)
                Console.WriteLine($"[Success] Found student: {result.Name}");
            else
                Console.WriteLine($"[Error] Student '{query}' not found.");
        }
    }
}