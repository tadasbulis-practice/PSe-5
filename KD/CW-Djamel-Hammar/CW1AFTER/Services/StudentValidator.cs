using CW1After.Models;

namespace CW1After.Services;
// class for controling data 
public class StudentValidator
{
    public List<string> Validate(Student student, List<Group> groups)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(student.Name))
            errors.Add("Name is empty");

        if (!student.Email.Contains('@') || !student.Email.Contains('.'))
            errors.Add("Email is not well-formed");

        if (groups.All(g => g.Code != student.GroupCode))
            errors.Add("Unknown group code");

        foreach (var grade in student.Grades)
        {
            if (grade < 1 || grade > 10)
            {
                errors.Add($"Grade {grade} out of range (1-10)");
                break;
            }
        }

        return errors;
    }
}