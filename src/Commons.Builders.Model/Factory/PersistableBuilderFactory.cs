using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.BuildAction;
using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Factory
{
    public abstract class PersistableBuilderFactory : IBuilderFactory
    {
        public IPreBuildAction PreBuild { get; }
        public IPostBuildAction PostBuild { get; }
        protected PersistableBuilderFactory(IPersistenceStrategy persistor)
        {
            if (persistor is null) throw new ArgumentNullException("PersistenceStrategy can not be null!");

            PreBuild = new EmptyAction();
            PostBuild = new PersistenceAction(persistor);
        }

        public abstract TBuilder Create<TBuilder>();
    }
}
