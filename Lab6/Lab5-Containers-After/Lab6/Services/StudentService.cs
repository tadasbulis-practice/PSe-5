using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Services;

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
        IStudentValidator validator)
    {
        _repository = repository;
        _printer = printer;
        _strategy = strategy;
        _validator = validator;
    }

    public void AddStudent(Student student)
    {
        if (_validator.Validate(student))
        {
            _repository.Add(student);
        }
    }

    public IReadOnlyList<Student> GetAllStudents()
    {
        return _repository.GetAll();
    }

    public Student? FindStudentById(int id)
    {
        return _repository.FindById(id);
    }

    public bool RemoveStudent(int id)
    {
        return _repository.Remove(id);
    }

    public void PrintAllStudents()
    {
        var students = _repository.GetAll();
        _printer.PrintStudents(students);
    }

    public double CalculateGroupAverage()
    {
        var validStudents = _validator.ValidateAll(_repository.GetAll());
        return _strategy.Calculate(validStudents);
    }
}
