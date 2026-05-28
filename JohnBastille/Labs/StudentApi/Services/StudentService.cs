using StudentApi.Interfaces;
using StudentApi.Models;

namespace StudentApi.Services;

public class StudentService
{
	private readonly IStudentRepository _repository;
	private readonly IStudentValidator _validator;
	private readonly IAverageStrategy _averageStrategy;
	private readonly IStudentPrinter _printer;

	public StudentService(
		IStudentRepository repository,
		IStudentValidator validator,
		IAverageStrategy averageStrategy,
		IStudentPrinter printer)
	{
		_repository = repository;
		_validator = validator;
		_averageStrategy = averageStrategy;
		_printer = printer;
	}

	public IEnumerable<(Student student, double average, string formatted)> GetAll()
	{
		foreach (var s in _repository.GetAll())
		{
			var avg = _averageStrategy.CalculateAverage(s.Grades);
			var formatted = _printer.Format(s, avg);
			yield return (s, avg, formatted);
		}
	}

	public (bool IsSuccess, Student? Student, string? Error) CreateStudent(string name, int age, List<int> grades)
	{
		if (!_validator.IsValid(name, age, grades, out var error))
			return (false, null, error);

		var student = new Student(name, age, grades);
		_repository.Add(student);
		return (true, student, null);
	}
}
