using Lab3.Models;

Group group = new("PSe-5");
group.Students.Add(new Student(1, "Jonas", new List<int> { 8, 9, 10 }));
group.Students.Add(new Student(2, "Ona", new List<int> { 6, 7, 8 }));

// Choose ONE implementation:

// Real printers:
// IStudentPrinter printer = new ConsoleStudentPrinter();
// IStudentPrinter printer = new FancyStudentPrinter();

// Fake printer:
IStudentPrinter printer = new FakeStudentPrinter();

// Stub printers (two different behaviors):
// IStudentPrinter printer = new StubStudentPrinter("STUB: Students printed successfully!");
// IStudentPrinter printer = new StubStudentPrinter("STUB: ERROR printing students!");

printer.Print(group);
