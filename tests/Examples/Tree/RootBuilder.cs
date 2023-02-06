using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Tree
{
    public class RootBuilder : ModelBuilder<Root>
    {
        private string _name;
        private string _description;
        private BuilderCollection<NodeBuilder, Node> _childs;

        public RootBuilder(IBuilderFactory factory) : base(factory)
        {
            _childs = new BuilderCollection<NodeBuilder, Node>(_factory);
            _name = $"Root {BuilderIndex}";
            _description = $"Root-Description {BuilderIndex}";
        }

        public RootBuilder WithName(string name) => Set(() => _name = name);
        public RootBuilder WithDescription(string description) => Set(() => _description = description);
        public RootBuilder AddChild(Action<NodeBuilder> buildAction) => Set(() => _childs.Add(buildAction));
        public RootBuilder AddChild(IBuilder<Node> builder) => Set(() => _childs.Add(builder));

        protected override Root BuildModel()
        {
            var _parent = new Root(_name, _description);
            foreach (var child in _childs)
            {
                _parent.Add(child.Build());
            }
            return _parent;
        }

        protected override RootBuilder Set(Action action) => Set<RootBuilder>(action);
        public override RootBuilder Recreate() => Recreate<RootBuilder>();
    }
}
