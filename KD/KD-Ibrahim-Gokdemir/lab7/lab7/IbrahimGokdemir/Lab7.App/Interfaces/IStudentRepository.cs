using Lab7.App.Models;

namespace Lab7.App.Interfaces;

// Specific to Student. Extends the generic IRepository<Student> with the
// extra lookups shown in the slide-29 "AFTER" architecture (Group, Faculty).
public interface IStudentRepository : IRepository<Student>
{
    Group? GetGroupByCode(string code);
    IReadOnlyList<Group> GetAllGroups();
    Faculty GetFaculty();
}
