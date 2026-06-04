using CW1.LinqDrills;

namespace CW1After;

// Self-test for the LINQ drills: runs each *_Linq vs *_Plain pair on the
// same input and checks the outputs match. Run with: dotnet run -- --drills
public static class DrillTests
{
    public static void RunAll()
    {
        Console.WriteLine("=== LINQ Drill self-test ===");

        var nums = new List<int> { 3, 8, -2, 5, 10, 1, 7, 9 };

        Check("Drill 1 (Where)",
            SameInts(LinqDrills.AtLeast5_Linq(nums), LinqDrills.AtLeast5_Plain(nums)));

        Check("Drill 2 (OrderByDesc + Take)",
            SameInts(LinqDrills.Top3Desc_Linq(nums), LinqDrills.Top3Desc_Plain(nums)));

        var sumLinq = LinqDrills.SumAndAvg_Linq(nums);
        var sumPlain = LinqDrills.SumAndAvg_Plain(nums);
        Check("Drill 3 (Sum + Average)",
            sumLinq.Sum == sumPlain.Sum && sumLinq.Avg == sumPlain.Avg);

        var bLinq = LinqDrills.Bools_Linq(nums);
        var bPlain = LinqDrills.Bools_Plain(nums);
        Check("Drill 4 (Count + Any + All)",
            bLinq.Above7 == bPlain.Above7
            && bLinq.AnyNegative == bPlain.AnyNegative
            && bLinq.AllNonNegative == bPlain.AllNonNegative);

        var students = new List<LinqDrills.MiniStudent>
        {
            new("Alice", 9.2),
            new("Bob", 6.5),
            new("Carol", 8.0),
            new("Dave", 7.0),
            new("Eve", 9.9)
        };
        Check("Drill 5 (Where + OrderBy + Select)",
            SameStrings(LinqDrills.TopNames_Linq(students), LinqDrills.TopNames_Plain(students)));

        Console.WriteLine("=== Done ===");
    }

    private static void Check(string name, bool passed) =>
        Console.WriteLine($"  [{(passed ? "PASS" : "FAIL")}] {name}");

    private static bool SameInts(List<int> a, List<int> b)
    {
        if (a.Count != b.Count) return false;
        for (int i = 0; i < a.Count; i++)
            if (a[i] != b[i]) return false;
        return true;
    }

    private static bool SameStrings(List<string> a, List<string> b)
    {
        if (a.Count != b.Count) return false;
        for (int i = 0; i < a.Count; i++)
            if (a[i] != b[i]) return false;
        return true;
    }
}