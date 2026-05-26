public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var grades = students
            .Where(s => s.Grades != null && s.Grades.Count > 0)
            .SelectMany(s => s.Grades)
            .OrderBy(g => g)
            .ToList();

        if (grades.Count == 0)
            return 0;

        int middle = grades.Count / 2;

        if (grades.Count % 2 == 0)
            return (grades[middle - 1] + grades[middle]) / 2.0;

        return grades[middle];
    }
}
