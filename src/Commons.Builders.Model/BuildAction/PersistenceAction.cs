using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.BuildAction
{
    public class PersistenceAction : IPostBuildAction
    {
        private IPersistenceStrategy _persistor;
        public PersistenceAction(IPersistenceStrategy persistor)
        {
            _persistor = persistor;
        }

        public void Execute<TModel>(TModel model)
        {
            if (model is null) throw new ArgumentNullException("The model for the post build action should never be null!");
            _persistor.Save(model);
        }
    }
}
