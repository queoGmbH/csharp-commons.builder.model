using System;

namespace Queo.Commons.Builders.Model.Builder
{
    //TODO: WIP - Immutabliity handling
    public class ImmutableBuilderProxy<TModel> : IBuilder<TModel>
    {
        private readonly IBuilder<TModel> _sourceBuilder;

        public ImmutableBuilderProxy(IBuilder<TModel> sourceBuilder)
        {
            _sourceBuilder = sourceBuilder;
        }

        public TModel Build()
        {
            //TEST: check if this is the right way to handle this
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
