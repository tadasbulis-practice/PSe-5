namespace Lab7.Interfaces;

public interface IRepository<T>
    where T : IEntity
{
    // ── Individual student access ─────────────────────────────────────
    IReadOnlyList<T> GetAll();
    T? GetById(int id); // O(1) when backed by Dictionary
    void Add(T student);
    bool Remove(int id);
}
