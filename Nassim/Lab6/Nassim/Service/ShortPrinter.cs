using System;
using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public class ShortPrinter : IStudentPrinter
    {
        public void PrintStudents(List<Student> students)
        {
            Console.WriteLine($"{students.Count} student(s):");
            foreach (var s in students)
                Console.WriteLine($"  → {s.FirstName} {s.LastName}");
        }
    }
}