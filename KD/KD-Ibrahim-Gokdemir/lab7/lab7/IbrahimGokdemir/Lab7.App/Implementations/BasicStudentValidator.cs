using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations;

public class BasicStudentValidator : IStudentValidator
{
    public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students)
    {
        return students.Where(s => s.Grades.Count > 0).ToList();
    }
}
