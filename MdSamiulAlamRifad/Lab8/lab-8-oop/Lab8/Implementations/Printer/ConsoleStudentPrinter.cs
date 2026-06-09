using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Implementations.Printer;

public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(IReadOnlyList<Student> students)
    {
        if (students.Count == 0) { Console.WriteLine("  (no students)"); return; }

        Console.WriteLine($"\n  {"ID",-5} {"Name",-25} {"Email",-30} {"Program",-20} {"Year"}");
        Console.WriteLine("  " + new string('─', 87));

        foreach (var s in students)
            Console.WriteLine($"  {s.Id,-5} {s.FullName,-25} {s.Email,-30} {s.StudyProgram,-20} {s.EnrollmentYear}");
    }

    public void PrintGroup(Group group)
    {
        Console.WriteLine($"\n  ┌─ [{group.Code}]  {group.StudyProgram}  |  Year: {group.EnrollmentYear}  |  Students: {group.Students.Count}");
        Console.WriteLine($"  │");

        foreach (var s in group.Students)
            Console.WriteLine($"  │  {s.Id,-5} {s.FullName,-25} {s.Email}");

        Console.WriteLine($"  └{'─',50}");
    }

    public void PrintFaculty(Faculty faculty)
    {
        Console.WriteLine($"\n  ╔══ {faculty.Name}");
        Console.WriteLine($"  ║   Groups: {faculty.Groups.Count}   |   Total students: {faculty.TotalStudents}");
        Console.WriteLine($"  ╠{'═',60}");

        foreach (var g in faculty.Groups)
            Console.WriteLine($"  ║   [{g.Code,-8}]  {g.StudyProgram,-25}  {g.EnrollmentYear}   — {g.Students.Count} students");

        Console.WriteLine($"  ╚{'═',60}");
    }
}
