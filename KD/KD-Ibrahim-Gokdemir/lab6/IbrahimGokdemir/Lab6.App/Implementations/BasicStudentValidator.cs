using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Implementations;

public class BasicStudentValidator : IStudentValidator
{
    public List<Student> ValidateAll(List<Student> students)
    {
        return students.Where(s => s.Grades.Count > 0).ToList();
    }
}