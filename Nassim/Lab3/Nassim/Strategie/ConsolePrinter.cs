using System;

namespace Nassim.Lab3.Nassim.Service
{
    public class ConsolePrinter : IStudentPrinter
    {
        public void Print(Group group)
        {
            Console.WriteLine($"=== Group: {group.Name} ===");
            foreach (var s in group.Students)
                Console.WriteLine($"[{s.Id}] {s.FirstName} {s.LastName} | {s.Email} | Avg: {s.AverageGrade}");
        }
    }
}