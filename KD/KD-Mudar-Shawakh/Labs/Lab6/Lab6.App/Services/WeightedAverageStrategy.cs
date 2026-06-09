using System.Collections.Generic;
using System.Linq;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class WeightedAverageStrategy : IAverageStrategy
    {
        // LAB-6 weighted version: each student is weighted by their position
        // in the collection. Later students count more — same "weight by index"
        // idea as my LAB-5 strategy, but now applied at the group level.
        public double Calculate(IReadOnlyList<Student> students)
        {
            if (students.Count == 0)
            {
                return 0;
            }

            double weightedSum = 0;
            double totalWeight = 0;

            for (int i = 0; i < students.Count; i++)
            {
                // Skip students who have no grades — they cannot contribute.
                if (students[i].Grades.Count == 0)
                {
                    continue;
                }

                // Each student's contribution is their own average,
                // multiplied by their position-based weight (i + 1).
                double studentAvg = students[i].Grades.Average();
                double weight = i + 1;

                weightedSum += studentAvg * weight;
                totalWeight += weight;
            }

            if (totalWeight == 0)
            {
                return 0;
            }

            return weightedSum / totalWeight;
        }
    }
}