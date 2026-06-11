using System;

namespace Nassim.Lab4.Nassim.Service{
    public class StubPrinter : IStudentPrinter
    {
        private readonly bool _showGrades;

        public StubPrinter(bool showGrades)
        {
            _showGrades = showGrades;
        }

        public void Print(Group group)
        {
            Console.WriteLine($"[STUB] Group: {group.Name}");
            foreach (var s in group.Students)
            {
                if (_showGrades)
                    Console.WriteLine($"  {s.FirstName} {s.LastName} — Avg: {s.AverageGrade}");
                else
                    Console.WriteLine($"  {s.FirstName} {s.LastName} — (grades hidden)");
            }
        }
    }
}