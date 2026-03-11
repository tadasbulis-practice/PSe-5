using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Services;
using JohnBastille.Lab_3.Interfaces;

Console.WriteLine("=====LAB_3=====");

Group group = new("Program systems");

IStudentService service = new StudentService();
IStudentFinder finder = new StudentFinderService();

Student student = new(1,"John Bastille");

Console.WriteLine("Enter New students into the Group. Type 'exit' to stop");

//this allows students to be added to the group list /(NEEDS CLEANUP)
while (true)    
{
    Console.WriteLine("Enter Student Name :   (or exit)");
    string nameInput = Console.ReadLine() ?? "";
    if (nameInput.ToLower() == "exit")
         break;
    Console.WriteLine("Enter Student ID");
    int id = int.Parse(Console.ReadLine());

    Student newStudent = new(id, nameInput);
    service.AddStudent(group, newStudent);
    Console.WriteLine("Student Added\n");
}
service.AddStudent(group, student); 
service.PrintAll(group);

// This logic allows for the student finder to be implemented but it is still quite messy /(NEEDS CLEANUP) 
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







Console.WriteLine("Finished");



