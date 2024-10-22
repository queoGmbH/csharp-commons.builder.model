using Queo.Commons.Builders.Model.Examples.Car.Builders;
using Queo.Commons.Builders.Model.Examples.DAG;
using Queo.Commons.Builders.Model.Examples.Person;
using Queo.Commons.Builders.Model.Examples.Person.Mocks;
using Queo.Commons.Builders.Model.Examples.Relations;
using Queo.Commons.Builders.Model.Examples.Tree;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples
{
    public static class Create
    {
        private static IBuilderFactory Factory = new ExampleFactory();
        private static IBuilderFactory GeneratorFactory = new GeneratorFactory(new MockDataGenerator());

        public static VertexBuilder Vertex() => Factory.Create<VertexBuilder>();
        public static SourceBuilder Source() => Factory.Create<SourceBuilder>();

        public static RootBuilder Root() => Factory.Create<RootBuilder>();
        public static NodeBuilder Node() => Factory.Create<NodeBuilder>();

        public static GarageBuilder Garage() => Factory.Create<GarageBuilder>();
        public static CarBuilder Car() => Factory.Create<CarBuilder>();
        public static ProxyBuilder Proxy() => Factory.Create<ProxyBuilder>();
        public static WheelBuilder Wheel() => Factory.Create<WheelBuilder>();

        public static PersonBuilder Person() => Factory.Create<PersonBuilder>();
        public static PersonGeneratorBuilder GeneratedPerson() => GeneratorFactory.Create<PersonGeneratorBuilder>();

        public static CountryBuilder Country() => Factory.Create<CountryBuilder>();
        public static PresidentBuilder President() => Factory.Create<PresidentBuilder>();

        public static UserBuilder User() => Factory.Create<UserBuilder>();
        public static OrgBuilder Org() => Factory.Create<OrgBuilder>();
    }
}
