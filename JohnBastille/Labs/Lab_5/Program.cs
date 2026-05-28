using JohnBastille.Lab_3;
using JohnBastille.Lab_3.Interfaces;

Console.WriteLine("===== LAB 3 =====");

// Determine which services to use based on command line arguments
// Demonstrates different menu service implementations
ApplicationServices services;

if (args.Length > 0)
{
    switch (args[0].ToLower())
    {
        case "debug":
            Console.WriteLine("🔧 Running in DEBUG mode with DebugMenuService");
            services = ServiceFactory.CreateDebugServices();
            break;
        case "web":
            Console.WriteLine("🌐 Running in WEB SIMULATION mode with WebMenuSimulationService");
            services = ServiceFactory.CreateWebSimulationServices();
            break;
        case "alternative":
            Console.WriteLine("🔄 Running in ALTERNATIVE mode with AlternativeMenuService");
            services = ServiceFactory.CreateAlternativeServices();
            break;
        case "test":
            Console.WriteLine("🧪 Running in TEST mode with fake/stub implementations");
            services = ServiceFactory.CreateTestServices();
            break;
        default:
            Console.WriteLine("📋 Running in PRODUCTION mode with ConsoleMenuService");
            services = ServiceFactory.CreateProductionServices();
            break;
    }
}
else
{
    Console.WriteLine("📋 Running in PRODUCTION mode with ConsoleMenuService");
    services = ServiceFactory.CreateProductionServices();
}

// Main application loop - no business logic here, just orchestration
IMenuService menu = services.MenuService;
while (true)
{
    menu.ShowMainMenu();
    int choice = menu.GetMenuChoice();
    menu.ExecuteChoice(choice);
}


