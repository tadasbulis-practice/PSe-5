// NOTE: Excluded from compilation. See .csproj <Compile Remove="LINQ_Drills.cs" />

using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1Friend;

public static class LinqDrills
{
    static List<int> nums    = new() { 2, 15, 8, 3, 11, 7, 19, 4, 13, 6 };
    static List<string> words = new() { "Diana", "Tom", "Sophie", "Alex", "Maria" };
    static List<double> pts  = new() { 5.5, 8.2, 3.0, 9.4, 7.1, 4.8, 6.6 };

    // ── Drill 1: filter numbers > 10 ──────────────────────────

    static List<int> Drill1_Linq()
        => nums.Where(n => n > 10).ToList();

    static List<int> Drill1_Plain()
    {
        var output = new List<int>();
        for (int i = 0; i < nums.Count; i++)
            if (nums[i] > 10) output.Add(nums[i]);
        return output;
    }

    // ── Drill 2: top 3 points descending ──────────────────────

    static List<double> Drill2_Linq()
        => pts.OrderByDescending(p => p).Take(3).ToList();

    static List<double> Drill2_Plain()
    {
        var temp   = new List<double>(pts);
        var output = new List<double>();

        for (int i = 0; i < 3 && temp.Count > 0; i++)
        {
            int best = 0;
            for (int j = 1; j < temp.Count; j++)
                if (temp[j] > temp[best]) best = j;
            output.Add(temp[best]);
            temp.RemoveAt(best);
        }

        return output;
    }

    // ── Drill 3: sum and average of points ────────────────────

    static (double sum, double avg) Drill3_Linq()
        => (pts.Sum(), pts.Average());

    static (double sum, double avg) Drill3_Plain()
    {
        double total = 0;
        for (int i = 0; i < pts.Count; i++) total += pts[i];
        double average = pts.Count > 0 ? total / pts.Count : 0;
        return (total, average);
    }

    // ── Drill 4: count > 6, any > 9, all > 2 ─────────────────

    static (int count, bool any, bool all) Drill4_Linq()
        => (pts.Count(p => p > 6), pts.Any(p => p > 9), pts.All(p => p > 2));

    static (int count, bool any, bool all) Drill4_Plain()
    {
        int  cnt  = 0;
        bool any  = false;
        bool all  = true;

        foreach (var p in pts)
        {
            if (p > 6) cnt++;
            if (p > 9) any = true;
            if (p <= 2) all = false;
        }

        return (cnt, any, all);
    }

    // ── Drill 5: words length > 4, sorted, lowercased ─────────

    static List<string> Drill5_Linq()
        => words.Where(w => w.Length > 4)
                .OrderBy(w => w)
                .Select(w => w.ToLower())
                .ToList();

    static List<string> Drill5_Plain()
    {
        var filtered = new List<string>();
        foreach (var w in words)
            if (w.Length > 4) filtered.Add(w);

        filtered.Sort((a, b) => string.Compare(a, b, StringComparison.Ordinal));

        var output = new List<string>();
        foreach (var w in filtered)
            output.Add(w.ToLower());

        return output;
    }

    public static void RunAll()
    {
        Console.WriteLine("=== LINQ Drills ===\n");

        Console.WriteLine("Drill 1 — Filter > 10");
        Console.WriteLine("  LINQ  : " + string.Join(", ", Drill1_Linq()));
        Console.WriteLine("  Plain : " + string.Join(", ", Drill1_Plain()));

        Console.WriteLine("\nDrill 2 — Top 3 points");
        Console.WriteLine("  LINQ  : " + string.Join(", ", Drill2_Linq()));
        Console.WriteLine("  Plain : " + string.Join(", ", Drill2_Plain()));

        Console.WriteLine("\nDrill 3 — Sum & Average");
        var (ls, la) = Drill3_Linq();
        var (ps, pa) = Drill3_Plain();
        Console.WriteLine($"  LINQ  : sum={ls:F2}  avg={la:F2}");
        Console.WriteLine($"  Plain : sum={ps:F2}  avg={pa:F2}");

        Console.WriteLine("\nDrill 4 — Count / Any / All");
        var (lc, lan, lal) = Drill4_Linq();
        var (pc, pan, pal) = Drill4_Plain();
        Console.WriteLine($"  LINQ  : count={lc}  any={lan}  all={lal}");
        Console.WriteLine($"  Plain : count={pc}  any={pan}  all={pal}");

        Console.WriteLine("\nDrill 5 — Filter → Sort → ToLower");
        Console.WriteLine("  LINQ  : " + string.Join(", ", Drill5_Linq()));
        Console.WriteLine("  Plain : " + string.Join(", ", Drill5_Plain()));
    }
}
