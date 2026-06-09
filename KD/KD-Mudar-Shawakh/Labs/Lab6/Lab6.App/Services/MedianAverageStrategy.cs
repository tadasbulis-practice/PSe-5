using System.Collections.Generic;
using System.Linq;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class MedianAverageStrategy : IAverageStrategy
    {
        // LAB-6 median: pool every grade from every student, sort, take the middle.
        // Same algorithm I used in LAB-5, just applied across the whole group.
        public double Calculate(IReadOnlyList<Student> students)
        {
            // Flatten all grades from all students into one list, then sort.
            var sortedGrades = students
                .SelectMany(s => s.Grades)
                .OrderBy(g => g)
                .ToList();

            int count = sortedGrades.Count;

            if (count == 0)
            {
                return 0;
            }

            if (count % 2 == 0)
            {
                // Even count: average of the two middle elements.
                return (sortedGrades[count / 2 - 1] + sortedGrades[count / 2]) / 2.0;
            }
            else
            {
                // Odd count: the middle element.
                return sortedGrades[count / 2];
            }
        }
    }
}