public class WeightedAverage : IAverageStrategy
{
    public double Calculate(List<int> grades)
    {
        if (grades == null || grades.Count == 0)
            return 0;

        double totalWeight = 0;
        double weightedSum = 0;

        for (int i = 0; i < grades.Count; i++)
        {
            int weight = i + 1;
            weightedSum += grades[i] * weight;
            totalWeight += weight;
        }

        return weightedSum / totalWeight;
    }
}
