using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly AverageCalculator _averageCalculator;
    private readonly StudentValidator _validator;

    public StudentService(
        IStudentRepository repository,
        AverageCalculator averageCalculator,
        StudentValidator validator)
    {
        _repository = repository;
        _averageCalculator = averageCalculator;
        _validator = validator;
    }

    public List<Student> GetAllStudents()
    {
        return _repository.GetAllStudents();
    }

    public List<Group> GetAllGroups()
    {
        return _repository.GetAllGroups();
    }

    public Student? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void AddStudent(Student student)
    {
        _repository.Add(student);
    }

    public double CalculateAverage(Student student)
    {
        return _averageCalculator.Calculate(student.Grades);
    }

    public List<string> ValidateStudent(Student student)
    {
        return _validator.Validate(
            student,
            _repository.GetAllGroups());
    }
}