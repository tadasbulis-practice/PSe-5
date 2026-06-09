using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class FileStudentPrinter : IStudentPrinter
    {
        private readonly string _filePath;

        // Constructor injection again — the path is not hard-coded inside the class.
        // Program.cs decides where to write; the printer only knows HOW to write.
        public FileStudentPrinter(string filePath = "students.txt")
        {
            _filePath = filePath;
        }

        public void PrintStudents(IReadOnlyList<Student> students)
        {
            var builder = new StringBuilder();
            builder.AppendLine("--- Students (File) ---");

            if (students.Count == 0)
            {
                builder.AppendLine("(no students)");
            }
            else
            {
                foreach (var s in students)
                {
                    builder.AppendLine($"[{s.Id}] {s.Name} | {s.Email} | Grades: [{string.Join(", ", s.Grades)}]");
                }
            }

            File.WriteAllText(_filePath, builder.ToString());

            // Tell the user something happened — silent file writes confuse demos.
            Console.WriteLine($"Wrote {students.Count} student(s) to '{_filePath}'.");
        }
    }
}