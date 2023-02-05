using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.DAG
{
    public class VertexBuilder : ModelBuilder<Vertex>
    {
        string _name;
        string _description;
        private IBuilder<Source> _source;
        private BuilderCollection<VertexBuilder, Vertex> _vertices;

        public VertexBuilder(IBuilderFactory factory) : base(factory)
        {
            _vertices = new BuilderCollection<VertexBuilder, Vertex>(_factory);
            _source = _factory.Create<SourceBuilder>();
            _name = $"Vertex {BuilderIndex}";
            _description = $"Vertex-Description {BuilderIndex}";
        }

        protected override Vertex BuildModel()
        {
            Source parent = _source.Build();
            var vertex = new Vertex(_name, _description, parent);
            foreach (var targetVertex in _vertices)
            {
                vertex.Add(targetVertex.Build());
            }
            return vertex;
        }

        public VertexBuilder WithName(string name) => Set(() => _name = name);
        public VertexBuilder WithDescription(string description) => Set(() => _description = description);
        public VertexBuilder AddChild(Action<VertexBuilder> buildAction) => Set(() =>
        {
            _vertices.Add(c => buildAction(c.WithSource(_source)));
        });
        public VertexBuilder AddChild(IBuilder<Vertex> builder) => Set(() => _vertices.Add(builder));
        public VertexBuilder WithSource(IBuilder<Source> sourceBuilder) => Set(() => _source = sourceBuilder);
        public VertexBuilder WithSource(Action<SourceBuilder> builderAction) => Set(() =>
        {
            _source = FromAction<SourceBuilder, Source>(builderAction);
        });

        protected override VertexBuilder Set(Action action) => Set<VertexBuilder>(action);
        public override VertexBuilder Recreate() => Recreate<VertexBuilder>();
    }
}
