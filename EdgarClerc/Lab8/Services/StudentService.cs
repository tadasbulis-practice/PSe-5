using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Services;

/// <summary>
/// Business logic layer. Uses IStudentRepository, knows nothing about:
/// - How data is stored (List? Dictionary? Database?)
/// - Where data comes from (memory? API? file?)
/// - What the internal data structure looks like
///
/// This is OCP in action: data source changed (Lab6 → Lab8),
/// this class did NOT change.
/// </summary>
public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter _printer;
    private readonly IAverageStrategy _strategy;
    private readonly IStudentValidator _validator;

    public StudentService(
        IStudentRepository repository,
        IStudentPrinter printer,
        IAverageStrategy strategy,
        IStudentValidator validator
    )
    {
        _repository = repository;
        _printer = printer;
        _strategy = strategy;
        _validator = validator;
    }

    // ── Student operations ─────────────────────────────────────────
    public void AddStudent(Student student)
    {
        if (_validator.Validate(student))
            _repository.Add(student);
        else
            Console.WriteLine($"  Validation failed for {student.FullName}.");
    }

    public IReadOnlyList<Student> GetAllStudents() => _repository.GetAll();

    public Student? FindStudentById(int id) => _repository.GetById(id);

    public bool RemoveStudent(int id) => _repository.Remove(id);

    public void PrintAllStudents() => _printer.PrintStudents(_repository.GetAll());

    public double CalculateGroupAverage()
    {
        var validated = _validator.ValidateAll(_repository.GetAll());
        return _strategy.Calculate(validated);
    }

    // ── Group operations ───────────────────────────────────────────
    public IReadOnlyList<Group> GetAllGroups() => _repository.GetAllGroups();

    public void PrintGroup(string groupCode)
    {
        var group = _repository.GetGroupByCode(groupCode);
        if (group is null)
            Console.WriteLine($"  Group '{groupCode}' not found.");
        else
            _printer.PrintGroup(group);
    }

    // ── Faculty operations ─────────────────────────────────────────
    public Faculty GetFaculty() => _repository.GetFaculty();

    public void PrintFaculty() => _printer.PrintFaculty(_repository.GetFaculty());
}
