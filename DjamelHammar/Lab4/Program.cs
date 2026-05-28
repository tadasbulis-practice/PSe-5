class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student { Name = "Malek", Grades = new List<int> { 8, 9, 10 } },
            new Student { Name = "John", Grades = new List<int> { 6, 7, 8 } }
        };

        IMenuService menu = new FancyMenu();
        IStudentPrinter printer = new DetailedStudentPrinter();
        IAverageStrategy averageStrategy = new SimpleAverage();
        IStudentFinder finder = new CaseInsensitiveSearch();
        IStudentValidator validator = new StrictValidator();

        menu.ShowMenu();
        Console.WriteLine();

        foreach (var student in students)
        {
            if (validator.Validate(student))
            {
                printer.Print(student);
                Console.WriteLine($"Average: {averageStrategy.Calculate(student.Grades):F2}");
                Console.WriteLine();
            }
        }

        var found = finder.Find(students, "malek");
        if (found != null)
        {
            Console.WriteLine("Search result:");
            printer.Print(found);
        }
    }
}
