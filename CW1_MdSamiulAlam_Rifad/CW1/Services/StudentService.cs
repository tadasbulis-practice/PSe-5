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

    // Menu item 1
    public List<Student> GetAllStudents() => _repository.GetAllStudents();

    // Menu item 2
    public void AddStudent(Student student) => _repository.AddStudent(student);

    // Menu item 3
    public bool AddGrade(int studentId, int grade)
    {
        var student = _repository.GetStudentById(studentId);
        if (student == null) return false;
        _repository.AddGrade(studentId, grade);
        return true;
    }

    // Menu item 4
    public double GetAverage(int studentId)
    {
        var student = _repository.GetStudentById(studentId);
        if (student == null) return -1;
        return _averageCalculator.Calculate(student);
    }

    // Menu item 5
    public Student? FindById(int id) => _repository.GetStudentById(id);

    // Menu item 6
    public List<string> ValidateStudent(int studentId)
    {
        var student = _repository.GetStudentById(studentId);
        if (student == null) return new List<string> { "Student not found." };
        return _validator.Validate(student);
    }

    public List<Group> GetAllGroups() => _repository.GetAllGroups();

    public int GetNextId()
    {
        var students = _repository.GetAllStudents();
        int maxId = 0;
        foreach (var s in students)
            if (s.Id > maxId) maxId = s.Id;
        return maxId + 1;
    }
}
