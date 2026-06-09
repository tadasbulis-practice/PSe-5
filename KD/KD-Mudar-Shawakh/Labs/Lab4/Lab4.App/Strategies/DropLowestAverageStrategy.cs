using System.Linq;
using Lab4.App.Models;

namespace Lab4.App.Strategies
{
    public class DropLowestAverageStrategy : IAverageStrategy
    {
        public double Calculate(Student s)
        {
            if (s.Grades.Count == 0) return 0;
            if (s.Grades.Count == 1) return s.Grades.First();
            
            // Sort grades, drop the lowest one, and calculate average
            return s.Grades.OrderBy(g => g).Skip(1).Average();
        }
    }
}