
using Lab5.Models;

namespace Lab5.Interfaces;

public interface IStudentValidator
{
    bool Validate(Student student);
    List<Student> ValidateAll(List<Student> students);
}