class Program
{
    static void Main()
    {
        IMenuService menuService = new ConsoleMenuService();
        IStudentPrinter printer = new JsonStudentPrinter();
        IAverageStrategy averageStrategy = new MedianAverage();
        IStudentRepository repository = new MemoryStudentRepository();

        var legacyValidator = new LegacyStudentValidationSystem();
        IStudentValidator validator = new StudentValidatorAdapter(legacyValidator);

        var studentService = new StudentService(
            repository,
            printer,
            validator,
            averageStrategy
        );

        menuService.ShowMenu();
        Console.WriteLine();

        studentService.AddStudent(new Student { Id = 1, Name = "Malek", Grades = new List<int> { 8, 9, 10 } });
        studentService.AddStudent(new Student { Id = 2, Name = "John", Grades = new List<int> { 6, 7, 8 } });
        studentService.AddStudent(new Student { Id = 3, Name = "Sara", Grades = new List<int> { 10, 10, 9 } });
        studentService.AddStudent(new Student { Id = 4, Name = "", Grades = new List<int> { 5, 6 } });

        studentService.PrintAllStudents();
        Console.WriteLine();
        studentService.ShowGroupAverage();
    }
}
