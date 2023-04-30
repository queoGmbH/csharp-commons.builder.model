namespace Queo.Commons.Builders.Model.Peristence;

/// <summary>
///     "Persists" the object in memory only
/// </summary>
public class InMemory : IPersistenceStrategy
{
    public T Save<T>(T entity)
    {
        //nothing to do
        return entity;
    }
}
