public class FileStudentRepository : IStudentRepository
{
    public List<Student> GetAll()
    {
        string path = "students_data.txt";

        if (!File.Exists(path))
        {
            File.WriteAllLines(path, new[]
            {
                "Malek:8,9,10",
                "John:6,7,8",
                "Sara:10,9,9"
            });
        }

        var students = new List<Student>();
        var lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length != 2) continue;

            var name = parts[0];
            var grades = parts[1]
                .Split(',')
                .Select(x => int.Parse(x))
                .ToList();

            students.Add(new Student
            {
                Name = name,
                Grades = grades
            });
        }

        return students;
    }
}