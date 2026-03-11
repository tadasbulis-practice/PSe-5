using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Services;
using JohnBastille.Lab_3.Interfaces;

Console.WriteLine("=====LAB_3=====");

Group group = new("Program systems");

IStudentService service = new StudentService();

Student student = new(1,"John Bastille");
Student student2 = new(2, "Jack Smith");
Student student3 = new(3, "Harry Ballzonya");


service.AddStudent(group, student);
service.PrintAll(group);

Console.WriteLine("Finished");



