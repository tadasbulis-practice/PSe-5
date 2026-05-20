using Lab3.Models;

Group group = new("PS-5");
group.Students.Add(new Student(1, "indre", new List<int> { 8, 9, 10 }));
group.Students.Add(new Student(2, "Migle", new List<int> { 6, 7, 8 }));

// Choose implementation here:
IStudentPrinter printer = new ConsoleStudentPrinter();
// OR
// IStudentPrinter printer = new FancyStudentPrinter();

printer.Print(group);
