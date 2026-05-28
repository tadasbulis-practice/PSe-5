using CW1After.Models;
using CW1After.Services;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;
    private readonly AverageCalculator _averageCalculator;
    private readonly string _repositoryInfo;

    public ConsoleMenu(StudentService studentService, ReportService reportService, AverageCalculator averageCalculator, string repositoryInfo)
    {
        _studentService = studentService;
        _reportService = reportService;
        _averageCalculator = averageCalculator;
        _repositoryInfo = repositoryInfo;
    }

    public void Run()
    {
        Console.WriteLine(_repositoryInfo);

        while (true)
        {
            PrintMenu();
            var choice = Console.ReadLine();

            if (choice == "0")
            {
                Console.WriteLine("Bye!");
                return;
            }

            switch (choice)
            {
                case "1":
                    ListAllStudents();
                    break;
                case "2":
                    AddNewStudent();
                    break;
                case "3":
                    AddGrade();
                    break;
                case "4":
                    ShowAverage();
                    break;
                case "5":
                    FindById();
                    break;
                case "6":
                    ValidateStudent();
                    break;
                case "7":
                    ShowTopByAverage();
                    break;
                case "8":
                    ShowStudentsInGroup();
                    break;
                case "9":
                    ShowStatistics();
                    break;
                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
    }

    private static void PrintMenu()
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
    }

    private void ListAllStudents()
    {
        foreach (var student in _studentService.GetAllStudents())
        {
            PrintStudentLine(student);
        }
    }

    private void AddNewStudent()
    {
        Console.Write("New ID / Naujas ID: ");
        if (!int.TryParse(Console.ReadLine(), out int newId))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        Console.Write("Name / Vardas: ");
        string newName = Console.ReadLine() ?? string.Empty;

        Console.Write("Email: ");
        string newEmail = Console.ReadLine() ?? string.Empty;

        Console.Write("Group code / Grupes kodas: ");
        string newGroup = Console.ReadLine() ?? string.Empty;

        try
        {
            var result = _studentService.AddStudent(newId, newName, newEmail, newGroup);
            Console.WriteLine(result.Message);
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        Console.Write("Grade (1..10): ");
        if (!int.TryParse(Console.ReadLine(), out int grade))
        {
            Console.WriteLine("Bad grade.");
            return;
        }

        try
        {
            var result = _studentService.AddGrade(studentId, grade);
            Console.WriteLine(result.Message);
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ShowAverage()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        var student = _studentService.FindById(id);
        if (student == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        double average = _averageCalculator.CalculateAverage(student.Grades);
        Console.WriteLine($"Average of {student.Name} = {average:0.00}");
    }

    private void FindById()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        var student = _studentService.FindById(id);
        if (student == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        PrintStudentLine(student);
        Console.WriteLine($"  Grades: [{string.Join(", ", student.Grades)}]");
    }

    private void ValidateStudent()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        var student = _studentService.FindById(id);
        if (student == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        var errors = _studentService.ValidateStudent(id);
        if (errors.Count == 0)
        {
            Console.WriteLine($"{student.Name} — OK");
        }
        else
        {
            Console.WriteLine($"{student.Name} — ERRORS: {string.Join("; ", errors)}");
        }
    }

    private void ShowTopByAverage()
    {
        Console.WriteLine("--- Top 3 by average (LINQ) ---");
        PrintAverageReports(_reportService.GetTopByAverage(3));

        Console.WriteLine("--- Top 3 by average (without LINQ) ---");
        PrintAverageReports(_reportService.GetTopByAverageWithoutLinq(3));
    }

    private void ShowStudentsInGroup()
    {
        Console.Write("Group code / Grupes kodas (pvz. PI23): ");
        string groupCode = Console.ReadLine() ?? string.Empty;

        Console.WriteLine($"--- Students in {groupCode}, sorted by name (LINQ) ---");
        PrintStudentsInGroup(_reportService.GetStudentsInGroupSortedByName(groupCode));

        Console.WriteLine($"--- Students in {groupCode}, sorted by name (without LINQ) ---");
        PrintStudentsInGroup(_reportService.GetStudentsInGroupSortedByNameWithoutLinq(groupCode));
    }

    private void ShowStatistics()
    {
        Console.WriteLine("--- Statistics (LINQ) ---");
        PrintStatistics(_reportService.GetStatistics());

        Console.WriteLine("--- Statistics (without LINQ) ---");
        PrintStatistics(_reportService.GetStatisticsWithoutLinq());
    }

    private void PrintStudentLine(Student student)
    {
        double average = _averageCalculator.CalculateAverage(student.Grades);
        Console.WriteLine($"  [{student.Id}] {student.Name} ({student.GroupCode})  email={student.Email}  avg={average:0.00}");
    }

    private static void PrintAverageReports(List<StudentAverageReport> reports)
    {
        foreach (var report in reports)
        {
            Console.WriteLine($"  {report.Student.Name,-25} avg={report.Average:0.00}");
        }
    }

    private void PrintStudentsInGroup(List<Student> students)
    {
        if (students.Count == 0)
        {
            Console.WriteLine("  (none)");
        }

        foreach (var student in students)
        {
            double average = _averageCalculator.CalculateAverage(student.Grades);
            Console.WriteLine($"  [{student.Id}] {student.Name,-25} avg={average:0.00}");
        }
    }

    private static void PrintStatistics(ReportStatistics statistics)
    {
        Console.WriteLine($"  Total students   : {statistics.TotalStudents}");
        Console.WriteLine($"  Total grades     : {statistics.TotalGrades}");
        Console.WriteLine($"  Mean of averages : {statistics.MeanOfAverages:0.00}");
        Console.WriteLine($"  Max grade        : {statistics.MaxGrade}");
        Console.WriteLine($"  Any failing (<5)? {statistics.HasFailing}");
        Console.WriteLine($"  All have email?  {statistics.AllHaveEmail}");
    }
}
