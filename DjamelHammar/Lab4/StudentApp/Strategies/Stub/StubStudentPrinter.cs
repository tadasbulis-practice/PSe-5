using StudentApp.Interfaces;
using System;

namespace StudentApp.Strategies.Stub
{
    public class StubStudentPrinter : IStudentPrinter
    {
        public void PrintStudent(string name, int average, bool isValid)
        {
            Console.WriteLine($"[STUB] {name}, Avg={average}, Valid={isValid}");
        }
    }
}