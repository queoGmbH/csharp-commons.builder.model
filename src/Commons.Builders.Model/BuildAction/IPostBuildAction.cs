namespace Queo.Commons.Builders.Model.BuildAction;

/// <summary>
///    Specifies an action to be executed directly after the ModelBuilder.Build() method is executed
/// </summary>
public interface IPostBuildAction
{
    void Execute<TModel>(TModel model);
}
