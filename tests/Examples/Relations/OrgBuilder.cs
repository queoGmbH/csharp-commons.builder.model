using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Relations;

public class OrgBuilder : ModelBuilder<Org>
{
    public string _name;
    public IBuilder<User> _admin;
    public BuilderCollection<UserBuilder, User> _members;

    public OrgBuilder(IBuilderFactory factory) : base(factory)
    {
        _name = $"Org-{BuilderIndex}";
        _admin = factory.Create<UserBuilder>();

        _members = new(factory);
    }

    public OrgBuilder WithName(string name) => Set(() => _name = name);

    public OrgBuilder WithAdmin(IBuilder<User> admin) => Set(() => _admin = admin);
    public OrgBuilder WithAdmin(Action<UserBuilder> buildAction) => Set(() =>
    {
        _admin = FromAction<UserBuilder, User>(buildAction);
    });

    public OrgBuilder AddMember(IBuilder<User> member) => Set(() => _members.Add(member));
    public OrgBuilder AddMember(Action<UserBuilder> buildAction) => Set(() =>
    {
        _members.Add(buildAction);
    });

    protected override Org BuildModel()
    {
        User admin = _admin.Build();
        Org o = new Org(_name, _admin.Build());

        admin.Org = o;
        o.Members.Add(admin);

        foreach (var member in _members.BuildModels())
        {
            member.Org = o;
            o.Members.Add(member);
        }

        return o;
    }

    protected override OrgBuilder Set(Action action) => Set<OrgBuilder>(action);
}

