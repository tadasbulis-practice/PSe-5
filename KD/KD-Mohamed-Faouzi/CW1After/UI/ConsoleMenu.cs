using CW1After.Models;
using CW1After.Services;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;

    public ConsoleMenu(
        StudentService studentService,
        ReportService reportService)
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
            Console.WriteLine("1) List all students");
            Console.WriteLine("2) Add new student");
            Console.WriteLine("3) Add grade");
            Console.WriteLine("4) Show average");
            Console.WriteLine("5) Find by id");
            Console.WriteLine("6) Validate student");
            Console.WriteLine("7) Top 3 by average");
            Console.WriteLine("8) Students in group");
            Console.WriteLine("9) Statistics");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            if (choice == "0")
            {
                Console.WriteLine("Bye!");
                return;
            }

            switch (choice)
            {
                case "1":
                    ShowAllStudents();
                    break;

                case "2":
                    AddStudent();
                    break;

                case "3":
                    AddGrade();
                    break;

                case "4":
                    ShowAverage();
                    break;

                case "5":
                    FindStudent();
                    break;

                case "6":
                    ValidateStudent();
                    break;
                
                case "7":
                    ShowTopStudents();
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

    private void ShowAllStudents()
    {
        var students = _studentService.GetAllStudents();

        foreach (var student in students)
        {
            double avg = _studentService.CalculateAverage(student);

            Console.WriteLine(
                $"[{student.Id}] {student.Name} " +
                $"({student.GroupCode}) " +
                $"email={student.Email} " +
                $"avg={avg:0.00}");
        }
    }

    private void AddStudent()
    {
        Console.Write("New ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        if (_studentService.GetById(id) != null)
        {
            Console.WriteLine("ID exists.");
            return;
        }

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Group code: ");
        string group = Console.ReadLine() ?? "";

        var student = new Student
        {
            Id = id,
            Name = name,
            Email = email,
            GroupCode = group,
            Grades = new List<int>()
        };

        var errors =
            _studentService.ValidateStudent(student);

        if (errors.Count > 0)
        {
            Console.WriteLine(string.Join(", ", errors));
            return;
        }

        _studentService.AddStudent(student);
        Console.WriteLine("Student added.");
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        var student = _studentService.GetById(id);

        if (student == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        Console.Write("Grade (1..10): ");

        if (!int.TryParse(Console.ReadLine(), out int grade))
        {
            Console.WriteLine("Bad grade.");
            return;
        }

        if (grade < 1 || grade > 10)
        {
            Console.WriteLine("Out of range.");
            return;
        }

        student.Grades.Add(grade);

        Console.WriteLine(
            $"Added {grade} to {student.Name}");
    }

    private void ShowAverage()
    {
        Console.Write("Student ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        var student = _studentService.GetById(id);

        if (student == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        double avg =
            _studentService.CalculateAverage(student);

        Console.WriteLine(
            $"Average of {student.Name} = {avg:0.00}");
    }

    private void FindStudent()
    {
        Console.Write("Student ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        var student = _studentService.GetById(id);

        if (student == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        double avg =
            _studentService.CalculateAverage(student);

        Console.WriteLine(
            $"[{student.Id}] {student.Name} " +
            $"({student.GroupCode}) " +
            $"email={student.Email} " +
            $"avg={avg:0.00}");

        Console.WriteLine(
            $"Grades: [{string.Join(", ", student.Grades)}]");
    }

    private void ValidateStudent()
    {
        Console.Write("Student ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID.");
            return;
        }

        var student = _studentService.GetById(id);

        if (student == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        var errors =
            _studentService.ValidateStudent(student);

        if (errors.Count == 0)
        {
            Console.WriteLine($"{student.Name} — OK");
        }
        else
        {
            Console.WriteLine(
                $"{student.Name} — ERRORS: " +
                $"{string.Join("; ", errors)}");
        }
    }

    private void ShowTopStudents()
    {
        Console.WriteLine("--- Top 3 by average (LINQ) ---");

        var top =
            _reportService
                .GetTopByAverage(3);

        foreach (var student in top)
        {
            double avg =
                _studentService
                    .CalculateAverage(student);

            Console.WriteLine(
                $"{student.Name} avg={avg:0.00}");
        }

        Console.WriteLine();
        Console.WriteLine(
            "--- Top 3 by average (Without LINQ) ---");

        var topNoLinq =
            _reportService
                .GetTopByAverageWithoutLinq(3);

        foreach (var student in topNoLinq)
        {
            double avg =
                _studentService
                    .CalculateAverage(student);

            Console.WriteLine(
                $"{student.Name} avg={avg:0.00}");
        }
    }

    private void ShowStudentsInGroup()
    {
        Console.Write("Group code: ");
        string code =
            Console.ReadLine() ?? "";

        Console.WriteLine(
            "--- Students in group (LINQ) ---");

        var students =
            _reportService
                .GetStudentsInGroupSortedByName(code);

        foreach (var student in students)
        {
            Console.WriteLine(student.Name);
        }

        Console.WriteLine();
        Console.WriteLine(
            "--- Students in group (Without LINQ) ---");

        var studentsNoLinq =
            _reportService
                .GetStudentsInGroupSortedByNameWithoutLinq(code);

        foreach (var student in studentsNoLinq)
        {
            Console.WriteLine(student.Name);
        }
    }

    private void ShowStatistics()
    {
        Console.WriteLine(
            "--- Statistics (LINQ) ---");

        Console.WriteLine(
            _reportService
                .GetStatistics());

        Console.WriteLine();

        Console.WriteLine(
            "--- Statistics (Without LINQ) ---");

        Console.WriteLine(
            _reportService
                .GetStatisticsWithoutLinq());
    }
}