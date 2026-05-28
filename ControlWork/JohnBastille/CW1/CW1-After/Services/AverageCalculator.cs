using System.Collections.Generic;

namespace CW1After.Services;
public class AverageCalculator
{
    public double Calculate(IEnumerable<int> grades)
    {
        if (grades == null) return 0.0;
        int count = 0;
        double sum = 0.0;
        foreach (var g in grades) { sum += g; count++; }
        return count == 0 ? 0.0 : sum / count;
    }
}