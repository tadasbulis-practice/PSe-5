using CW1.Models;
using CW1.Services;

namespace CW1.UI;

/// <summary>
/// Handles all console input and output.
/// No business logic lives here — everything delegates to IStudentService.
/// </summary>
public class StudentUI
{
    private readonly IStudentService _svc;

    public StudentUI(IStudentService svc) => _svc = svc;

    public void Run()
    {
        while (true)
        {
            PrintMenu();
            var choice = (Console.ReadLine() ?? "").Trim();
            if (choice == "0") { Console.WriteLine("Goodbye!"); return; }

            switch (choice)
            {
                case "1":  ListAll();              break;
                case "2":  AddStudent();           break;
                case "3":  AddGrade();             break;
                case "4":  ShowAverage();          break;
                case "5":  FindById();             break;
                case "6":  ValidateStudent();      break;
                case "7":  Top3(linq: true);       break;
                case "7b": Top3(linq: false);      break;
                case "8":  InGroup(linq: true);    break;
                case "8b": InGroup(linq: false);   break;
                case "9":  Stats(linq: true);      break;
                case "9b": Stats(linq: false);     break;
                default:   Console.WriteLine("Unknown choice. Please try again."); break;
            }
        }
    }

    // -------------------------------------------------------------------------
    // Menu
    // -------------------------------------------------------------------------

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("========== CW-1 Student Management System ==========");
        Console.WriteLine("  1)  List all students");
        Console.WriteLine("  2)  Add a new student");
        Console.WriteLine("  3)  Add a grade");
        Console.WriteLine("  4)  Show student average");
        Console.WriteLine("  5)  Find student by ID");
        Console.WriteLine("  6)  Validate student");
        Console.WriteLine("  7)  Top 3 by average          [LINQ]");
        Console.WriteLine("  7b) Top 3 by average          [no-LINQ]");
        Console.WriteLine("  8)  Students in group         [LINQ]");
        Console.WriteLine("  8b) Students in group         [no-LINQ]");
        Console.WriteLine("  9)  Statistics                [LINQ]");
        Console.WriteLine("  9b) Statistics                [no-LINQ]");
        Console.WriteLine("  0)  Exit");
        Console.Write("Your choice: ");
    }

    // -------------------------------------------------------------------------
    // Menu handlers
    // -------------------------------------------------------------------------

    private void ListAll()
    {
        var students = _svc.GetAll();
        if (students.Count == 0) { Console.WriteLine("No students found."); return; }

        Console.WriteLine();
        foreach (var s in students)
        {
            double avg = _svc.GetAverage(s.Id);
            Console.WriteLine($"  [{s.Id}] {s.Name,-22} Group: {s.GroupCode}  Email: {s.Email,-22} Avg: {avg:0.00}");
        }
    }

    private void AddStudent()
    {
        Console.Write("New student ID: ");
        var idInput = Console.ReadLine() ?? "";
        if (!int.TryParse(idInput.Trim(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a number.");
            return;
        }

        Console.Write("Full name: ");
        var name = (Console.ReadLine() ?? "").Trim();

        Console.Write("Email address: ");
        var email = Console.ReadLine() ?? "";

        Console.Write("Group code (e.g. CS23): ");
        var group = Console.ReadLine() ?? "";

        var student = new Student { Id = id, Name = name, Email = email, GroupCode = group };

        if (_svc.Add(student, out var err))
            Console.WriteLine($"Student '{name}' added successfully.");
        else
            Console.WriteLine($"Could not add student: {err}");
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse((Console.ReadLine() ?? "").Trim(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a number.");
            return;
        }

        Console.Write("Grade (1-10): ");
        if (!int.TryParse((Console.ReadLine() ?? "").Trim(), out int grade))
        {
            Console.WriteLine("Invalid grade. Please enter a number.");
            return;
        }

        if (_svc.AddGrade(id, grade, out var err))
            Console.WriteLine($"Grade {grade} added successfully.");
        else
            Console.WriteLine($"Could not add grade: {err}");
    }

    private void ShowAverage()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse((Console.ReadLine() ?? "").Trim(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a number.");
            return;
        }

        var s = _svc.GetById(id);
        if (s == null) { Console.WriteLine($"No student found with ID {id}."); return; }

        Console.WriteLine($"Average grade for {s.Name}: {_svc.GetAverage(id):0.00}");
    }

    private void FindById()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse((Console.ReadLine() ?? "").Trim(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a number.");
            return;
        }

        var s = _svc.GetById(id);
        if (s == null) { Console.WriteLine($"No student found with ID {id}."); return; }

        Console.WriteLine($"  [{s.Id}] {s.Name} | Group: {s.GroupCode} | Email: {s.Email} | Avg: {_svc.GetAverage(id):0.00}");
        Console.WriteLine($"  Grades: [{string.Join(", ", s.Grades)}]");
    }

    private void ValidateStudent()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse((Console.ReadLine() ?? "").Trim(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a number.");
            return;
        }

        var s = _svc.GetById(id);
        if (s == null) { Console.WriteLine($"No student found with ID {id}."); return; }

        var errors = _svc.Validate(s);
        if (errors.Count == 0)
            Console.WriteLine($"{s.Name} — Valid (no errors)");
        else
            Console.WriteLine($"{s.Name} — Invalid: {string.Join("; ", errors)}");
    }

    private void Top3(bool linq)
    {
        var version = linq ? "LINQ" : "no-LINQ";
        Console.WriteLine($"\n--- Top 3 students by average ({version}) ---");

        var list = linq ? _svc.Top3ByAverage_Linq() : _svc.Top3ByAverage_Plain();
        for (int i = 0; i < list.Count; i++)
        {
            var s = list[i];
            Console.WriteLine($"  {i + 1}. {s.Name,-25} Avg: {_svc.GetAverage(s.Id):0.00}");
        }
    }

    private void InGroup(bool linq)
    {
        Console.Write("Enter group code (e.g. CS23): ");
        var gc = (Console.ReadLine() ?? "").Trim();

        var version = linq ? "LINQ" : "no-LINQ";
        Console.WriteLine($"\n--- Students in group '{gc}', sorted by name ({version}) ---");

        var list = linq ? _svc.StudentsInGroup_Linq(gc) : _svc.StudentsInGroup_Plain(gc);

        if (list.Count == 0) { Console.WriteLine("  No students found in this group."); return; }

        foreach (var s in list)
            Console.WriteLine($"  [{s.Id}] {s.Name,-25} Avg: {_svc.GetAverage(s.Id):0.00}");
    }

    private void Stats(bool linq)
    {
        var version = linq ? "LINQ" : "no-LINQ";
        Console.WriteLine($"\n--- Statistics ({version}) ---");

        var r = linq ? _svc.Statistics_Linq() : _svc.Statistics_Plain();
        Console.WriteLine($"  Total students       : {r.Total}");
        Console.WriteLine($"  Total grades entered : {r.TotalGrades}");
        Console.WriteLine($"  Mean of averages     : {r.MeanOfMeans:0.00}");
        Console.WriteLine($"  Highest grade        : {r.MaxGrade}");
        Console.WriteLine($"  Any failing (< 5)?   : {r.HasFailing}");
        Console.WriteLine($"  All students have email? : {r.AllHaveEmail}");
    }
}
