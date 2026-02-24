using Week3.LiveCoding.Models;
using Week3.LiveCoding.Services;

Console.WriteLine("=== Week 3 Live Coding Demo ===");

Group group = new("PS-5");

// Change implementation here:
// IStudentService service = new StudentService();
IStudentService service = new FakeStudentService();

Student student = new(1, "Jonas");

service.AddStudent(group, student);
service.PrintAll(group);

Console.WriteLine("Done.");
