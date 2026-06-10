using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Services;

// Task 5 — Service Logic with Containers
// All business logic lives here. Program.cs stays clean.
public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter _printer;
    private readonly IAverageStrategy _averageStrategy;
    private readonly IStudentValidator _validator;

    public StudentService(
        IStudentRepository repository,
        IStudentPrinter printer,
        IAverageStrategy averageStrategy,
        IStudentValidator validator)
    {
        _repository = repository;
        _printer = printer;
        _averageStrategy = averageStrategy;
        _validator = validator;
    }

    // Add student — validates first
    public void AddStudent(Student student)
    {
        if (_validator.Validate(student))
        {
            _repository.Add(student);
            Console.WriteLine($"[Service] Added: {student.Name}");
        }
        else
        {
            Console.WriteLine($"[Service] Rejected (invalid): {student.Name} | {student.Email}");
        }
    }

    // Get all students from repository
    public List<Student> GetAllStudents()
    {
        return _repository.GetAll();
    }

    // Find one student by id
    public Student? FindStudentById(int id)
    {
        return _repository.GetById(id);
    }

    // Remove student by id
    public bool RemoveStudent(int id)
    {
        var removed = _repository.Remove(id);
        Console.WriteLine(removed
            ? $"[Service] Removed student ID={id}"
            : $"[Service] Student ID={id} not found");
        return removed;
    }

    // Print all students using injected printer
    public void PrintAllStudents()
    {
        var students = _repository.GetAll();
        _printer.PrintStudents(students);
    }

    // Print only valid students
    public void PrintValidStudents()
    {
        var all = _repository.GetAll();
        var valid = _validator.ValidateAll(all);
        Console.WriteLine($"[Service] Valid: {valid.Count}/{all.Count} students");
        _printer.PrintStudents(valid);
    }

    // Calculate group average — only valid students counted
    public double CalculateGroupAverage()
    {
        var all = _repository.GetAll();
        var valid = _validator.ValidateAll(all);
        return _averageStrategy.Calculate(valid);
    }

    // Filter by minimum grade average
    public List<Student> FilterByMinAverage(double minAverage)
    {
        return _repository.GetAll()
            .Where(s => s.Grades.Count > 0 && s.GetAverage() >= minAverage)
            .ToList();
    }

    // Sort students by average grade descending
    public List<Student> GetStudentsSortedByAverage()
    {
        return _repository.GetAll()
            .OrderByDescending(s => s.GetAverage())
            .ToList();
    }
}
