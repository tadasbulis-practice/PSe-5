
using Lab5.Models;

namespace Lab5.Interfaces;

public interface IStudentRepository
{
    Student? Find(string query);
}
