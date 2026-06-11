using System.Collections.Generic;
using System.Linq;

namespace Nassim.Lab4.Nassim.Service
{
    public class MemoryStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();

        public List<Student> GetAll() => new List<Student>(_students);

        public Student GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public void Add(Student student) => _students.Add(student);

        public bool Remove(int id)
        {
            var student = GetById(id);
            if (student == null) return false;
            _students.Remove(student);
            return true;
        }
    }
}