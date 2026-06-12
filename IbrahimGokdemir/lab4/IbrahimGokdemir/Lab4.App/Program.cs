using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Student student = new Student(
            1,
            "Ali",
            "Yilmaz",
            "ali@mail.com",
            new List<int> { 8, 9, 10 }
        );

        IAverageStrategy realStrategy = new SimpleAverageStrategy();
        StudentReportService realReportService = new StudentReportService(realStrategy);
        Console.WriteLine("=== Real Strategy ===");
        Console.WriteLine(realReportService.GetStudentStatus(student));

        IAverageStrategy fakeStrategy = new FakeAverageStrategy();
        StudentReportService fakeReportService = new StudentReportService(fakeStrategy);
        Console.WriteLine("\n=== Fake Strategy ===");
        Console.WriteLine(fakeReportService.GetStudentStatus(student));

        IAverageStrategy stubSuccess = new StubAverageStrategy(9.5);
        StudentReportService stubSuccessService = new StudentReportService(stubSuccess);
        Console.WriteLine("\n=== Stub Strategy (Success Branch) ===");
        Console.WriteLine(stubSuccessService.GetStudentStatus(student));

        IAverageStrategy stubFailure = new StubAverageStrategy(4.0);
        StudentReportService stubFailureService = new StudentReportService(stubFailure);
        Console.WriteLine("\n=== Stub Strategy (Failure Branch) ===");
        Console.WriteLine(stubFailureService.GetStudentStatus(student));

        Console.ReadKey();
    }
}