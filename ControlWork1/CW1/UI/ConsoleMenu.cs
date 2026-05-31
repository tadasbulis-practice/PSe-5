using CW1After.Models;
using CW1After.Services;

namespace CW1After.UI;


/// All Console.WriteLine / Console.ReadLine calls live here.
/// Delegates all business logic to StudentService and ReportService.

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService  _reportService;

    public ConsoleMenu(StudentService studentService, ReportService reportService)
    {
        _studentService = studentService;
        _reportService  = reportService;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========== CW-1 Student Menu ==========");
            Console.WriteLine(" 1) List all students");
            Console.WriteLine(" 2) Add new student");
            Console.WriteLine(" 3) Add grade");
            Console.WriteLine(" 4) Show average");
            Console.WriteLine(" 5) Find by id");
            Console.WriteLine(" 6) Validate student");
            Console.WriteLine(" 7) Top 3 by average   [LINQ / no-LINQ]");
            Console.WriteLine(" 8) Students in group  [LINQ / no-LINQ]");
            Console.WriteLine(" 9) Statistics         [LINQ / no-LINQ]");
            Console.WriteLine(" 0) Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();
            if (choice == "0") { Console.WriteLine("Bye!"); return; }

            switch (choice)
            {
                case "1": ShowAll();           break;
                case "2": AddStudent();        break;
                case "3": AddGrade();          break;
                case "4": ShowAverage();       break;
                case "5": FindById();          break;
                case "6": ValidateStudent();   break;
                case "7": ShowTopByAverage();  break;
                case "8": ShowInGroup();       break;
                case "9": ShowStatistics();    break;
                default:  Console.WriteLine("Unknown choice."); break;
            }
        }
    }


    private void ShowAll()
    {
        foreach (var s in _studentService.GetAll())
            Console.WriteLine($"  [{s.Id}] {s.Name} ({s.GroupCode})  email={s.Email}  avg={AverageCalculator.Calculate(s):0.00}");
    }

    private void AddStudent()
    {
        Console.Write("New ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Bad ID."); return; }
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";
        Console.Write("Group code: ");
        var groupCode = Console.ReadLine() ?? "";

        var (_, message) = _studentService.AddStudent(id, name, email, groupCode);
        Console.WriteLine(message);
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Bad ID."); return; }
        Console.Write("Grade (1..10): ");
        if (!int.TryParse(Console.ReadLine(), out int grade)) { Console.WriteLine("Bad grade."); return; }

        var (_, message) = _studentService.AddGrade(id, grade);
        Console.WriteLine(message);
    }

    private void ShowAverage()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Bad ID."); return; }
        var (_, result, error) = _studentService.GetAverage(id);
        Console.WriteLine(result ?? error);
    }

    private void FindById()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Bad ID."); return; }
        var s = _studentService.FindById(id);
        if (s == null) { Console.WriteLine("Not found."); return; }
        Console.WriteLine($"  [{s.Id}] {s.Name} ({s.GroupCode})  email={s.Email}  avg={AverageCalculator.Calculate(s):0.00}");
        Console.WriteLine($"  Grades: [{string.Join(", ", s.Grades)}]");
    }

    private void ValidateStudent()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Bad ID."); return; }
        var (_, result, error) = _studentService.ValidateStudent(id);
        Console.WriteLine(result ?? error);
    }


    private void ShowTopByAverage()
    {
        Console.WriteLine("--- Top 3 by average (LINQ) ---");
        foreach (var x in _reportService.GetTopByAverage(3))
            Console.WriteLine($"  {x.Student.Name,-25} avg={x.Avg:0.00}");

        Console.WriteLine("--- Top 3 by average (no-LINQ) ---");
        foreach (var x in _reportService.GetTopByAverageWithoutLinq(3))
            Console.WriteLine($"  {x.Student.Name,-25} avg={x.Avg:0.00}");
    }

    private void ShowInGroup()
    {
        Console.Write("Group code (e.g. PI23): ");
        var code = Console.ReadLine() ?? "";

        Console.WriteLine($"--- Students in {code}, sorted by name (LINQ) ---");
        var linq = _reportService.GetStudentsInGroupSortedByName(code);
        if (linq.Count == 0) Console.WriteLine("  (none)");
        foreach (var x in linq)
            Console.WriteLine($"  [{x.Student.Id}] {x.Student.Name,-25} avg={x.Avg:0.00}");

        Console.WriteLine($"--- Students in {code}, sorted by name (no-LINQ) ---");
        var plain = _reportService.GetStudentsInGroupSortedByNameWithoutLinq(code);
        if (plain.Count == 0) Console.WriteLine("  (none)");
        foreach (var x in plain)
            Console.WriteLine($"  [{x.Student.Id}] {x.Student.Name,-25} avg={x.Avg:0.00}");
    }

    private void ShowStatistics()
    {
        Console.WriteLine("--- Statistics (LINQ) ---");
        PrintStats(_reportService.GetStatistics());

        Console.WriteLine("--- Statistics (no-LINQ) ---");
        PrintStats(_reportService.GetStatisticsWithoutLinq());
    }

    private static void PrintStats(Statistics s)
    {
        Console.WriteLine($"  Total students    : {s.TotalStudents}");
        Console.WriteLine($"  Total grades      : {s.TotalGrades}");
        Console.WriteLine($"  Mean of averages  : {s.MeanOfMeans:0.00}");
        Console.WriteLine($"  Max grade         : {s.MaxGrade}");
        Console.WriteLine($"  Any failing (<5)? : {s.HasFailing}");
        Console.WriteLine($"  All have email?   : {s.AllHaveEmail}");
    }
}
