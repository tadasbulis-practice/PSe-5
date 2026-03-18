using System;
using StudentApp.Interfaces;

namespace StudentApp.Strategies.Real
{
    public class SimpleStudentPrinter : IStudentPrinter
    {
        public void PrintStudent(string name, int average, bool isValid)
        {
            Console.WriteLine($"{name}: Average = {average}, Valid = {isValid}");
        }
    }
}