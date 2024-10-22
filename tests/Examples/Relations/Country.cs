namespace Queo.Commons.Builders.Model.Examples.Relations;

public class Country(string name)
{
    public string Name => name;
    public President? President { set; get; }
}
