namespace CW1.Models;

public class Student
{
    private int _id;
    private string _name;
    private string _email;
    private string _groupCode;
    private List<int> _grades = new List<int>();

    public Student(int id, string name, string email, string groupCode, List<int> grades)
    {
        _id = id;
        _name = name;
        _email = email;
        _groupCode = groupCode;
        _grades = grades;
    }

    public override string ToString()
    {
        return $"[{_id}] {_name} ({_groupCode}) email={_email}";
    }
}
