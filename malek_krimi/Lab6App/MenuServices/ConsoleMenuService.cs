public class ConsoleMenuService : IMenuService
{
    public void ShowMenu()
    {
        Console.WriteLine("=== MAIN MENU ===");
        Console.WriteLine("1. Show students");
        Console.WriteLine("2. Exit");
    }
}