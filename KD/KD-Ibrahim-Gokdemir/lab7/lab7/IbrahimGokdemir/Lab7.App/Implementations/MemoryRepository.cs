using Lab7.App.Interfaces;

namespace Lab7.App.Implementations;

// Written ONCE, works for any T : IEntity. This is the DRY/OCP payoff:
// no more separate MemoryStudentRepository / MemoryOrderRepository internals.
public class MemoryRepository<T> : IRepository<T> where T : IEntity
{
    private readonly Dictionary<int, T> _items = new();

    public void Add(T item)
        => _items[item.Id] = item; // O(1)

    public T? GetById(int id)
        => _items.TryGetValue(id, out var item) ? item : null; // O(1)

    public IReadOnlyList<T> GetAll()
        => _items.Values.ToList();

    public bool Remove(int id)
        => _items.Remove(id); // O(1)
}
