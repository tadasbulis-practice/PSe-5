using Lab5.Interfaces;
using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Implementations.Printer
{
    using System.Collections.Generic;
    using System.IO;

    public class FileStudentPrinter : IStudentPrinter
    {
        private readonly string _path;

        public FileStudentPrinter(string path)
        {
            _path = path;
        }

        public void PrintStudents(List<Student> students)
        {
            using StreamWriter writer = new StreamWriter(_path);

            foreach (var s in students)
            {
                writer.WriteLine($"{s.Id},{s.Name},{s.Grade},{s.Weight}");
            }
        }
    }
}
