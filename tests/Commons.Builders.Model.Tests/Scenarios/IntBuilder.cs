using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Tests.Scenarios;

public class IntBuilder : IBuilder<int>
{
    private int _value;

    public IntBuilder(int value)
    {
        _value = value;
    }

    public int Build()
    {
        return _value;
    }
}
