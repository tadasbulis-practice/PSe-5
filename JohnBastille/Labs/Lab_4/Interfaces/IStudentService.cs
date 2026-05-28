namespace JohnBastille.Lab_4.Interfaces;

using JohnBastille.Lab_4.Models;
using System;


public interface IStudentService
{
    void AddStudent(Group group, Student student);
    void PrintAll(Group group);
}