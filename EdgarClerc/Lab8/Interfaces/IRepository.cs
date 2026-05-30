namespace Lab8.Interfaces;

public interface IRepository<T>
{
    // ── Individual student access ─────────────────────────────────────
    IReadOnlyList<T> GetAll();
    T? GetById(int id); // O(1) when backed by Dictionary
    void Add(T student);
    bool Remove(int id);
}
