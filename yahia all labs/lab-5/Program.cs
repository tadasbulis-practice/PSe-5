class Program
{
    static void Main()
    {
        IMenuService menuService = new ConsoleMenuService();

        // Runtime switching examples:
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

        studentService.ProcessStudents();
    }
}