using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Nassim.Lab4.Nassim.Service
{
    public class JsonStudentPrinter : IStudentPrinter
    {
        public void PrintStudents(List<Student> students)
        {
            var data = students.Select(s => new {
                s.Id, s.FirstName, s.LastName, s.Email, s.AverageGrade
            });
            Console.WriteLine(JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}