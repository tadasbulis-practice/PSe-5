using Lab9.Models;

namespace Lab9.Interfaces;

public interface IAverageStrategy
{
    double Calculate(IReadOnlyList<Student> students);
}
