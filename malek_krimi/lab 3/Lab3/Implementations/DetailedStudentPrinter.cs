public class DetailedStudentPrinter : IStudentPrinter
{
    public void Print(Student student)
    {
        Console.WriteLine("----- STUDENT -----");
        Console.WriteLine($"Name: {student.Name}");
        Console.WriteLine($"Grades: {string.Join(",", student.Grades)}");
    }
}