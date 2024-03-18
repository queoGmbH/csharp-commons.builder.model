namespace Queo.Commons.Builders.Model.Builder;

/// <summary>
///     Interface describing a type that can be recreated (similar to clone or copy)
///     Used for <see cref="ModelBuilder{TModel}"/> because its return the same model instance every time
/// </summary>
/// <typeparam name="T">Type to recreate (BuilderType)</typeparam>
public interface IRecreatable<out T>
{
    /// <summary>
    ///    Creates a new Instance of the Builder, but copies all its properties
    ///    The new builder instance should be modifyable (not already locked)
    /// </summary>
    T Recreate();
}
