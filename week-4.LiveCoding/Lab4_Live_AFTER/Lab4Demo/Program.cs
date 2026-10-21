using Lab4Demo.Models;
using Lab4Demo.Services;

var group = new Group();
group.Add(new Student(1, "Tomas", "tomas@test.com"));

Console.WriteLine("=== Using Fake ===");
var fakeService = new StudentService(new FakeStudentFinder());
Console.WriteLine(fakeService.Search(group, "anything"));

Console.WriteLine("\n=== Using Stub SUCCESS ===");
var stubSuccess = new StudentService(
    new StubStudentFinder(new Student(2, "Stub Student", "stub@test.com")));
Console.WriteLine(stubSuccess.Search(group, "anything"));

Console.WriteLine("\n=== Using Stub FAIL ===");
var stubFail = new StudentService(
    new StubStudentFinder(null));
Console.WriteLine(stubFail.Search(group, "anything"));
