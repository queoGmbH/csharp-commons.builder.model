using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Builder
{
    public class ImmutableBuilderProxy<TModel> : IModelBuilder<TModel>
    {
        private readonly IModelBuilder<TModel> _sourceBuilder;

        public ImmutableBuilderProxy(IModelBuilder<TModel> sourceBuilder)
        {
            _sourceBuilder = sourceBuilder;
        }

        public TModel Build()
        {
            //TODO: dunno if this handling is right
            if (_sourceBuilder is ModelBuilder<TModel> modelBuilder)
            {
                if (modelBuilder.IsFinal)
                {
                    return modelBuilder.Build();
                }
                else
                {
                    throw new InvalidOperationException("The source builder is not configured completely yet, therefore you can't access the model yet!");
                }
            }
            else
            {
                throw new InvalidOperationException("You are not allowed to call build on the builder.");
            }
        }
    }
}
