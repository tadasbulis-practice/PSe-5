using System;
using System.Collections.Generic;
using System.Linq;

public class GroupService
{
    public bool RegisterStudent(Group group, Student student)
    {
        if (!student.Email.Contains("@"))
        {
            return false;
        }

        if (student.Grades.Any(g => g < 0 || g > 10))
        {
            return false;
        }

        group.Students.Add(student);
        return true;
    }

    public List<Student> GetAllStudents(Group group)
    {
        return group.Students;
    }

    public Student? FindById(Group group, int id)
    {
        return group.Students.FirstOrDefault(s => s.Id == id);
    }
}