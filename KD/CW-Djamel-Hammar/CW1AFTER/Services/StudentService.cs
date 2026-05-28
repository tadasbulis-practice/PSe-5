using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly StudentValidator _validator;
    private readonly AverageCalculator _averageCalculator;

    public StudentService(
        IStudentRepository repository,
        StudentValidator validator,
        AverageCalculator averageCalculator)
    {
        _repository = repository;
        _validator = validator;
        _averageCalculator = averageCalculator;
    }

    public List<Student> GetAllStudents() => _repository.GetStudents();

    public Student? FindById(int id)
        => _repository.GetStudents().FirstOrDefault(s => s.Id == id);

    public (bool success, List<string> errors) AddStudent(
        string name, string email, string groupCode, List<int> grades)
    {
        var newId = _repository.GetStudents().Any()
            ? _repository.GetStudents().Max(s => s.Id) + 1
            : 1;

        var student = new Student
        {
            Id = newId,
            Name = name,
            Email = email,
            GroupCode = groupCode,
            Grades = grades
        };

        var errors = _validator.Validate(student, _repository.GetGroups());
        if (errors.Any())
            return (false, errors);

        _repository.Add(student);
        return (true, new List<string>());
    }

    public (bool success, string message) AddGrade(int studentId, int grade)
    {
        if (grade < 1 || grade > 10)
            return (false, "Grade must be between 1 and 10.");

        var student = FindById(studentId);
        if (student == null)
            return (false, "Student not found.");

        student.Grades.Add(grade);
        _repository.UpdateStudent(student);
        return (true, "Grade added.");
    }

    public double GetAverageForStudent(Student student)
        => _averageCalculator.Calculate(student);

    public List<string> ValidateStudent(Student student)
        => _validator.Validate(student, _repository.GetGroups());
}