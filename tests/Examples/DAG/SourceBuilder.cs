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
            return new Source(_name, _description);
        }

        public IEnumerable<Vertex> GetChildren()
        {
            Build();
            return _children.BuildModels();
        }

        public SourceBuilder WithName(string name) => Set(() => _name = name);
        public SourceBuilder WithDescription(string description) => Set(() => _description = description);
        public SourceBuilder AddChild(Action<VertexBuilder> buildAction) => Set(() =>
        {
            VertexBuilder childBuilder = FromAction<VertexBuilder, Vertex>(buildAction);
            childBuilder.WithSource(this);
            _children.Add(childBuilder);
        });

        protected override SourceBuilder Set(Action action) => Set<SourceBuilder>(action);
        public override SourceBuilder Recreate() => Recreate<SourceBuilder>();
    }
}
