public class WebMenuSimulationService : IMenuService
{
    public void ShowMenu()
    {
        Console.WriteLine("<web-menu>");
        Console.WriteLine("  <button>Show students</button>");
        Console.WriteLine("  <button>Exit</button>");
        Console.WriteLine("</web-menu>");
    }
}