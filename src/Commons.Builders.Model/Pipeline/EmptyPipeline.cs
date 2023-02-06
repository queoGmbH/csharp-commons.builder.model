using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public class EmptyPipeline : IPreBuildPipeline, IPostBuildPipeline
    {
        public void Execute<TBuilder, TModel>(TBuilder builder) where TBuilder : IBuilder<TModel>
        {
            // Do nothing
        }
        public void Execute<TModel>(TModel model)
        {
            // Do nothing
        }
    }
}
