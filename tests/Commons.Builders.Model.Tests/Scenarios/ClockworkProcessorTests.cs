using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Builder.Scenario;

namespace Queo.Commons.Builders.Model.Tests.Scenarios;

public class ClockworkProcessorTests
{
    [Test]
    public void Test_Single()
    {
        var intSuppliers = Enumerable.Range(1, 10).Select(i => new IntBuilder(i));
        var suppliers = new List<ICyclingSupplier<CollectorConsumer>>();

        ICyclingSupplier<CollectorConsumer> intSupplier = new BuilderSupplier<CollectorConsumer, int>(intSuppliers, (c, i) => c.ConsumeInt(i.Build()));
        suppliers.Add(intSupplier);
        var clockwork = new ClockworkProcessor<CollectorConsumer>(suppliers, 1);

        var consumer = new CollectorConsumer();
        while (clockwork.IsTicking())
        {
            clockwork.Process(consumer);
            consumer.Commit();
        }

        consumer.Combinations.Should().HaveCount(10);
        consumer.Combinations.Distinct().Should().HaveCount(10);
    }

    [Test]
    public void Test_DoubleRound()
    {
        var intSuppliers = Enumerable.Range(1, 10).Select(i => new IntBuilder(i));
        var suppliers = new List<ICyclingSupplier<CollectorConsumer>>();

        ICyclingSupplier<CollectorConsumer> intSupplier = new BuilderSupplier<CollectorConsumer, int>(intSuppliers, (c, i) => c.ConsumeInt(i.Build()));
        suppliers.Add(intSupplier);
        var twoRoundClockwork = new ClockworkProcessor<CollectorConsumer>(suppliers, 2);

        var consumer = new CollectorConsumer();
        while (twoRoundClockwork.IsTicking())
        {
            twoRoundClockwork.Process(consumer);
            consumer.Commit();
        }

        // 10 Int * 2 Rounds
        consumer.Combinations.Should().HaveCount(20);
        consumer.Combinations.Distinct().Should().HaveCount(10);
    }

    [Test]
    public void Test_TwoSuppliers()
    {
        var intSuppliers = Enumerable.Range(1, 10).Select(i => new IntBuilder(i));
        var strSuppliers = Enumerable.Range(65, 26).Select(i => new StrBuilder("" + (char)i));
        var suppliers = new List<ICyclingSupplier<CollectorConsumer>>();

        ICyclingSupplier<CollectorConsumer> intSupplier = new BuilderSupplier<CollectorConsumer, int>(intSuppliers, (c, i) => c.ConsumeInt(i.Build()));
        ICyclingSupplier<CollectorConsumer> strSupplier = new BuilderSupplier<CollectorConsumer, string>(strSuppliers, (c, s) => c.ConsumeString(s.Build()));
        suppliers.Add(intSupplier);
        suppliers.Add(strSupplier);
        var twoRoundClockwork = new ClockworkProcessor<CollectorConsumer>(suppliers, 2);

        var consumer = new CollectorConsumer();
        while (twoRoundClockwork.IsTicking())
        {
            twoRoundClockwork.Process(consumer);
            consumer.Commit();
        }

        // 10 Ints * 26 Strings * 2 Rounds
        consumer.Combinations.Should().HaveCount(520);
        consumer.Combinations.Distinct().Should().HaveCount(260);
    }

    [Test]
    public void Test_ThreeSuppliers()
    {
        var intSuppliers = Enumerable.Range(1, 10).Select(i => new IntBuilder(i));
        var strSuppliers = Enumerable.Range(65, 26).Select(i => new StrBuilder("" + (char)i));
        var boolSuppliers = new bool[] { true, false }.Select(b => new BoolBuilder(b));
        var suppliers = new List<ICyclingSupplier<CollectorConsumer>>();

        ICyclingSupplier<CollectorConsumer> intSupplier = new BuilderSupplier<CollectorConsumer, int>(intSuppliers, (c, i) => c.ConsumeInt(i.Build()));
        ICyclingSupplier<CollectorConsumer> strSupplier = new BuilderSupplier<CollectorConsumer, string>(strSuppliers, (c, s) => c.ConsumeString(s.Build()));
        ICyclingSupplier<CollectorConsumer> boolSupplier = new BuilderSupplier<CollectorConsumer, bool>(boolSuppliers, (c, b) => c.ConsumeBool(b.Build()));
        suppliers.Add(intSupplier);
        suppliers.Add(strSupplier);
        suppliers.Add(boolSupplier);
        var twoRoundClockwork = new ClockworkProcessor<CollectorConsumer>(suppliers, 2);

        var consumer = new CollectorConsumer();
        while (twoRoundClockwork.IsTicking())
        {
            twoRoundClockwork.Process(consumer);
            consumer.Commit();
        }

        // 10 Ints * 26 Strings * 2 bools
        consumer.Combinations.Should().HaveCount(1040);
        consumer.Combinations.Distinct().Should().HaveCount(520);
    }
}

public class CollectorConsumer
{
    public bool? CurrentBool { get; private set; } = null;
    public int CurrentInt { get; private set; } = -1;
    public string CurrentString { get; private set; } = "";

    public ICollection<string> Combinations { get; } = new List<string>();

    public void ConsumeBool(bool value)
    {
        CurrentBool = value;
    }

    public void ConsumeInt(int value)
    {
        CurrentInt = value;
    }

    public void ConsumeString(string value)
    {
        CurrentString = value;
    }


    public void Commit()
    {
        string combinations = "";

        if (CurrentBool is not null)
        {
            combinations += CurrentBool.ToString();
        }

        if (CurrentInt != -1)
        {
            combinations += " " + CurrentInt;
        }

        if (!string.IsNullOrEmpty(CurrentString))
        {
            combinations += " " + CurrentString;
        }

        Combinations.Add(combinations);
    }
}

