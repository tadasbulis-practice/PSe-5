using Lab4Demo.Interfaces;
using Lab4Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Implementations
{
    public class MemoryStudentRepository : IStudentRepository
    {
        public List<Student> GetAll() => new()
    {
        new Student { Name = "Alice", Grades = new() { 8, 9, 10 } }
    };
    }
}
