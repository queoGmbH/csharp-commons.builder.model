using System;

using Queo.Commons.Builders.Model.Examples.Car.Builders;
using Queo.Commons.Builders.Model.Examples.DAG;
using Queo.Commons.Builders.Model.Examples.Person;
using Queo.Commons.Builders.Model.Examples.Tree;
using Queo.Commons.Builders.Model.Factory;
using Queo.Commons.Builders.Model.Pipeline;

namespace Queo.Commons.Builders.Model.Examples
{
    public class ExampleFactory : IBuilderFactory
    {
        public IPreBuildPipeline PreBuildPipeline { get; }
        public IPostBuildPipeline<object> PostBuildPipeline { get; }

        public ExampleFactory()
        {
            PreBuildPipeline = new ExamplePreBuildPipeline();
            PostBuildPipeline = new PersistorPipeline(new ExamplePersistor());
        }

        public TBuilder Create<TBuilder>()
        {
            object result = typeof(TBuilder) switch
            {
                Type vertex when vertex == typeof(VertexBuilder) => new VertexBuilder(this),
                Type source when source == typeof(SourceBuilder) => new SourceBuilder(this),
                Type root when root == typeof(RootBuilder) => new RootBuilder(this),
                Type node when node == typeof(NodeBuilder) => new NodeBuilder(this),
                Type person when person == typeof(PersonBuilder) => new PersonBuilder(this),
                Type garage when garage == typeof(GarageBuilder) => new GarageBuilder(this),
                Type car when car == typeof(CarBuilder) => new CarBuilder(this),
                Type proxy when proxy == typeof(ProxyBuilder) => new ProxyBuilder(this),
                Type wheel when wheel == typeof(WheelBuilder) => new WheelBuilder(this),
                _ => throw new InvalidOperationException($"Factory does not know how to instantiate: {typeof(TBuilder).Name}")
            };

            return (TBuilder)result;
        }
    }
}
