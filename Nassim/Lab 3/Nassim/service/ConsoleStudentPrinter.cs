public class ConsoleStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        foreach (var s in group.Students)
        Console.WriteLine(s.Name);
    }
}