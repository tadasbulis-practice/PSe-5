using System.Collections.Generic;
using Lab7.App.Models;

namespace Lab7.App.Interfaces
{
    public interface IAverageStrategy
    {
        double Calculate(IReadOnlyList<Student> students);
    }
}