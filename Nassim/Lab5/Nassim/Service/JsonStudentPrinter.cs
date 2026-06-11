using System;
using System.Text.Json;

namespace Nassim.Lab4.Nassim.Service
{
    public class JsonStudentPrinter : IStudentPrinter
    {
        public void Print(Group group)
        {
            var data = new
            {
                group.Name,
                Students = group.Students.Select(s => new
                {
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    s.Email,
                    s.AverageGrade
                })
            };
            Console.WriteLine(JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}