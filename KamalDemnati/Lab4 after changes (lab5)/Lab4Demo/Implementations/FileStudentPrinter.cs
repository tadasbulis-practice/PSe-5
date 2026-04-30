using Lab4Demo.Interfaces;
using Lab4Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Implementations
{
    public class FileStudentPrinter : IStudentPrinter
    {
        public void Print(Student s)
            => File.WriteAllText("student.txt", s.Name);
    }
}
