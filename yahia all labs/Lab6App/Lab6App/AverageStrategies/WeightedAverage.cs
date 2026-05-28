public class WeightedAverage : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var allGrades = students.SelectMany(s => s.Grades).ToList();
        if (allGrades.Count == 0)
            return 0;

        double weightedSum = 0;
        double totalWeight = 0;

        for (int i = 0; i < allGrades.Count; i++)
        {
            int weight = i + 1;
            weightedSum += allGrades[i] * weight;
            totalWeight += weight;
        }

        return weightedSum / totalWeight;
    }
}
