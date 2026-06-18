namespace Lab7.App.Interfaces;

// Any model stored in a generic repository must have an Id.
// This is the "where T : IEntity" constraint used by IRepository<T>.
public interface IEntity
{
    int Id { get; }
}
