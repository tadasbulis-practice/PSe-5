using Lab8.Models;

namespace Lab8.Interfaces;

public interface IStudentValidator
{
    bool Validate(Student student);
    IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students);
}
