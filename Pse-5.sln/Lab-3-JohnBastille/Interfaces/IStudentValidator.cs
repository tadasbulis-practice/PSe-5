using Lab-3-JohnBastille.Models;

namespace Lab-3-JohnBastille.Interfaces;

public interface IStudentValidator
{
    bool IsValid(Student student, out string? errorMessage);
}


