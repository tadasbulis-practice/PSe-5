using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Strategy;

// Task 3 — WeightedAverageStrategy
// Each student's average is weighted by how many grades they have.
// Students with more grades have more influence on the group result.
public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var studentsWithGrades = students.Where(s => s.Grades.Count > 0).ToList();

        if (studentsWithGrades.Count == 0) return 0;

        double weightedSum = studentsWithGrades.Sum(s => s.GetAverage() * s.Grades.Count);
        int totalGrades = studentsWithGrades.Sum(s => s.Grades.Count);

        return totalGrades == 0 ? 0 : weightedSum / totalGrades;
    }
}
