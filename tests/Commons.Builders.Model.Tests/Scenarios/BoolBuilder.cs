using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Tests.Scenarios;

public class BoolBuilder : IBuilder<bool>
{
    private bool _value;

    public BoolBuilder(bool value)
    {
        _value = value;
    }

    public bool Build()
    {
        return _value;
    }
}
