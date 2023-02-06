using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public interface IPreBuildPipeline
    {
        void Execute<TBuilder, TModel>(TBuilder builder) where TBuilder : IBuilder<TModel>;
    }
}
