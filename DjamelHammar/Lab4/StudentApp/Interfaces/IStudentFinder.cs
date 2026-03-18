using System.Collections.Generic;
using StudentApp.Models;

namespace StudentApp.Interfaces
{
    public interface IStudentFinder
    {
        Student Find(List<Student> students, string name);
    }
}