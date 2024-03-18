using System;

using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Factory;

/// <summary>
///    Factory implementation for a transient builder.
///    This factory will not persist the objects in the database.
/// </summary>
public class InMemoryFactory : PersistableBuilderFactory
{
    public InMemoryFactory() : base(new InMemory()) { }

    public override TBuilder Create<TBuilder>()
    {
        throw new InvalidOperationException("Create is not supposed to be called on an empty factory! " +
                                            "Your builder is configured with the wrong type of factory!");
    }
}
