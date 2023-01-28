using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using FluentAssertions;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.Car;
using Queo.Commons.Builders.Model.Examples.Car.Builders;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Tests.SameInstanceBehaviour
{
    [TestFixture]
    public class TestRecreate
    {
        [Test]
        public void Recreate_ShouldNotThrow()
        {
            WheelBuilder builder = Create.Wheel();
            builder.Build();

            Action reuse = () => builder.WithBrand("ChangedBrand");
            Action recreate = () => builder.Recreate().WithBrand("ChangedBrand");

            reuse.Should().Throw<InvalidOperationException>();
            recreate.Should().NotThrow<InvalidOperationException>();
        }

        [Test]
        public void Recreate_Primitives_TestValues()
        {
            WheelBuilder builder = Create.Wheel()
                                                                     .WithBrand("Michelin")
                                                                     .WithPosition(3);
            Wheel original = builder.Build();

            WheelBuilder builderCopy = builder.Recreate()
                                                                              .WithBrand("Continental");
            Wheel copy = builderCopy.Build();

            original.Brand.Should().NotBe(copy.Brand);
            original.Position.Should().Be(copy.Position);
        }

        [Test]
        public void Recreate_InvalidBuilderValues()
        {
            InvalidBuilder builder = new InvalidBuilder(new EmptyFactory());
            builder.Build();

            Action act = () => builder.Recreate();

            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Recreate_SubBuilders_TestValues()
        {
            GarageBuilder builder = Create.Garage().WithAddress("Dresden")
                                                                                       .WithCar(c => c.WithName("Polo")
                                                                                                                      .AddWheel(w => w.WithBrand("Michelin")));
            Garage original = builder.Build();

            GarageBuilder builderCopy = builder.Recreate()
                                                                               .WithCar(c => c.WithName("Golf")
                                                                                                              .AddWheel(w => w.WithBrand("Dunlop")));
            Garage copy = builderCopy.Build();

            original.Address.Should().Be(copy.Address);
            original.Car.Should().NotBe(copy.Car);
            original.Car.Name.Should().Be("Polo");
            copy.Car.Name.Should().Be("Golf");
            original.Car.Wheels[0].Brand.Should().Be("Michelin");
            copy.Car.Wheels[0].Brand.Should().Be("Dunlop");
        }

        [Test]
        public void Recreate_BuilderCollection_TestValues()
        {
            CarBuilder builder = Create.Car().WithName("Polo")
                                                                             .AddWheel(w => w.WithBrand("Michelin").WithPosition(2))
                                                                             .AddWheel(w => w.WithBrand("Continental").WithPosition(4));
            Car original = builder.Build();

            CarBuilder builderCopy = builder.Recreate()
                                                                            .AddWheel(w => w.WithBrand("Dunlop").WithPosition(22));
            Car copy = builderCopy.Build();

            original.Name.Should().Be(copy.Name);
            original.Wheels.Should().HaveCount(2);
            copy.Wheels.Should().HaveCount(3);
            original.Wheels[0].Should().BeEquivalentTo(copy.Wheels[0]);
            original.Wheels[1].Should().BeEquivalentTo(copy.Wheels[1]);
        }

        [Test]
        public void Recreate_BuilderCollection_ShouldContainNewBuilders()
        {
            CarBuilder builder = Create.Car().WithName("Polo")
                                                                             .AddWheel(w => w.WithBrand("Michelin").WithPosition(2))
                                                                             .AddWheel(w => w.WithBrand("Continental").WithPosition(4));
            Car original = builder.Build();

            CarBuilder builderCopy = builder.Recreate();

            Action modify1 = () => builderCopy.GetWheels().First().WithBrand("Bridgestone");
            Action modify2 = () => builderCopy.GetWheels().Skip(1).First().WithBrand("Dunlop");
            modify1.Should().NotThrow<InvalidOperationException>();
            modify2.Should().NotThrow<InvalidOperationException>();

            Car copy = builderCopy.Build();
            copy.Wheels[0].Brand.Should().Be("Bridgestone");
            copy.Wheels[1].Brand.Should().Be("Dunlop");
            original.Wheels[0].Brand.Should().Be("Michelin");
            original.Wheels[1].Brand.Should().Be("Continental");
        }

        [Test]
        public void Recreate_ProxyBuilder_ShouldBeNew()
        {
            ProxyBuilder builder = Create.Proxy().WithName("Polo")
                                                                                     .AddWheel(w => w.WithBrand("Michelin").WithPosition(2))
                                                                                     .AddWheel(w => w.WithBrand("Continental").WithPosition(4));
            Garage original = builder.Build();

            ProxyBuilder builderCopy = builder.Recreate();

            Action modify1 = () => builderCopy.WithName("Golf");
            Action modify2 = () => builderCopy.AddWheel(w => w.WithBrand("Dunlop").WithPosition(3));
            modify1.Should().NotThrow<InvalidOperationException>();
            modify2.Should().NotThrow<InvalidOperationException>();

            Garage copy = builderCopy.Build();
            original.Car.Name.Should().Be("Polo");
            original.Car.Wheels.Should().HaveCount(2);
            copy.Address.Should().Be(original.Address);
            copy.Car.Name.Should().Be("Golf");
            copy.Car.Wheels.Should().HaveCount(3);
            copy.Car.Wheels[0].Should().NotBe(original.Car.Wheels[0]);
            copy.Car.Wheels[1].Should().NotBe(original.Car.Wheels[1]);
        }

        //----------------------
        //	BUILDER COLLECTION
        //----------------------

        [Test]
        public void Recreate_BuilderCollection_HappyPath()
        {
            var collection = new BuilderCollection<CarBuilder, Car>(new EmptyFactory());
            CarBuilder carBuilder1 = Create.Car().WithName("Polo");
            CarBuilder carBuilder2 = Create.Car().WithName("Golf");
            collection.Add(carBuilder1);
            collection.Add(carBuilder2);
            collection.BuildModels();

            BuilderCollection<CarBuilder, Car> collectionCopy = collection.Recreate();

            collectionCopy.GetBuilders().Should().HaveCount(2);
            CarBuilder copyBuilder1 = collectionCopy.GetDefaultBuilders().First();
            CarBuilder copyBuilder2 = collectionCopy.GetDefaultBuilders().Skip(1).First();

            copyBuilder1.Build().Should().NotBe(carBuilder1.Build());
            copyBuilder2.Build().Should().NotBe(carBuilder2.Build());
            copyBuilder1.Build().Name.Should().Be(carBuilder1.Build().Name);
            copyBuilder2.Build().Name.Should().Be(carBuilder2.Build().Name);
        }

        [Test]
        public void Recreate_BuilderCollection_NonRecreatable()
        {
            var collection = new BuilderCollection<CarBuilder, Car>(new EmptyFactory());
            var nonModelBuilder = new NonRecreatableBuilder();
            collection.Add(nonModelBuilder);

            Action act = () => collection.Recreate();
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Recreate_BuilderColleciton_CustomRecreatable()
        {
            var collection = new BuilderCollection<CarBuilder, Car>(new EmptyFactory());
            var nonModelBuilder = new NonModelBuilder();
            collection.Add(nonModelBuilder);

            Action act = () => collection.Recreate();
            act.Should().NotThrow<ValidationException>();
        }
    }
}
