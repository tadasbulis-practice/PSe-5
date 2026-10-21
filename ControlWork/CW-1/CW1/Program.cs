// =============================================================================
//  CW-1 / KD-1  (Kontrolinis darbas / Control Work — Starter)
//
//  LT:  Sis projektas yra "viskas viename" Program.cs faile.
//       Visa logika (modeliai, validacija, vidurkio skaiciavimas, spausdinimas,
//       meniu, duomenu saugykla) sukista cia. Programa veikia, bet pazeidzia
//       Single Responsibility Principle bei daugiasluoksnes architekturos
//       principus. Jusu uzduotis — perdaryti ja i svaria OOP struktura su
//       Modeliu, Servisu ir UI sluoksniais.
//
//       PAPILDOMA (Task 2): meniu punktai 7, 8, 9 yra paraseti TIK su LINQ.
//       Jusu CW-1-after kiekvienai is siu funkciju turi but DVI versijos:
//          (a) su LINQ — kaip parodyta cia,
//          (b) be LINQ — naudojant tik for/foreach/if (jokio Where, OrderBy,
//              Sum, Average, Count, Any, All ar pan.).
//       Tai parodo, kad zinote, KA LINQ daro po kapotu.
//
//  EN:  This is the "everything in one file" Program.cs version.
//       All logic (models, validation, average, printing, menu, in-memory
//       repository) lives here. The program runs, but it breaks SRP and the
//       layered-architecture principles. Your task: refactor it into clean
//       OOP layers — Models, Services and UI — with at least one Interface.
//
//       BONUS (Task 2): menu items 7, 8 and 9 are written ONLY with LINQ.
//       In CW-1-after each of these three features must exist in TWO versions:
//          (a) the LINQ version — exactly as shown here,
//          (b) the no-LINQ version — using only for/foreach/if (no Where,
//              OrderBy, Sum, Average, Count, Any, All, etc.).
//       This proves you understand WHAT LINQ does under the hood.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace CW1;

public class Student
{
    public int Id;
    public string Name = "";
    public string Email = "";
    public string GroupCode = "";
    public List<int> Grades = new();
}

public class Group
{
    public string Code = "";
    public string Name = "";
}

public static class Program
{
    private static readonly List<Student> _students = new();
    private static readonly List<Group> _groups = new();

    public static void Main()
    {
        _groups.Add(new Group { Code = "PI23", Name = "Programu inzinerija 2023" });
        _groups.Add(new Group { Code = "PI24", Name = "Programu inzinerija 2024" });

        _students.Add(new Student
        {
            Id = 1, Name = "Jonas Jonaitis", Email = "jonas@kauko.lt",
            GroupCode = "PI23", Grades = new List<int> { 8, 9, 7, 10 }
        });
        _students.Add(new Student
        {
            Id = 2, Name = "Greta Petraityte", Email = "greta@kauko.lt",
            GroupCode = "PI23", Grades = new List<int> { 6, 5, 7, 8 }
        });
        _students.Add(new Student
        {
            Id = 3, Name = "Mantas Kazlauskas", Email = "mantas@kauko.lt",
            GroupCode = "PI24", Grades = new List<int> { 9, 9, 10, 8 }
        });
        _students.Add(new Student
        {
            Id = 4, Name = "Ieva Andriukaityte", Email = "ieva@kauko.lt",
            GroupCode = "PI23", Grades = new List<int> { 10, 10, 9, 9 }
        });
        _students.Add(new Student
        {
            Id = 5, Name = "Tomas Bagdonas", Email = "tomas@kauko.lt",
            GroupCode = "PI24", Grades = new List<int> { 5, 6, 6, 7 }
        });

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========== CW-1 Student Menu / Studentu meniu ==========");
            Console.WriteLine(" 1) List all students  /  Rodyti visus studentus");
            Console.WriteLine(" 2) Add new student    /  Prideti nauja studenta");
            Console.WriteLine(" 3) Add grade          /  Ivesti pazymi");
            Console.WriteLine(" 4) Show average       /  Rodyti vidurki");
            Console.WriteLine(" 5) Find by id         /  Rasti pagal ID");
            Console.WriteLine(" 6) Validate student   /  Validuoti studenta");
            Console.WriteLine(" 7) Top 3 by average   /  Top 3 pagal vidurki   [LINQ]");
            Console.WriteLine(" 8) Students in group  /  Studentai grupeje     [LINQ]");
            Console.WriteLine(" 9) Statistics         /  Statistika            [LINQ]");
            Console.WriteLine(" 0) Exit               /  Iseiti");
            Console.Write("Choice / Pasirinkimas: ");

            var choice = Console.ReadLine();

            if (choice == "0") { Console.WriteLine("Bye!"); return; }

            switch (choice)
            {
                case "1":
                    foreach (var s in _students)
                    {
                        double avg = s.Grades.Count == 0 ? 0.0 : s.Grades.Sum() / (double)s.Grades.Count;
                        Console.WriteLine($"  [{s.Id}] {s.Name} ({s.GroupCode})  email={s.Email}  avg={avg:0.00}");
                    }
                    break;

                case "2":
                    Console.Write("New ID / Naujas ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int newId)) { Console.WriteLine("Bad ID."); break; }
                    if (_students.Any(x => x.Id == newId)) { Console.WriteLine("ID exists."); break; }
                    Console.Write("Name / Vardas: ");
                    var newName = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    var newEmail = Console.ReadLine() ?? "";
                    Console.Write("Group code / Grupes kodas: ");
                    var newGroup = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(newName)) { Console.WriteLine("Name required."); break; }
                    if (!newEmail.Contains('@') || !newEmail.Contains('.'))
                    { Console.WriteLine("Bad email."); break; }
                    if (_groups.All(g => g.Code != newGroup))
                    { Console.WriteLine("Group not found."); break; }

                    _students.Add(new Student
                    {
                        Id = newId, Name = newName, Email = newEmail,
                        GroupCode = newGroup, Grades = new List<int>()
                    });
                    Console.WriteLine("Student added.");
                    break;

                case "3":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int gid)) { Console.WriteLine("Bad ID."); break; }
                    var st3 = _students.FirstOrDefault(x => x.Id == gid);
                    if (st3 == null) { Console.WriteLine("Not found."); break; }
                    Console.Write("Grade (1..10): ");
                    if (!int.TryParse(Console.ReadLine(), out int grade)) { Console.WriteLine("Bad grade."); break; }
                    if (grade < 1 || grade > 10) { Console.WriteLine("Out of range."); break; }
                    st3.Grades.Add(grade);
                    Console.WriteLine($"Added {grade} to {st3.Name}.");
                    break;

                case "4":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int aid)) { Console.WriteLine("Bad ID."); break; }
                    var st4 = _students.FirstOrDefault(x => x.Id == aid);
                    if (st4 == null) { Console.WriteLine("Not found."); break; }
                    double avg4 = st4.Grades.Count == 0 ? 0.0 : st4.Grades.Sum() / (double)st4.Grades.Count;
                    Console.WriteLine($"Average of {st4.Name} = {avg4:0.00}");
                    break;

                case "5":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int fid)) { Console.WriteLine("Bad ID."); break; }
                    var st5 = _students.FirstOrDefault(x => x.Id == fid);
                    if (st5 == null) { Console.WriteLine("Not found."); break; }
                    double avg5 = st5.Grades.Count == 0 ? 0.0 : st5.Grades.Sum() / (double)st5.Grades.Count;
                    Console.WriteLine($"  [{st5.Id}] {st5.Name} ({st5.GroupCode})  email={st5.Email}  avg={avg5:0.00}");
                    Console.WriteLine($"  Grades: [{string.Join(", ", st5.Grades)}]");
                    break;

                case "6":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int vid)) { Console.WriteLine("Bad ID."); break; }
                    var st6 = _students.FirstOrDefault(x => x.Id == vid);
                    if (st6 == null) { Console.WriteLine("Not found."); break; }
                    var errors = new List<string>();
                    if (string.IsNullOrWhiteSpace(st6.Name)) errors.Add("Name empty");
                    if (!st6.Email.Contains('@') || !st6.Email.Contains('.')) errors.Add("Bad email");
                    if (_groups.All(g => g.Code != st6.GroupCode)) errors.Add("Unknown group");
                    foreach (var gr in st6.Grades)
                        if (gr < 1 || gr > 10) { errors.Add($"Grade {gr} out of range"); break; }

                    if (errors.Count == 0) Console.WriteLine($"{st6.Name} — OK");
                    else Console.WriteLine($"{st6.Name} — ERRORS: {string.Join("; ", errors)}");
                    break;

                // =========================================================
                // LT: zemiau — TASK 2 zona. Visi trys punktai parasyti TIK su
                //     LINQ. Jusu CW-1-after kiekvienai funkcijai privalo but
                //     ANTRA versija be LINQ (tik for/foreach/if).
                // EN: below — TASK 2 zone. All three items use ONLY LINQ.
                //     In CW-1-after each of these must also have a no-LINQ
                //     version using just for/foreach/if.
                // =========================================================

                case "7":
                    // LT: TOP 3 pagal vidurki — LINQ grandine
                    // EN: TOP 3 by average — LINQ chain
                    Console.WriteLine("--- Top 3 by average (LINQ) ---");
                    var top3 = _students
                        .Select(s => new { Student = s, Avg = s.Grades.Count == 0 ? 0.0 : s.Grades.Average() })
                        .OrderByDescending(x => x.Avg)
                        .Take(3)
                        .ToList();
                    foreach (var x in top3)
                        Console.WriteLine($"  {x.Student.Name,-25} avg={x.Avg:0.00}");
                    break;

                case "8":
                    // LT: studentai grupeje, surusiuoti pagal varda — LINQ Where + OrderBy
                    // EN: students in a group, sorted by name — LINQ Where + OrderBy
                    Console.Write("Group code / Grupes kodas (pvz. PI23): ");
                    var gc = Console.ReadLine() ?? "";
                    Console.WriteLine($"--- Students in {gc}, sorted by name (LINQ) ---");
                    var inGroup = _students
                        .Where(s => s.GroupCode == gc)
                        .OrderBy(s => s.Name)
                        .ToList();
                    if (inGroup.Count == 0) Console.WriteLine("  (none)");
                    foreach (var s in inGroup)
                    {
                        double avg8 = s.Grades.Count == 0 ? 0.0 : s.Grades.Average();
                        Console.WriteLine($"  [{s.Id}] {s.Name,-25} avg={avg8:0.00}");
                    }
                    break;

                case "9":
                    // LT: statistika — Count, Average, Sum, Max, Any, All (LINQ agregavimas)
                    // EN: statistics — Count, Average, Sum, Max, Any, All (LINQ aggregation)
                    Console.WriteLine("--- Statistics (LINQ) ---");
                    int totalStudents     = _students.Count;
                    int totalGrades       = _students.Sum(s => s.Grades.Count);
                    double meanOfMeans    = _students.Average(s => s.Grades.Count == 0 ? 0.0 : s.Grades.Average());
                    int    maxGrade       = _students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max();
                    bool   hasFailing     = _students.Any(s => s.Grades.Any(g => g < 5));
                    bool   allHaveEmail   = _students.All(s => !string.IsNullOrWhiteSpace(s.Email));
                    Console.WriteLine($"  Total students : {totalStudents}");
                    Console.WriteLine($"  Total grades   : {totalGrades}");
                    Console.WriteLine($"  Mean of averages : {meanOfMeans:0.00}");
                    Console.WriteLine($"  Max grade      : {maxGrade}");
                    Console.WriteLine($"  Any failing (<5)? {hasFailing}");
                    Console.WriteLine($"  All have email?  {allHaveEmail}");
                    break;

                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
    }
}
