using System;

using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.BuildAction;

/// <summary>
///    Default PostBuild action, that persists the the model according to the persistors implementaiton after it is build
/// </summary>
public class PersistenceAction : IPostBuildAction
{
    private IPersistenceStrategy _persistor;

    public PersistenceAction(IPersistenceStrategy persistor)
    {
        _persistor = persistor;
    }

    public virtual void Execute<TModel>(TModel model)
    {
        if (model is null) throw new ArgumentNullException("The model for the post build action should never be null!");
        _persistor.Save(model);
    }
}
