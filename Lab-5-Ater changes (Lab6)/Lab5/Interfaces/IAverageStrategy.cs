
using Lab5.Models;

namespace Lab5.Interfaces;

public interface IAverageStrategy
{
    double Calculate(List<Student> students);
}
