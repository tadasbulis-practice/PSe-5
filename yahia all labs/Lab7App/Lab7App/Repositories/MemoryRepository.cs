public class MemoryRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _items = new();

    public List<T> GetAll()
    {
        return _items.ToList();
    }

    public T? GetById(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public void Add(T entity)
    {
        _items.Add(entity);
    }

    public bool Remove(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return false;

        _items.Remove(item);
        return true;
    }
}
