using System.Collections.Generic;
using System.Linq;
using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations.Strategy
{
    public class SimpleAverageStrategy : IAverageStrategy
    {
        public double Calculate(IReadOnlyList<Student> students)
        {
            var allGrades = students.SelectMany(s => s.Grades).ToList();

            if (allGrades.Count == 0)
            {
                return 0;
            }

            return allGrades.Average();
        }
    }
}