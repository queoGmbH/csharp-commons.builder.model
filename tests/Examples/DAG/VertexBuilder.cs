using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.DAG
{
    public class VertexBuilder : ModelBuilder<Vertex>
    {
        string _name;
        string _description;
        private SourceBuilder _source;
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
            //ParentBuilder parentBuilder = Create.Parent();
            //_parentBuilderAction.Invoke(parentBuilder);
            Source parent = _source.Build();
            var vertex = new Vertex(_name, _description, parent);
            foreach (var targetVertex in _vertices)
            {
                vertex.Add(targetVertex.Build());
            }
            return vertex;
        }

        public VertexBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public VertexBuilder WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public VertexBuilder AddChild(Action<VertexBuilder> buildAction)
        {
            _vertices.Add(c => buildAction(c.WithParent(_source)));
            // ChildBuilder childBuilder = Create.Child();
            // childBuilder.WithParent(_parentBuilder);
            // buildAction.Invoke(childBuilder);
            // _builders.Add(childBuilder);

            // ChildBuilder childBuilder = Create.Child();
            // childBuilder.WithParent(_parentBuilder);
            // buildAction.Invoke(childBuilder);
            // _holder.Add(childBuilder);
            return this;
        }

        public VertexBuilder AddChild(VertexBuilder builder)
        {
            // Diese Methode sollte es in diesem Fall wahrscheinlich nicht geben.
            _vertices.Add(builder);
            return this;
        }

        //public ChildBuilder WithParent(Action<ParentBuilder> parentBuilderAction)
        //{
        //    _parentBuilderAction = parentBuilderAction;
        //    return this;
        //}

        public VertexBuilder WithParent(SourceBuilder parentBuilder)
        {
            _source = parentBuilder;
            return this;
        }
    }
}
