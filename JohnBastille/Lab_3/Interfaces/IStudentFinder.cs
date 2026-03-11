namespace JohnBastille.Lab_3.Interfaces;

using JohnBastille.Lab_3.Models;


public interface IStudentFinder
{
    Student? FindByName(Group group, string name);
}


	


