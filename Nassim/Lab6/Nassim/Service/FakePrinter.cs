using System;
using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public class FakePrinter : IStudentPrinter
    {
        public void PrintStudents(List<Student> students)
        {
            Console.WriteLine("[FAKE] Students count: " + students.Count);
        }
    }
}