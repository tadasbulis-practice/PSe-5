using System;
using System.Collections.Generic;
using CW1.Models;
using CW1.Services;

namespace CW1.UI;

public class ConsoleMenu
{
    private readonly StudentService _studentService;
    private readonly ReportService _reportService;

    // Constructor Injection: tum priklausomybes gaunamos is isores (Task 1 & Task 3)
    public ConsoleMenu(StudentService studentService, ReportService reportService)
    {
        _studentService = studentService;
        _reportService = reportService;
    }

    public void Show()
    {
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
            Console.WriteLine(" 7) Students in group, sorted by name  [LINQ / Plain]");
            Console.WriteLine(" 8) Top N students by average          [LINQ / Plain]");
            Console.WriteLine(" 9) Statistics                         [LINQ / Plain]");
            Console.WriteLine(" 0) Exit               /  Iseiti");
            Console.Write("Choice / Pasirinkimas: ");

            var choice = Console.ReadLine();

            if (choice == "0")
            {
                Console.WriteLine("Bye!");
                return;
            }

            switch (choice)
            {
                case "1":
                    ListAllStudents();
                    break;
                case "2":
                    AddNewStudent();
                    break;
                case "3":
                    AddGrade();
                    break;
                case "4":
                    ShowAverage();
                    break;
                case "5":
                    FindById();
                    break;
                case "6":
                    ValidateStudent();
                    break;
                case "7":
                    HandleStudentsInGroup();
                    break;
                case "8":
                    HandleTopStudents();
                    break;
                case "9":
                    HandleStatistics();
                    break;
                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
    }

    private void ListAllStudents()
    {
        var students = _studentService.GetAllStudents();
        if (students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }
        foreach (var s in students)
        {
            Console.WriteLine(_studentService.FormatStudent(s));
        }
    }

    private void AddNewStudent()
    {
        try
        {
            Console.Write("New ID / Naujas ID: ");
            if (!int.TryParse(Console.ReadLine(), out int newId)) { Console.WriteLine("Bad ID."); return; }

            if (_studentService.FindById(newId) != null) { Console.WriteLine("ID exists."); return; }

            Console.Write("Name / Vardas: ");
            var newName = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            var newEmail = Console.ReadLine() ?? "";
            Console.Write("Group code / Grupes kodas: ");
            var newGroup = Console.ReadLine() ?? "";

            var newStudent = new Student
            {
                Id = newId,
                Name = newName,
                Email = newEmail,
                GroupCode = newGroup,
                Grades = new List<int>()
            };

            if (!_studentService.ValidateStudent(newStudent, out string error))
            {
                Console.WriteLine($"Validation failed: {error}");
                return;
            }

            var groups = _studentService.GetAllGroups();
            if (!groups.Exists(g => g.Code.Equals(newGroup, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Group not found.");
                return;
            }

            _studentService.AddStudent(newStudent);
            Console.WriteLine("Student added.");
        }
        catch (NotSupportedException ex)
        {
            // Task 3.1: stub repository kullanilirken Add() cagrisi NotSupportedException firlatabilir
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
    }

    private void AddGrade()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int gid)) { Console.WriteLine("Bad ID."); return; }

        var student = _studentService.FindById(gid);
        if (student == null) { Console.WriteLine("Not found."); return; }

        Console.Write("Grade (1..10): ");
        if (!int.TryParse(Console.ReadLine(), out int grade)) { Console.WriteLine("Bad grade."); return; }
        if (grade < 1 || grade > 10) { Console.WriteLine("Out of range."); return; }

        student.Grades.Add(grade);
        Console.WriteLine($"Added {grade} to {student.Name}.");
    }

    private void ShowAverage()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int aid)) { Console.WriteLine("Bad ID."); return; }

        var student = _studentService.FindById(aid);
        if (student == null) { Console.WriteLine("Not found."); return; }

        double avg = _studentService.CalculateAverage(student.Grades);
        Console.WriteLine($"Average of {student.Name} = {avg:0.00}");
    }

    private void FindById()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int fid)) { Console.WriteLine("Bad ID."); return; }

        var student = _studentService.FindById(fid);
        if (student == null) { Console.WriteLine("Not found."); return; }

        Console.WriteLine(_studentService.FormatStudent(student));
    }

    private void ValidateStudent()
    {
        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int vid)) { Console.WriteLine("Bad ID."); return; }

        var student = _studentService.FindById(vid);
        if (student == null) { Console.WriteLine("Not found."); return; }

        if (_studentService.ValidateStudent(student, out string error))
        {
            Console.WriteLine($"{student.Name} — OK");
        }
        else
        {
            Console.WriteLine($"{student.Name} — ERROR: {error}");
        }
    }

    // Task 2: leidziame paleisti TIEK LINQ, TIEK Plain versija is to paties meniu —
    // taip lengva akimirksniu palyginti, ar abi versijos grazina ta pati rezultata.
    private bool AskForLinq()
    {
        Console.Write("Run with LINQ? (y/n): ");
        var mode = Console.ReadLine()?.Trim().ToLower();
        return mode == "y" || mode == "yes";
    }

    private void HandleStudentsInGroup()
    {
        Console.Write("Group code / Grupes kodas (e.g., PI23): ");
        var gc = Console.ReadLine() ?? "";
        bool useLinq = AskForLinq();

        List<Student> result;
        if (useLinq)
        {
            Console.WriteLine($"--- Students in {gc}, sorted by name (LINQ) ---");
            result = _reportService.GetStudentsInGroupSortedByName(gc);
        }
        else
        {
            Console.WriteLine($"--- Students in {gc}, sorted by name (Loops - No LINQ) ---");
            result = _reportService.GetStudentsInGroupSortedByNameWithoutLinq(gc);
        }

        if (result.Count == 0) Console.WriteLine("  (none)");
        foreach (var s in result)
        {
            Console.WriteLine(_studentService.FormatStudent(s));
        }
    }

    private void HandleTopStudents()
    {
        Console.Write("How many top students (N)? / Kiek top studentu (N)? ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Invalid N, using default 3.");
            n = 3;
        }

        bool useLinq = AskForLinq();
        List<Student> result;

        if (useLinq)
        {
            Console.WriteLine($"--- Top {n} students by average (LINQ) ---");
            result = _reportService.GetTopByAverage(n);
        }
        else
        {
            Console.WriteLine($"--- Top {n} students by average (Loops - No LINQ) ---");
            result = _reportService.GetTopByAverageWithoutLinq(n);
        }

        if (result.Count == 0) Console.WriteLine("  (none)");
        foreach (var s in result)
        {
            Console.WriteLine(_studentService.FormatStudent(s));
        }
    }

    private void HandleStatistics()
    {
        bool useLinq = AskForLinq();
        var stats = useLinq ? _reportService.GetStatistics() : _reportService.GetStatisticsWithoutLinq();

        Console.WriteLine(useLinq ? "--- Statistics (LINQ) ---" : "--- Statistics (Plain Loops - No LINQ) ---");
        Console.WriteLine($"  Total students   : {stats.TotalStudents}");
        Console.WriteLine($"  Total grades     : {stats.TotalGrades}");
        Console.WriteLine($"  Mean of averages : {stats.MeanOfAverages:0.00}");
        Console.WriteLine($"  Max grade        : {stats.MaxGrade}");
        Console.WriteLine($"  Any failing (<5)?: {stats.HasFailingGrade}");
        Console.WriteLine($"  All have email?  : {stats.AllHaveEmail}");
    }
}
