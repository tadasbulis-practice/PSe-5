using JohnBastille.Lab_4.Interfaces;
using JohnBastille.Lab_4.Models;

namespace Lab_4.Services;

/// <summary>
/// Web simulation implementation of IMenuService.
/// Simulates a web-based user interface in the console.
/// </summary>
public class WebMenuSimulationService : IMenuService
{
    private readonly List<Student> _students;
    private readonly IStudentFinder _finder;
    private readonly IStudentPrinter _printer;
    private readonly IStudentValidator _validator;
    private readonly IAverageStrategy _averageStrategy;

    public WebMenuSimulationService(
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
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    🎓 STUDENT MANAGEMENT SYSTEM               ║");
        Console.WriteLine("║                           WEB SIMULATION                      ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════ ╣");
        Console.WriteLine("║                                                               ║");
        Console.WriteLine("║  [1] ➕ ADD STUDENT         [2] 🔍 SEARCH STUDENT            ║");
        Console.WriteLine("║                                                               ║");
        Console.WriteLine("║  [3] 📋 LIST STUDENTS       [4] 📊 SYSTEM STATISTICS         ║");
        Console.WriteLine("║                                                               ║");
        Console.WriteLine("║  [0] 🚪 LOGOUT                                                ║");
        Console.WriteLine("║                                                               ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine($"📈 Current Students: {_students.Count} | 🏫 System Status: Online");
    }

    public int GetMenuChoice()
    {
        Console.Write("👆 Select option (0-4): ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 4)
        {
            return choice;
        }

        Console.WriteLine("❌ Invalid option. Please enter 0-4.");
        Thread.Sleep(1500); // Simulate web delay
        return -1;
    }

    public void ExecuteChoice(int choice)
    {
        Console.WriteLine("\n⏳ Processing request...");
        Thread.Sleep(500); // Simulate web processing delay

        switch (choice)
        {
            case 1:
                AddStudent();
                break;
            case 2:
                SearchStudent();
                break;
            case 3:
                ListStudents();
                break;
            case 4:
                ShowStatistics();
                break;
            case 0:
                Logout();
                break;
            default:
                Console.WriteLine("❌ Invalid option selected.");
                break;
        }

        if (choice != 0)
        {
            Console.WriteLine("\n🔄 Press any key to return to main menu...");
            Console.ReadKey();
        }
    }

    private void AddStudent()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        ➕ ADD NEW STUDENT                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        // Name input
        Console.Write("👤 Student Name: ");
        string name = Console.ReadLine() ?? "";

        // Age input
        Console.Write("🎂 Age: ");
        string ageInput = Console.ReadLine() ?? "";

        Console.WriteLine("\n⏳ Validating input...");
        Thread.Sleep(800);

        if (!_validator.Validate(name, ageInput, out int age))
        {
            Console.WriteLine("❌ Validation failed! Please check your input.");
            Console.WriteLine("   - Name cannot be empty");
            Console.WriteLine("   - Age must be a valid number between 1-120");
            return;
        }

        Console.WriteLine("✅ Validation passed!");

        var student = new Student(_averageStrategy)
        {
            Name = name,
            Age = age
        };

        // Grades input
        Console.Write("📚 Grades (space-separated): ");
        string gradeInput = Console.ReadLine() ?? "";
        var gradeStrings = gradeInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine("\n⏳ Processing grades...");
        Thread.Sleep(600);

        int validGrades = 0;
        foreach (var g in gradeStrings)
        {
            if (int.TryParse(g, out int grade) && grade >= 0 && grade <= 100)
            {
                student.Grades.Add(grade);
                validGrades++;
            }
        }

        _students.Add(student);

        Console.WriteLine("✅ Student added successfully!");
        Console.WriteLine($"📋 Name: {name}");
        Console.WriteLine($"🎂 Age: {age}");
        Console.WriteLine($"📚 Grades added: {validGrades}");
        Console.WriteLine($"📊 Current average: {student.GetAverage():F2}");
    }

    private void SearchStudent()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                       🔍 SEARCH STUDENT                      ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        if (_students.Count == 0)
        {
            Console.WriteLine("⚠️  No students found in the system.");
            return;
        }

        Console.Write("🔍 Search query: ");
        string query = Console.ReadLine() ?? "";

        Console.WriteLine("\n⏳ Searching database...");
        Thread.Sleep(1000);

        var student = _finder.Find(_students, query);

        if (student == null)
        {
            Console.WriteLine("❌ No student found matching your search.");
            return;
        }

        Console.WriteLine("✅ Student found!");
        Console.WriteLine("📋 Student Details:");
        Console.WriteLine("─".PadRight(50, '─'));
        _printer.Print(student);
    }

    private void ListStudents()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                      📋 STUDENT DIRECTORY                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        if (_students.Count == 0)
        {
            Console.WriteLine("📭 No students enrolled yet.");
            return;
        }

        Console.WriteLine($"📊 Total Students: {_students.Count}");
        Console.WriteLine("─".PadRight(60, '─'));

        for (int i = 0; i < _students.Count; i++)
        {
            var student = _students[i];
            Console.Write($"[{i + 1:D2}] ");
            _printer.Print(student);
            Thread.Sleep(200); // Simulate loading delay between students
        }

        Console.WriteLine("─".PadRight(60, '─'));
    }

    private void ShowStatistics()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     📊 SYSTEM STATISTICS                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        if (_students.Count == 0)
        {
            Console.WriteLine("📭 No data available for statistics.");
            return;
        }

        Console.WriteLine("⏳ Calculating statistics...");
        Thread.Sleep(1200);

        int totalGrades = _students.Sum(s => s.Grades.Count);
        double averageAge = _students.Average(s => s.Age);
        double highestAverage = _students.Max(s => s.GetAverage());
        double lowestAverage = _students.Min(s => s.GetAverage());
        double overallAverage = _students.Average(s => s.GetAverage());

        Console.WriteLine("📈 System Statistics:");
        Console.WriteLine($"   👥 Total Students: {_students.Count}");
        Console.WriteLine($"   📚 Total Grades Recorded: {totalGrades}");
        Console.WriteLine($"   🎂 Average Age: {averageAge:F1} years");
        Console.WriteLine($"   📊 Overall Average Grade: {overallAverage:F2}");
        Console.WriteLine($"   🏆 Highest Average: {highestAverage:F2}");
        Console.WriteLine($"   📉 Lowest Average: {lowestAverage:F2}");

        var topStudent = _students.OrderByDescending(s => s.GetAverage()).First();
        Console.WriteLine($"   👑 Top Performer: {topStudent.Name} ({topStudent.GetAverage():F2})");
    }

    private void Logout()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        🚪 LOGGING OUT...                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("👋 Thank you for using the Student Management System!");
        Console.WriteLine("🔒 Session terminated successfully.");

        Thread.Sleep(2000);
        Environment.Exit(0);
    }
}