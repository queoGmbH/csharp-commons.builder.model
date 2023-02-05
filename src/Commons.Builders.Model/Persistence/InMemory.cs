namespace Queo.Commons.Builders.Model.Peristence;

/// <summary>
///     "Persists" the object in memory only
/// </summary>
public class InMemory : IPersistenceStrategy
{
    public void Save(object entity)
    {
        //nothing to do
    }
}
