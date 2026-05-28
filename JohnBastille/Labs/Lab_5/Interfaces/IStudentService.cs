namespace JohnBastille.Lab_5.Interfaces;

using JohnBastille.Lab_5.Models;
using System;


public interface IStudentService
{
    void AddStudent(Group group, Student student);
    void PrintAll(Group group);
}