public class MedianAverage : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var allGrades = students.SelectMany(s => s.Grades).OrderBy(x => x).ToList();
        if (allGrades.Count == 0)
            return 0;

        int count = allGrades.Count;
        if (count % 2 == 1)
            return allGrades[count / 2];

        return (allGrades[count / 2 - 1] + allGrades[count / 2]) / 2.0;
    }
}
