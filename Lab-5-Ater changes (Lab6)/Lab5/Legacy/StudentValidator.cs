
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Legacy;

public class StudentValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        return !string.IsNullOrWhiteSpace(student.Name)
               && student.Grade >= 0
               && student.Grade <= 10;
    }

    public List<Student> ValidateAll(List<Student> students)
    {
        return students.Where(Validate).ToList();
    }
}