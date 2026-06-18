namespace Lab7.App.Interfaces;

// Generic repository contract. Works for Student, and for any future
// entity (Order, Course, ...) as long as it implements IEntity.
public interface IRepository<T> where T : IEntity
{
    void Add(T item);
    T? GetById(int id);
    IReadOnlyList<T> GetAll();
    bool Remove(int id);
}
