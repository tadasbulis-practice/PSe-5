class Program
{
    static void Main()
    {
        IMenuService menuService = new ConsoleMenuService();
        IRepository<Student> repository = new MemoryRepository<Student>();
        IStudentPrinter printer = new ConsoleStudentPrinter();
        IStudentValidator validator = new StudentValidator();
        IAverageStrategy averageStrategy = new MedianAverageStrategy();

        var studentService = new StudentService(
            repository,
            printer,
            validator,
            averageStrategy
        );

        studentService.AddStudent(new Student { Id = 1, Name = "Malek", Grades = new List<int> { 8, 9, 10 } });
        studentService.AddStudent(new Student { Id = 2, Name = "John", Grades = new List<int> { 6, 7, 8 } });
        studentService.AddStudent(new Student { Id = 3, Name = "Sara", Grades = new List<int> { 10, 9, 9 } });
        studentService.AddStudent(new Student { Id = 4, Name = "", Grades = new List<int> { 5, 6 } });

        menuService.ShowMenu();
        Console.WriteLine();

        studentService.PrintAllStudents();
        Console.WriteLine();
        Console.WriteLine($"Group average: {studentService.CalculateGroupAverage():F2}");
    }
}
