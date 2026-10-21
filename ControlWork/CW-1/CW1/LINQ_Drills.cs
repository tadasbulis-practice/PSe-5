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
        // TODO: parasykite ekvivalencia versija be LINQ
        // TODO: write the equivalent version without LINQ
        throw new NotImplementedException();
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
        // TODO: parasykite ekvivalencia versija be LINQ.
        //       Patarimas: nukopijuokite saraso, paeiliui randekite max,
        //       isimkite ji ir pakartokite 3 kartus.
        // TODO: write the equivalent without LINQ.
        //       Hint: copy the list, find max, remove it, repeat 3 times.
        throw new NotImplementedException();
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
        // TODO: parasykite ekvivalencia versija be LINQ
        // TODO: write the equivalent without LINQ
        throw new NotImplementedException();
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
        // TODO: parasykite ekvivalencia versija be LINQ
        // TODO: write the equivalent without LINQ
        throw new NotImplementedException();
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
        // TODO: parasykite ekvivalencia versija be LINQ
        // TODO: write the equivalent without LINQ
        throw new NotImplementedException();
    }
}
