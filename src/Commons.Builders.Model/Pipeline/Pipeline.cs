using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public class Pipeline<TModel> : IPreBuildPipeline, IPostBuildPipeline
    {
        private ICollection<Action<TModel>> _postBuildCommands;
        private ICollection<Action<Type, Type>> _preBuildCommands;
        public Pipeline()
        {
            _postBuildCommands = new List<Action<TModel>>();
            _preBuildCommands = new List<Action<IModelBuilder<TModel>>>();
        }

        public void Execute()
        {
            foreach (var action in _preBuildCommands)
            {
                action(builderType, modelType);
            }
        }
        public void Execute<TModel>(TModel model)
        {
            foreach (var action in _postBuildCommands)
            {
                action(model);
            }
        }

        public void Add(Action<TModel> command) => _postBuildCommands.Add(command);
        public void Add(Action<Type, Type> command) => _preBuildCommands.Add(command);

    }
}
