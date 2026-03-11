using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Services;

Console.WriteLine("=====LAB_3=====");

Group group = new("Program systems");

IStudentService service = new StudentService();

Student student = new(1,"John Bastille");

service.AddStudent(group, student);
service.PrintAll(group);

Console.WriteLine("Finished");



