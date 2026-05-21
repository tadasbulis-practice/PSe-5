using CW1After.Models;
using CW1After.Services;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService _service;
    private readonly ReportService _reports;

    public ConsoleMenu(StudentService service, ReportService reports)
    {
        _service = service;
        _reports = reports;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========== CW-1 Student Menu / Studentu meniu ==========");
            Console.WriteLine(" 1) List all students  /  Rodyti visus studentus");
            Console.WriteLine(" 2) Add new student    /  Prideti nauja studenta");
            Console.WriteLine(" 3) Add grade          /  Ivesti pazymi");
            Console.WriteLine(" 4) Show average       /  Rodyti vidurki");
            Console.WriteLine(" 5) Find by id         /  Rasti pagal ID");
            Console.WriteLine(" 6) Validate student   /  Validuoti studenta");
            Console.WriteLine(" 7) Top 3 by average   /  Top 3 pagal vidurki");
            Console.WriteLine(" 8) Students in group  /  Studentai grupeje");
            Console.WriteLine(" 9) Statistics         /  Statistika");
            Console.WriteLine(" 0) Exit               /  Iseiti");
            Console.Write("Choice / Pasirinkimas: ");

            var choice = Console.ReadLine();

            if (choice == "0") { Console.WriteLine("Bye!"); return; }

            switch (choice)
            {
                case "1": ListAll(); break;
                case "2": AddStudent(); break;
                case "3": AddGrade(); break;
                case "4": ShowAverage(); break;
                case "5": FindById(); break;
                case "6": ValidateStudent(); break;
                case "7": Top3(); break;
                case "8": StudentsInGroup(); break;
                case "9": Statistics(); break;
                default: Console.WriteLine("Unknown choice."); break;
            }
        }
    }

    private void ListAll()
    {
        foreach (var s in _service.GetAll())
        {
            double avg = _service.GetAverage(s);
            Console.WriteLine($"  [{s.Id}] {s.Name} ({s.GroupCode})  email={s.Email}  avg={avg:0.00}");
        }
    }

    private void AddStudent()
    {
        Console.Write("New ID / Naujas ID: ");
        if (!int.TryParse(Console.ReadLine(), out int newId)) { Console.WriteLine("Bad ID."); return; }
        Console.Write("Name / Vardas: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";
        Console.Write("Group code / Grupes kodas: ");
        var group = Console.ReadLine() ?? "";

        var error = _service.TryAddStudent(newId, name, email, group);
        Console.WriteLine(error ?? "Student added.");
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int gid)) { Console.WriteLine("Bad ID."); return; }
        Console.Write("Grade (1..10): ");
        if (!int.TryParse(Console.ReadLine(), out int grade)) { Console.WriteLine("Bad grade."); return; }

        var error = _service.TryAddGrade(gid, grade);
        if (error != null) { Console.WriteLine(error); return; }

        var student = _service.GetById(gid);
        Console.WriteLine($"Added {grade} to {student!.Name}.");
    }

    private void ShowAverage()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int aid)) { Console.WriteLine("Bad ID."); return; }
        var s = _service.GetById(aid);
        if (s == null) { Console.WriteLine("Not found."); return; }
        Console.WriteLine($"Average of {s.Name} = {_service.GetAverage(s):0.00}");
    }

    private void FindById()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int fid)) { Console.WriteLine("Bad ID."); return; }
        var s = _service.GetById(fid);
        if (s == null) { Console.WriteLine("Not found."); return; }
        Console.WriteLine($"  [{s.Id}] {s.Name} ({s.GroupCode})  email={s.Email}  avg={_service.GetAverage(s):0.00}");
        Console.WriteLine($"  Grades: [{string.Join(", ", s.Grades)}]");
    }

    private void ValidateStudent()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int vid)) { Console.WriteLine("Bad ID."); return; }
        var s = _service.GetById(vid);
        if (s == null) { Console.WriteLine("Not found."); return; }

        var errors = _service.Validate(s);
        if (errors.Count == 0) Console.WriteLine($"{s.Name} — OK");
        else Console.WriteLine($"{s.Name} — ERRORS: {string.Join("; ", errors)}");
    }

    private void Top3()
    {
        Console.WriteLine("--- Top 3 by average (LINQ) ---");
        foreach (var x in _reports.GetTopByAverage(3))
            Console.WriteLine($"  {x.Student.Name,-25} avg={x.Average:0.00}");

        Console.WriteLine("--- Top 3 by average (no LINQ) ---");
        foreach (var x in _reports.GetTopByAverageWithoutLinq(3))
            Console.WriteLine($"  {x.Student.Name,-25} avg={x.Average:0.00}");
    }

    private void StudentsInGroup()
    {
        Console.Write("Group code / Grupes kodas (e.g. PI23): ");
        var gc = Console.ReadLine() ?? "";

        Console.WriteLine($"--- Students in {gc}, sorted by name (LINQ) ---");
        PrintGroup(_reports.GetStudentsInGroupSortedByName(gc));

        Console.WriteLine($"--- Students in {gc}, sorted by name (no LINQ) ---");
        PrintGroup(_reports.GetStudentsInGroupSortedByNameWithoutLinq(gc));
    }

    private void PrintGroup(List<Student> students)
    {
        if (students.Count == 0) { Console.WriteLine("  (none)"); return; }
        foreach (var s in students)
            Console.WriteLine($"  [{s.Id}] {s.Name,-25} avg={_service.GetAverage(s):0.00}");
    }

    private void Statistics()
    {
        Console.WriteLine("--- Statistics (LINQ) ---");
        PrintStats(_reports.GetStatistics());

        Console.WriteLine("--- Statistics (no LINQ) ---");
        PrintStats(_reports.GetStatisticsWithoutLinq());
    }

    private void PrintStats(Statistics st)
    {
        Console.WriteLine($"  Total students   : {st.TotalStudents}");
        Console.WriteLine($"  Total grades     : {st.TotalGrades}");
        Console.WriteLine($"  Mean of averages : {st.MeanOfAverages:0.00}");
        Console.WriteLine($"  Max grade        : {st.MaxGrade}");
        Console.WriteLine($"  Any failing (<5)?: {st.HasFailing}");
        Console.WriteLine($"  All have email?  : {st.AllHaveEmail}");
    }
}