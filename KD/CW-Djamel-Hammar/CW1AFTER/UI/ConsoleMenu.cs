
using CW1After.Services;
using CW1After.Models;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;

    private readonly bool _linqOnly;
    private readonly bool _noLinqOnly;

    public ConsoleMenu(
        StudentService studentService,
        ReportService reportService,
        bool linqOnly,
        bool noLinqOnly)
    {
        _studentService = studentService;
        _reportService = reportService;

        _linqOnly = linqOnly;
        _noLinqOnly = noLinqOnly;
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
        {
            Console.WriteLine($"[{s.Id}] {s.Name} ({s.GroupCode})");
        }
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
            grades = gradesInput
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
        catch
        {
            Console.WriteLine("Invalid grade format.");
            return;
        }

        var (success, errors) =
            _studentService.AddStudent(name, email, groupCode, grades);

        if (success)
        {
            Console.WriteLine("Student added successfully.");
        }
        else
        {
            errors.ForEach(e => Console.WriteLine($"Error: {e}"));
        }
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

        var (success, message) =
            _studentService.AddGrade(id, grade);

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

        double avg =
            _studentService.GetAverageForStudent(student);

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
        {
            Console.WriteLine("Not found.");
        }
        else
        {
            Console.WriteLine(
                $"[{student.Id}] {student.Name} | {student.Email} | {student.GroupCode} | Grades: {string.Join(",", student.Grades)}"
            );
        }
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
            grades = gradesInput
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
        catch
        {
            Console.WriteLine("Invalid grade format.");
            return;
        }

        var dummyStudent = new Student
        {
            Name = name,
            Email = email,
            GroupCode = groupCode,
            Grades = grades
        };

        var errors =
            _studentService.ValidateStudent(dummyStudent);

        if (errors.Any())
        {
            errors.ForEach(e => Console.WriteLine($"Error: {e}"));
        }
        else
        {
            Console.WriteLine("Student data is valid.");
        }
    }

    private void ShowTop3()
    {
        if (!_noLinqOnly)
        {
            Console.WriteLine("=== LINQ VERSION ===");

            var top3Linq =
                _reportService.GetTopByAverage(3);

            foreach (var s in top3Linq)
            {
                var avg =
                    _studentService.GetAverageForStudent(s);

                Console.WriteLine(
                    $"[{s.Id}] {s.Name} avg={avg:0.00}"
                );
            }

            Console.WriteLine();
        }

        if (!_linqOnly)
        {
            Console.WriteLine("=== WITHOUT LINQ VERSION ===");

            var top3WithoutLinq =
                _reportService.GetTopByAverageWithoutLinq(3);

            foreach (var s in top3WithoutLinq)
            {
                var avg =
                    _studentService.GetAverageForStudent(s);

                Console.WriteLine(
                    $"[{s.Id}] {s.Name} avg={avg:0.00}"
                );
            }
        }
    }

    private void ListStudentsInGroup()
    {
        Console.Write("Group code: ");
        string code = Console.ReadLine() ?? "";

        Console.WriteLine();

        if (!_noLinqOnly)
        {
            Console.WriteLine("=== LINQ VERSION ===");

            var studentsLinq =
                _reportService.GetStudentsInGroupSortedByName(code);

            foreach (var s in studentsLinq)
            {
                var avg =
                    _studentService.GetAverageForStudent(s);

                Console.WriteLine(
                    $"[{s.Id}] {s.Name} avg={avg:0.00}"
                );
            }

            Console.WriteLine();
        }

        if (!_linqOnly)
        {
            Console.WriteLine("=== WITHOUT LINQ VERSION ===");

            var studentsWithoutLinq =
                _reportService.GetStudentsInGroupSortedByNameWithoutLinq(code);

            foreach (var s in studentsWithoutLinq)
            {
                var avg =
                    _studentService.GetAverageForStudent(s);

                Console.WriteLine(
                    $"[{s.Id}] {s.Name} avg={avg:0.00}"
                );
            }
        }
    }

    private void ShowStatistics()
    {
        if (!_noLinqOnly)
        {
            Console.WriteLine("=== LINQ VERSION ===");

            var (count, sum, avg, max, any, all) =
                _reportService.GetStatistics();

            Console.WriteLine($"Count: {count}");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Avg: {avg:0.00}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Any failing: {any}");
            Console.WriteLine($"All valid: {all}");

            Console.WriteLine();
        }

        if (!_linqOnly)
        {
            Console.WriteLine("=== WITHOUT LINQ VERSION ===");

            var (count2, sum2, avg2, max2, any2, all2) =
                _reportService.GetStatisticsWithoutLinq();

            Console.WriteLine($"Count: {count2}");
            Console.WriteLine($"Sum: {sum2}");
            Console.WriteLine($"Avg: {avg2:0.00}");
            Console.WriteLine($"Max: {max2}");
            Console.WriteLine($"Any failing: {any2}");
            Console.WriteLine($"All valid: {all2}");
        }
    }
}

