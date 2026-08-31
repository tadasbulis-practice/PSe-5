public class SimpleAverage : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var allGrades = students.SelectMany(s => s.Grades).ToList();
        if (allGrades.Count == 0)
            return 0;

        return allGrades.Average();
    }
}
