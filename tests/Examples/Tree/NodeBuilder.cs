using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Tree
{
    public class NodeBuilder : ModelBuilder<Node>
    {
        private string _name;
        private string _description;
        private BuilderCollection<NodeBuilder, Node> _childs;

        public NodeBuilder(IBuilderFactory factory) : base(factory)
        {
            _childs = new BuilderCollection<NodeBuilder, Node>(_factory);
            _name = $"Node {BuilderIndex}";
            _description = $"Node-Description {BuilderIndex}";
        }

        public NodeBuilder WithName(string name) => Set(() => _name = name);
        public NodeBuilder WithDescription(string description) => Set(() => _description = description);
        public NodeBuilder AddChild(Action<NodeBuilder> buildAction) => Set(() => _childs.Add(buildAction));
        public NodeBuilder AddChild(IBuilder<Node> builder) => Set(() => _childs.Add(builder));

        protected override Node BuildModel()
        {
            var node = new Node(_name, _description);
            foreach (var child in _childs)
            {
                node.Add(child.Build());
            }
            return node;
        }

        protected override NodeBuilder Set(Action action) => Set<NodeBuilder>(action);
        public override NodeBuilder Recreate() => Recreate<NodeBuilder>();
    }
}
