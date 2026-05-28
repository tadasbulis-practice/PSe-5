using System;

public class AdvancedMenuService : IMenuService
{
    private readonly Group _group;
    private readonly IStudentPrinter _printer;
    private readonly IAverageStrategy _average;
    private readonly IStudentFinder _finder;
    private readonly IStudentValidator _validator;

    public AdvancedMenuService(
        Group group,
        IStudentPrinter printer,
        IAverageStrategy average,
        IStudentFinder finder,
        IStudentValidator validator)
    {
        _group = group;
        _printer = printer;
        _average = average;
        _finder = finder;
        _validator = validator;
    }

    public void Run()
    {
        Console.WriteLine("=== Students ===");
        _printer.Print(_group);

        Console.WriteLine("\n=== Averages ===");
        foreach (var s in _group.Students)
        {
            Console.WriteLine($"{s.Name}: {_average.Calculate(s)}");
        }

        Console.WriteLine("\n=== Search Demo (Find ID 1) ===");
        var found = _finder.Find(_group, "1");
        if (found != null)
            Console.WriteLine($"Found: {found.Name}");

        Console.WriteLine("\n=== Validation Demo ===");
        foreach (var s in _group.Students)
        {
            Console.WriteLine($"{s.Name} valid: {_validator.Validate(s)}");
        }
    }
}