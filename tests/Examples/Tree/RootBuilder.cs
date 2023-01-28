using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Tree
{
    public class RootBuilder : ModelBuilder<Root>
    {
        private string _name;
        private string _description;
        private BuilderCollection<NodeBuilder, Node> _subBuilderHolder;

        public RootBuilder(IBuilderFactory factory) : base(factory)
        {
            _subBuilderHolder = new BuilderCollection<NodeBuilder, Node>(_factory);
            _name = $"Root {BuilderIndex}";
            _description = $"Root-Description {BuilderIndex}";
        }

        protected override Root BuildModel()
        {
            var _parent = new Root(_name, _description);
            foreach (var child in _subBuilderHolder)
            {
                _parent.Add(child.Build());
            }
            return _parent;
        }

        public RootBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public RootBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public RootBuilder AddChild(Action<NodeBuilder> buildAction)
        {
            _subBuilderHolder.Add(buildAction);
            return this;
        }
    }
}
