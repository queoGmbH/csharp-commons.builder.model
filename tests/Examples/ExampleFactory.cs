using System;

using Queo.Commons.Builders.Model.BuildAction;
using Queo.Commons.Builders.Model.Examples.Car.Builders;
using Queo.Commons.Builders.Model.Examples.DAG;
using Queo.Commons.Builders.Model.Examples.Person;
using Queo.Commons.Builders.Model.Examples.Relations;
using Queo.Commons.Builders.Model.Examples.Tree;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples
{
    public class ExampleFactory : IBuilderFactory
    {
        public IPreBuildAction PreBuild { get; }
        public IPostBuildAction PostBuild { get; }

        public ExampleFactory()
        {
            PreBuild = new LogAction();
            PostBuild = new PersistenceAction(new ExamplePersistor());
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
                Type t when t == typeof(CountryBuilder) => new CountryBuilder(this),
                Type t when t == typeof(PresidentBuilder) => new PresidentBuilder(this),
                Type t when t == typeof(UserBuilder) => new UserBuilder(this),
                Type t when t == typeof(OrgBuilder) => new OrgBuilder(this),
                _ => throw new InvalidOperationException($"Factory does not know how to instantiate: {typeof(TBuilder).Name}")
            };

            return (TBuilder)result;
        }
    }
}
