using System;

class Program
{
    static void Main(string[] args)
    {
        IStudentRepository repository = new MemoryStudentRepository();
        IStudentPrinter printer = new ConsoleStudentPrinter();
        IAverageStrategy averageStrategy = new SimpleAverageStrategy();
        IStudentValidator validator = new StudentValidatorAdapter(new LegacyStudentValidation());

        if (args.Length > 0)
        {
            if (args[0].ToLower() == "file")
                repository = new FileStudentRepository();
            else if (args[0].ToLower() == "api")
                repository = new ApiStudentRepository();
        }

        if (args.Length > 1)
        {
            if (args[1].ToLower() == "file")
                printer = new FileStudentPrinter();
            else if (args[1].ToLower() == "json")
                printer = new JsonStudentPrinter();
        }

        if (args.Length > 2)
        {
            if (args[2].ToLower() == "weighted")
                averageStrategy = new WeightedAverageStrategy();
            else if (args[2].ToLower() == "median")
                averageStrategy = new MedianAverageStrategy();
        }

        if (args.Length > 3)
        {
            if (args[3].ToLower() == "strict")
                validator = new StrictStudentValidator();
            else if (args[3].ToLower() == "flex")
                validator = new FlexibleStudentValidator();
        }

        StudentService studentService = new StudentService(
            repository,
            printer,
            averageStrategy,
            validator
        );

        IMenuService menuService = new ConsoleMenuService(studentService);

        if (args.Length > 4)
        {
            if (args[4].ToLower() == "debug")
                menuService = new DebugMenuService(studentService);
            else if (args[4].ToLower() == "web")
                menuService = new WebMenuSimulationService(studentService);
        }

        menuService.Run();
    }
}