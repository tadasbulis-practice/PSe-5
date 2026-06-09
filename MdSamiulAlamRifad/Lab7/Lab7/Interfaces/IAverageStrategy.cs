using Lab7.Models;

namespace Lab7.Interfaces;

// Task 3 — Strategy for Collections
public interface IAverageStrategy
{
    double Calculate(List<Student> students);
}
