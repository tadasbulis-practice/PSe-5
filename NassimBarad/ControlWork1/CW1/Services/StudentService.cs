using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;


/// Orchestrates student CRUD operations.
/// Depends on IStudentRepository via constructor injection — never on a concrete repository class.

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly StudentValidator   _validator;

    public StudentService(IStudentRepository repository, StudentValidator validator)
    {
        _repository = repository;
        _validator  = validator;
    }

    public IReadOnlyList<Student> GetAll()       => _repository.GetAllStudents();
    public IReadOnlyList<Group>   GetAllGroups() => _repository.GetAllGroups();
    public Student?               FindById(int id) => _repository.GetStudentById(id);

    public (bool Success, string Message) AddStudent(int id, string name, string email, string groupCode)
    {
        if (_repository.GetStudentById(id) != null)
            return (false, "ID exists.");

        var student = new Student { Id = id, Name = name, Email = email, GroupCode = groupCode };
        var errors  = _validator.Validate(student);

        if (errors.Count > 0)
            return (false, string.Join("; ", errors));

        _repository.AddStudent(student);
        return (true, "Student added.");
    }

    public (bool Success, string Message) AddGrade(int studentId, int grade)
    {
        var student = _repository.GetStudentById(studentId);
        if (student == null) return (false, "Not found.");
        if (grade < 1 || grade > 10) return (false, "Out of range.");

        _repository.AddGrade(studentId, grade);
        return (true, $"Added {grade} to {student.Name}.");
    }

    public (bool Success, string? Result, string? Error) GetAverage(int studentId)
    {
        var student = _repository.GetStudentById(studentId);
        if (student == null) return (false, null, "Not found.");
        double avg = AverageCalculator.Calculate(student);
        return (true, $"Average of {student.Name} = {avg:0.00}", null);
    }

    public (bool Success, string? Result, string? Error) ValidateStudent(int studentId)
    {
        var student = _repository.GetStudentById(studentId);
        if (student == null) return (false, null, "Not found.");
        var errors = _validator.Validate(student);
        return errors.Count == 0
            ? (true, $"{student.Name} — OK", null)
            : (true, $"{student.Name} — ERRORS: {string.Join("; ", errors)}", null);
    }
}
