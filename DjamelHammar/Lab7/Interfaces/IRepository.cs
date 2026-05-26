public interface IRepository<T> where T : IEntity
{
    List<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    bool Remove(int id);
}
