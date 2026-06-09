using System.Collections.Generic;
using Lab7.App.Models;

namespace Lab7.App.Interfaces
{
    public interface IStudentRepository
    {
        // Student-level
        IReadOnlyList<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
        bool Remove(int id);

        // Group-level (NEW in LAB-7 vs LAB-6)
        IReadOnlyList<Group> GetAllGroups();
        Group? GetGroupByCode(string code);

        // Faculty-level (NEW in LAB-7 vs LAB-6)
        Faculty GetFaculty();
    }
}