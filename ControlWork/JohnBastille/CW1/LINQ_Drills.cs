// =============================================================================
//  LINQ DRILLS — papildoma uzduotis / bonus exercise  (NEPRIVALOMA / OPTIONAL)
//
//  LT:  Sis failas YRA islaikytas kompiliacijos atzvilgiu (zr. .csproj
//       <Compile Remove="LINQ_Drills.cs" />). Tai apsauga, kad nebaigtas
//       drill nelustytu jusu Program.cs build'o. Norint isbandyti viena
//       drill — laikinai ji nukopijuokite i savo Main(), arba paimkite
//       jo turini i `dotnet-script` / scratch projekta.
//
//  EN:  This file is EXCLUDED from compilation (see .csproj
//       <Compile Remove="LINQ_Drills.cs" />). That way unfinished drills
//       never break your main build. To run a drill, copy it into a Main(),
//       or paste into `dotnet-script` / a scratch project.
//
//  UZDUOTIS / TASK:
//     5 mini-pavyzdziai. Kiekvienas turi:
//        (A) gatav?? LINQ versija
//        (B) TODO blok??, kuriame turite parasyti ekvivalencia versija
//            be LINQ — tik su for / foreach / if / List<T>.Sort.
//     Negalima naudoti: Where, Select, OrderBy, Sum, Average, Count,
//                       Any, All, Take, Min, Max, GroupBy, etc.
// =============================================================================

#nullable enable
// =============================================================================
//  LINQ DRILLS — papildoma uzduotis / bonus exercise  (NEPRIVALOMA / OPTIONAL)
//
#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1.LinqDrills;

public static class LinqDrills
{
    // -------------------------------------------------------------------------
    // Drill 1 — Where  (filtravimas)
    //   Surasti visus numerius, kurie >= 5.
    //   Find all numbers >= 5.
    // -------------------------------------------------------------------------
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

    // -------------------------------------------------------------------------
    // Drill 2 — OrderByDescending + Take  (rusiavimas + paemimas pirmu N)
    //   Issaugoti TOP 3 didziausius numerius (descending).
    //   Return the top 3 largest numbers (descending order).
    // -------------------------------------------------------------------------
    public static List<int> Top3Desc_Linq(List<int> input) =>
        input.OrderByDescending(n => n).Take(3).ToList();

    public static List<int> Top3Desc_Plain(List<int> input)
    {
        // copy the list so we don't mutate the original
        var temp = new List<int>(input);
        // sort descending using List<T>.Sort
        temp.Sort((a, b) => b.CompareTo(a));
        var result = new List<int>();
        for (int i = 0; i < temp.Count && i < 3; i++)
            result.Add(temp[i]);
        return result;
    }

    // -------------------------------------------------------------------------
    // Drill 3 — Sum + Average  (agregavimas)
    //   Apskaiciuoti suma ir vidurki.
    //   Compute the sum and the average.
    // -------------------------------------------------------------------------
    public static (int Sum, double Avg) SumAndAvg_Linq(List<int> input) =>
        (input.Sum(), input.Count == 0 ? 0.0 : input.Average());

    public static (int Sum, double Avg) SumAndAvg_Plain(List<int> input)
    {
        int sum = 0;
        foreach (var n in input)
            sum += n;

        double avg = input.Count == 0 ? 0.0 : (double)sum / input.Count;
        return (sum, avg);
    }

    // -------------------------------------------------------------------------
    // Drill 4 — Count + Any + All  (booleaniniai agregatai)
    //   Suskaiciuoti, kiek elementu > 7;
    //   ar yra nors vienas neigiamas; ar visi >= 0.
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
    // Drill 5 — Where + OrderBy + Select  (kombinacija)
    //   Studentu vardai (lowercase'inti), kuriu vidurkis > 7, surusiuoti pagal vidurki desc.
    //   Names (lowercased) of students with avg > 7, sorted by avg desc.
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
        var filtered = new List<MiniStudent>();
        foreach (var s in input)
        {
            if (s.Avg > 7)
                filtered.Add(s);
        }

        // sort by Avg descending
        filtered.Sort((a, b) => b.Avg.CompareTo(a.Avg));

        var names = new List<string>();
        foreach (var s in filtered)
            names.Add(s.Name.ToLowerInvariant());

        return names;
    }
}