
#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace CW1After.LinqDrills;

public static class LinqDrills
{
  

    public static List<int> AtLeast5_Linq(List<int> input) =>
        input.Where(n => n >= 5).ToList();

    public static List<int> AtLeast5_Plain(List<int> input)
    {
        var result = new List<int>();
        foreach (var n in input)
        {
            if (n >= 5)
                result.Add(n);
        }
        return result;
    }


    public static List<int> Top3Desc_Linq(List<int> input) =>
        input.OrderByDescending(n => n).Take(3).ToList();

    public static List<int> Top3Desc_Plain(List<int> input)
    {

        var copy   = new List<int>(input);
        var result = new List<int>();

        for (int i = 0; i < 3 && copy.Count > 0; i++)
        {
            int maxIndex = 0;
            for (int j = 1; j < copy.Count; j++)
            {
                if (copy[j] > copy[maxIndex])
                    maxIndex = j;
            }
            result.Add(copy[maxIndex]);
            copy.RemoveAt(maxIndex);
        }

        return result;
    }

    public static (int Sum, double Avg) SumAndAvg_Linq(List<int> input) =>
        (input.Sum(), input.Count == 0 ? 0.0 : input.Average());

    public static (int Sum, double Avg) SumAndAvg_Plain(List<int> input)
    {
        int sum = 0;
        foreach (var n in input)
            sum += n;

        double avg = input.Count == 0 ? 0.0 : sum / (double)input.Count;
        return (sum, avg);
    }

   

    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Linq(List<int> input) =>
        (input.Count(n => n > 7), input.Any(n => n < 0), input.All(n => n >= 0));

    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Plain(List<int> input)
    {
        int  above7        = 0;
        bool anyNegative   = false;
        bool allNonNeg     = true;

        foreach (var n in input)
        {
            if (n > 7) above7++;
            if (n < 0) anyNegative = true;
            if (n < 0) allNonNeg   = false;
        }

        return (above7, anyNegative, allNonNeg);
    }

 

    public sealed record MiniStudent(string Name, double Avg);

    public static List<string> TopNames_Linq(List<MiniStudent> input) =>
        input
            .Where(s => s.Avg > 7)
            .OrderByDescending(s => s.Avg)
            .Select(s => s.Name.ToLowerInvariant())
            .ToList();

    public static List<string> TopNames_Plain(List<MiniStudent> input)
    {
        
        var filtered = new List<MiniStudent>();
        foreach (var s in input)
        {
            if (s.Avg > 7)
                filtered.Add(s);
        }

        filtered.Sort((a, b) => b.Avg.CompareTo(a.Avg));

       
        var result = new List<string>();
        foreach (var s in filtered)
            result.Add(s.Name.ToLowerInvariant());

        return result;
    }
}
