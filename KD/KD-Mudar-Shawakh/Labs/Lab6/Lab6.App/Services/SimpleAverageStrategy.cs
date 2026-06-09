using System.Collections.Generic;
using System.Linq;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class SimpleAverageStrategy : IAverageStrategy
    {
        // LAB-6: the average is computed across the GROUP, not one student.
        // We pool every grade from every student and take a flat average.
        public double Calculate(IReadOnlyList<Student> students)
        {
            // SelectMany flattens List<List<int>> into a single IEnumerable<int>.
            var allGrades = students.SelectMany(s => s.Grades).ToList();

            if (allGrades.Count == 0)
            {
                return 0;
            }

            return allGrades.Average();
        }
    }
}