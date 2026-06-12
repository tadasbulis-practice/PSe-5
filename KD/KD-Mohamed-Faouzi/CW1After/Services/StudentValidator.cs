using CW1After.Models;

namespace CW1After.Services;

public class StudentValidator
{
    public List<string> Validate(Student student, List<Group> groups)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(student.Name))
            errors.Add("Name empty");

        if (!student.Email.Contains('@') ||
            !student.Email.Contains('.'))
            errors.Add("Bad email");

        bool groupExists = groups.Any(g => g.Code == student.GroupCode);

        if (!groupExists)
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