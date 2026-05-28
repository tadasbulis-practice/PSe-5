using System.Collections.Generic;

using CW1After.Models;



namespace CW1After.Interfaces;

public interface IStudentService
{
    IEnumerable<Student> ListAll();
    (bool Success, List<string> Errors) AddStudent(Student s);
    bool AddGrade(int studentId, int grade);
    Student? FindById(int id);
    double GetAverage(Student s);
}


