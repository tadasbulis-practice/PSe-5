using Lab5.Interfaces;
using Lab5.Models;

public class FileStudentRepository : IStudentRepository
{
    public Student? Find(string query)
    {
        if (!File.Exists("students.txt"))
            return null;

        var lines = File.ReadAllLines("students.txt");

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts[0] == query)
                return new Student(int.Parse(parts[0]), parts[1], parts[2]);
        }

        return null;
    }
}
