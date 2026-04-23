using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;

namespace JohnBastille.Lab_3.Services;

/// <summary>
/// Alternative menu service implementation that provides different user experience.
/// Demonstrates multiple implementations of the same interface.
/// </summary>
public class AlternativeMenuService : IMenuService
{
    private readonly List<Student> _students;
    private readonly IStudentFinder _finder;
    private readonly IStudentPrinter _printer;
    private readonly IStudentValidator _validator;
    private readonly IAverageStrategy _averageStrategy;

    public AlternativeMenuService(
        List<Student> students,
        IStudentFinder finder,
        IStudentPrinter printer,
        IStudentValidator validator,
        IAverageStrategy averageStrategy)
    {
        _students = students;
        _finder = finder;
        _printer = printer;
        _validator = validator;
        _averageStrategy = averageStrategy;
    }

    public void ShowMainMenu()
    {
        Console.WriteLine("=== STUDENT MANAGEMENT SYSTEM ===");
        Console.WriteLine("Available operations:");
        Console.WriteLine("1. ➕ Add New Student");
        Console.WriteLine("2. 🔍 Search for Student");
        Console.WriteLine("3. 📋 Display All Students");
        Console.WriteLine("5. 📊 Show Statistics");
        Console.WriteLine("0. 🚪 Exit Program");
        Console.WriteLine("================================");
    }

    public int GetMenuChoice()
    {
        Console.Write("Please select an option (0-5): ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 5)
        {
            return choice;
        }

        Console.WriteLine("❌ Invalid choice. Please enter a number between 0 and 5.");
        return -1;
    }

    public void ExecuteChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                AddStudent();
                break;
            case 2:
                SearchStudent();
                break;
            case 3:
                PrintStudents();
                break;
            case 5:
                ShowStatistics();
                break;
            case 0:
                Console.WriteLine("👋 Thank you for using the Student Management System!");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("❌ Invalid choice. Please try again.");
                break;
        }
    }

    private void AddStudent()
    {
        Console.WriteLine("\n=== ADD NEW STUDENT ===");

        Console.Write("👤 Student name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("🎂 Student age: ");
        string ageInput = Console.ReadLine() ?? "";

        if (!_validator.Validate(name, ageInput, out int age))
        {
            Console.WriteLine("❌ Invalid student data. Please check name and age.");
            return;
        }

        var student = new Student(_averageStrategy)
        {
            Name = name,
            Age = age
        };

        Console.Write("📚 Grades (space-separated): ");
        string gradeInput = Console.ReadLine() ?? "";
        var gradeStrings = gradeInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var g in gradeStrings)
        {
            if (int.TryParse(g, out int grade) && grade >= 0 && grade <= 100)
            {
                student.Grades.Add(grade);
            }
        }

        _students.Add(student);
        Console.WriteLine($"✅ Student '{name}' added successfully!");
    }

    private void SearchStudent()
    {
        Console.WriteLine("\n=== SEARCH STUDENT ===");

        if (_students.Count == 0)
        {
            Console.WriteLine("⚠️  No students in the system yet.");
            return;
        }

        Console.Write("🔍 Search query: ");
        string query = Console.ReadLine() ?? "";

        var student = _finder.Find(_students, query);

        if (student == null)
        {
            Console.WriteLine("❌ No student found matching the query.");
            return;
        }

        Console.WriteLine("📋 Search result:");
        _printer.Print(student);
    }

    private void PrintStudents()
    {
        Console.WriteLine("\n=== ALL STUDENTS ===");

        if (_students.Count == 0)
        {
            Console.WriteLine("⚠️  No students in the system yet.");
            return;
        }

        Console.WriteLine($"📊 Total students: {_students.Count}");
        Console.WriteLine("─".PadRight(50, '─'));

        foreach (var student in _students)
        {
            _printer.Print(student);
        }

        Console.WriteLine("─".PadRight(50, '─'));
    }

    private void ShowStatistics()
    {
        Console.WriteLine("\n=== SYSTEM STATISTICS ===");

        if (_students.Count == 0)
        {
            Console.WriteLine("⚠️  No students in the system yet.");
            return;
        }

        int totalGrades = _students.Sum(s => s.Grades.Count);
        double averageAge = _students.Average(s => s.Age);
        double highestAverage = _students.Max(s => s.GetAverage());
        double lowestAverage = _students.Min(s => s.GetAverage());

        Console.WriteLine($"📊 Total students: {_students.Count}");
        Console.WriteLine($"📚 Total grades recorded: {totalGrades}");
        Console.WriteLine($"🎂 Average age: {averageAge:F1} years");
        Console.WriteLine($"🏆 Highest average: {highestAverage:F2}");
        Console.WriteLine($"📉 Lowest average: {lowestAverage:F2}");

        var topStudent = _students.OrderByDescending(s => s.GetAverage()).First();
        Console.WriteLine($"👑 Top performer: {topStudent.Name} ({topStudent.GetAverage():F2})");
    }
}