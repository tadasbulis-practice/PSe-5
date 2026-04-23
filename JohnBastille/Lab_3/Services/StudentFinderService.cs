using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Interfaces;
using System;

namespace Lab_3.Services;

public class StudentFinderService : IStudentFinder
{
    public Student? Find(List<Student> students, string query)
    {
        throw new NotImplementedException();
    }

    public Student? FindByName(Group group, string name)
    {
        foreach (var student in group.Students)
        {
            if (student.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true)
            {
                return student;
            }
        }

        return null;
    }
}
