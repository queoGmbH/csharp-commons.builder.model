using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public class Pipeline<TModel> : IPreBuildPipeline, IPostBuildPipeline<TModel>
    {
        private ICollection<Action<TModel>> _postBuildCommands;
        private ICollection<Action> _preBuildCommands;
        public Pipeline()
        {
            _postBuildCommands = new List<Action<TModel>>();
            _preBuildCommands = new List<Action>();
        }

        public void Execute()
        {
            foreach (var action in _preBuildCommands)
            {
                action();
            }
        }
        public void Execute(TModel model)
        {
            foreach (var action in _postBuildCommands)
            {
                action(model);
            }
        }

        public void Add(Action<TModel> command) => _postBuildCommands.Add(command);
        public void Add(Action command) => _preBuildCommands.Add(command);

    }
}
