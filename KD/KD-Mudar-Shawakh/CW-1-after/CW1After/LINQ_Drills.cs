#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1.LinqDrills;

public static class LinqDrills
{
    // -------------------------------------------------------------------------
    // Drill 1 — Where (filtering): find all numbers >= 5.
    // -------------------------------------------------------------------------
    public static List<int> AtLeast5_Linq(List<int> input) =>
        input.Where(n => n >= 5).ToList();

    public static List<int> AtLeast5_Plain(List<int> input)
    {
        var result = new List<int>();
        foreach (var n in input)
            if (n >= 5)
                result.Add(n);
        return result;
    }

    // -------------------------------------------------------------------------
    // Drill 2 — OrderByDescending + Take: top 3 largest numbers (descending).
    // -------------------------------------------------------------------------
    public static List<int> Top3Desc_Linq(List<int> input) =>
        input.OrderByDescending(n => n).Take(3).ToList();

    public static List<int> Top3Desc_Plain(List<int> input)
    {
        // Copy the list so we don't mutate the caller's data.
        var copy = new List<int>();
        foreach (var n in input)
            copy.Add(n);

        var result = new List<int>();
        // Find the max, add it, remove it — repeat up to 3 times.
        for (int round = 0; round < 3 && copy.Count > 0; round++)
        {
            int maxIndex = 0;
            for (int i = 1; i < copy.Count; i++)
                if (copy[i] > copy[maxIndex])
                    maxIndex = i;

            result.Add(copy[maxIndex]);
            copy.RemoveAt(maxIndex);
        }
        return result;
    }

    // -------------------------------------------------------------------------
    // Drill 3 — Sum + Average: compute the sum and the average.
    // -------------------------------------------------------------------------
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

    // -------------------------------------------------------------------------
    // Drill 4 — Count + Any + All: count > 7; any negative; all >= 0.
    // -------------------------------------------------------------------------
    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Linq(List<int> input) =>
        (input.Count(n => n > 7), input.Any(n => n < 0), input.All(n => n >= 0));

    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Plain(List<int> input)
    {
        int above7 = 0;
        bool anyNegative = false;
        bool allNonNegative = true;

        foreach (var n in input)
        {
            if (n > 7) above7++;
            if (n < 0) anyNegative = true;
            if (n < 0) allNonNegative = false;
        }

        return (above7, anyNegative, allNonNegative);
    }

    // -------------------------------------------------------------------------
    // Drill 5 — Where + OrderBy + Select: names (lowercased) of students with
    //           avg > 7, sorted by avg descending.
    // -------------------------------------------------------------------------
    public sealed record MiniStudent(string Name, double Avg);

    public static List<string> TopNames_Linq(List<MiniStudent> input) =>
        input
            .Where(s => s.Avg > 7)
            .OrderByDescending(s => s.Avg)
            .Select(s => s.Name.ToLowerInvariant())
            .ToList();

    public static List<string> TopNames_Plain(List<MiniStudent> input)
    {
        // 1) Filter: avg > 7
        var filtered = new List<MiniStudent>();
        foreach (var s in input)
            if (s.Avg > 7)
                filtered.Add(s);

        // 2) Sort by avg descending
        filtered.Sort((a, b) => b.Avg.CompareTo(a.Avg));

        // 3) Project: lowercased names
        var result = new List<string>();
        foreach (var s in filtered)
            result.Add(s.Name.ToLowerInvariant());

        return result;
    }
}