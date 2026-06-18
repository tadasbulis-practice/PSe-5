using System.Collections.Generic;
using CW1.Models;

namespace CW1.Interfaces;

public interface IStudentRepository
{
    List<Student> GetAllStudents();
    List<Group> GetAllGroups();
    void AddStudent(Student student);
}