#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1.LinqDrills;

public static class LinqDrills
{
    // -------------------------------------------------------------------------
    // Drill 1 — Where (Filtravimas)
    // -------------------------------------------------------------------------
    public static List<int> AtLeast5_Linq(List<int> input) =>
        input.Where(n => n >= 5).ToList();

    public static List<int> AtLeast5_Plain(List<int> input)
    {
        List<int> result = new List<int>();
        foreach (int n in input)
        {
            if (n >= 5)
            {
                result.Add(n);
            }
        }
        return result;
    }

    // -------------------------------------------------------------------------
    // Drill 2 — OrderByDescending + Take (Rusiavimas + paemimas pirmu N)
    // -------------------------------------------------------------------------
    public static List<int> Top3Desc_Linq(List<int> input) =>
        input.OrderByDescending(n => n).Take(3).ToList();

    public static List<int> Top3Desc_Plain(List<int> input)
    {
        List<int> copy = new List<int>(input);
        
        copy.Sort((a, b) => b.CompareTo(a));

        List<int> result = new List<int>();
        int limit = copy.Count < 3 ? copy.Count : 3;
        for (int i = 0; i < limit; i++)
        {
            result.Add(copy[i]);
        }
        return result;
    }

    // -------------------------------------------------------------------------
    // Drill 3 — Sum + Average (Agregavimas)
    // -------------------------------------------------------------------------
    public static (int Sum, double Avg) SumAndAvg_Linq(List<int> input) =>
        (input.Sum(), input.Count == 0 ? 0.0 : input.Average());

    public static (int Sum, double Avg) SumAndAvg_Plain(List<int> input)
    {
        if (input.Count == 0) return (0, 0.0);

        int sum = 0;
        foreach (int n in input)
        {
            sum += n;
        }
        double avg = (double)sum / input.Count;
        return (sum, avg);
    }

    // -------------------------------------------------------------------------
    // Drill 4 — Count + Any + All (Booleaniniai agregatai)
    // -------------------------------------------------------------------------
    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Linq(List<int> input) =>
        (input.Count(n => n > 7), input.Any(n => n < 0), input.All(n => n >= 0));

    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Plain(List<int> input)
    {
        int above7 = 0;
        bool anyNegative = false;
        bool allNonNegative = true;

        foreach (int n in input)
        {
            if (n > 7) above7++;
            if (n < 0) anyNegative = true;
            if (n < 0) allNonNegative = false;
        }

        return (above7, anyNegative, allNonNegative);
    }

    // -------------------------------------------------------------------------
    // Drill 5 — Where + OrderBy + Select (Kombinacija)
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
        List<MiniStudent> filtered = new List<MiniStudent>();
        
        foreach (var s in input)
        {
            if (s.Avg > 7) filtered.Add(s);
        }

        filtered.Sort((a, b) => b.Avg.CompareTo(a.Avg));

        List<string> result = new List<string>();
        foreach (var s in filtered)
        {
            result.Add(s.Name.ToLowerInvariant());
        }

        return result;
    }
}