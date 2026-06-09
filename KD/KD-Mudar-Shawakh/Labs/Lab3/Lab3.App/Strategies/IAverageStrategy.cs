using Lab3.App.Models;

namespace Lab3.App.Strategies
{
    public interface IAverageStrategy
    {
        double Calculate(Student s);
    }
}