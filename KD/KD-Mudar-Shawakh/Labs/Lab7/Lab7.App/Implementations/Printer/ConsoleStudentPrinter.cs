using System;
using System.Collections.Generic;
using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations.Printer
{
    public class ConsoleStudentPrinter : IStudentPrinter
    {
        public void PrintStudents(IReadOnlyList<Student> students)
        {
            Console.WriteLine("\n--- Students ---");

            if (students.Count == 0)
            {
                Console.WriteLine("(no students)");
                return;
            }

            // Header row — fixed-width columns.
            Console.WriteLine($"  {"ID",-5} {"Full Name",-25} {"Email",-30} {"Program",-20} Year");
            Console.WriteLine($"  {new string('-', 90)}");

            foreach (var s in students)
            {
                Console.WriteLine(
                    $"  {s.Id,-5} {s.FullName,-25} {s.Email,-30} {s.StudyProgram,-20} {s.EnrollmentYear}");
            }
        }
    }
}