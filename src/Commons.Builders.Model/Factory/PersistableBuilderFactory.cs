using System;

using Queo.Commons.Builders.Model.BuildAction;
using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Factory;

/// <summary>
///    Factory basis that uses an persistence strategy.
///    Extend this factory if you want to save the builders' models into a db
///    or another persistence source.
///
///    This Factory will have as only action the saving of model according to the PersistenceStrategy.
/// </summary>
public abstract class PersistableBuilderFactory : IBuilderFactory
{
    public IPreBuildAction PreBuild { get; }
    public IPostBuildAction PostBuild { get; }

    protected PersistableBuilderFactory(IPersistenceStrategy persistor)
    {
        if (persistor is null) throw new ArgumentNullException("PersistenceStrategy can not be null!");

        PreBuild = new EmptyAction();
        PostBuild = new PersistenceAction(persistor);
    }

    public abstract TBuilder Create<TBuilder>();
}
