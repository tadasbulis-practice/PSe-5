using System;
using System.Collections.Generic;
using CW1.Interfaces;
using CW1.Models;

namespace CW1.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;

    // Constructor injection: StudentService depends on the IStudentRepository
    // abstraction, never on a concrete repository class.
    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public List<Student> GetAllStudents() => _repository.GetAllStudents();

    public List<Group> GetAllGroups() => _repository.GetAllGroups();

    public void AddStudent(Student student) => _repository.AddStudent(student);

    public Student? FindById(int id) => _repository.GetAllStudents().Find(x => x.Id == id);

    // Single place where the average formula lives (DRY). Implemented with a
    // plain loop (not LINQ's Average()) so it is also safe to call from the
    // *WithoutLinq report methods in ReportService.
    public double CalculateAverage(List<int> grades)
    {
        if (grades == null || grades.Count == 0) return 0.0;

        int sum = 0;
        foreach (var g in grades)
        {
            sum += g;
        }
        return (double)sum / grades.Count;
    }

    public bool ValidateStudent(Student s, out string error)
    {
        if (string.IsNullOrWhiteSpace(s.Name))
        {
            error = "Name cannot be empty.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(s.Email) || !s.Email.Contains("@"))
        {
            error = "Invalid email address.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(s.GroupCode))
        {
            error = "Group code cannot be empty.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    // Returns a formatted line for a student. No Console calls here —
    // Console.WriteLine/ReadLine are only allowed inside UI/.
    public string FormatStudent(Student s)
    {
        double avg = CalculateAverage(s.Grades);
        string gradesStr = s.Grades.Count > 0 ? string.Join(", ", s.Grades) : "no grades";
        return $"[{s.Id}] {s.Name} ({s.Email}) | Group: {s.GroupCode} | Grades: [{gradesStr}] | Avg: {avg:0.00}";
    }
}
