using CW1.Models;
using CW1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CW1.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;
    private readonly StudentValidator _validator;
    private readonly AverageCalculator _calculator;
    private readonly QueryMode _mode;


    public ConsoleMenu(
        StudentService studentService,
        ReportService reportService,
        StudentValidator validator,
        AverageCalculator calculator,
        QueryMode mode)
    {
        _studentService = studentService;
        _reportService = reportService;
        _validator = validator;
        _calculator = calculator;
        _mode = mode;
    }

    public void Run()
    {
        while (true)
        {
            PrintMenu();

            Console.Write("Choice: ");
            string? choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ListStudents();
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
                    ShowTop3();
                    break;

                case "8":
                    ShowGroup();
                    break;

                case "9":
                    ShowStats();
                    break;

                case "0":
                    Console.WriteLine("Bye");
                    return;

                default:
                    Console.WriteLine("Unknown option");
                    break;
            }
        }
    }

    private void PrintMenu()
    {

        Console.WriteLine("\n");
        Console.WriteLine("===== CW1 MENU =====");
        Console.WriteLine("1 - List students");
        Console.WriteLine("2 - Add student");
        Console.WriteLine("3 - Add grade");
        Console.WriteLine("4 - Show average");
        Console.WriteLine("5 - Find by ID");
        Console.WriteLine("6 - Validate");
        Console.WriteLine("7 - Top 3");
        Console.WriteLine("8 - Group");
        Console.WriteLine("9 - Statistics");
        Console.WriteLine("0 - Exit");
    }

    private void ListStudents()
    {
        foreach (var s in _studentService.GetAllStudents())
        {
            Console.WriteLine(
                $"[{s.Id}] {s.Name} ({s.GroupCode}) avg={_calculator.Calculate(s):0.00}");
        }
    }

    private void AddStudent()
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID");
            return;
        }

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Group: ");
        string group = Console.ReadLine() ?? "";

        var student = new Student(id, name, email, group);

        var errors = _validator.Validate(student, _studentService.GetAllGroups());

        if (errors.Count > 0)
        {
            Console.WriteLine(string.Join("; ", errors));
            return;
        }

        _studentService.AddStudent(student);
        Console.WriteLine("Added");
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID");
            return;
        }

        var student = _studentService.FindById(id);

        if (student == null)
        {
            Console.WriteLine("Not found");
            return;
        }

        Console.Write("Grade: ");
        if (!int.TryParse(Console.ReadLine(), out int grade))
        {
            Console.WriteLine("Bad grade");
            return;
        }

        if (grade < 1 || grade > 10)
        {
            Console.WriteLine("Out of range");
            return;
        }

        student.AddGrade(grade);

        Console.WriteLine("Added grade");
    }

    private void ShowAverage()
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID");
            return;
        }

        var student = _studentService.FindById(id);

        if (student == null)
        {
            Console.WriteLine("Not found");
            return;
        }

        Console.WriteLine(
            $"Average: {_calculator.Calculate(student):0.00}");
    }

    private void FindStudent()
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID");
            return;
        }

        var s = _studentService.FindById(id);

        if (s == null)
        {
            Console.WriteLine("Not found");
            return;
        }

        Console.WriteLine($"[{s.Id}] {s.Name} {s.Email}");
        Console.WriteLine($"Grades: {string.Join(", ", s.Grades)}");
    }

    private void ValidateStudent()
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Bad ID");
            return;
        }

        var s = _studentService.FindById(id);

        if (s == null)
        {
            Console.WriteLine("Not found");
            return;
        }

        var errors = _validator.Validate(s, _studentService.GetAllGroups());

        Console.WriteLine(errors.Count == 0
            ? "OK"
            : string.Join("; ", errors));
    }


    private void ShowTop3()
    {
        var top = _mode == QueryMode.Linq
            ? _reportService.GetTopByAverage(3)
            : _reportService.GetTopByAverageWithoutLinq(3);

        Console.WriteLine($"=== {_mode} ===");

        foreach (var s in top)
        {
            Console.WriteLine($"{s.Name} avg={_calculator.Calculate(s):0.00}");
        }
    }

    private void ShowGroup()
    {
        Console.Write("Group: ");
        string code = Console.ReadLine() ?? "";

        var students = _mode == QueryMode.Linq
            ? _reportService.GetStudentsInGroupSortedByName(code)
            : _reportService.GetStudentsInGroupSortedByNameWithoutLinq(code);

        Console.WriteLine($"=== {_mode} ===");

        foreach (var s in students)
        {
            Console.WriteLine($"{s.Name} avg={_calculator.Calculate(s):0.00}");
        }
    }

    private void ShowStats()
    {
        var stats = _mode == QueryMode.Linq
            ? _reportService.GetStatistics()
            : _reportService.GetStatisticsWithoutLinq();

        Console.WriteLine($"=== {_mode} ===");

        Console.WriteLine($"Students: {stats.TotalStudents}");
        Console.WriteLine($"Grades: {stats.TotalGrades}");
        Console.WriteLine($"Avg: {stats.MeanAverage:0.00}");
        Console.WriteLine($"Max: {stats.MaxGrade}");
        Console.WriteLine($"Failing: {stats.HasFailing}");
        Console.WriteLine($"Emails OK: {stats.AllHaveEmail}");
    }
}