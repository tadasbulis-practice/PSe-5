using Lab6.Models;

namespace Lab6.Interfaces;

public interface IStudentValidator
{
    bool Validate(Student student);
    IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students);
}
