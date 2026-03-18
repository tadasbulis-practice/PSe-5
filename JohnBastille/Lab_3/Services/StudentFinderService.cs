namespace JohnBastille.Lab_3.Services;

using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Interfaces;
using System;




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
            if (student.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return student;
            }
        }

        return null;
    }

}
