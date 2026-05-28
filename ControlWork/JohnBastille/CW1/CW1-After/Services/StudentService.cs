
using System.Collections.Generic;
using System.Linq;
using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;
public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    private readonly StudentValidator _validator;
    private readonly AverageCalculator _avgCalc;

    public StudentService(IStudentRepository repo, StudentValidator validator, AverageCalculator avgCalc)
    {
        _repo = repo;
        _validator = validator;
        _avgCalc = avgCalc;
    }

    public IEnumerable<Student> ListAll() => _repo.GetAll();

    public (bool Success, List<string> Errors) AddStudent(Student s)
    {
        if (s == null) return (false, new List<string> { "Student null" });
        var errors = _validator.Validate(s);

        // unique id check
        foreach (var ex in _repo.GetAll())
        {
            if (ex.Id == s.Id) { errors.Add("ID exists"); break; }
        }

        if (errors.Count > 0) return (false, errors);
        _repo.Add(s);
        return (true, new List<string>());
    }

    public bool AddGrade(int studentId, int grade)
    {
        if (grade < 1 || grade > 10) return false;
        var s = _repo.GetById(studentId);
        if (s == null) return false;
        _repo.AddGrade(studentId, grade);
        return true;
    }

    public Student? FindById(int id) => _repo.GetById(id);

    public double GetAverage(Student s) => _avgCalc.Calculate(s?.Grades ?? Enumerable.Empty<int>());

    // forward ReportService-like methods for convenience (delegate to ReportService if you prefer)
    public IEnumerable<(Student Student, double Avg)> Top3ByAverage_Linq()
    {
        return _repo.GetAll()
                    .Select(s => (Student: s, Avg: _avgCalc.Calculate(s.Grades)))
                    .OrderByDescending(x => x.Avg)
                    .Take(3);
    }

    



   
}