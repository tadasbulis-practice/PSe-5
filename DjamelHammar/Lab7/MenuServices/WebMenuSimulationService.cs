public class WebMenuSimulationService : IMenuService
{
    public void ShowMenu()
    {
        Console.WriteLine("<menu>");
        Console.WriteLine("  <item>Show students</item>");
        Console.WriteLine("  <item>Calculate average</item>");
        Console.WriteLine("</menu>");
    }
}
