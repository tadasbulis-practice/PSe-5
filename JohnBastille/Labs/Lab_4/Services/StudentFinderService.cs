using JohnBastille.Lab_4.Models;
using JohnBastille.Lab_4.Interfaces;
using System;

namespace Lab_4.Services;

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
