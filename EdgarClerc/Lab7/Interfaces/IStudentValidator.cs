using Lab7.Models;

namespace Lab7.Interfaces;

public interface IStudentValidator
{
    bool                   Validate(Student student);
    IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students);
}
