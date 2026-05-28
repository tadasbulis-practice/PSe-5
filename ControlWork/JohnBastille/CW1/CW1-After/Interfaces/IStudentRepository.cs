
using System.Collections.Generic;
using CW1After.Models;

namespace CW1After.Interfaces;

public interface IStudentRepository
{
    IEnumerable<Student> GetAll();
    Student? GetById(int id);
    void Add(Student student);
    void AddGrade(int id, int grade);
    IEnumerable<Group> GetGroups();
}