using System;

public class CompactStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        Console.WriteLine($"[{group.Name}] Students={group.Students.Count}");
        foreach (var s in group.Students)
        {
            Console.WriteLine($"{s.Id}: {s.FirstName} {s.LastName}");
        }
    }
}