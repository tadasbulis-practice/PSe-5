public class StubStudentPrinter : IStudentPrinter
{
    private readonly string _message;

    public StubStudentPrinter(string message)
    {
        _message = message;
    }

    public void Print(Group group)
    {
        Console.WriteLine(_message);
    }
}
