namespace JohnBastille.Lab_3.Services;

using JohnBastille.Lab_3.Models;
using System;


public interface IStudentService
{
    void AddStudent(Group group, Student student);
    void PrintAll(Group group);
}