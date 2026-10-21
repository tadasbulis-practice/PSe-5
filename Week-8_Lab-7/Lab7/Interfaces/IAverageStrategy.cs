using Lab7.Models;

namespace Lab7.Interfaces;

public interface IAverageStrategy
{
    double Calculate(IReadOnlyList<Student> students);
}
