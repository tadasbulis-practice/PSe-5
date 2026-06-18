using Lab6.App.Models;

namespace Lab6.App.Interfaces;

public interface IStudentValidator
{
    List<Student> ValidateAll(List<Student> students);
}