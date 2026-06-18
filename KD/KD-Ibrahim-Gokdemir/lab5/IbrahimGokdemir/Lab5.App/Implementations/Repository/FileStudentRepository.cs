using System.Collections.Generic;
using System.IO;

public class FileStudentRepository : IStudentRepository
{
    public List<Student> GetAll()
    {
        string path = "students.txt";

        if (!File.Exists(path))
        {
            File.WriteAllLines(path, new[]
            {
                "1,Alice,alice@test.com,8;9;10",
                "2,Bob,bob@test.com,6;7;8",
                "3,Charlie,charlie@test.com,9;9;10"
            });
        }

        var lines = File.ReadAllLines(path);
        List<Student> students = new List<Student>();

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var gradesText = parts[3].Split(';');
            List<int> grades = new List<int>();

            foreach (var g in gradesText)
            {
                grades.Add(int.Parse(g));
            }

            students.Add(new Student(
                int.Parse(parts[0]),
                parts[1],
                parts[2],
                grades
            ));
        }

        return students;
    }
}