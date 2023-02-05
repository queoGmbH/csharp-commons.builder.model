using Queo.Commons.Builders.Model.BuildAction;

namespace Queo.Commons.Builders.Model.Factory;

/// <summary>
///    Factory defintion for the creation of builders
///    Builders should always be created through the factory.
///
///    The factory pattern is used to be able to pass parameters down to each builder
///    The most important parameter is is the <see cref="IPersistenceStrategy"/> 
///    within the PostBuild action
/// </summary>
public interface IBuilderFactory
{
    public IPreBuildAction PreBuild { get; }
    public IPostBuildAction PostBuild { get; }
    TBuilder Create<TBuilder>();

}
