using Lab6.App.Models;

namespace Lab6.App.Interfaces;

public interface IAverageStrategy
{
    double Calculate(List<Student> students);
}