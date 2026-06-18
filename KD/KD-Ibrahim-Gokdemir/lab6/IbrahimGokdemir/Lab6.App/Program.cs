using Lab6.App.Implementations;
using Lab6.App.Models;
using Lab6.App.Services;

class Program
{
    static void Main(string[] args)
    {
        var repo = new MemoryStudentRepository();
        var printer = new ConsoleStudentPrinter();
        var strategy = new SimpleAverageStrategy();
        var validator = new BasicStudentValidator();

        repo.Add(new Student(1, "Ali", "ali@test.com", new List<int> { 8, 9, 10 }));
        repo.Add(new Student(2, "Ayse", "ayse@test.com", new List<int> { 7, 6, 8 }));
        repo.Add(new Student(3, "Mehmet", "mehmet@test.com", new List<int> { 10, 10, 9 }));

        var service = new StudentService(repo, printer, strategy, validator);
        service.Run();
    }
}