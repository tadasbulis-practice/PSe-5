public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter _printer;
    private readonly IStudentValidator _validator;
    private readonly IAverageStrategy _averageStrategy;

    public StudentService(
        IStudentRepository repository,
        IStudentPrinter printer,
        IStudentValidator validator,
        IAverageStrategy averageStrategy)
    {
        _repository = repository;
        _printer = printer;
        _validator = validator;
        _averageStrategy = averageStrategy;
    }

    public void ProcessStudents()
    {
        var students = _repository.GetAll();

        var validStudents = students
            .Where(s => _validator.Validate(s))
            .ToList();

        _printer.Print(validStudents);

        Console.WriteLine();
        Console.WriteLine("Averages:");

        foreach (var student in validStudents)
        {
            double average = _averageStrategy.Calculate(student.Grades);
            Console.WriteLine($"{student.Name}: {average:F2}");
        }
    }
}