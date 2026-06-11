using System;

namespace Nassim.Lab3.Nassim.Service
{
    public class ShortPrinter : IStudentPrinter
    {
        public void Print(Group group)
        {
            Console.WriteLine($"Group {group.Name} — {group.Students.Count} students:");
            foreach (var s in group.Students)
                Console.WriteLine($"  → {s.FirstName} {s.LastName}");
        }
    }
}