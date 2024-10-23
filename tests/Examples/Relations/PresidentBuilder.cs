
using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Relations;

public class PresidentBuilder : ModelBuilder<President>
{
    private string _name;
    private IBuilder<Country> _country;

    public PresidentBuilder(IBuilderFactory factory) : base(factory)
    {
        _name = $"President-{BuilderIndex}";
        _country = factory.Create<CountryBuilder>();
    }

    public PresidentBuilder WithName(string name) => Set(() => _name = name);
    public PresidentBuilder WithCountry(IBuilder<Country> country) => Set(() => _country = country);
    public PresidentBuilder WithCountry(Action<CountryBuilder> buildAction) => Set(() =>
    {
        _country = FromAction<CountryBuilder, Country>(buildAction);
    });

    protected override President BuildModel()
    {
        President p = new(_name, _country.Build());
        _country.Build().President = p;

        return p;
    }
    protected override PresidentBuilder Set(Action action) => Set<PresidentBuilder>(action);
}

