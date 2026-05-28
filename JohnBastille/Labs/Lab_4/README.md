✅ 1. Dependency Isolation in This Project     LAB 4 
Dependency isolation is achieved by ensuring:

No class creates its own dependencies using new

All dependencies are passed through constructors

All business logic depends on interfaces, not implementations

Program.cs orchestrates only interfaces

✔ Example: Student class
csharp
private readonly IAverageStrategy _averageStrategy;
public Student(IAverageStrategy averageStrategy)
{
    _averageStrategy = averageStrategy;
}
The Student class does not know which average strategy is used — Simple, Stub, Fake, etc.

✔ Example: Menu services
Every menu service receives its dependencies through constructor injection:

csharp
public ConsoleMenuService(
    List<Student> students,
    IStudentFinder finder,
    IStudentPrinter printer,
    IStudentValidator validator,
    IAverageStrategy averageStrategy)
No service creates its own finder, validator, or strategy.

✔ Example: Program.cs
Program.cs never creates concrete classes.
It selects a configuration:

csharp
services = ServiceFactory.CreateProductionServices();
IMenuService menu = services.MenuService;
This ensures the main program is completely isolated from implementation details.

✅ 2. Fake Implementations (Imitating Unfinished Modules)
Fake classes return stable, predictable results and allow development even when real modules are missing.

✔ FakeStudentFinder
csharp
public class FakeStudentFinder : IStudentFinder
{
    private readonly Student _fakeStudent;

    public FakeStudentFinder(Student fakeStudent)
    {
        _fakeStudent = fakeStudent;
    }

    public Student? Find(List<Student> students, string query)
    {
        return _fakeStudent;
    }
}
Used for testing search logic without relying on real data.

✔ FakeStudentValidator
Always returns a controlled validation result.

✅ 3. Stub Implementations (Controlled Behavior)
Stubs allow controlling the output to test different branches of business logic.

✔ StubAverageStrategy
csharp
public class StubAverageStrategy : IAverageStrategy
{
    private readonly double _fixedAverage;

    public StubAverageStrategy(double fixedAverage)
    {
        _fixedAverage = fixedAverage;
    }

    public double CalculateAverage(List<int> grades)
    {
        return _fixedAverage;
    }
}
This allows testing:

High average branch

Low average branch

Zero average branch

without modifying business logic.

✔ StubStudentPrinter
Captures output instead of printing to console — useful for automated tests.

✅ 4. Demonstration of Logical Branches
By injecting different Stub values, the system can demonstrate:

Student found / student not found

Valid input / invalid input

High average / low average

Empty student list / populated list

All without modifying the business logic.

✅ 5. Runtime Selection (Challenge Requirement)
The program supports runtime selection of implementations:

Code
dotnet run debug
dotnet run web
dotnet run alternative
dotnet run test
Each mode uses a different combination of:

Real implementations

Fake implementations

Stub implementations

This demonstrates how large systems can switch behavior without changing code.