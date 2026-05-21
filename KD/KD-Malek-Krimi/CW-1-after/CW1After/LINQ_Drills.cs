// =============================================================================
//  LINQ DRILLS — papildoma uzduotis / bonus exercise
//  This file is excluded from compilation in CW1After.csproj, as in the starter.
// =============================================================================

#nullable enable
using System;
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
        foreach (int number in input)
        {
            if (number >= 5)
            {
                result.Add(number);
            }
        }

        return result;
    }

    public static List<int> Top3Desc_Linq(List<int> input) =>
        input.OrderByDescending(n => n).Take(3).ToList();

    public static List<int> Top3Desc_Plain(List<int> input)
    {
        var copy = new List<int>();
        foreach (int number in input)
        {
            copy.Add(number);
        }

        var result = new List<int>();
        int limit = 3;
        if (copy.Count < limit)
        {
            limit = copy.Count;
        }

        for (int i = 0; i < limit; i++)
        {
            int maxIndex = 0;
            for (int j = 1; j < copy.Count; j++)
            {
                if (copy[j] > copy[maxIndex])
                {
                    maxIndex = j;
                }
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
        foreach (int number in input)
        {
            sum += number;
        }

        double average = input.Count == 0 ? 0.0 : sum / (double)input.Count;
        return (sum, average);
    }

    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Linq(List<int> input) =>
        (input.Count(n => n > 7), input.Any(n => n < 0), input.All(n => n >= 0));

    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Plain(List<int> input)
    {
        int above7 = 0;
        bool anyNegative = false;
        bool allNonNegative = true;

        foreach (int number in input)
        {
            if (number > 7)
            {
                above7++;
            }

            if (number < 0)
            {
                anyNegative = true;
                allNonNegative = false;
            }
        }

        return (above7, anyNegative, allNonNegative);
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
        foreach (var student in input)
        {
            if (student.Avg > 7)
            {
                filtered.Add(student);
            }
        }

        filtered.Sort((a, b) => b.Avg.CompareTo(a.Avg));

        var names = new List<string>();
        foreach (var student in filtered)
        {
            names.Add(student.Name.ToLowerInvariant());
        }

        return names;
    }
}
