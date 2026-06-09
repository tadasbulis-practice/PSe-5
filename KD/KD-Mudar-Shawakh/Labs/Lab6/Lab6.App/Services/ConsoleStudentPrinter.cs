using System;
using System.Collections.Generic;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class ConsoleStudentPrinter : IStudentPrinter
    {
        public void PrintStudents(IReadOnlyList<Student> students)
        {
            Console.WriteLine("--- Students (Console) ---");

            if (students.Count == 0)
            {
                Console.WriteLine("(no students)");
                return;
            }

            foreach (var s in students)
            {
                // Same line format as LAB-5's PrintStudentReport — kept consistent on purpose.
                Console.WriteLine($"[{s.Id}] {s.Name} | {s.Email} | Grades: [{string.Join(", ", s.Grades)}]");
            }
        }
    }
}