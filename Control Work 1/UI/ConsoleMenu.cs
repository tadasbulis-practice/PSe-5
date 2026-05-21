using CW1Friend.Models;
using CW1Friend.Services;

namespace CW1Friend.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;
    private readonly AverageCalculator _calc;

    public ConsoleMenu(StudentService studentService, ReportService reportService, AverageCalculator calc)
    {
        _studentService = studentService;
        _reportService  = reportService;
        _calc           = calc;
    }

    public void Start()
    {
        bool active = true;

        while (active)
        {
            ShowMenu();
            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1": ShowAllStudents();     break;
                case "2": CreateStudent();       break;
                case "3": EnterGrade();          break;
                case "4": DisplayAverage();      break;
                case "5": SearchById();          break;
                case "6": RunValidation();       break;
                case "7": ShowTopStudents();     break;
                case "8": ShowGroupStudents();   break;
                case "9": ShowStats();           break;
                case "0": active = false;        break;
                default:  Console.WriteLine(">> Invalid choice, please try again."); break;
            }

            if (active) Console.WriteLine();
        }

        Console.WriteLine(">> Session ended. Bye!");
    }

    private void ShowMenu()
    {
        Console.WriteLine("╔══════════════════════════╗");
        Console.WriteLine("║     STUDENT REGISTRY     ║");
        Console.WriteLine("╠══════════════════════════╣");
        Console.WriteLine("║ 1. View all students     ║");
        Console.WriteLine("║ 2. Register student      ║");
        Console.WriteLine("║ 3. Enter grade           ║");
        Console.WriteLine("║ 4. View average          ║");
        Console.WriteLine("║ 5. Search by ID          ║");
        Console.WriteLine("║ 6. Validate student      ║");
        Console.WriteLine("║ 7. Top N students        ║");
        Console.WriteLine("║ 8. Students by group     ║");
        Console.WriteLine("║ 9. Statistics            ║");
        Console.WriteLine("║ 0. Exit                  ║");
        Console.WriteLine("╚══════════════════════════╝");
        Console.Write("Your choice: ");
    }

    // ── Items 1–6 ────────────────────────────────────────────

    private void ShowAllStudents()
    {
        var list = _studentService.ListAll();
        Console.WriteLine($"\n>> All students ({list.Count} total):");
        Console.WriteLine(new string('-', 55));
        foreach (var s in list)
            PrintRow(s);
        Console.WriteLine(new string('-', 55));
    }

    private void CreateStudent()
    {
        Console.WriteLine("\n>> Register new student");
        Console.Write("Full name   : ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Email       : ");
        string email = Console.ReadLine() ?? "";

        Console.WriteLine("Groups available:");
        foreach (var g in _studentService.ListGroups())
            Console.WriteLine($"   {g.Code}  ({g.GroupName})");

        Console.Write("Group code  : ");
        string code = Console.ReadLine() ?? "";

        var student = new Student
        {
            Id           = _studentService.NextId(),
            FullName     = name,
            EmailAddress = email,
            GroupCode    = code
        };

        _studentService.AddStudent(student);
        Console.WriteLine($">> Registered. Assigned ID: {student.Id}");
    }

    private void EnterGrade()
    {
        Console.WriteLine("\n>> Enter grade");
        Console.Write("Student ID : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine(">> Not a valid ID."); return; }

        Console.Write("Grade (1–10): ");
        if (!int.TryParse(Console.ReadLine(), out int grade) || grade < 1 || grade > 10)
        { Console.WriteLine(">> Grade must be 1–10."); return; }

        bool ok = _studentService.AddGrade(id, grade);
        Console.WriteLine(ok ? ">> Grade saved." : ">> Student not found.");
    }

    private void DisplayAverage()
    {
        Console.WriteLine("\n>> View average");
        Console.Write("Student ID : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine(">> Not a valid ID."); return; }

        double avg = _studentService.GetAverage(id);
        if (avg < 0)
            Console.WriteLine(">> Student not found.");
        else
            Console.WriteLine($">> Average grade: {avg:F2}");
    }

    private void SearchById()
    {
        Console.WriteLine("\n>> Search by ID");
        Console.Write("Student ID : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine(">> Not a valid ID."); return; }

        var s = _studentService.FindStudent(id);
        if (s == null)
            Console.WriteLine(">> No student found.");
        else
            PrintRow(s);
    }

    private void RunValidation()
    {
        Console.WriteLine("\n>> Validate student");
        Console.Write("Student ID : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine(">> Not a valid ID."); return; }

        var issues = _studentService.Validate(id);
        if (issues.Count == 0)
            Console.WriteLine(">> Student data is valid.");
        else
        {
            Console.WriteLine(">> Validation issues found:");
            foreach (var issue in issues)
                Console.WriteLine($"   - {issue}");
        }
    }

    // ── Items 7–9 ─────────────────────────────────────────────

    private void ShowTopStudents()
    {
        Console.WriteLine("\n>> Top N students by average");
        Console.Write("How many (N): ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 1)
        { Console.WriteLine(">> Invalid number."); return; }

        Console.WriteLine("\n  [LINQ]");
        foreach (var s in _reportService.TopStudents(n))
            PrintRow(s);

        Console.WriteLine("\n  [No LINQ]");
        foreach (var s in _reportService.TopStudentsNoLinq(n))
            PrintRow(s);
    }

    private void ShowGroupStudents()
    {
        Console.WriteLine("\n>> Students by group (sorted by name)");
        Console.Write("Group code : ");
        string code = Console.ReadLine() ?? "";

        Console.WriteLine("\n  [LINQ]");
        var r1 = _reportService.ByGroupSorted(code);
        if (r1.Count == 0) Console.WriteLine("   (empty)");
        else foreach (var s in r1) PrintRow(s);

        Console.WriteLine("\n  [No LINQ]");
        var r2 = _reportService.ByGroupSortedNoLinq(code);
        if (r2.Count == 0) Console.WriteLine("   (empty)");
        else foreach (var s in r2) PrintRow(s);
    }

    private void ShowStats()
    {
        Console.WriteLine("\n>> Statistics");

        Console.WriteLine("  [LINQ]");
        PrintStats(_reportService.BuildStats());

        Console.WriteLine("\n  [No LINQ]");
        PrintStats(_reportService.BuildStatsNoLinq());
    }

    // ── Helpers ───────────────────────────────────────────────

    private void PrintRow(Student s)
    {
        double avg = _calc.GetAverage(s);
        Console.WriteLine($"   ID:{s.Id,-4} {s.FullName,-22} [{s.GroupCode}]  avg={avg:F2}  {s.EmailAddress}");
    }

    private void PrintStats(GroupStatistics st)
    {
        Console.WriteLine($"   Students     : {st.TotalStudents}");
        Console.WriteLine($"   Sum of avgs  : {st.SumOfAverages:F2}");
        Console.WriteLine($"   Overall avg  : {st.OverallAverage:F2}");
        Console.WriteLine($"   Highest avg  : {st.HighestAverage:F2}");
        Console.WriteLine($"   Someone > 5  : {st.SomeoneAbove5}");
        Console.WriteLine($"   All graded   : {st.EveryoneGraded}");
    }
}
