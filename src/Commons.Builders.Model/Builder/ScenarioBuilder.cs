using System;
using System.Collections.Generic;
using System.Linq;

using Queo.Commons.Builders.Model.Factory;
using Queo.Commons.Builders.Model.Utils;

namespace Queo.Commons.Builders.Model.Builder;

//TODO: WIP - Proper abstractions
public abstract class ScenarioBuilder<T> : IBuilder<IEnumerable<IBuilder<T>>>
{
    protected IBuilderFactory _factory;

    /// <summary>
    ///     A scenario builder doesn't want to persist himself, because he creates a collection of builders
    ///     So not the collection itself, but the models of the builders in the collections should be persisted
    ///     (All according to the persistor thats defined in the IBuilderFactory!)
    /// </summary>
    /// <param name="factory">The factory for the builders the scenario creates </param>
    protected ScenarioBuilder(IBuilderFactory factory) { _factory = factory; }


    /// <summary>
    ///    Resolves all the buliders, so that this builder can not be changed anymore
    ///    Important to call if none of the other methods are called, that resolve this
    ///    Only when the bulider is resolved the entities are written via the persitence strategy
    /// </summary>
    public ScenarioBuilder<T> Commit()
    {
        All();
        return this;
    }


    private IEnumerable<IBuilder<T>>? _model;

    public IEnumerable<IBuilder<T>> Build()
    {
        if (_model is null)
        {
            //no pipeline calls im contrast to ModelBuilder
            _model = BuildModel();
        }
        return _model;
    }

    public IEnumerable<T> Get(Func<T, bool> condition)
    {
        //We want to resolve it here, that the persiting is executed!
        var results = Build().Select(b => b.Build())
                             .ToArray();
        return results.Where(condition).ToArray();
    }

    public IEnumerable<T> All()
    {
        return Get(v => true);
    }

    protected abstract IEnumerable<IBuilder<T>> BuildModel();

    /// <summary>
    ///		Converts a build action into a builder
    ///		This is used, when the builder doesn't use the Builder collection
    ///		But just a ModelBuilder instead
    /// </summary>
    /// <param name="builderAction">Builder action</param>
    /// <typeparam name="TBuilder">Builder type to be created</typeparam>
    /// <typeparam name="TBuilderModel">Model type that the builder produces</typeparam>
    protected TBuilder FromAction<TBuilder, TBuilderModel>(Action<TBuilder> builderAction)
                       where TBuilder : IBuilder<TBuilderModel>
    {
        return builderAction.ToBuilder(_factory);
    }

    /// <summary>
    ///		QoL Method for simpler builder method definition
    ///		Override this method and call the generic version with your builder type
    /// </summary>
    /// <param name="action">Action that sets the member variable</param>
    protected virtual IBuilder<IEnumerable<IBuilder<T>>> Set(Action action) => Set<ScenarioBuilder<T>>(action);

    /// <summary>
    ///		QoL Method for simpler builder method definition
    ///		It handles calling of the Validate method as well as returning the correct builder
    /// </summary>
    /// <param name="setAction">Action that sets the member variable</param>
    /// <typeparam name="TBuilder">The actual builder type</typeparam>
    /// <returns></returns>
    protected TBuilder Set<TBuilder>(Action setAction) where TBuilder : ScenarioBuilder<T>
    {
        //TODO: - Same handling as Model builder or different?
        // This is probably not required because the scenario holds a list of builders
        // And not the model itself
        // Validate();
        setAction();
        return (TBuilder)this;
    }
}
