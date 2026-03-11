using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Services;
using JohnBastille.Lab_3.Interfaces;

Console.WriteLine("=====LAB_3=====");

Group group = new("Program systems");

IStudentService service = new StudentService();
IStudentFinder finder = new StudentFinderService();

Console.WriteLine("Enter Name of Student: \n");
string name = Console.ReadLine() ?? "";
Student? found = finder.FindByName(group, name);
if (found == null)
{
    Console.WriteLine("No student with that name.");
}
else
{
    Console.WriteLine("Found student:");
    Console.WriteLine(found.Describe());
}


Student student = new(1,"John Bastille");
Student student2 = new(2, "Jack Smith");
Student student3 = new(3, "Harry Ballzonya");


service.AddStudent(group, student);
service.PrintAll(group);

Console.WriteLine("Finished");



