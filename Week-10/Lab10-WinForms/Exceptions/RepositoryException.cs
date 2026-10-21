namespace Lab10.Exceptions;

/// <summary>
/// Custom exception for repository-layer errors. From Week 9 — wraps an inner
/// exception so callers see a clean message but root cause is preserved.
/// </summary>
public class RepositoryException : Exception
{
    public RepositoryException(string message) : base(message) { }
    public RepositoryException(string message, Exception inner) : base(message, inner) { }
}
