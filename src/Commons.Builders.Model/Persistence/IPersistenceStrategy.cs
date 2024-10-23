namespace Queo.Commons.Builders.Model.Peristence;

/// <summary>
///    Strategy that is used to persist an object.
///    Implement this interface in a class that connect's it to the underlying data source
///    and then pass it to the PostBuild action of the <see cref="IBuilderFactor"/>
/// </summary>
public interface IPersistenceStrategy
{
    /// <summary>
    ///     Saves the object according to persistence strategy.
    ///     Might need a connection to a database or other kind of storage
    /// </summary>
    /// <param name="entity">The object to be persistet</param>
    T Save<T>(T entity);
}
