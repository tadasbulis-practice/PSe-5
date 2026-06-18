using System;
using System.Collections.Generic;
using CW1.Interfaces;
using CW1.Models;

namespace CW1.Services;

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();
    private readonly List<Group> _groups = new();

    public StubStudentRepository()
    {
        _groups.Add(new Group { Code = "TEST", Name = "Test group" });

        _students.Add(new Student
        {
            Id = 999,
            Name = "Test Student",
            Email = "test@test.lt",
            GroupCode = "TEST",
            Grades = new List<int> { 10, 10, 10 }
        });
    }

    public List<Student> GetAllStudents() => _students;

    public List<Group> GetAllGroups() => _groups;

    public void AddStudent(Student student)
    {
        throw new NotSupportedException("Stub repository does not support adding students.");
    }
}