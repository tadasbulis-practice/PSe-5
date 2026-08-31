public class Student : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<int> Grades { get; set; } = new();
}
