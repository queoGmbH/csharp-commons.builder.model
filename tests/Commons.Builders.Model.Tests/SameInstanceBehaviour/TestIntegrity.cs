using System;
using System.Reflection;

using FluentAssertions;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.Car;
using Queo.Commons.Builders.Model.Examples.Car.Builders;
using Queo.Commons.Builders.Model.Tests.SameInstanceBehaviour.Util;

namespace Queo.Commons.Builders.Model.Tests.SameInstanceBehaviour
{
    [TestFixture]
    public class TestIntegrity
    {
        [SetUp]
        public void ResetStaticVariables()
        {
            Type wheelBuilder = typeof(ModelBuilder<Wheel>);
            Type carBuilder = typeof(ModelBuilder<Car>);

            FieldInfo wheelBuilderIndex = wheelBuilder.GetField("BUILDER_INDEX",
                                                                                                                    BindingFlags.NonPublic |
                                                                                                                    BindingFlags.Static)!;
            wheelBuilderIndex.SetValue(null, 0);
            FieldInfo carBuilderIndex = carBuilder.GetField("BUILDER_INDEX",
                                                                                                            BindingFlags.NonPublic |
                                                                                                            BindingFlags.Static)!;
            carBuilderIndex.SetValue(null, 0);
        }

        [Test]
        public void TestBuilderIndexCounting()
        {
            Wheel w1 = Create.Wheel();
            Wheel w2 = Create.Wheel();

            w1.Brand.Should().Be("Brand 1");
            w2.Brand.Should().Be("Brand 2");
        }

        [Test]
        public void TestRecreate_IndexCounting()
        {
            WheelBuilder wheelBuilder = Create.Wheel();
            Wheel w1 = wheelBuilder;
            int wheelBuilderIndex = wheelBuilder.BuilderIndex();
            WheelBuilder wheelBuilder2 = wheelBuilder.Recreate();
            Wheel w2 = wheelBuilder2;
            int wheelBuilder2Index = wheelBuilder2.BuilderIndex();

            //Recreate copies the values from the previous configuration
            //They are not newly created with the builder index
            w1.Brand.Should().Be("Brand 1");
            w2.Brand.Should().Be("Brand 1");
            wheelBuilderIndex.Should().Be(1);
            wheelBuilder2Index.Should().Be(2);
        }

        [Test]
        public void TestBuilderIndex_TypeIntegrity()
        {
            Wheel w1 = Create.Wheel();
            Car c1 = Create.Car();

            w1.Brand.Should().Be("Brand 1");
            c1.Name.Should().Be("CarName 1");
        }

        [Test]
        public void TestSingleInstanceFunctionality()
        {
            WheelBuilder builder = Create.Wheel();
            builder.IsFinal.Should().BeFalse();

            Wheel w1 = builder.Build();
            builder.IsFinal.Should().BeTrue();

            Wheel w2 = builder.Build();
            w1.Should().BeSameAs(w2);

            Action act = () => builder.WithBrand("xyz");
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
