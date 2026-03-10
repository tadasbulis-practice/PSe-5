using Lab-3-JohnBastille.Interfaces;

namespace Lab-3-JohnBastille.Strategies;

public class ConsoleMenuService : IMenuService
{
    public int ShowMainMenuAndGetChoice()
    {
        Console.WriteLine();
        Console.WriteLine("=== Student Menu ===");
        Console.WriteLine("1. Add student");
        Console.WriteLine("2. Print students");
        Console.WriteLine("3. Search student");
        Console.WriteLine("4. Exit");
        Console.Write("Choice: ");

        var input = Console.ReadLine();
        return int.TryParse(input, out int choice) ? choice : 0;
    }
}