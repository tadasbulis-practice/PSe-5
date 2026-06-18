using System;

public class ConsoleStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        Console.WriteLine($"Group: {group.Name}");
        foreach (var student in group.Students)
        {
            Console.WriteLine(student.Describe());
        }
    }
}