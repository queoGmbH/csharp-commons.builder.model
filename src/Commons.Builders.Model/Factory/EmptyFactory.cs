using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Pipeline;

namespace Queo.Commons.Builders.Model.Factory
{
    public class EmptyFactory : IBuilderFactory
    {
        public EmptyFactory()
        {
            var emptyPipeline = new EmptyPipeline();
            PreBuildPipeline = emptyPipeline;
            PostBuildPipeline = emptyPipeline;
        }

        public IPreBuildPipeline PreBuildPipeline { get; }
        public IPostBuildPipeline<object> PostBuildPipeline { get; }

        public TBuilder Create<TBuilder>()
        {
            throw new InvalidOperationException("Create is not supposed to be called on an empty factory! " +
                                                "Your builder is configured with the wrong type of factory!");
        }
    }
}
