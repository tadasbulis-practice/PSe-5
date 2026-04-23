using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Services;

namespace JohnBastille.Lab_3;

/// <summary>
/// Composition root for dependency injection.
/// Handles creation and wiring of all dependencies.
/// </summary>
public static class ServiceFactory
{
    /// <summary>
    /// Creates production-ready services for normal application use.
    /// </summary>
    public static ApplicationServices CreateProductionServices()
    {
        var students = new List<Student>();
        var finder = new ExactNameFinder();
        var printer = new SimpleStudentPrinter();
        var validator = new BasicStudentValidator();
        var averageStrategy = new SimpleAverageStrategy();

        var menu = new ConsoleMenuService(students, finder, printer, validator, averageStrategy);

        return new ApplicationServices(students, menu, finder, printer, validator, averageStrategy);
    }

    /// <summary>
    /// Creates test services with fake/stub implementations for testing.
    /// </summary>
    public static ApplicationServices CreateTestServices()
    {
        var students = new List<Student>();

        // Create a fake student for testing
        var fakeAverageStrategy = new StubAverageStrategy(85.5);
        var fakeStudent = new Student(fakeAverageStrategy)
        {
            Name = "Test Student",
            Age = 20
        };
        fakeStudent.Grades.AddRange(new[] { 80, 85, 90 });

        var finder = new FakeStudentFinder(fakeStudent);
        var printer = new StubStudentPrinter();
        var validator = new FakeStudentValidator(true, 25); // Always valid, fixed age 25
        var averageStrategy = new StubAverageStrategy(87.5);

        var menu = new ConsoleMenuService(students, finder, printer, validator, averageStrategy);

        return new ApplicationServices(students, menu);
    }

    /// <summary>
    /// Creates services with debug menu for development and testing.
    /// </summary>
    public static ApplicationServices CreateDebugServices()
    {
        var students = new List<Student>();
        var finder = new ExactNameFinder();
        var printer = new SimpleStudentPrinter();
        var validator = new BasicStudentValidator();
        var averageStrategy = new SimpleAverageStrategy();

        var menu = new DebugMenuService(students, finder, printer, validator, averageStrategy);

        return new ApplicationServices(students, menu, finder, printer, validator, averageStrategy);
    }

    /// <summary>
    /// Creates services with alternative implementations to demonstrate logical branches.
    /// </summary>
    public static ApplicationServices CreateAlternativeServices()
    {
        var students = new List<Student>();
        var finder = new PartialNameFinder(); // Different finder implementation
        var printer = new SimpleStudentPrinter();
        var validator = new BasicStudentValidator();
        var averageStrategy = new SimpleAverageStrategy();

        var menu = new AlternativeMenuService(students, finder, printer, validator, averageStrategy);

        return new ApplicationServices(students, menu);
    }
}

/// <summary>
/// Container for application services with single implementations.
/// </summary>
public record ApplicationServices(
    List<Student> Students,
    IMenuService MenuService,
    IStudentFinder Finder,
    IStudentPrinter Printer,
    IStudentValidator Validator,
    IAverageStrategy AverageStrategy);