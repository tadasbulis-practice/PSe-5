using System.Collections.Generic;
using System.Linq;

namespace Nassim.Lab4.Nassim.Service
{
    public class WeightedAverageStrategy : IAverageStrategy
    {
        public double Calculate(List<Student> students)
        {
            if (students.Count == 0) return 0;
            // Pondération : les notes élevées comptent plus
            double weightedSum = students.Sum(s => s.AverageGrade * s.AverageGrade);
            double weightSum = students.Sum(s => s.AverageGrade);
            return weightSum == 0 ? 0 : weightedSum / weightSum;
        }
    }
}