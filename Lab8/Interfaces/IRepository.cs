namespace Lab7.Interfaces;

public interface IRepository<T> where T : IEntity
{
    void Add(T item);
    T? GetById(int id);
    IReadOnlyList<T> GetAll();
    bool Remove(int id);
}