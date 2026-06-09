using System.Linq;
using Lab5.App.Models;

namespace Lab5.App.Strategies
{
    public class MedianAverageStrategy : IAverageStrategy
    {
        public double Calculate(Student s)
        {
            if (s.Grades.Count == 0) return 0;
            
            var sortedGrades = s.Grades.OrderBy(g => g).ToList();
            int count = sortedGrades.Count;
            
            if (count % 2 == 0)
            {
                // Average of the two middle elements
                return (sortedGrades[count / 2 - 1] + sortedGrades[count / 2]) / 2.0;
            }
            else
            {
                // Middle element
                return sortedGrades[count / 2];
            }
        }
    }
}