using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Tests.Scenarios;

public class IntConsumerBuilder : IBuilder<int>
{
    private IBuilder<int> _value;

    public IntConsumerBuilder()
    {
        _value = new IntBuilder(0);
    }

    public IntConsumerBuilder WithValue(IBuilder<int> valueBuilder)
    {
        _value = valueBuilder;
        return this;
    }

    public int Build()
    {
        return _value.Build();
    }
}
