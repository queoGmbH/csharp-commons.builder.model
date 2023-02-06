using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Pipeline;

namespace Queo.Commons.Builders.Model.Factory
{
    public interface IBuilderFactory
    {
        public IPreBuildPipeline PreBuildPipeline { get; }
        public IPostBuildPipeline PostBuildPipeline { get; }
        TBuilder Create<TBuilder>();
    }
}
