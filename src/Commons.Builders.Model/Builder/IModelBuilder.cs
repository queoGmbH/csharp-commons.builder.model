namespace Queo.Commons.Builders.Model.Builder
{
    /// <summary>
    ///		Generalized interface for a ModelBuilder
    /// </summary>
    /// <typeparam name="TModel">Type that is used as model class</typeparam>
    public interface IModelBuilder<out TModel>
    {
        /// <summary>
        ///		Creates the specified model, based on the previous builder configuration
        /// </summary>
        TModel Build();
    }
}
