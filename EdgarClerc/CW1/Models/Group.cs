namespace CW1.Models;

public class Group
{
    public string Code { get; }
    public string Name { get; }

    public Group(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
