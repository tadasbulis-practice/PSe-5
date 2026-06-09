using System.Linq;
using Lab5.App.Models;

namespace Lab5.App.Strategies
{
    public class DropLowestAverageStrategy : IAverageStrategy
    {
        public double Calculate(Student s)
        {
            if (s.Grades.Count == 0) return 0;
            if (s.Grades.Count == 1) return s.Grades.First();
            
            return s.Grades.OrderBy(g => g).Skip(1).Average();
        }
    }
}