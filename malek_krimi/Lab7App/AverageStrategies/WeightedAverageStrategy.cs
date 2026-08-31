public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var allGrades = students
            .Where(s => s.Grades != null && s.Grades.Count > 0)
            .SelectMany(s => s.Grades.Select((grade, index) => new
            {
                Grade = grade,
                Weight = index + 1
            }))
            .ToList();

        if (allGrades.Count == 0)
            return 0;

        double weightedSum = allGrades.Sum(x => x.Grade * x.Weight);
        double totalWeight = allGrades.Sum(x => x.Weight);

        return weightedSum / totalWeight;
    }
}
