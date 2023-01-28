using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Tree
{
    public class NodeBuilder : ModelBuilder<Node>
    {
        string _name;
        string _description;
        private BuilderCollection<NodeBuilder, Node> _childs;

        public NodeBuilder(IBuilderFactory factory) : base(factory)
        {
            _childs = new BuilderCollection<NodeBuilder, Node>(_factory);
            _name = $"Node {BuilderIndex}";
            _description = $"Node-Description {BuilderIndex}";
        }

        protected override Node BuildModel()
        {
            var node = new Node(_name, _description);
            foreach (var child in _childs)
            {
                node.Add(child.Build());
            }
            return node;
        }

        public NodeBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public NodeBuilder WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public NodeBuilder AddChild(Action<NodeBuilder> buildAction)
        {
            _childs.Add(buildAction);
            return this;
        }

        public NodeBuilder AddChild(NodeBuilder builder)
        {
            _childs.Add(builder);
            return this;
        }
    }
}
