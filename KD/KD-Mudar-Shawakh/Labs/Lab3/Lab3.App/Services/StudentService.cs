using System;
using Lab3.App.Models;
using Lab3.App.Strategies;

namespace Lab3.App.Services
{
    public class StudentService : IStudentService
    {
        public void AddGrade(Student student, int grade)
        {
            student.AddGrade(grade);
        }

        public void PrintStudentReport(Student student, IAverageStrategy strategy)
        {
            // The service relies on the interface contract to get the calculation
            double average = strategy.Calculate(student);
            Console.WriteLine($"Student: {student.Name} | Strategy: {strategy.GetType().Name} | Average = {average:F2}");
        }
    }
}