using CW1After.Services;
using CW1After.Models;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;

    public ConsoleMenu(StudentService studentService, ReportService reportService)
    {
        _studentService = studentService;
        _reportService = reportService;
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
            Console.WriteLine(" 4) Show average of a student");
            Console.WriteLine(" 5) Find by id");
            Console.WriteLine(" 6) Validate student data");
            Console.WriteLine(" 7) Top 3 by average");
            Console.WriteLine(" 8) Students in group");
            Console.WriteLine(" 9) Statistics");
            Console.WriteLine(" 0) Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

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
                    ShowTop3();
                    break;
                case "8":
                    ListStudentsInGroup();
                    break;
                case "9":
                    ShowStatistics();
                    break;
                case "0":
                    Console.WriteLine("Bye!");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void ListAllStudents()
    {
        var students = _studentService.GetAllStudents();
        foreach (var s in students)
            Console.WriteLine($"[{s.Id}] {s.Name} ({s.GroupCode})");
    }

    private void AddNewStudent()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";
        Console.Write("Group code: ");
        string groupCode = Console.ReadLine() ?? "";
        Console.Write("Grades (comma separated, e.g. 8,9,10): ");
        string gradesInput = Console.ReadLine() ?? "";
        List<int> grades;
        try
        {
            grades = gradesInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse).ToList();
        }
        catch
        {
            Console.WriteLine("Invalid grade format.");
            return;
        }

        var (success, errors) = _studentService.AddStudent(name, email, groupCode, grades);
        if (success)
            Console.WriteLine("Student added successfully.");
        else
            errors.ForEach(e => Console.WriteLine($"Error: {e}"));
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }
        Console.Write("Grade (1-10): ");
        if (!int.TryParse(Console.ReadLine(), out int grade))
        {
            Console.WriteLine("Invalid grade.");
            return;
        }
        var (success, message) = _studentService.AddGrade(id, grade);
        Console.WriteLine(message);
    }

    private void ShowAverage()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }
        var student = _studentService.FindById(id);
        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }
        double avg = _studentService.GetAverageForStudent(student);
        Console.WriteLine($"Average grade: {avg:0.00}");
    }

    private void FindById()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }
        var student = _studentService.FindById(id);
        if (student == null)
            Console.WriteLine("Not found.");
        else
            Console.WriteLine($"[{student.Id}] {student.Name} | {student.Email} | {student.GroupCode} | Grades: {string.Join(",", student.Grades)}");
    }

    private void ValidateStudent()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";
        Console.Write("Group code: ");
        string groupCode = Console.ReadLine() ?? "";
        Console.Write("Grades (comma separated): ");
        string gradesInput = Console.ReadLine() ?? "";
        List<int> grades;
        try
        {
            grades = gradesInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse).ToList();
        }
        catch
        {
            Console.WriteLine("Invalid grade format.");
            return;
        }

        var dummyStudent = new Student { Name = name, Email = email, GroupCode = groupCode, Grades = grades };
        var errors = _studentService.ValidateStudent(dummyStudent);
        if (errors.Any())
            errors.ForEach(e => Console.WriteLine($"Error: {e}"));
        else
            Console.WriteLine("Student data is valid.");
    }

    private void ShowTop3()
    {
        var top3 = _reportService.GetTopByAverage(3);
        foreach (var s in top3)
        {
            var avg = _studentService.GetAverageForStudent(s);
            Console.WriteLine($"[{s.Id}] {s.Name} avg={avg:0.00}");
        }
    }

    private void ListStudentsInGroup()
    {
        Console.Write("Group code: ");
        string code = Console.ReadLine() ?? "";
        var students = _reportService.GetStudentsInGroupSortedByName(code);
        foreach (var s in students)
        {
            var avg = _studentService.GetAverageForStudent(s);
            Console.WriteLine($"[{s.Id}] {s.Name} avg={avg:0.00}");
        }
    }

    private void ShowStatistics()
    {
        var (count, sum, avg, max, any, all) = _reportService.GetStatistics();
        Console.WriteLine($"Count: {count}");
        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Avg: {avg:0.00}");
        Console.WriteLine($"Max: {max}");
        Console.WriteLine($"Any failing: {any}");
        Console.WriteLine($"All valid: {all}");
    }
}