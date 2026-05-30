using Lab7.Models;

namespace Lab7.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    // ── Group and Faculty access (complexity fully hidden inside) ─────
    IReadOnlyList<Group> GetAllGroups();
    Group? GetGroupByCode(string code); // O(1) when backed by Dictionary
    Faculty GetFaculty();
}
