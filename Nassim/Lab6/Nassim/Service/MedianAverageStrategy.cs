using System.Collections.Generic;
using System.Linq;

namespace Nassim.Lab4.Nassim.Service
{
    public class MedianAverageStrategy : IAverageStrategy
    {
        public double Calculate(List<Student> students)
        {
            if (students.Count == 0) return 0;
            var sorted = students.Select(s => s.AverageGrade).OrderBy(g => g).ToList();
            int mid = sorted.Count / 2;
            return sorted.Count % 2 == 0
                ? (sorted[mid - 1] + sorted[mid]) / 2.0
                : sorted[mid];
        }
    }
}