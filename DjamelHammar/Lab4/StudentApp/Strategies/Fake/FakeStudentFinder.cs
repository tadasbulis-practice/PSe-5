using System.Collections.Generic;
using StudentApp.Interfaces;
using StudentApp.Models;

namespace StudentApp.Strategies.Fake
{
    public class FakeStudentFinder : IStudentFinder
    {
        public Student Find(List<Student> students, string name)
        {
            return new Student(name, new List<int>()); // always returns a dummy student
        }
    }
}