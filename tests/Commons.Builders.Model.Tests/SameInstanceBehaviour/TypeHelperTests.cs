using System.Collections;
using System.Collections.Generic;

using FluentAssertions;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Examples.Car;
using Queo.Commons.Builders.Model.Examples.Car.Builders;
using Queo.Commons.Builders.Model.Utils;

namespace Queo.Commons.Builders.Model.Tests.SameInstanceBehaviour
{
    [TestFixture]
    public class TypeHelperTests
    {
        [Test]
        public void TestSameType_SimpleInheritance()
        {
            bool isA = typeof(string).IsOfType(typeof(object));

            isA.Should().BeTrue();
        }

        [Test]
        public void TestCollection_GenericType()
        {
            bool isA = typeof(BuilderCollection<,>).IsOfType(typeof(object));

            isA.Should().BeTrue();
        }

        [Test]
        public void TestCollect_SpecificCollection()
        {
            bool isA = typeof(BuilderCollection<CarBuilder, Car>).IsOfType(typeof(BuilderCollection<,>));

            isA.Should().BeTrue();
        }


        [Test]
        public void TestIsModelBuilder()
        {
            bool isA = typeof(CarBuilder).IsOfType(typeof(ModelBuilder<>));

            isA.Should().BeTrue();
        }


        //------------------------
        //  Interface Type Tests
        //------------------------

        [Test]
        public void TestModelBuilder_InterFaceType()
        {
            bool isA = typeof(ModelBuilder<>).ImplementsInterface(typeof(IModelBuilder<>));

            isA.Should().BeTrue();
        }

        [Test]
        public void TestModelBuilder_SpecificOnGeneric()
        {
            bool isA = typeof(ModelBuilder<Car>).ImplementsInterface(typeof(IModelBuilder<>));

            isA.Should().BeTrue();
        }

        [Test]
        public void TestInterfaceType()
        {
            bool isA = typeof(List<string>).ImplementsInterface(typeof(IEnumerable<string>));

            isA.Should().BeTrue();
        }

        [Test]
        public void TestGenericInterfaceType()
        {
            bool isA = typeof(List<string>).ImplementsInterface(typeof(IEnumerable<>));

            isA.Should().BeTrue();
        }

        [Test]
        public void TestRawInterfaceType()
        {
            bool isA = typeof(List<string>).ImplementsInterface(typeof(IEnumerable));

            isA.Should().BeTrue();
        }

        //-------------------------
        //	Generic Arguments Test
        //-------------------------

        [Test]
        public void TestGenericArguments_DirectType()
        {
            var types = typeof(ModelBuilder<Car>).GetGenericArgsOf(typeof(IModelBuilder<>));

            types.Should().Equal(typeof(Car));
        }

        [Test]
        public void TestGenericArguments_BuilderType()
        {
            var types = typeof(CarBuilder).GetGenericArgsOf(typeof(IModelBuilder<>));

            types.Should().Equal(typeof(Car));
        }
    }
}
