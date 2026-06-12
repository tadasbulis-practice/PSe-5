// =============================================================================
//  LINQ DRILLS — completed bonus exercise
//
//  Each drill has two versions:
//    (A) LINQ version  — original, unchanged
//    (B) Plain version — only for/foreach/if, zero LINQ operators
//                        (no Where, Select, OrderBy, Sum, Average,
//                         Count, Any, All, Take, Min, Max, GroupBy, etc.)
// =============================================================================

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1.LinqDrills;

public static class LinqDrills
{
    // -------------------------------------------------------------------------
    // Drill 1 — Where  (filtering)
    //   Find all numbers >= 5.
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
    // Drill 2 — OrderByDescending + Take  (sort + take first N)
    //   Return the top 3 largest numbers in descending order.
    // -------------------------------------------------------------------------
    public static List<int> Top3Desc_Linq(List<int> input) =>
        input.OrderByDescending(n => n).Take(3).ToList();

    public static List<int> Top3Desc_Plain(List<int> input)
    {
        // Work on a copy so we do not mutate the caller's list
        var copy = new List<int>(input);
        var result = new List<int>();

        for (int take = 0; take < 3 && copy.Count > 0; take++)
        {
            // Find index of the current maximum
            int maxIdx = 0;
            for (int i = 1; i < copy.Count; i++)
                if (copy[i] > copy[maxIdx])
                    maxIdx = i;

            result.Add(copy[maxIdx]);
            copy.RemoveAt(maxIdx);
        }
        return result;
    }

    // -------------------------------------------------------------------------
    // Drill 3 — Sum + Average  (aggregation)
    //   Compute the sum and the average.
    // -------------------------------------------------------------------------
    public static (int Sum, double Avg) SumAndAvg_Linq(List<int> input) =>
        (input.Sum(), input.Count == 0 ? 0.0 : input.Average());

    public static (int Sum, double Avg) SumAndAvg_Plain(List<int> input)
    {
        int sum = 0;
        int count = 0;
        foreach (var n in input)
        {
            sum += n;
            count++;
        }
        double avg = count == 0 ? 0.0 : sum / (double)count;
        return (sum, avg);
    }

    // -------------------------------------------------------------------------
    // Drill 4 — Count + Any + All  (boolean aggregates)
    //   Count items > 7; whether any is negative; whether all are >= 0.
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
    // Drill 5 — Where + OrderBy + Select  (combination)
    //   Lowercased names of students whose average is > 7, sorted by avg desc.
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
        // 1. Filter: keep only students with Avg > 7
        var filtered = new List<MiniStudent>();
        foreach (var s in input)
            if (s.Avg > 7)
                filtered.Add(s);

        // 2. Sort descending by Avg (insertion sort)
        for (int i = 1; i < filtered.Count; i++)
        {
            var key = filtered[i];
            int j = i - 1;
            while (j >= 0 && filtered[j].Avg < key.Avg)
            {
                filtered[j + 1] = filtered[j];
                j--;
            }
            filtered[j + 1] = key;
        }

        // 3. Project to lowercase names
        var result = new List<string>();
        foreach (var s in filtered)
            result.Add(s.Name.ToLowerInvariant());

        return result;
    }
}
