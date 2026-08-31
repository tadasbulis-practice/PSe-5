public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<int> Grades { get; set; } = new();
}
