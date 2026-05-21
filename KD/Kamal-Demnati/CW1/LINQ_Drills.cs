
#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1.LinqDrills;

public static class LinqDrills
{


    public static List<int> AtLeast5_Plain(List<int> input)
    {
        var result = new List<int>();

        foreach (var n in input)
        {
            if (n >= 5)
            {
                result.Add(n);
            }
        }

        return result;
    }



    public static List<int> Top3Desc_Plain(List<int> input)
    {
        var copy = new List<int>(input);
        var result = new List<int>();

        for (int i = 0; i < 3 && copy.Count > 0; i++)
        {
            int max = copy[0];

            foreach (var n in copy)
            {
                if (n > max)
                    max = n;
            }

            result.Add(max);
            copy.Remove(max);
        }

        return result;
    }


    public static (int Sum, double Avg) SumAndAvg_Plain(List<int> input)
    {
        int sum = 0;

        foreach (var n in input)
        {
            sum += n;
        }

        double avg = input.Count == 0 ? 0.0 : (double)sum / input.Count;

        return (sum, avg);
    }


    public static (int Above7, bool AnyNegative, bool AllNonNegative) Bools_Plain(List<int> input)
    {
        int above7 = 0;
        bool anyNegative = false;
        bool allNonNegative = true;

        foreach (var n in input)
        {
            if (n > 7)
                above7++;

            if (n < 0)
                anyNegative = true;

            if (n < 0)
                allNonNegative = false;
        }

        return (above7, anyNegative, allNonNegative);
    }

    public static List<string> TopNames_Plain(List<MiniStudent> input)
    {
        var filtered = new List<MiniStudent>();

        foreach (var s in input)
        {
            if (s.Avg > 7)
            {
                filtered.Add(s);
            }
        }

        filtered.Sort((a, b) => b.Avg.CompareTo(a.Avg));

        var result = new List<string>();

        foreach (var s in filtered)
        {
            result.Add(s.Name.ToLowerInvariant());
        }

        return result;
    }
