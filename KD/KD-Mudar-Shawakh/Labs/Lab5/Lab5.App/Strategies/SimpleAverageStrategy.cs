using System.Linq;
using Lab5.App.Models;

namespace Lab5.App.Strategies
{
    public class SimpleAverageStrategy : IAverageStrategy
    {
        public double Calculate(Student s)
        {
            if (s.Grades.Count == 0) return 0;
            return s.Grades.Average();
        }
    }
}