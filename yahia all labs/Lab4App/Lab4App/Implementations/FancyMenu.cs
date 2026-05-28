public class FancyMenu : IMenuService
{
    public void ShowMenu()
    {
        Console.WriteLine("===== MENU =====");
        Console.WriteLine("[1] Show Students");
        Console.WriteLine("[2] Exit");
        Console.WriteLine("================");
    }
}
