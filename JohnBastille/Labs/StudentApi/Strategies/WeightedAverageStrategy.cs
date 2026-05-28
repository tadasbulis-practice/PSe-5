using StudentApi.Interfaces;

namespace StudentApi.Strategies;

public class WeightedAverageStrategy : IAverageStrategy
{
	public double CalculateAverage(List<int> grades)
	{
		if (grades == null || grades.Count == 0) return 0;

		// Simple example: later grades are more important
		double sum = 0;
		double weightSum = 0;
		for (int i = 0; i < grades.Count; i++)
		{
			var weight = i + 1;
			sum += grades[i] * weight;
			weightSum += weight;
		}

		return sum / weightSum;
	}
}
