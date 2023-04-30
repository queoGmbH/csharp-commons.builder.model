namespace Queo.Commons.Builders.Model.Builder.Scenario;

/// <summary>
///    An iterator that cycles around it's items,
///    without the need of calling reset.
///
///    The items are used as supply for the target of type T.
/// </summary>
public interface ICyclingSupplier<T>
{
    /// <summary>
    ///    Count of the items the iterator holds
    /// </summary>
    public int Count { get; }


    /// <summary>
    ///    Moves the iterator one step forward.
    ///    Will return true if the cycle ended / resetted
    ///    When true is return, the current iterator value is at index 0
    ///    which means the first item will be used
    /// </summary>
    bool Continue();


    /// <summary>
    ///    Supplies the curretn iterator item to the target
    ///    How the item is supplied is implementation specific
    /// </summary>
    void SupplyTo(T supplyTarget);
}
