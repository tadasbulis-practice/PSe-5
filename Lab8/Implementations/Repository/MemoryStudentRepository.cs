using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

/// <summary>
/// In-memory repository for use when the Docker API is not running.
/// Shared repository logic is inherited from StudentRepositoryBase.
/// </summary>
public class MemoryStudentRepository : StudentRepositoryBase
{
    public MemoryStudentRepository()
        : base("Faculty of Technology (Demo)")
    {
        var alice = new Student(
            1, "Alice", "Smith",
            "alice@uni.lt",
            "Computer Science",
            2023);

        alice.AddGrade(9);
        alice.AddGrade(8);

        var bob = new Student(
            2, "Bob", "Jones",
            "bob@uni.lt",
            "Computer Science",
            2023);

        bob.AddGrade(7);
        bob.AddGrade(10);

        var charlie = new Student(
            3, "Charlie", "Brown",
            "charlie@uni.lt",
            "Computer Science",
            2024);

        charlie.AddGrade(8);
        charlie.AddGrade(9);

        var diana = new Student(
            4, "Diana", "Lee",
            "diana@uni.lt",
            "Informatics",
            2023);

        diana.AddGrade(10);
        diana.AddGrade(9);

        var eve = new Student(
            5, "Eve", "Williams",
            "eve@uni.lt",
            "Informatics",
            2023);

        eve.AddGrade(6);
        eve.AddGrade(8);

        var frank = new Student(
            6, "Frank", "Miller",
            "frank@uni.lt",
            "Informatics",
            2024);

        frank.AddGrade(7);
        frank.AddGrade(9);

        var seed = new[]
        {
            alice,
            bob,
            charlie,
            diana,
            eve,
            frank
        };

        foreach (var student in seed)
        {
            RegisterStudent(student);
        }
    }

    public override IReadOnlyList<Student> GetAll()
    {
        return _studentsById.Values.ToList();
    }

    public override Student? GetById(int id)
    {
        return _studentsById.TryGetValue(id, out var student) ? student : null;
    }

    public override IReadOnlyList<Group> GetAllGroups()
    {
        return _groupsByCode.Values.ToList();
    }

    public override Group? GetGroupByCode(string code)
    {
        return _groupsByCode.TryGetValue(code, out var group) ? group : null;
    }

    public override Faculty GetFaculty()
    {
        return _faculty;
    }

    public override void Add(Student student)
    {
        RegisterStudent(student);
    }

    public override bool Remove(int id)
    {
        if (!_studentsById.Remove(id))
        {
            return false;
        }

        return true;
    }
}