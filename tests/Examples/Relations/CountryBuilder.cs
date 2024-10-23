
using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Relations;

public class CountryBuilder : ModelBuilder<Country>
{
    private string _name;
    private IBuilder<President>? _president;

    public CountryBuilder(IBuilderFactory factory) : base(factory)
    {
        _name = $"Country-{BuilderIndex}";
        _president = null;
    }

    public CountryBuilder WithName(string name) => Set(() => _name = name);
    public CountryBuilder WithPresident(Action<PresidentBuilder> buildAction) => Set(() =>
    {
        var builder = FromAction<PresidentBuilder, President>(buildAction);
        builder.WithCountry(this);
        _president = builder;
    });

    protected override Country BuildModel() => new(_name);

    protected override void PostBuild(Country model)
    {
        if (_president is not null)
        {
            // President can only be build, if the country is already available
            model.President = _president.Build();
        }
    }

    protected override CountryBuilder Set(Action action) => Set<CountryBuilder>(action);
}
