using JohnBastille.Lab_3;
using JohnBastille.Lab_3.Interfaces;

Console.WriteLine("===== LAB 3 =====");

// Determine which services to use based on command line arguments
// This demonstrates logical branches: production vs test vs alternative modes
ApplicationServices services;

if (args.Length > 0 && args[0] == "test")
{
    Console.WriteLine("Running in TEST mode with fake/stub implementations");
    services = ServiceFactory.CreateTestServices();
}
else if (args.Length > 0 && args[0] == "alternative")
{
    Console.WriteLine("Running in ALTERNATIVE mode with partial name finder");
    services = ServiceFactory.CreateAlternativeServices();
}
else
{
    Console.WriteLine("Running in PRODUCTION mode");
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


