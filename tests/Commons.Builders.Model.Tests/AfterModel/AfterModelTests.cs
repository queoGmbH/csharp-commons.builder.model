using NUnit.Framework;

using FluentAssertions;

using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.Relations;

namespace Queo.Commons.Builders.Model.Tests.AfterModel;

[TestFixture]
public class AfterModelTests
{
    [Test]
    public void CreateCountry_WithPresident()
    {
        Country c = Create.Country().WithName("USA")
                                    .WithPresident(p => p.WithName("Roosevelt"));

        c.Name.Should().Be("USA");
        c.President?.Name.Should().Be("Roosevelt");
        c.Should().Be(c.President?.Country);
    }

    [Test]
    public void CreatePresident_WithCountry()
    {
        President p = Create.President().WithName("Roosevelt")
                                        .WithCountry(c => c.WithName("USA"));

        p.Name.Should().Be("Roosevelt");
        p.Country.Name.Should().Be("USA");
        p.Country.President.Should().Be(p);
    }

    [Test]
    public void CreateUser_WithOrg()
    {
        User u = Create.User().WithName("George")
                              .WithOrg(o => o.WithName("GOrg"));

        u.Name.Should().Be("George");
        u.Org?.Name.Should().Be("GOrg");
        u.Org?.Members.Should().Contain(u);
    }

    [Test]
    public void CreateOrg_WithUsers()
    {
        Org o = Create.Org().WithName("GOrg")
                            .WithAdmin(u => u.WithName("George"))
                            .AddMember(u => u.WithName("Other"));

        o.Name.Should().Be("GOrg");
        o.Admin.Name.Should().Be("George");
        o.Admin.Org.Should().Be(o);
        o.Members.Should().Contain(m => m.Name.Equals("Other"));
        o.Members.Should().Contain(m => m.Name.Equals("George"));
    }
}
