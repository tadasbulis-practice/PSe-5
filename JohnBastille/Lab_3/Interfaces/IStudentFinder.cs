namespace JohnBastille.Lab_3.Interfaces
using System;

using JohnBastille.Models

public interface StudentFinder 
{
	Student? FindByName(Group group, string name);

}
