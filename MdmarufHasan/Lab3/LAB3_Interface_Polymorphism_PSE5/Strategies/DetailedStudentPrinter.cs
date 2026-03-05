using System;

public class DetailedStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        foreach (var s in group.Students)
            Console.WriteLine($"{s.Id} | {s.Name} | {s.Email}");
    }
}