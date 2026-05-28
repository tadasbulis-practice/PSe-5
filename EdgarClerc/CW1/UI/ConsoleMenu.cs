using CW1.Models;
using CW1.Services;

namespace CW1.UI;

public class ConsoleMenu
{
    private readonly StudentService _service;

    public ConsoleMenu(StudentService service)
    {
        _service = service;
    }

    public void StartMenu()
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
            Console.WriteLine(" 7) Top 3 by average   /  Top 3 pagal vidurki   [LINQ]");
            Console.WriteLine(" 8) Students in group  /  Studentai grupeje     [LINQ]");
            Console.WriteLine(" 9) Statistics         /  Statistika            [LINQ]");
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
                    IReadOnlyList<Student> students = _service.GetAll();
                    foreach (var s in students)
                    {
                        double avg = _service.GetAverage(s);
                        Console.WriteLine(s + $" avg={avg}");
                    }

                    break;

                case "2":
                    //add new student
                    Console.Write("New ID / Naujas ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int newId))
                    {
                        Console.WriteLine("Bad ID.");
                        break;
                    }

                    try
                    {
                        _service.GetById(newId);
                        Console.WriteLine("ID already exists.");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Id available");
                    }

                    Console.Write("Name / Vardas: ");
                    var newName = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    var newEmail = Console.ReadLine() ?? "";
                    Console.Write("Group code / Grupes kodas: ");
                    var newGroup = Console.ReadLine() ?? "";

                    Student newStudent = new Student(
                        id: newId,
                        name: newName,
                        email: newEmail,
                        groupCode: newGroup,
                        grades: new()
                    );
                    List<string> newStErr = _service.Validate(newStudent);

                    if (newStErr.Count == 0)
                    {
                        Console.WriteLine($"{newStudent.Name} — OK");
                    }
                    else
                    {
                        Console.WriteLine($"{newStudent.Name} — ERRORS: {string.Join("; ", newStErr)}");
                        break;
                    }

                    _service.Add(
                        new Student(
                            id: newId,
                            name: newName,
                            email: newEmail,
                            groupCode: newGroup,
                            grades: new()
                        )
                    );
                    Console.WriteLine("Student added.");
                    break;

                case "3":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int gid))
                    {
                        Console.WriteLine("Bad ID.");
                        break;
                    }

                    Student st3;
                    try
                    {
                        st3 = _service.GetById(gid);
                    }
                    catch
                    {
                        Console.WriteLine("Student not found.");
                        break;
                    }

                    Console.Write("Grade (1..10): ");
                    if (!int.TryParse(Console.ReadLine(), out int grade))
                    {
                        Console.WriteLine("Bad grade.");
                        break;
                    }

                    if (grade < 1 || grade > 10)
                    {
                        Console.WriteLine("Out of range.");
                        break;
                    }

                    st3.Grades.Add(grade);
                    Console.WriteLine($"Added {grade} to {st3.Name}.");
                    break;

                case "4":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int aid))
                    {
                        Console.WriteLine("Bad ID.");
                        break;
                    }

                    Student st4;
                    try
                    {
                        st4 = _service.GetById(aid);
                    }
                    catch
                    {
                        Console.WriteLine("User Not found.");
                        break;
                    }

                    double avg4 = _service.GetAverage(st4);
                    Console.WriteLine($"Average of {st4.Name} = {avg4:0.00}");
                    break;

                case "5":
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int fid))
                    {
                        Console.WriteLine("Bad ID.");
                        break;
                    }

                    Student st5;
                    try
                    {
                        st5 = _service.GetById(fid);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    double avg5 = _service.GetAverage(st5);
                    Console.WriteLine(_service + $" avg={avg5:0.00}");
                    break;

                case "6":
                    //validate student
                    Console.Write("Student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int vid))
                    {
                        Console.WriteLine("Bad ID.");
                        break;
                    }

                    Student st6;
                    try
                    {
                        st6 = _service.GetById(vid);
                    }
                    catch
                    {
                        Console.WriteLine("User not found");
                        throw;
                    }

                    List<string> errors = _service.Validate(st6);

                    if (errors.Count == 0)
                    {
                        Console.WriteLine($"{st6.Name} — OK");
                    }
                    else
                    {
                        Console.WriteLine($"{st6.Name} — ERRORS: {string.Join("; ", errors)}");
                    }
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
                    var top3 = _service
                    foreach (var x in top3)
                        Console.WriteLine($"  {x.student.Name, -25} avg={x.avg:0.00}");
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
                    if (inGroup.Count == 0)
                        Console.WriteLine("  (none)");
                    foreach (var s in inGroup)
                    {
                        double avg8 = s.Grades.Count == 0 ? 0.0 : s.Grades.Average();
                        Console.WriteLine($"  [{s.Id}] {s.Name, -25} avg={avg8:0.00}");
                    }

                    break;

                case "9":
                    // LT: statistika — Count, Average, Sum, Max, Any, All (LINQ agregavimas)
                    // EN: statistics — Count, Average, Sum, Max, Any, All (LINQ aggregation)
                    Console.WriteLine("--- Statistics (LINQ) ---");
                    IReadOnlyList<Student> students9 = _service.getAll();
                    int totalStudents = students9.Count;
                    int totalGrades = students9.Sum(s => s.Grades.Count);
                    double meanOfMeans = students9.Average(s =>
                        s.Grades.Count == 0 ? 0.0 : s.Grades.Average()
                    );
                    int maxGrade = students9.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max();
                    bool hasFailing = students9.Any(s => s.Grades.Any(g => g < 5));
                    bool allHaveEmail = students9.All(s => !string.IsNullOrWhiteSpace(s.Email));
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
