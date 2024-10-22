using System.Collections.Generic;

namespace Queo.Commons.Builders.Model.Examples.Relations;

public class Org(string name, User admin)
{
    public string Name => name;
    public User Admin => admin;

    public ICollection<User> Members { get; } = [];
}

