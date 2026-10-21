using CW1.Models;

namespace CW1.Services;

/// <summary>
/// In-memory repository — holds all Students and Groups.
/// Seeded with sample data in the constructor.
/// </summary>
public class StudentRepository
{
    public List<Student> Students { get; } = new();
    public List<Group>   Groups   { get; } = new();

    public StudentRepository()
    {
        Groups.Add(new Group { Code = "CS23", Name = "Computer Science 2023" });
        Groups.Add(new Group { Code = "CS24", Name = "Computer Science 2024" });

        Students.Add(new Student { Id = 1, Name = "Alice Johnson",   Email = "alice@uni.edu",   GroupCode = "CS23", Grades = new List<int> { 8, 9, 7, 10 } });
        Students.Add(new Student { Id = 2, Name = "Bob Smith",       Email = "bob@uni.edu",     GroupCode = "CS23", Grades = new List<int> { 6, 5, 7, 8  } });
        Students.Add(new Student { Id = 3, Name = "Carol Williams",  Email = "carol@uni.edu",   GroupCode = "CS24", Grades = new List<int> { 9, 9, 10, 8 } });
        Students.Add(new Student { Id = 4, Name = "David Brown",     Email = "david@uni.edu",   GroupCode = "CS23", Grades = new List<int> { 10, 10, 9, 9 } });
        Students.Add(new Student { Id = 5, Name = "Emma Davis",      Email = "emma@uni.edu",    GroupCode = "CS24", Grades = new List<int> { 5, 6, 6, 7  } });
    }
}
