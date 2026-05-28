public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var allGrades = students
            .Where(s => s.Grades != null && s.Grades.Count > 0)
            .SelectMany(s => s.Grades)
            .ToList();

        return allGrades.Count == 0 ? 0 : allGrades.Average();
    }
}
