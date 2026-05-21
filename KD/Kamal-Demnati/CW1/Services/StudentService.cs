using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW1.Interfaces;
using CW1.Models;

namespace CW1.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetStudents();
        }

        public List<Group> GetAllGroups()
        {
            return _repository.GetGroups();
        }

        public Student? FindById(int id)
        {
            foreach (var s in _repository.GetStudents())
            {
                if (s.Id == id)
                {
                    return s;
                }
            }

            return null;
        }

        public void AddStudent(Student student)
        {
            _repository.AddStudent(student);
        }
    }
}
