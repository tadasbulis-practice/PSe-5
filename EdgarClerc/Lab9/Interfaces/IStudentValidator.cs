using Lab9.Models;

namespace Lab9.Interfaces;

public interface IStudentValidator
{
    bool Validate(Student student);
    IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students);
}
