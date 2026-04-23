using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;

namespace JohnBastille.Lab_3.Services;

/// <summary>
/// Debug implementation of IMenuService for testing and debugging.
/// Provides detailed logging and debugging information.
/// </summary>
public class DebugMenuService : IMenuService
{
    private readonly List<Student> _students;
    private readonly IStudentFinder _finder;
    private readonly IStudentPrinter _printer;
    private readonly IStudentValidator _validator;
    private readonly IAverageStrategy _averageStrategy;

    public DebugMenuService(
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
        Console.WriteLine("[DEBUG] === DEBUG MENU SERVICE ===");
        Console.WriteLine($"[DEBUG] Current student count: {_students.Count}");
        Console.WriteLine($"[DEBUG] Finder type: {_finder.GetType().Name}");
        Console.WriteLine($"[DEBUG] Printer type: {_printer.GetType().Name}");
        Console.WriteLine($"[DEBUG] Validator type: {_validator.GetType().Name}");
        Console.WriteLine($"[DEBUG] Average strategy type: {_averageStrategy.GetType().Name}");
        Console.WriteLine();

        Console.WriteLine("=== DEBUG MENU ===");
        Console.WriteLine("1. [ADD] Add Student (with validation debug)");
        Console.WriteLine("2. [FIND] Search Student (with search debug)");
        Console.WriteLine("3. [LIST] Print All Students (with print debug)");
        Console.WriteLine("4. [DEBUG] Show System Debug Info");
        Console.WriteLine("5. [EXIT] Exit Program");
        Console.WriteLine("==================");
    }

    public int GetMenuChoice()
    {
        Console.Write("[DEBUG] Enter choice: ");
        string input = Console.ReadLine() ?? "";
        Console.WriteLine($"[DEBUG] User input received: '{input}'");

        if (int.TryParse(input, out int choice))
        {
            Console.WriteLine($"[DEBUG] Parsed choice: {choice}");
            return choice;
        }

        Console.WriteLine("[DEBUG] Invalid input - returning -1");
        return -1;
    }

    public void ExecuteChoice(int choice)
    {
        Console.WriteLine($"[DEBUG] Executing choice: {choice}");

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
            case 4:
                ShowDebugInfo();
                break;
            case 5:
                Console.WriteLine("[DEBUG] Exit requested");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("[DEBUG] Invalid choice");
                break;
        }
    }

    private void AddStudent()
    {
        Console.WriteLine("[DEBUG] === ADD STUDENT DEBUG ===");

        Console.Write("[DEBUG] Name prompt> ");
        string name = Console.ReadLine() ?? "";
        Console.WriteLine($"[DEBUG] Name input: '{name}'");

        Console.Write("[DEBUG] Age prompt> ");
        string ageInput = Console.ReadLine() ?? "";
        Console.WriteLine($"[DEBUG] Age input: '{ageInput}'");

        Console.WriteLine($"[DEBUG] Calling validator.Validate('{name}', '{ageInput}', out age)");
        bool isValid = _validator.Validate(name, ageInput, out int age);
        Console.WriteLine($"[DEBUG] Validation result: {isValid}, parsed age: {age}");

        if (!isValid)
        {
            Console.WriteLine("[DEBUG] Validation failed - aborting student creation");
            return;
        }

        Console.WriteLine($"[DEBUG] Creating student with average strategy: {_averageStrategy.GetType().Name}");
        var student = new Student(_averageStrategy)
        {
            Name = name,
            Age = age
        };

        Console.Write("[DEBUG] Grades prompt> ");
        string gradeInput = Console.ReadLine() ?? "";
        Console.WriteLine($"[DEBUG] Grade input: '{gradeInput}'");

        var gradeStrings = gradeInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"[DEBUG] Parsed {gradeStrings.Length} grade tokens");

        foreach (var g in gradeStrings)
        {
            if (int.TryParse(g, out int grade))
            {
                student.Grades.Add(grade);
                Console.WriteLine($"[DEBUG] Added grade: {grade}");
            }
            else
            {
                Console.WriteLine($"[DEBUG] Skipped invalid grade: '{g}'");
            }
        }

        _students.Add(student);
        Console.WriteLine($"[DEBUG] Student added. Total students: {_students.Count}");
    }

    private void SearchStudent()
    {
        Console.WriteLine("[DEBUG] === SEARCH STUDENT DEBUG ===");

        if (_students.Count == 0)
        {
            Console.WriteLine("[DEBUG] No students in system");
            return;
        }

        Console.Write("[DEBUG] Search query prompt> ");
        string query = Console.ReadLine() ?? "";
        Console.WriteLine($"[DEBUG] Search query: '{query}'");

        Console.WriteLine($"[DEBUG] Calling finder.Find() with {_students.Count} students");
        var student = _finder.Find(_students, query);

        if (student == null)
        {
            Console.WriteLine("[DEBUG] Finder returned null - no match found");
            return;
        }

        Console.WriteLine($"[DEBUG] Finder returned student: {student.Name}");
        Console.WriteLine($"[DEBUG] Calling printer.Print() on found student");
        _printer.Print(student);
    }

    private void PrintStudents()
    {
        Console.WriteLine("[DEBUG] === PRINT STUDENTS DEBUG ===");

        if (_students.Count == 0)
        {
            Console.WriteLine("[DEBUG] No students to print");
            return;
        }

        Console.WriteLine($"[DEBUG] Printing {_students.Count} students");
        foreach (var student in _students)
        {
            Console.WriteLine($"[DEBUG] Printing student: {student.Name}");
            _printer.Print(student);
        }
    }

    private void ShowDebugInfo()
    {
        Console.WriteLine("[DEBUG] === SYSTEM DEBUG INFO ===");
        Console.WriteLine($"[DEBUG] Total students: {_students.Count}");

        if (_students.Count > 0)
        {
            Console.WriteLine("[DEBUG] Student details:");
            for (int i = 0; i < _students.Count; i++)
            {
                var student = _students[i];
                Console.WriteLine($"[DEBUG]   [{i}] {student.Name}, Age: {student.Age}, Grades: [{string.Join(", ", student.Grades)}], Average: {student.GetAverage():F2}");
            }
        }

        Console.WriteLine($"[DEBUG] Finder implementation: {_finder.GetType().Name}");
        Console.WriteLine($"[DEBUG] Printer implementation: {_printer.GetType().Name}");
        Console.WriteLine($"[DEBUG] Validator implementation: {_validator.GetType().Name}");
        Console.WriteLine($"[DEBUG] Average strategy implementation: {_averageStrategy.GetType().Name}");
        Console.WriteLine("[DEBUG] ============================");
    }
}