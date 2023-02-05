using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public interface IPostBuildPipeline
    {
        void Execute<TModel>(TModel model);
    }
}
