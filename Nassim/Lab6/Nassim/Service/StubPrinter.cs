using System;
using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public class StubPrinter : IStudentPrinter
    {
        private readonly bool _showGrades;

        public StubPrinter(bool showGrades)
        {
            _showGrades = showGrades;
        }

        public void PrintStudents(List<Student> students)
        {
            Console.WriteLine($"[STUB] {students.Count} student(s):");
            foreach (var s in students)
            {
                if (_showGrades)
                    Console.WriteLine($"  {s.FirstName} {s.LastName} — Avg: {s.AverageGrade}");
                else
                    Console.WriteLine($"  {s.FirstName} {s.LastName} — (grades hidden)");
            }
        }
    }
}