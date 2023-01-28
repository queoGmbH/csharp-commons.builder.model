using System;
using System.Collections.Generic;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.DAG
{
    public class SourceBuilder : ModelBuilder<Source>
    {
        private string _name;
        private string _description;
        private BuilderCollection<VertexBuilder, Vertex> _children;

        public SourceBuilder(IBuilderFactory factory) : base(factory)
        {
            _children = new BuilderCollection<VertexBuilder, Vertex>(_factory);
            _name = $"Sourcename {BuilderIndex}";
            _description = $"Source-Description {BuilderIndex}";
        }

        protected override Source BuildModel()
        {
            var _parent = new Source(_name, _description);
            // foreach (Vertex child in _childBuilders)
            // {
            //     _children.Add(child);
            // }

            return _parent;
        }

        public IEnumerable<Vertex> GetChildren()
        {
            Build();
            return _children.BuildModels();
        }

        public SourceBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SourceBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public SourceBuilder AddChild(Action<VertexBuilder> buildAction)
        {
            VertexBuilder childBuilder = _factory.Create<VertexBuilder>();
            childBuilder.WithParent(this);
            buildAction(childBuilder);
            _children.Add(childBuilder);

            // childBuilder.WithParent(this);
            // buildAction(childBuilder);
            // _subBuilderHolder.Add(childBuilder);
            return this;
        }
    }
}
