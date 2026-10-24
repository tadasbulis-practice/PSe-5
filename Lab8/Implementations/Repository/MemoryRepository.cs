using Lab7.Interfaces;

namespace Lab7.Implementations.Repository;

public class MemoryRepository<T> : IRepository<T>
    where T : IEntity
{
    private readonly Dictionary<int, T> _items = new();

    public void Add(T item)
    {
        _items[item.Id] = item;
    }

    public T? GetById(int id)
    {
        return _items.TryGetValue(id, out var item) ? item : default;
    }

    public IReadOnlyList<T> GetAll()
    {
        return _items.Values.ToList();
    }

    public bool Remove(int id)
    {
        return _items.Remove(id);
    }
}