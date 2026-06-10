using Lab8.Models;

namespace Lab8.Interfaces;

public interface IAverageStrategy
{
    double Calculate(IReadOnlyList<Student> students);
}
