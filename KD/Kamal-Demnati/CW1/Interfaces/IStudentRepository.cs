using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW1.Models;

namespace CW1.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();

        List<Group> GetGroups();

        void AddStudent(Student student);
    }

}
