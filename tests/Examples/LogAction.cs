using System;

using Queo.Commons.Builders.Model.BuildAction;
using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Examples
{
    public class LogAction : IPreBuildAction
    {
        public void Execute<TModel>(IBuilder<TModel> builder)
        {
            var builderType = builder.GetType();
            var modelType = typeof(TModel);
            if (builder is ModelBuilder<TModel> modelBuilder)
            {
                Console.WriteLine($"PreBuild: About to build {modelType.Name} using a {builderType.Name} builder with index {modelBuilder.BuilderIndex}.");
            }
            else
            {
                Console.WriteLine($"PreBuild: About to build {modelType.Name} using a {builderType.Name} builder.");
            }
        }
    }
}
