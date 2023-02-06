using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Pipeline;

namespace Queo.Commons.Builders.Model.Examples
{
    public class ExamplePreBuildPipeline : IPreBuildPipeline
    {
        //FIXME: this seems quite useless. I would need to know at least what type is executed?
        public void Execute<TBuilder, TModel>(TBuilder builder) where TBuilder : IBuilder<TModel>
        {
            var builderType = builder.GetType();
            //TODO: is it good that this is possible?
            //builde.Build()
            var modelType = typeof(TModel);
            Console.WriteLine($"Pipeline: About to build {modelType.Name} using a {builderType.Name} builder.");
        }
    }
}
