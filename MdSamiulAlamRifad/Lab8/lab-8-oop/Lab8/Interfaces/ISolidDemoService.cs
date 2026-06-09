namespace Lab8.Interfaces;

// SOLID — Interface Segregation Principle (ISP)
// Instead of one fat interface with 10 methods that every class must implement,
// we keep interfaces small and focused.
// IMenuService → just Run()
// IStudentPrinter → just printing
// IStudentValidator → just validating
// IStudentRepository → just data access
// ISolidDemoService → just the SOLID demo flow
public interface ISolidDemoService
{
    void RunSolidDemo();
}
