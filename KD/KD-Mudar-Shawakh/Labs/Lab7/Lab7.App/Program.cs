using Lab7.App.Implementations.Adapter;
using Lab7.App.Implementations.Menu;
using Lab7.App.Implementations.Printer;
using Lab7.App.Implementations.Repository;
using Lab7.App.Implementations.Strategy;
using Lab7.App.Interfaces;
using Lab7.App.Services;

namespace Lab7.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // =============================================================
            // COMPOSITION ROOT
            // The only place where concrete classes are chosen.
            // Everything below uses interfaces only.
            //
            // To switch from sample data to the real API:
            //   1) docker compose up   (from the Lab7 root folder)
            //   2) set useApi = true   (one line, below)
            // =============================================================

            const string apiUrl = "http://localhost:6001";
            const bool useApi = false;   // <-- flip to true when Docker is running

            // Repository — the ONE line that changes between Memory and Api.
            IStudentRepository repository = useApi
                ? new ApiStudentRepository(apiUrl)
                : new MemoryStudentRepository();

            // All other collaborators stay the same regardless of repository.
            IStudentPrinter   printer   = new ConsoleStudentPrinter();
            IAverageStrategy  strategy  = new SimpleAverageStrategy();
            IStudentValidator validator = new StudentValidatorAdapter();

            // Wire and run.
            var service = new StudentService(repository, printer, strategy, validator);
            IMenuService menu = new ConsoleMenuService(service);
            menu.Run();
        }
    }
}