using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Peristence;
using Queo.Commons.Builders.Model.Pipeline;

namespace Queo.Commons.Builders.Model.Factory
{
    public abstract class PersistableBuilderFactory : IBuilderFactory
    {
        protected PersistableBuilderFactory(IPersistor persistor)
        {
            if (persistor is null) throw new ArgumentNullException("Persistor can not be null!");
            var pipeline = new PersistorPipeline(persistor);
            PreBuildPipeline = pipeline;
            PostBuildPipeline = pipeline;
        }

        public abstract TBuilder Create<TBuilder>();

        public IPreBuildPipeline PreBuildPipeline { get; }

        public IPostBuildPipeline<object> PostBuildPipeline { get; }
    }
}
