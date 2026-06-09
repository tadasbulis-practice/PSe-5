using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class JsonStudentPrinter : IStudentPrinter
    {
        private readonly string _filePath;

        public JsonStudentPrinter(string filePath = "students.json")
        {
            _filePath = filePath;
        }

        public void PrintStudents(IReadOnlyList<Student> students)
        {
            // System.Text.Json serializes the Id / Name / Email / Grades properties automatically.
            // WriteIndented = true makes the output human-readable.
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(students, options);

            File.WriteAllText(_filePath, json);

            Console.WriteLine($"Wrote {students.Count} student(s) as JSON to '{_filePath}'.");
        }
    }
}