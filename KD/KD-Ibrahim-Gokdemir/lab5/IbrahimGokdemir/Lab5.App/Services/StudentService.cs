using System;

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

    public Group BuildValidatedGroup(string groupName)
    {
        var students = _repository.GetAll();
        var validStudents = new System.Collections.Generic.List<Student>();

        foreach (var student in students)
        {
            if (_validator.Validate(student))
            {
                validStudents.Add(student);
            }
        }

        return new Group(groupName, validStudents);
    }

    public void PrintStudents(Group group)
    {
        _printer.Print(group);
    }

    public void PrintAverages(Group group)
    {
        Console.WriteLine("Averages:");
        foreach (var student in group.Students)
        {
            Console.WriteLine($"{student.Name}: {_averageStrategy.Calculate(student):0.00}");
        }
    }
}