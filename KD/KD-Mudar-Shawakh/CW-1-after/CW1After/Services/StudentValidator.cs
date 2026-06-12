using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentValidator
{
    private readonly IStudentRepository _repository;

    public StudentValidator(IStudentRepository repository)
    {
        _repository = repository;
    }

    public List<string> Validate(Student student)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(student.Name))
            errors.Add("Name empty");

        if (!student.Email.Contains('@') || !student.Email.Contains('.'))
            errors.Add("Bad email");

        if (!_repository.GroupExists(student.GroupCode))
            errors.Add("Unknown group");

        foreach (var grade in student.Grades)
        {
            if (grade < 1 || grade > 10)
            {
                errors.Add($"Grade {grade} out of range");
                break;
            }
        }

        return errors;
    }
}