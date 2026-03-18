using StudentApp.Interfaces;
using System;

namespace StudentApp.Strategies.Fake
{
    public class FakeStudentPrinter : IStudentPrinter
    {
        public void PrintStudent(string name, int average, bool isValid)
        {
            Console.WriteLine($"[FAKE PRINT] {name}");
        }
    }
}