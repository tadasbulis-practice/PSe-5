namespace Lab7.App.Models;

// New in Lab 7. There is only one Faculty in this exercise, so it
// doesn't need its own repository — just a value the student repository
// can hand back.
public class Faculty
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Faculty(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
