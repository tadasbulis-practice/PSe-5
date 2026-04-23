class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student { Name = "Malek", Grades = new List<int> { 8, 9, 10 } },
            new Student { Name = "John", Grades = new List<int> { 6, 7, 8 } }
        };

        IStudentPrinter printer = new DetailedStudentPrinter();
        IAverageStrategy average = new SimpleAverage();
        IMenuService menu = new FancyMenu();
        IStudentFinder finder = new CaseInsensitiveSearch();
        IStudentValidator validator = new StrictValidator();

        menu.ShowMenu();

        foreach (var s in students)
        {
            if (validator.Validate(s))
            {
                printer.Print(s);
                Console.WriteLine("Average: " + average.Calculate(s.Grades));
            }
        }

        var found = finder.Find(students, "malek");
        if (found != null)
            Console.WriteLine("Found: " + found.Name);
    }
}
