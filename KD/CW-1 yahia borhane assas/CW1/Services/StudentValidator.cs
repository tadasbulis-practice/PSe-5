using CW1After.Models;

namespace CW1After.Services;

public class StudentValidator
{
    public void Validate(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name))
        {
            throw new Exception("Student name is required.");
        }

        if (string.IsNullOrWhiteSpace(student.Email))
        {
            throw new Exception("Student email is required.");
        }

        if (student.Grades.Count == 0)
        {
            throw new Exception("Student must have grades.");
        }
    }
}