using Lab9.Interfaces;
using Lab9.Models;

namespace Lab9.Implementations.Repository;

public abstract class StudentRepositoryBase : IStudentRepository
{
    protected readonly Dictionary<int, Student> StudentsById = new();
    protected readonly Dictionary<string, Group> GroupsByCode = new();
    protected Faculty Faculty;

    protected StudentRepositoryBase(string facultyName) => Faculty = new Faculty(facultyName);

    protected static string BuildGroupCode(string program, int year)
    {
        var words = program.Split(' ');
        return $"{string.Concat(words.Select(w => char.ToUpper(w[0])))}-{year % 100:D2}";
    }

    protected void RegisterStudent(Student student)
    {
        StudentsById[student.Id] = student;
        var code = BuildGroupCode(student.StudyProgram, student.EnrollmentYear);
        if (!GroupsByCode.TryGetValue(code, out var group))
        {
            group = new Group(code, student.StudyProgram, student.EnrollmentYear);
            GroupsByCode[code] = group;
            Faculty.AddGroup(group);
        }
        group.AddStudent(student);
    }

    public abstract IReadOnlyList<Student> GetAll();
    public abstract Student? GetById(int id);
    public abstract void Add(Student student);
    public abstract bool Remove(int id);
    public abstract IReadOnlyList<Group> GetAllGroups();
    public abstract Group? GetGroupByCode(string code);
    public abstract Faculty GetFaculty();
}
