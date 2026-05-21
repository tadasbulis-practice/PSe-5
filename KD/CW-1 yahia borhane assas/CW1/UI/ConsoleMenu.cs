using CW1After.Models;
using CW1After.Services;

namespace CW1After.UI;

public class ConsoleMenu
{
    private readonly StudentService studentService;

    private readonly AverageCalculator calculator;

    private readonly ReportService reportService;

    public ConsoleMenu(
        StudentService studentService,
        AverageCalculator calculator,
        ReportService reportService)
    {
        this.studentService = studentService;
        this.calculator = calculator;
        this.reportService = reportService;
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("      STUDENT MANAGEMENT SYSTEM");
            Console.WriteLine("======================================");
            Console.WriteLine();

            Console.WriteLine("1 - Show all students");
            Console.WriteLine("2 - Show top 3 students (LINQ)");
            Console.WriteLine("3 - Show top 3 students (NO LINQ)");
            Console.WriteLine("4 - Show PI23 students (LINQ)");
            Console.WriteLine("5 - Show PI23 students (NO LINQ)");
            Console.WriteLine("6 - Show statistics (LINQ)");
            Console.WriteLine("7 - Show statistics (NO LINQ)");
            Console.WriteLine("8 - Add new student");
            Console.WriteLine("9 - Show groups");
            Console.WriteLine("0 - Exit");

            Console.WriteLine();
            Console.Write("Choose option: ");

            string? choice = Console.ReadLine();

            List<Student> students =
                studentService.GetAllStudents();

            Console.WriteLine();

            switch (choice)
            {
                case "1":

                    ShowAllStudents(students);

                    break;

                case "2":

                    ShowTopStudentsLinq(students);

                    break;

                case "3":

                    ShowTopStudentsNoLinq(students);

                    break;

                case "4":

                    ShowPI23StudentsLinq(students);

                    break;

                case "5":

                    ShowPI23StudentsNoLinq(students);

                    break;

                case "6":

                    ShowStatisticsLinq(students);

                    break;

                case "7":

                    ShowStatisticsNoLinq(students);

                    break;

                case "8":

                    AddStudent();

                    break;

                case "9":

                    ShowGroups();

                    break;

                case "0":

                    Console.WriteLine("Goodbye!");

                    return;

                default:

                    Console.WriteLine("Invalid option.");

                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }

    private void ShowAllStudents(List<Student> students)
    {
        Console.WriteLine("===== ALL STUDENTS =====");
        Console.WriteLine();

        foreach (Student student in students)
        {
            double avg =
                calculator.Calculate(student.Grades);

            Console.WriteLine(
                $"[{student.Id}] {student.Name}");

            Console.WriteLine(
                $"Email: {student.Email}");

            Console.WriteLine(
                $"Group: {student.GroupCode}");

            Console.WriteLine(
                $"Average: {avg:F2}");

            Console.WriteLine("----------------------------------");
        }
    }

    private void ShowTopStudentsLinq(List<Student> students)
    {
        Console.WriteLine("===== TOP STUDENTS (LINQ) =====");
        Console.WriteLine();

        List<Student> top =
            reportService.GetTopByAverage(
                students,
                3);

        foreach (Student student in top)
        {
            double avg =
                calculator.Calculate(student.Grades);

            Console.WriteLine(
                $"{student.Name} | Avg: {avg:F2}");
        }
    }

    private void ShowTopStudentsNoLinq(List<Student> students)
    {
        Console.WriteLine("===== TOP STUDENTS (NO LINQ) =====");
        Console.WriteLine();

        List<Student> top =
            reportService.GetTopByAverageWithoutLinq(
                students,
                3);

        foreach (Student student in top)
        {
            double avg =
                calculator.Calculate(student.Grades);

            Console.WriteLine(
                $"{student.Name} | Avg: {avg:F2}");
        }
    }

    private void ShowPI23StudentsLinq(List<Student> students)
    {
        Console.WriteLine("===== PI23 STUDENTS (LINQ) =====");
        Console.WriteLine();

        List<Student> result =
            reportService.GetStudentsInGroupSortedByName(
                students,
                "PI23");

        foreach (Student student in result)
        {
            Console.WriteLine(student.Name);
        }
    }

    private void ShowPI23StudentsNoLinq(List<Student> students)
    {
        Console.WriteLine("===== PI23 STUDENTS (NO LINQ) =====");
        Console.WriteLine();

        List<Student> result =
            reportService
            .GetStudentsInGroupSortedByNameWithoutLinq(
                students,
                "PI23");

        foreach (Student student in result)
        {
            Console.WriteLine(student.Name);
        }
    }

    private void ShowStatisticsLinq(List<Student> students)
    {
        Console.WriteLine("===== STATISTICS (LINQ) =====");
        Console.WriteLine();

        reportService.GetStatistics(students);
    }

    private void ShowStatisticsNoLinq(List<Student> students)
    {
        Console.WriteLine("===== STATISTICS (NO LINQ) =====");
        Console.WriteLine();

        reportService.GetStatisticsWithoutLinq(students);
    }

    private void AddStudent()
    {
        Console.WriteLine("===== ADD NEW STUDENT =====");
        Console.WriteLine();

        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.Write("Name: ");
        string name = Console.ReadLine()!;

        Console.Write("Email: ");
        string email = Console.ReadLine()!;

        Console.Write("Group Code: ");
        string groupCode = Console.ReadLine()!;

        Console.Write("Grades separated by comma: ");

        string gradesInput = Console.ReadLine()!;

        List<int> grades = gradesInput
            .Split(',')
            .Select(g => int.Parse(g.Trim()))
            .ToList();

        Student student = new Student
        {
            Id = id,
            Name = name,
            Email = email,
            GroupCode = groupCode,
            Grades = grades
        };

        studentService.AddStudent(student);

        Console.WriteLine();
        Console.WriteLine("Student added successfully!");
    }

    private void ShowGroups()
    {
        Console.WriteLine("===== GROUPS =====");
        Console.WriteLine();

        List<Group> groups =
            studentService.GetAllGroups();

        foreach (Group group in groups)
        {
            Console.WriteLine(
                $"{group.Code} - {group.Name}");
        }
    }
}