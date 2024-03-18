using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Builder.Scenario;

namespace Queo.Commons.Builders.Model.Tests.Scenarios;

public class BuilderSupplierTest
{
    [Test]
    public void TestBuilderSupplier()
    {
        int upperEnd = 5;
        int cycleCount = 4;
        IEnumerable<IBuilder<int>> suppy = Enumerable.Range(1, upperEnd)
                                                     .Select(n => new IntBuilder(n));

        ICyclingSupplier<IntConsumerBuilder> supplier = new BuilderSupplier<IntConsumerBuilder, int>(suppy, (c, s) => c.WithValue(s));

        IntConsumerBuilder builder = new IntConsumerBuilder();

        for (int i = 0; i < cycleCount * upperEnd; i++)
        {
            int expNum = (i % upperEnd) + 1;

            supplier.SupplyTo(builder);
            builder.Build().Should().Be(expNum);
            supplier.Continue();
        }
    }
}
