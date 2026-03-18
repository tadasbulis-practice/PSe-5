using System.Collections.Generic;
using StudentApp.Interfaces;
using StudentApp.Models;

namespace StudentApp.Strategies.Real
{
    public class LinearStudentFinder : IStudentFinder
    {
        public Student Find(List<Student> students, string name)
        {
            foreach (var s in students)
                if (s.Name == name) return s;
            return null;
        }
    }
}