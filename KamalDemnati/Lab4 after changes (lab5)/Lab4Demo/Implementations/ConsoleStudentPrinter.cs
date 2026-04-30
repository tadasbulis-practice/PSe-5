using Lab4Demo.Interfaces;
using Lab4Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Implementations
{
    public class ConsoleStudentPrinter : IStudentPrinter
    {
        public void Print(Student s)
            => Console.WriteLine($"{s.Name}");
    }
}
