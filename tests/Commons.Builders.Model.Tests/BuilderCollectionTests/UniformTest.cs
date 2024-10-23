using System;

using FluentAssertions;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.Person;

namespace Queo.Commons.Builders.Model.Tests.BuilderCollectionTests
{
    [TestFixture]
    public class UniformTest
    {

        [Test]
        public void TestModelEquality()
        {
            var builder = Create.Person().WithName("Jochen").WithAge(25);
            builder.Build().Should().BeSameAs(builder.Build());
        }

        [Test]
        public void TestModelEquality_ChangeAfter()
        {
            var builder = Create.Person().WithName("Jochen").WithAge(25);

            Action act = () => builder.Build().Should().NotBe(builder.WithName("James").Build());
            act.Should().Throw<InvalidOperationException>();
        }


        [Test]
        public void BasicUniformTest()
        {
            var jochen = Create.Person().WithName("Jochen").WithAge(25);
            var james = Create.Person().WithName("James").WithAge(30);
            var jojo = Create.Person().WithName("Jojo").WithAge(35);
            var father = Create.Person().WithName("Dad").WithAge(60)
                                                                    .HasChild(jochen)
                                                                    .HasChild(james)
                                                                    .HasChild(jojo);

            father.Build().Children.Should().BeEquivalentTo(new Person[] { jochen.Build(), james.Build(), jojo.Build() });
        }
    }
}
