
using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.BuildAction;


/// <summary>
///    Specifies an action to be executed directly before ModelBuilder.Build() is executed
/// </summary>
public interface IPreBuildAction
{
    void Execute<TModel>(IBuilder<TModel> builder);
}
