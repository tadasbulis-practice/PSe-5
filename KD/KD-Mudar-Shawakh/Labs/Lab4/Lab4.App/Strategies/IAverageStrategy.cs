using Lab4.App.Models;

namespace Lab4.App.Strategies
{
    public interface IAverageStrategy
    {
        double Calculate(Student s);
    }
}