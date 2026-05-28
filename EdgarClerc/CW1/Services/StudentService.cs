using CW1.Interface;
using CW1.Models;

namespace CW1.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly AverageStategy _averageStategy;

    public StudentService(IStudentRepository repository, AverageStategy averageStategy)
    {
        _repository = repository;
        _averageStategy = averageStategy;
    }

    public IReadOnlyList<Student> getAll()
    {
        return _repository.FindAll();
    }

    public void Add(Student student)
    {
        _repository.Add(student);
    }

    public void Remove(Student student)
    {
        _repository.Remove(student);
    }

    public Student GetById(int id)
    {
        return _repository.FindById(id);
    }

    public double GetAverage(Student student)
    {
        return _averageStategy.GetAverage(student);
    }

    public List<(Student student, double avg)> GetTop3Avg()
    {
        return _repository
            .FindAll()
            .Select(s => (student: s, avg: _averageStategy.GetAverage(s)))
            .OrderByDescending(x => x.avg)
            .Take(3)
            .ToList();
    }
}
