using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Factory;
using Queo.Commons.Builders.Model.Peristence;
using Queo.Commons.Builders.Model.Utils;

namespace Queo.Commons.Builders.Model.Builder
{
    //TODO: the type of this is kind of a BuilderCollection
    public abstract class ScenarioBuilder<T> : IModelBuilder<IEnumerable<IModelBuilder<T>>>
    {
        protected IBuilderFactory _factory;
        /// <summary>
        ///     A scenario builder doesn't want to persist himself, because he creates a collection of builders
        ///     So not the collection itself, but the models of the builders in the collections should be persisted
        ///     (All according to the persistor thats defined in the IBuilderFactory!)
        /// </summary>
        /// <param name="factory">The factory for the builders the scenario creates </param>
        protected ScenarioBuilder(IBuilderFactory factory) { _factory = factory; }

        private IEnumerable<IModelBuilder<T>>? _model;

        public IEnumerable<IModelBuilder<T>> Build()
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
            return Build().Select(b => b.Build())
                          .ToList()
                          .Where(condition);
        }

        public IEnumerable<T> All()
        {
            return Get(v => true);
        }

        protected abstract IEnumerable<IModelBuilder<T>> BuildModel();

        /// <summary>
        ///		Converts a build action into a builder
        ///		This is used, when the builder doesn't use the Builder collection
        ///		But just a ModelBuilder instead
        /// </summary>
        /// <param name="builderAction">Builder action</param>
        /// <typeparam name="TBuilder">Builder type to be created</typeparam>
        /// <typeparam name="TBuilderModel">Model type that the builder produces</typeparam>
        protected TBuilder FromAction<TBuilder, TBuilderModel>(Action<TBuilder> builderAction)
                           where TBuilder : IModelBuilder<TBuilderModel>
        {
            return builderAction.ToBuilder(_factory);
        }

        /// <summary>
        ///		QoL Method for simpler builder method definition
        ///		Override this method and call the generic version with your builder type
        /// </summary>
        /// <param name="action">Action that sets the member variable</param>
        protected virtual IModelBuilder<IEnumerable<IModelBuilder<T>>> Set(Action action) => Set<ScenarioBuilder<T>>(action);

        /// <summary>
        ///		QoL Method for simpler builder method definition
        ///		It handles calling of the Validate method as well as returning the correct builder
        /// </summary>
        /// <param name="setAction">Action that sets the member variable</param>
        /// <typeparam name="TBuilder">The actual builder type</typeparam>
        /// <returns></returns>
        protected TBuilder Set<TBuilder>(Action setAction) where TBuilder : ScenarioBuilder<T>
        {
            //TODO:?
            // Validate();
            setAction();
            return (TBuilder)this;
        }

        // IEnumerable<IModelBuilder<T>> IModelBuilder<IEnumerable<IModelBuilder<T>>>.Build()
        // {
        //     throw new NotImplementedException();
        // }
    }
}
