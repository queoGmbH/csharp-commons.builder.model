
using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Relations;

public class UserBuilder : ModelBuilder<User>
{
    private string _name;
    private IBuilder<Org>? _org;

    public UserBuilder(IBuilderFactory factory) : base(factory)
    {
        _name = $"User-{BuilderIndex}";
        _org = null;
    }

    public UserBuilder WithName(string name) => Set(() => _name = name);
    public UserBuilder WithOrg(IBuilder<Org> org) => Set(() => _org = org);
    public UserBuilder WithOrg(Action<OrgBuilder> buildAction) => Set(() =>
    {
        _org = FromAction<OrgBuilder, Org>(buildAction);
    });

    protected override User BuildModel() => new(_name);

    protected override void PostBuild(User model)
    {
        if (_org is not null)
        {
            var org = _org.Build();
            model.Org = org;
            org.Members.Add(model);
        }
    }

    protected override UserBuilder Set(Action action) => Set<UserBuilder>(action);
}
