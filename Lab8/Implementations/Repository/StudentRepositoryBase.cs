using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

public abstract class StudentRepositoryBase : IStudentRepository
{
    protected readonly Dictionary<int, Student> _studentsById = new();
    protected readonly Dictionary<string, Group> _groupsByCode = new();
    protected Faculty _faculty;

    protected StudentRepositoryBase(string facultyName)
    {
        _faculty = new Faculty(facultyName);
    }

    protected static string BuildGroupCode(string studyProgram, int year)
    {
        var words = studyProgram.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var prefix = string.Concat(words.Select(w => char.ToUpper(w[0])));
        return $"{prefix}-{year % 100:D2}";
    }

    protected void RegisterStudent(Student student)
    {
        _studentsById[student.Id] = student;

        var code = BuildGroupCode(student.StudyProgram, student.EnrollmentYear);

        if (!_groupsByCode.TryGetValue(code, out var group))
        {
            group = new Group(code, student.StudyProgram, student.EnrollmentYear);
            _groupsByCode[code] = group;
            _faculty.AddGroup(group);
        }

        group.AddStudent(student);
    }

    public abstract IReadOnlyList<Student> GetAll();
    public abstract Student? GetById(int id);
    public abstract IReadOnlyList<Group> GetAllGroups();
    public abstract Group? GetGroupByCode(string code);
    public abstract Faculty GetFaculty();
    public abstract void Add(Student student);
    public abstract bool Remove(int id);
}