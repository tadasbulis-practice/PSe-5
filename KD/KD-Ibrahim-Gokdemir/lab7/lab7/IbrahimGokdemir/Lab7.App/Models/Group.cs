namespace Lab7.App.Models;

// New in Lab 7. Not stored through IRepository<T> — groups are simple
// reference data, looked up by Code rather than a numeric Id.
public class Group
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string FacultyName { get; set; }

    public Group(string code, string name, string facultyName)
    {
        Code = code;
        Name = name;
        FacultyName = facultyName;
    }
}
