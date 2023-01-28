using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Utils
{
    public static class BuilderExtensions
    {
        public static IModelBuilder<TModel> Immutable<TModel>(this IModelBuilder<TModel> mutableBuilder)
        {
            return new ImmutableBuilderProxy<TModel>(mutableBuilder);
        }

        public static IEnumerable<IModelBuilder<TModel>> Immutable<TModel>(this IEnumerable<IModelBuilder<TModel>> mutableCollection)
        {
            return mutableCollection.Select(b => b.Immutable());
        }

    }
}
