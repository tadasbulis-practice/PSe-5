
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Services;

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

    public void AddStudent(Student student)
    {
        if (_validator.Validate(student))
        {
            _repository.Add(student);
        }
    }

    public void PrintAllStudents()
    {
        var students = _repository.GetAll();
        _printer.PrintStudents(students);
    }

    public double CalculateGroupAverage(IAverageStrategy strategy)
    {
        var students = _repository.GetAll();
        var valid = _validator.ValidateAll(students);
        return strategy.Calculate(valid);
    }


}