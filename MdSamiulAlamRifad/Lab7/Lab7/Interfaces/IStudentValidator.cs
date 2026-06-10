using Lab7.Models;

namespace Lab7.Interfaces;

// Task 4 — Validation for Collections
public interface IStudentValidator
{
    bool Validate(Student student);
    List<Student> ValidateAll(List<Student> students);
}
