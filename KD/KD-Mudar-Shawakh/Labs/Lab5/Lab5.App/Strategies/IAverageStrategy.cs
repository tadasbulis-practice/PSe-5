using Lab5.App.Models;

namespace Lab5.App.Strategies
{
    public interface IAverageStrategy
    {
        double Calculate(Student s);
    }
}