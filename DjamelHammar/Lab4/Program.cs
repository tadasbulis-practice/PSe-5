using System;
using System.Collections.Generic;
using StudentApp.Interfaces;
using StudentApp.Models;
using StudentApp.Services;
using StudentApp.Strategies.Real;
using StudentApp.Strategies.Fake;
using StudentApp.Strategies.Stub;

class Program
{
    static void Main(string[] args)
    {
        // Create a sample group
        var group = new Group
        {
            Students = new List<Student>
            {
                new Student("Djamel", new List<int> { 12, 15, 18 }),
                new Student("Sara", new List<int> { 10, 14, 16 })
            }
        };

        // -------------------------
        // REAL IMPLEMENTATIONS
        // -------------------------
        IStudentPrinter realPrinter = new SimpleStudentPrinter();
        IAverageStrategy realAverage = new StandardAverageStrategy();
        IStudentValidator realValidator = new StrictStudentValidator();
        IStudentFinder realFinder = new LinearStudentFinder();

        var serviceReal = new StudentService(realPrinter, realAverage, realValidator, realFinder);

        Console.WriteLine("=== REAL IMPLEMENTATIONS ===");
        serviceReal.Run(group);

        // -------------------------
        // FAKE IMPLEMENTATIONS
        // -------------------------
        IStudentPrinter fakePrinter = new FakeStudentPrinter();
        IAverageStrategy fakeAverage = new FakeAverageStrategy();
        IStudentValidator fakeValidator = new FakeStudentValidator();
        IStudentFinder fakeFinder = new FakeStudentFinder();

        var serviceFake = new StudentService(fakePrinter, fakeAverage, fakeValidator, fakeFinder);

        Console.WriteLine("\n=== FAKE IMPLEMENTATIONS ===");
        serviceFake.Run(group);

        // -------------------------
        // STUB IMPLEMENTATIONS
        // -------------------------
        var stubAverage = new StubAverageStrategy { Result = 10 };
        var stubValidator = new StubStudentValidator { IsValid = true };

        IStudentPrinter stubPrinter = new StubStudentPrinter();
        IStudentFinder stubFinder = new StubStudentFinder();

        var serviceStub = new StudentService(stubPrinter, stubAverage, stubValidator, stubFinder);

        Console.WriteLine("\n=== STUB IMPLEMENTATIONS - Scenario 1 ===");
        serviceStub.Run(group);

        // Change behavior to demonstrate second scenario
        stubAverage.Result = 5;
        stubValidator.IsValid = false;

        Console.WriteLine("\n=== STUB IMPLEMENTATIONS - Scenario 2 ===");
        serviceStub.Run(group);
    }
}