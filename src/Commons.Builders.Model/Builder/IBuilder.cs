namespace Queo.Commons.Builders.Model.Builder
{
    /// <summary>
    ///		Generalized builder interface.
    /// </summary>
    /// <typeparam name="TBuildResult">Type that the builder produces</typeparam>
    public interface IBuilder<out TBuildResult>
    {
        /// <summary>
        ///		Creates this builders result object
        /// </summary>
        TBuildResult Build();
    }
}
