// NOTE: This file is EXCLUDED from compilation in the .csproj.
// It is a standalone exercise file for the Drills bonus task.

// To include it temporarily, comment out the <Compile Remove="LINQ_Drills.cs" /> line in .csproj.

using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1After;

public static class LinqDrills
{
    // ──────────────────────────────────────────────────────────────
    //  Sample data used by all drills
    // ──────────────────────────────────────────────────────────────
    static List<int> numbers  = new() { 3, 7, 1, 9, 4, 6, 2, 8, 5, 10 };
    static List<string> names = new() { "Carol", "Alice", "Eve", "Bob", "Diana" };
    static List<double> scores = new() { 4.5, 7.0, 3.2, 9.1, 6.8, 5.5, 8.3 };

    // ──────────────────────────────────────────────────────────────
    //  Drill 1 — Filter numbers > 5
    // ──────────────────────────────────────────────────────────────

    // LINQ version (given)
    static List<int> Drill1_Linq()
        => numbers.Where(n => n > 5).ToList();

    // Plain version (no LINQ)
    static List<int> Drill1_Plain()
    {
        var result = new List<int>();
        foreach (var n in numbers)
            if (n > 5)
                result.Add(n);
        return result;
    }

    // ──────────────────────────────────────────────────────────────
    //  Drill 2 — Top 3 scores (descending)
    // ──────────────────────────────────────────────────────────────

    // LINQ version (given)
    static List<double> Drill2_Linq()
        => scores.OrderByDescending(s => s).Take(3).ToList();

    // Plain version (no LINQ)
    static List<double> Drill2_Plain()
    {
        var copy   = new List<double>(scores);
        var result = new List<double>();

        for (int i = 0; i < 3 && copy.Count > 0; i++)
        {
            // find max
            int maxIdx = 0;
            for (int j = 1; j < copy.Count; j++)
                if (copy[j] > copy[maxIdx]) maxIdx = j;

            result.Add(copy[maxIdx]);
            copy.RemoveAt(maxIdx);
        }

        return result;
    }

    // ──────────────────────────────────────────────────────────────
    //  Drill 3 — Sum and Average of scores
    // ──────────────────────────────────────────────────────────────

    // LINQ version (given)
    static (double sum, double avg) Drill3_Linq()
        => (scores.Sum(), scores.Average());

    // Plain version (no LINQ)
    static (double sum, double avg) Drill3_Plain()
    {
        double sum = 0;
        for (int i = 0; i < scores.Count; i++)
            sum += scores[i];

        double avg = scores.Count > 0 ? sum / scores.Count : 0;
        return (sum, avg);
    }

    // ──────────────────────────────────────────────────────────────
    //  Drill 4 — Count above 5, Any above 9, All above 3
    // ──────────────────────────────────────────────────────────────

    // LINQ version (given)
    static (int count, bool any, bool all) Drill4_Linq()
        => (scores.Count(s => s > 5),
            scores.Any(s => s > 9),
            scores.All(s => s > 3));

    // Plain version (no LINQ)
    static (int count, bool any, bool all) Drill4_Plain()
    {
        int  count = 0;
        bool any   = false;
        bool all   = true;

        foreach (var s in scores)
        {
            if (s > 5)  count++;
            if (s > 9)  any = true;
            if (s <= 3) all = false;
        }

        return (count, any, all);
    }

    // ──────────────────────────────────────────────────────────────
    //  Drill 5 — Names with length > 3, sorted, uppercased
    // ──────────────────────────────────────────────────────────────

    // LINQ version (given)
    static List<string> Drill5_Linq()
        => names.Where(n => n.Length > 3)
                .OrderBy(n => n)
                .Select(n => n.ToUpper())
                .ToList();

    // Plain version (no LINQ)
    static List<string> Drill5_Plain()
    {
        // 1. filter
        var filtered = new List<string>();
        foreach (var n in names)
            if (n.Length > 3)
                filtered.Add(n);

        // 2. sort
        filtered.Sort((a, b) => string.Compare(a, b, StringComparison.Ordinal));

        // 3. transform to uppercase
        var result = new List<string>();
        foreach (var n in filtered)
            result.Add(n.ToUpper());

        return result;
    }

    // ──────────────────────────────────────────────────────────────
    //  Entry point for manual testing
    // ──────────────────────────────────────────────────────────────
    public static void RunAll()
    {
        Console.WriteLine("=== LINQ Drills ===\n");

        Console.WriteLine("Drill 1 — Filter > 5");
        Console.WriteLine("  LINQ : " + string.Join(", ", Drill1_Linq()));
        Console.WriteLine("  Plain: " + string.Join(", ", Drill1_Plain()));

        Console.WriteLine("\nDrill 2 — Top 3 scores");
        Console.WriteLine("  LINQ : " + string.Join(", ", Drill2_Linq()));
        Console.WriteLine("  Plain: " + string.Join(", ", Drill2_Plain()));

        Console.WriteLine("\nDrill 3 — Sum & Average");
        var (ls, la) = Drill3_Linq();
        var (ps, pa) = Drill3_Plain();
        Console.WriteLine($"  LINQ : sum={ls:F2} avg={la:F2}");
        Console.WriteLine($"  Plain: sum={ps:F2} avg={pa:F2}");

        Console.WriteLine("\nDrill 4 — Count / Any / All");
        var (lc, lan, lal) = Drill4_Linq();
        var (pc, pan, pal) = Drill4_Plain();
        Console.WriteLine($"  LINQ : count={lc} any={lan} all={lal}");
        Console.WriteLine($"  Plain: count={pc} any={pan} all={pal}");

        Console.WriteLine("\nDrill 5 — Filter → Sort → ToUpper");
        Console.WriteLine("  LINQ : " + string.Join(", ", Drill5_Linq()));
        Console.WriteLine("  Plain: " + string.Join(", ", Drill5_Plain()));
    }
}
