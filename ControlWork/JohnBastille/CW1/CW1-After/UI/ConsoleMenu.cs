
using CW1After.Interfaces;
using CW1After.Models;
using CW1After.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CW1After.UI;
public class ConsoleMenu
{
    private readonly IStudentService _studentService;
    private readonly ReportService _reportService;
    private readonly StudentValidator _validator;


    public ConsoleMenu(IStudentService studentService, ReportService reportService, StudentValidator validator)
    {
        _studentService = studentService;
        _reportService = reportService;
        _validator = validator;
    }





    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========== CW-1 Student Menu ==========");
            Console.WriteLine(" 1) List all students");
            Console.WriteLine(" 2) Add new student");
            Console.WriteLine(" 3) Add grade");
            Console.WriteLine(" 4) Show average");
            Console.WriteLine(" 5) Find by id");
            Console.WriteLine(" 6) Validate student");
            Console.WriteLine(" 7) Top 3 by average   [LINQ]");
            Console.WriteLine(" 8) Students in group  [LINQ]");
            Console.WriteLine(" 9) Statistics         [LINQ]");
            Console.WriteLine(" 0) Exit");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            if (choice == "0") { Console.WriteLine("Bye!"); return; }

            switch (choice)
            {
                case "1":
                    foreach (var s in _studentService.ListAll())
                    {
                        double avg = _studentService.GetAverage(s);
                        Console.WriteLine($"  [{s.Id}] {s.Name} ({s.GroupCode})  email={s.Email}  avg={avg:0.00}");
                    }
                    break;

                case "2":
                    Console.Write("New ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int newId)) { Console.WriteLine("Bad ID."); break; }
                    Console.Write("Name: ");
                    var newName = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    var newEmail = Console.ReadLine() ?? "";
                    Console.Write("Group code: ");
                    var newGroup = Console.ReadLine() ?? "";
                    var newStudent = new Student { Id = newId, Name = newName, Email = newEmail, GroupCode = newGroup };
                    var (ok, errors) = _studentService.AddStudent(newStudent);
                    if (ok) Console.WriteLine("Student added.");
                    else Console.WriteLine($"Failed: {string.Join("; ", errors)}");
                    break;

                case "3":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int gid)) { Console.WriteLine("Bad ID."); break; }
                    Console.Write("Grade (1..10): ");
                    if (!int.TryParse(Console.ReadLine(), out int grade)) { Console.WriteLine("Bad grade."); break; }
                    if (_studentService.AddGrade(gid, grade)) Console.WriteLine("Grade added.");
                    else Console.WriteLine("Failed to add grade.");
                    break;

                case "4":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int aid)) { Console.WriteLine("Bad ID."); break; }
                    var st4 = _studentService.FindById(aid);
                    if (st4 == null) { Console.WriteLine("Not found."); break; }
                    double avg4 = _studentService.GetAverage(st4);
                    Console.WriteLine($"Average of {st4.Name} = {avg4:0.00}");
                    break;

                case "5":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int fid)) { Console.WriteLine("Bad ID."); break; }
                    var st5 = _studentService.FindById(fid);
                    if (st5 == null) { Console.WriteLine("Not found."); break; }
                    double avg5 = _studentService.GetAverage(st5);
                    Console.WriteLine($"  [{st5.Id}] {st5.Name} ({st5.GroupCode})  email={st5.Email}  avg={avg5:0.00}");
                    Console.WriteLine($"  Grades: [{string.Join(", ", st5.Grades)}]");
                    break;

                case "6":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int vid)) { Console.WriteLine("Bad ID."); break; }
                    var st6 = _studentService.FindById(vid);
                    if (st6 == null) { Console.WriteLine("Not found."); break; }
                    var errors6 = _validator.Validate(st6);
                    if (errors6.Count == 0) Console.WriteLine($"{st6.Name} — OK");
                    else Console.WriteLine($"{st6.Name} — ERRORS: {string.Join("; ", errors6)}");
                    break;


                case "7":
                    Console.WriteLine("--- Top 3 by average (LINQ) ---");
                    foreach (var x in _reportService.Top3ByAverage_Linq())
                        Console.WriteLine($"  {x.Student.Name,-25} avg={x.Avg:0.00}");
                    break;

                case "8":
                    Console.Write("Group code (e.g. PI23): ");
                    var gc = Console.ReadLine() ?? "";
                    Console.WriteLine($"--- Students in {gc}, sorted by name (LINQ) ---");
                    var inGroup = _reportService.StudentsInGroup_Linq(gc).ToList();
                    if (inGroup.Count == 0) Console.WriteLine("  (none)");
                    foreach (var s in inGroup)
                    {
                        double avg = _studentService.GetAverage(s);
                        Console.WriteLine($"  [{s.Id}] {s.Name,-25} avg={avg:0.00}");
                    }
                    break;

                case "9":
                    Console.WriteLine("--- Statistics (LINQ) ---");
                    var stats = _report_service_stats();
                    Console.WriteLine($"  Total students : {stats.TotalStudents}");
                    Console.WriteLine($"  Total grades   : {stats.TotalGrades}");
                    Console.WriteLine($"  Mean of averages : {stats.MeanOfAverages:0.00}");
                    Console.WriteLine($"  Max grade      : {stats.MaxGrade}");
                    Console.WriteLine($"  Any failing (<5)? {stats.HasFailing}");
                    Console.WriteLine($"  All have email?  {stats.AllHaveEmail}");
                    break;

                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
    }

    private (int TotalStudents, int TotalGrades, double MeanOfAverages, int MaxGrade, bool HasFailing, bool AllHaveEmail) _report_service_stats()
    {
        return _reportService.Statistics_Linq();
    }
}