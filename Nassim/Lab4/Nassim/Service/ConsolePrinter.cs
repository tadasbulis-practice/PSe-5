using System;

namespace Nassim.Lab4.Nassim.Service
{
    public class ConsolePrinter : IStudentPrinter
    {
        public void Print(Group group)
        {
            Console.WriteLine($"=== Group: {group.Name} ===");
            foreach (var s in group.Students)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName} | Email: {s.Email} | Avg: {s.AverageGrade:F2}");
            }
        }
    }
}