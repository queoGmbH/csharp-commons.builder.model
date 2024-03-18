using System;

using FluentAssertions;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.Person;
using Queo.Commons.Builders.Model.Examples.Person.Mocks;

namespace Queo.Commons.Builders.Model.Tests.BuilderCollectionTests
{
    [TestFixture]
    public class MixedTest
    {

        [Test]
        public void GeneratorBaseTest()
        {
            PersonGeneratorBuilder builder = Create.GeneratedPerson();
            builder.WithMinAge(10).WithMaxAge(25);
            Console.WriteLine(builder.Build());
            builder.Build().Name.Should().NotBeEmpty();
            builder.Build().Age.Should().BeInRange(10, 25);
        }

        [Test]
        public void DefaultBuilder_WithGeneratorChilds()
        {
            PersonBuilder parent = Create.Person().WithName("Gustav").WithAge(45);
            for (int i = 0; i < 12; i++)
            {
                var child = Create.GeneratedPerson().WithMinAge(10).WithMaxAge(30);
                parent.HasChild(child);
            }
            Console.WriteLine(parent.Build());
            parent.Build().Children.Should().HaveCount(12);
        }

        [Test]
        public void DefaultBuilder_WithMixedChilds()
        {

            PersonBuilder parent = Create.Person().WithName("Gustav").WithAge(45);
            for (int i = 0; i < 12; i++)
            {
                var child = Create.GeneratedPerson().WithMinAge(10).WithMaxAge(30);
                parent.HasChild(child);
            }
            var specialChild = Create.Person().WithName("Jones").WithAge(5);
            parent.HasChild(specialChild);
            parent.Build().Children.Should().HaveCount(13);
            parent.Build().Children.Should().Contain(specialChild.Build());
        }

    }
}
