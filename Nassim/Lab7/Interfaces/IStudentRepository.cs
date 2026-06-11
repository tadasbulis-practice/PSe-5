using Lab7.Models;

namespace Lab7.Interfaces;

public interface IStudentRepository
{
    // ── Individual student access ─────────────────────────────────────
    IReadOnlyList<Student> GetAll();
    Student?               GetById(int id);   // O(1) when backed by Dictionary
    void                   Add(Student student);
    bool                   Remove(int id);

    // ── Group and Faculty access (complexity fully hidden inside) ─────
    IReadOnlyList<Group>   GetAllGroups();
    Group?                 GetGroupByCode(string code);  // O(1) when backed by Dictionary
    Faculty                GetFaculty();
}
