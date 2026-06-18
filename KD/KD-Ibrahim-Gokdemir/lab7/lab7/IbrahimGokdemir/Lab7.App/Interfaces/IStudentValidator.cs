using Lab7.App.Models;

namespace Lab7.App.Interfaces;

public interface IStudentValidator
{
    IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students);
}
