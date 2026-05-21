using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly StudentValidator _validator;

    public StudentService(IStudentRepository repository, StudentValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public List<Student> GetAllStudents() => _repository.GetStudents();

    public Student? FindById(int id) => _repository.GetStudentById(id);

    public (bool Success, string Message) AddStudent(int id, string name, string email, string groupCode)
    {
        if (_repository.GetStudentById(id) != null)
        {
            return (false, "ID exists.");
        }

        var student = new Student
        {
            Id = id,
            Name = name,
            Email = email,
            GroupCode = groupCode,
            Grades = new List<int>()
        };

        var errors = _validator.Validate(student);
        if (errors.Count > 0)
        {
            return (false, string.Join("; ", errors));
        }

        _repository.AddStudent(student);
        return (true, "Student added.");
    }

    public (bool Success, string Message) AddGrade(int studentId, int grade)
    {
        var student = _repository.GetStudentById(studentId);
        if (student == null)
        {
            return (false, "Not found.");
        }

        if (grade < 1 || grade > 10)
        {
            return (false, "Out of range.");
        }

        _repository.AddGrade(studentId, grade);
        return (true, $"Added {grade} to {student.Name}.");
    }

    public List<string> ValidateStudent(int id)
    {
        var student = _repository.GetStudentById(id);
        if (student == null)
        {
            return new List<string> { "Not found." };
        }

        return _validator.Validate(student);
    }
}
