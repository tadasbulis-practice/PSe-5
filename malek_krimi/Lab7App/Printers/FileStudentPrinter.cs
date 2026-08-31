public class FileStudentPrinter : IStudentPrinter
{
    public void PrintStudents(List<Student> students)
    {
        var lines = students
            .Select(s => $"{s.Id} - {s.Name} -> {string.Join(", ", s.Grades)}")
            .ToList();

        File.WriteAllLines("students.txt", lines);
        Console.WriteLine("Students saved to students.txt");
    }
}
