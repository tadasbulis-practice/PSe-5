
using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(int id);
        void Add(Student student);
        bool Remove(int id);
    }
}