using System.Collections.Generic;
using Lab6.App.Models;

namespace Lab6.App.Interfaces
{
    public interface IStudentRepository
    {
        IReadOnlyList<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
        bool Remove(int id);
    }
}