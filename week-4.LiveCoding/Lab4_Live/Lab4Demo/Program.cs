using Lab4Demo.Models;
using Lab4Demo.Services;

var group = new Group();
group.Add(new Student(1, "Tomas", "tomas@test.com"));

var service = new StudentService();

Console.WriteLine(service.Search(group, "1"));
Console.WriteLine(service.Search(group, "99"));
