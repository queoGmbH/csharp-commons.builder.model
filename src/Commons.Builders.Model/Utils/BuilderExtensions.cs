using System.Collections.Generic;
using System.Linq;

using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Utils;

//TODO: WIP
public static class BuilderExtensions
{
    public static IBuilder<TModel> Immutable<TModel>(this IBuilder<TModel> mutableBuilder)
    {
        return new ImmutableBuilderProxy<TModel>(mutableBuilder);
    }

    public static IEnumerable<IBuilder<TModel>> Immutable<TModel>(this IEnumerable<IBuilder<TModel>> mutableCollection)
    {
        return mutableCollection.Select(b => b.Immutable());
    }
}
