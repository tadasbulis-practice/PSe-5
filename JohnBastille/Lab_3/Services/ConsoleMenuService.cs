using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;

public class ConsoleMenuService : IMenuService
{
    private readonly List<Student> _students;
    private readonly IStudentFinder _finder;
    private readonly IStudentPrinter _printer;
    private readonly IStudentValidator _validator;
    private readonly IAverageStrategy _averageStrategy;

    public ConsoleMenuService(
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
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. Search Student");
        Console.WriteLine("3. Print Students");
        Console.WriteLine("4. Exit");
    }

    public int GetMenuChoice()
    {
        Console.Write("Enter choice: ");
        return int.TryParse(Console.ReadLine(), out int choice) ? choice : -1;
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

            case 4:
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }

    private void AddStudent()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Enter age: ");
        string ageInput = Console.ReadLine();

        if (!_validator.Validate(name, ageInput, out int age))
        {
            Console.WriteLine("Invalid student data.");
            return;
        }

        var student = new Student(_averageStrategy)
        {
            Name = name,
            Age = age
        };

        Console.Write("Enter grades separated by spaces: ");
        string gradeInput = Console.ReadLine();
        var gradeStrings = gradeInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var g in gradeStrings)
        {
            if (int.TryParse(g, out int grade))
                student.Grades.Add(grade);
        }

        _students.Add(student);
        Console.WriteLine("Student added.");
    }

    private void SearchStudent()
    {
        Console.Write("Enter search query: ");
        string query = Console.ReadLine();

        var student = _finder.Find(_students, query);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        _printer.Print(student);
    }

    private void PrintStudents()
    {
        if (_students.Count == 0)
        {
            Console.WriteLine("No students available.");
            return;
        }

        foreach (var student in _students)
        {
            _printer.Print(student);
        }
    }
}