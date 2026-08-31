public class FileStudentRepository : IStudentRepository
{
    private readonly string _path = "students_data.txt";
    private readonly List<Student> students = new();

    public FileStudentRepository()
    {
        LoadFromFile();
    }

    public List<Student> GetAll()
    {
        return students.ToList();
    }

    public Student? GetById(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public void Add(Student student)
    {
        students.Add(student);
        SaveToFile();
    }

    public bool Remove(int id)
    {
        var student = GetById(id);
        if (student == null)
            return false;

        students.Remove(student);
        SaveToFile();
        return true;
    }

    private void LoadFromFile()
    {
        if (!File.Exists(_path))
        {
            File.WriteAllLines(_path, new[]
            {
                "1:Malek:8,9,10",
                "2:John:6,7,8",
                "3:Sara:10,9,9"
            });
        }

        students.Clear();
        foreach (var line in File.ReadAllLines(_path))
        {
            var parts = line.Split(':');
            if (parts.Length != 3)
                continue;

            students.Add(new Student
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Grades = parts[2].Split(',').Select(int.Parse).ToList()
            });
        }
    }

    private void SaveToFile()
    {
        var lines = students.Select(s => $"{s.Id}:{s.Name}:{string.Join(',', s.Grades)}");
        File.WriteAllLines(_path, lines);
    }
}
