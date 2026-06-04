using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly AverageCalculator _calculator;
    private readonly StudentValidator _validator;

    public StudentService(
        IStudentRepository repository,
        AverageCalculator calculator,
        StudentValidator validator)
    {
        _repository = repository;
        _calculator = calculator;
        _validator = validator;
    }

    public IReadOnlyList<Student> GetAll() => _repository.GetAllStudents();

    public Student? GetById(int id) => _repository.GetById(id);

    public double GetAverage(Student student) => _calculator.Average(student);

    public bool GroupExists(string code) => _repository.GroupExists(code);

    public List<string> Validate(Student student) => _validator.Validate(student);

    // Returns null on success, or an error message describing why the add failed.
    public string? TryAddStudent(int id, string name, string email, string groupCode)
    {
        if (_repository.GetById(id) != null)
            return "ID exists.";

        if (string.IsNullOrWhiteSpace(name))
            return "Name required.";

        if (!email.Contains('@') || !email.Contains('.'))
            return "Bad email.";

        if (!_repository.GroupExists(groupCode))
            return "Group not found.";

        _repository.AddStudent(new Student
        {
            Id = id,
            Name = name,
            Email = email,
            GroupCode = groupCode,
            Grades = new List<int>()
        });

        return null;
    }

    // Returns null on success, or an error message.
    public string? TryAddGrade(int studentId, int grade)
    {
        var student = _repository.GetById(studentId);
        if (student == null)
            return "Not found.";

        if (grade < 1 || grade > 10)
            return "Out of range.";

        student.Grades.Add(grade);
        return null;
    }
}
