public class ConsoleMenuService : IMenuService
{
    public void ShowMenu()
    {
        Console.WriteLine("=== LAB 7 MENU ===");
        Console.WriteLine("1. Show students");
        Console.WriteLine("2. Calculate average");
        Console.WriteLine("3. Exit");
    }
}
