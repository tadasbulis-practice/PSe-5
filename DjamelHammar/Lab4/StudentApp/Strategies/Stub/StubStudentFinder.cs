using System.Collections.Generic;
using StudentApp.Interfaces;
using StudentApp.Models;

namespace StudentApp.Strategies.Stub
{
    public class StubStudentFinder : IStudentFinder
    {
        public Student Find(List<Student> students, string name)
        {
            return new Student(name, new List<int>()); // dummy student
        }
    }
}