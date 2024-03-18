using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Tests.Scenarios;

public class StrBuilder : IBuilder<string>
{
    private string _value;

    public StrBuilder(string str)
    {
        _value = str;
    }

    public string Build()
    {
        return _value;
    }
}
