using Lab5.Interfaces;
using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab5.Implementations.Printer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    public class JsonStudentPrinter : IStudentPrinter
    {
        private readonly string _path;

        public JsonStudentPrinter(string path)
        {
            _path = path;
        }

        public void PrintStudents(List<Student> students)
        {
            var json = JsonSerializer.Serialize(students, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_path, json);
        }
    }
}
