public class FakeStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        Console.WriteLine("FAKE OUTPUT: No real students.");
    }
}
