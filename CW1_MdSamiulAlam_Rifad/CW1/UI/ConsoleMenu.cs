using CW1After.Models;
using CW1After.Services;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;
    private readonly AverageCalculator _averageCalculator;

    public ConsoleMenu(
        StudentService studentService,
        ReportService reportService,
        AverageCalculator averageCalculator)
    {
        _studentService = studentService;
        _reportService = reportService;
        _averageCalculator = averageCalculator;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            PrintMenu();
            string? input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1": MenuListAllStudents();   break;
                case "2": MenuAddStudent();        break;
                case "3": MenuAddGrade();          break;
                case "4": MenuShowAverage();       break;
                case "5": MenuFindById();          break;
                case "6": MenuValidateStudent();   break;
                case "7": MenuTopByAverage();      break;
                case "8": MenuStudentsInGroup();   break;
                case "9": MenuStatistics();        break;
                case "0": running = false;         break;
                default:  Console.WriteLine("Unknown option. Try again."); break;
            }

            if (running) Console.WriteLine();
        }

        Console.WriteLine("Goodbye!");
    }

    // ─────────────────────────────────────────────
    //  Menu
    // ─────────────────────────────────────────────

    private void PrintMenu()
    {
        Console.WriteLine("========= STUDENT MENU =========");
        Console.WriteLine("1. List all students");
        Console.WriteLine("2. Add new student");
        Console.WriteLine("3. Add grade");
        Console.WriteLine("4. Show average");
        Console.WriteLine("5. Find by ID");
        Console.WriteLine("6. Validate student");
        Console.WriteLine("7. Top N students by average");
        Console.WriteLine("8. Students in group sorted by name");
        Console.WriteLine("9. Statistics");
        Console.WriteLine("0. Exit");
        Console.Write("Choose: ");
    }

    // ─────────────────────────────────────────────
    //  Items 1–6
    // ─────────────────────────────────────────────

    private void MenuListAllStudents()
    {
        var students = _studentService.GetAllStudents();
        Console.WriteLine($"\n--- All Students ({students.Count}) ---");
        foreach (var s in students)
            PrintStudent(s);
    }

    private void MenuAddStudent()
    {
        Console.WriteLine("\n--- Add New Student ---");
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.WriteLine("Available groups:");
        foreach (var g in _studentService.GetAllGroups())
            Console.WriteLine($"  {g.Code} — {g.Name}");

        Console.Write("Group code: ");
        string groupCode = Console.ReadLine() ?? "";

        var student = new Student
        {
            Id        = _studentService.GetNextId(),
            Name      = name,
            Email     = email,
            GroupCode = groupCode
        };

        _studentService.AddStudent(student);
        Console.WriteLine($"Student added with Id={student.Id}.");
    }

    private void MenuAddGrade()
    {
        Console.WriteLine("\n--- Add Grade ---");
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine("Invalid ID."); return; }

        Console.Write("Grade (1–10): ");
        if (!int.TryParse(Console.ReadLine(), out int grade) || grade < 1 || grade > 10)
        { Console.WriteLine("Invalid grade."); return; }

        bool ok = _studentService.AddGrade(id, grade);
        Console.WriteLine(ok ? "Grade added." : "Student not found.");
    }

    private void MenuShowAverage()
    {
        Console.WriteLine("\n--- Show Average ---");
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine("Invalid ID."); return; }

        double avg = _studentService.GetAverage(id);
        if (avg < 0)
            Console.WriteLine("Student not found.");
        else
            Console.WriteLine($"Average: {avg:F2}");
    }

    private void MenuFindById()
    {
        Console.WriteLine("\n--- Find by ID ---");
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine("Invalid ID."); return; }

        var student = _studentService.FindById(id);
        if (student == null)
            Console.WriteLine("Not found.");
        else
            PrintStudent(student);
    }

    private void MenuValidateStudent()
    {
        Console.WriteLine("\n--- Validate Student ---");
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        { Console.WriteLine("Invalid ID."); return; }

        var errors = _studentService.ValidateStudent(id);
        if (errors.Count == 0)
            Console.WriteLine("Student is valid.");
        else
            foreach (var e in errors)
                Console.WriteLine($"  ERROR: {e}");
    }

    // ─────────────────────────────────────────────
    //  Items 7–9  (LINQ + no-LINQ, shown side-by-side)
    // ─────────────────────────────────────────────

    private void MenuTopByAverage()
    {
        Console.WriteLine("\n--- Top N Students by Average ---");
        Console.Write("N: ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 1)
        { Console.WriteLine("Invalid number."); return; }

        Console.WriteLine("\n[LINQ version]");
        foreach (var s in _reportService.GetTopByAverage(n))
            PrintStudent(s);

        Console.WriteLine("\n[No-LINQ version]");
        foreach (var s in _reportService.GetTopByAverageWithoutLinq(n))
            PrintStudent(s);
    }

    private void MenuStudentsInGroup()
    {
        Console.WriteLine("\n--- Students in Group (sorted by name) ---");
        Console.Write("Group code: ");
        string code = Console.ReadLine() ?? "";

        Console.WriteLine("\n[LINQ version]");
        var linqResult = _reportService.GetStudentsInGroupSortedByName(code);
        if (linqResult.Count == 0) Console.WriteLine("  (none)");
        else foreach (var s in linqResult) PrintStudent(s);

        Console.WriteLine("\n[No-LINQ version]");
        var plainResult = _reportService.GetStudentsInGroupSortedByNameWithoutLinq(code);
        if (plainResult.Count == 0) Console.WriteLine("  (none)");
        else foreach (var s in plainResult) PrintStudent(s);
    }

    private void MenuStatistics()
    {
        Console.WriteLine("\n--- Statistics ---");

        Console.WriteLine("[LINQ version]");
        PrintStatistics(_reportService.GetStatistics());

        Console.WriteLine("\n[No-LINQ version]");
        PrintStatistics(_reportService.GetStatisticsWithoutLinq());
    }

    // ─────────────────────────────────────────────
    //  Helpers  (ONLY place Console.Write is used)
    // ─────────────────────────────────────────────

    private void PrintStudent(Student s)
    {
        double avg = _averageCalculator.Calculate(s);
        Console.WriteLine($"  [{s.Id}] {s.Name} ({s.GroupCode}) avg={avg:F2}  email={s.Email}");
    }

    private void PrintStatistics(Statistics st)
    {
        Console.WriteLine($"  Count       : {st.Count}");
        Console.WriteLine($"  Sum of avgs : {st.Sum:F2}");
        Console.WriteLine($"  Avg of avgs : {st.Average:F2}");
        Console.WriteLine($"  Max avg     : {st.Max:F2}");
        Console.WriteLine($"  Any above 5 : {st.AnyAbove5}");
        Console.WriteLine($"  All graded  : {st.AllHaveGrades}");
    }
}
