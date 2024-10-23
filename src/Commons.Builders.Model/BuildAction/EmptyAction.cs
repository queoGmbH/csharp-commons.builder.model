using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.BuildAction;

public class EmptyAction : IPreBuildAction, IPostBuildAction
{
    public void Execute<TModel>(TModel model)
    {
        // Do nothing
    }
    public void Execute<TModel>(IBuilder<TModel> builder)
    {
        //Do nothing
    }
}
