using Lab6.Models;

namespace Lab6.Interfaces;

public interface IStudentPrinter
{
    void Print(IReadOnlyList<Student> students);
}
