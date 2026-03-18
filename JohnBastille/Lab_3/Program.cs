using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Services;

Console.WriteLine("===== LAB 3 =====");

// Shared student list
List<Student> students = new List<Student>();

// Choose strategy implementations
IStudentFinder finder = new ExactNameFinder();               // or PartialNameFinder
IStudentPrinter printer = new SimpleStudentPrinter();        // one implementation is fine
IStudentValidator validator = new BasicStudentValidator();   // or StrictStudentValidator
IAverageStrategy averageStrategy = new SimpleAverageStrategy(); // or WeightedAverageStrategy

// Inject everything into the menu
IMenuService menu = new ConsoleMenuService(
    students,
    finder,
    printer,
    validator,
    averageStrategy
);

// Main loop
while (true)
{
    menu.ShowMainMenu();
    int choice = menu.GetMenuChoice();
    menu.ExecuteChoice(choice);
}


