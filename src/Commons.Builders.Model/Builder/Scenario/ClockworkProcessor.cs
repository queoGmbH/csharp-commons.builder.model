using System;
using System.Collections.Generic;
using System.Linq;

namespace Queo.Commons.Builders.Model.Builder.Scenario;

/// <summary>
///    The Clockwork processor is a structure that creates the permutations
///    For any given input collection. It accepts a range of iterators (or suppliers)
///    of which each element will be combined with every other element in the other suppliers
///
///    This can be used to create every possible combination of datasets for the suppliers.
///    This is done by always moving the next iterator on step further if the previous iterator
///    resets (loops). This process repeats until the last iterator is is in the last position.
/// </summary>
public class ClockworkProcessor<TConsumer>
{
    private int _targetRounds;
    private int _completedRounds = 0;
    private bool _allRoundsCompleted = false;

    /// <summary>
    ///    Enumerater is the second gear (aka minute pointer)
    /// </summary>
    private IEnumerator<ICyclingSupplier<TConsumer>> _enumerator;

    /// <summary>
    ///    The CyclingSupplier is the first gear (aka seconds pointer)
    /// </summary>
    private ICyclingSupplier<TConsumer>[] _suppliers;

    public ClockworkProcessor(IEnumerable<ICyclingSupplier<TConsumer>> iterators,
                              int rounds)
    {
        _suppliers = iterators.ToArray();
        _enumerator = iterators.GetEnumerator();

        // Move to first
        _enumerator.MoveNext();
        _targetRounds = rounds > 0 ? rounds : 1;
    }


    public bool IsTicking()
    {
        return !_allRoundsCompleted;
    }

    private void Tick()
    {
        // Move the cycling supplier to the next value
        // Aka move the first gear one step forward
        if (_enumerator.Current.Continue())
        {
            Tock();
        }
    }

    private void Tock()
    {
        if (_enumerator.MoveNext())
        {
            Tick();
        }
        else
        {
            if (++_completedRounds >= _targetRounds)
            {
                _allRoundsCompleted = true;
            }
        }

        _enumerator.Reset();
        _enumerator.MoveNext();
    }

    /// <summary>
    ///    Supplies the next item of the permutation to the consumer and 
    ///    moves to the next iteration. 
    ///
    ///    The consumer should be a ModelBuilder where each supplier provides
    ///    a value that should be put into the builder.
    /// </summary>
    public void Process(TConsumer consumer)
    {
        if (_allRoundsCompleted) throw new NotSupportedException("The clockwork has finished.");

        foreach (var it in _suppliers)
        {
            it.SupplyTo(consumer);
        }

        Tick();
    }
}
