using Lab4Demo.Interfaces;
using Lab4Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Implementations
{
    public class FileStudentRepository : IStudentRepository
    {
        public List<Student> GetAll()
        {
            // fake read
            return new List<Student>();
        }
    }
}
