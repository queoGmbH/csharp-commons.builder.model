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
        //FIXME: how to use generics for that? overridable?
        //OR do it just on the get method?
        //Also i don't want the pipelines to be changeable after the fact
        public IPostBuildPipeline<object> PostBuildPipeline { get; }
        TBuilder Create<TBuilder>();
    }
}
