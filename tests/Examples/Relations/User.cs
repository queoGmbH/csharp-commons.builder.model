namespace Queo.Commons.Builders.Model.Examples.Relations;

public class User(string name)
{
    public string Name = name;
    public Org? Org { set; get; }
}
