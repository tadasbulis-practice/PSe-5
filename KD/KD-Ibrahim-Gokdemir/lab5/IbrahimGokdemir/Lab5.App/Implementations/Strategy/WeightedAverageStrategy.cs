public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        int totalWeight = 0;
        int weightedSum = 0;

        for (int i = 0; i < student.Grades.Count; i++)
        {
            int weight = i + 1;
            weightedSum += student.Grades[i] * weight;
            totalWeight += weight;
        }

        return (double)weightedSum / totalWeight;
    }
}