using System;

using Queo.Commons.Builders.Model.BuildAction;

namespace Queo.Commons.Builders.Model.Factory;

public class EmptyFactory : IBuilderFactory
{
    public EmptyFactory()
    {
        var emptyPipeline = new EmptyAction();
        PreBuild = emptyPipeline;
        PostBuild = emptyPipeline;
    }

    public IPreBuildAction PreBuild { get; }
    public IPostBuildAction PostBuild { get; }
    public TBuilder Create<TBuilder>()
    {
        throw new InvalidOperationException("Create is not supposed to be called on an empty factory! " +
                                            "Your builder is configured with the wrong type of factory!");
    }
}
