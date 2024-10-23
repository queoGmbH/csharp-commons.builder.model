using System;
using System.Collections.Generic;
using System.Linq;

namespace Queo.Commons.Builders.Model.Builder.Scenario;

/// <summary>
///    Cycling iterator implementation that supplies builders.
///    The TargetBuilder is the one who receieves the Builders from the collection.
///    This is used for distributing the builders in the collection to different TargetBuilders.
/// </summary>
public class BuilderSupplier<TConsumerBuilder, TModel> : ICyclingSupplier<TConsumerBuilder>
{
    private readonly IBuilder<TModel>[] _supply;
    private readonly Action<TConsumerBuilder, IBuilder<TModel>> _builderAction;

    private int _index = 0;

    public int Count { get; }

    public BuilderSupplier(IEnumerable<IBuilder<TModel>> supply,
                           Action<TConsumerBuilder, IBuilder<TModel>> supplyAction)
    {
        _builderAction = supplyAction;
        _supply = supply.ToArray();
        Count = _supply.Length;
    }

    /// <inheritdoc/>
    public bool Continue()
    {
        _index++;
        if (_index >= _supply.Length)
        {
            _index = 0;
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public void SupplyTo(TConsumerBuilder consumerBuilder)
    {
        if (Count == 0) return;

        //receive next result
        _builderAction(consumerBuilder, _supply[_index]);
    }
}
