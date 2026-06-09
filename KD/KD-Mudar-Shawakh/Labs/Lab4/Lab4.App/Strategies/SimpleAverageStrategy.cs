using System.Linq;
using Lab4.App.Models;

namespace Lab4.App.Strategies
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