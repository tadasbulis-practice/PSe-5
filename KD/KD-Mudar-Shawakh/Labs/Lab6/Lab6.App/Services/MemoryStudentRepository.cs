using System.Collections.Generic;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class MemoryStudentRepository : IStudentRepository
    {
        // PRIVATE container — the rule from LAB-6.
        // Nothing outside this class can touch _students directly.
        private readonly List<Student> _students = new List<Student>();

        public IReadOnlyList<Student> GetAll()
        {
            // Returning the list "as read-only" gives callers iteration access
            // but blocks Add / Remove / Clear from the outside.
            return _students.AsReadOnly();
        }

        public Student? GetById(int id)
        {
            // Plain foreach + if — keeping it simple and explicit on purpose.
            foreach (var student in _students)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }
            return null;
        }

        public void Add(Student student)
        {
            _students.Add(student);
        }

        public bool Remove(int id)
        {
            // GetById already does the lookup — reuse it instead of duplicating logic.
            var existing = GetById(id);
            if (existing == null)
            {
                return false;
            }
            _students.Remove(existing);
            return true;
        }
    }
}