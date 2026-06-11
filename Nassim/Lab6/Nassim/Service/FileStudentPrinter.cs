using System.Collections.Generic;
using System.IO;

namespace Nassim.Lab4.Nassim.Service
{
    public class FileStudentPrinter : IStudentPrinter
    {
        private readonly string _filePath;

        public FileStudentPrinter(string filePath) => _filePath = filePath;

        public void PrintStudents(List<Student> students)
        {
            using var writer = new StreamWriter(_filePath, append: false);
            foreach (var s in students)
                writer.WriteLine($"[{s.Id}] {s.FirstName} {s.LastName} | {s.Email} | Avg: {s.AverageGrade}");
        }
    }
}