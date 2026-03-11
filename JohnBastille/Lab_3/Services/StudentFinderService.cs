namespace JohnBastille.Lab_3.Services;

using JohnBastille.Lab_3.Models;
using JohnBastille.Lab_3.Interfaces;
using System;




public class StudentFinderService : StudentFinder
{
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
