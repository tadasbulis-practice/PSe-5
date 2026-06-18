using Lab7.App.Implementations;
using Lab7.App.Interfaces;
using Lab7.App.Models;
using Lab7.App.Services;

class Program
{
    static void Main(string[] args)
    {
        // Composition root: the ONLY place that knows about concrete classes.
        // Flip useApi to true once `docker compose up` is running for
        // SimpleStudentApi — StudentService below never has to change.
        const bool useApi = false;
        const string apiBaseUrl = "http://localhost:6001";

        IStudentRepository repo;

        if (useApi)
        {
            try
            {
                var apiRepo = new ApiStudentRepository(apiBaseUrl);
                _ = apiRepo.GetAll(); // fail fast here if the API isn't reachable
                repo = apiRepo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API unavailable ({ex.Message}). Falling back to MemoryStudentRepository.\n");
                repo = new MemoryStudentRepository();
            }
        }
        else
        {
            repo = new MemoryStudentRepository();
        }

        var printer = new ConsoleStudentPrinter();
        var strategy = new SimpleAverageStrategy();
        var validator = new BasicStudentValidator();

        if (repo is MemoryStudentRepository)
        {
            // Seed sample data only for the in-memory fallback —
            // the Api repository already gets data from SimpleStudentApi.
            repo.Add(new Student(1, "Ali", "ali@test.com", new List<int> { 8, 9, 10 }, "IF-21"));
            repo.Add(new Student(2, "Ayse", "ayse@test.com", new List<int> { 7, 6, 8 }, "IF-21"));
            repo.Add(new Student(3, "Mehmet", "mehmet@test.com", new List<int> { 10, 10, 9 }, "IF-22"));
        }

        var service = new StudentService(repo, printer, strategy, validator);
        service.Run();
    }
}
