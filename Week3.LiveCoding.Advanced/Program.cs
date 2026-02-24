
using Week3.LiveCoding.Advanced.Models;
using Week3.LiveCoding.Advanced.Services;
using Week3.LiveCoding.Advanced.Strategies;

Console.WriteLine("=== Week 3 Advanced Demo ===");

Group group = new("PS-5");

// Switch between strategies:
// IStudentService service = new StudentService(new AverageStrategy());
IStudentService service = new StudentService(new WeightedAverageStrategy());

service.AddStudent(group, new Student(1, "Jonas", new List<int> {8, 9, 10}));
service.AddStudent(group, new Student(2, "Ona", new List<int> {6, 7, 8}));

service.PrintAll(group);

Console.WriteLine("Done.");
