using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Queo.Commons.Builders.Model.Factory;
using Queo.Commons.Builders.Model.Utils;

namespace Queo.Commons.Builders.Model.Builder
{
    public class BuilderCollection<TDefaultBuilder, TModel>
                    : IRecreatable<BuilderCollection<TDefaultBuilder, TModel>>,
                      IEnumerable<IModelBuilder<TModel>>
                    where TDefaultBuilder : IModelBuilder<TModel>
    {
        private readonly IBuilderFactory _factory;
        public BuilderCollection(IBuilderFactory factory)
        {
            _factory = factory;
        }

        private readonly ICollection<IModelBuilder<TModel>> _builders = new List<IModelBuilder<TModel>>();

        /// <summary>
        ///		Generalized add method to add any kind of builder for the Model
        /// </summary>
        public void Add(IModelBuilder<TModel> builder)
        {
            _builders.Add(builder);
        }

        /// <summary>
        /// 	Add method for the most common builder for this Collection
        /// </summary>
        public void Add(TDefaultBuilder builder)
        {
            _builders.Add(builder);
        }

        public void Add(IEnumerable<TDefaultBuilder> builders)
        {
            foreach (TDefaultBuilder b in builders)
            {
                _builders.Add(b);
            }
        }

        /// <summary>
        ///		Add method for a specific type of builder
        /// </summary>
        /// <typeparam name="TCustomBuilder">A specific builder that retuns the Colletions Model</typeparam>
        public void Add<TCustomBuilder>(TCustomBuilder builder) where TCustomBuilder : IModelBuilder<TModel>
        {
            _builders.Add(builder);
        }

        public void Add<TCustomBuilder>(IEnumerable<TCustomBuilder> builders) where TCustomBuilder : IModelBuilder<TModel>
        {
            foreach (TCustomBuilder b in builders)
            {
                _builders.Add(b);
            }
        }

        /// <summary>
        ///		Adds a builder in form of a build action for the default builder.
        ///		Will result in a builder of TDefaultBuilder type
        /// </summary>
        /// <param name="buildAction">Build action to base the builder on</param>
        public void Add(Action<TDefaultBuilder> buildAction)
        {
            _builders.Add(buildAction.ToBuilder(_factory));
        }

        /// <summary>
        ///		Adds a builden in form of a build action for a specific type of builder.
        ///		Will result in a builder of the specified type
        /// </summary>
        /// <param name="buildAction">Build action to base the builder on</param>
        /// <typeparam name="TCustomBuilder">Type of builder that should be created</typeparam>
        public void Add<TCustomBuilder>(Action<TCustomBuilder> buildAction) where TCustomBuilder : IModelBuilder<TModel>
        {
            _builders.Add(buildAction.ToBuilder(_factory));
        }


        /// <summary>
        ///     Returns All model builders that where added to the collection
        /// </summary>
        public IEnumerable<IModelBuilder<TModel>> GetBuilders()
        {
            return _builders;
        }

        /// <summary>
        ///		Returns all default builders of the collection
        /// </summary>
        /// <remarks>
        /// 	This does not return all builders neccessarly.
        ///		If builders of another type are part of the collection,
        /// 	these will NOT be returned here
        /// </remarks>

        public IEnumerable<TDefaultBuilder> GetDefaultBuilders()
        {
            return _builders.OfType<TDefaultBuilder>();
        }

        /// <summary>
        ///		Returns all builders of the specified generic type
        /// </summary>
        /// <typeparam name="TBuilderType">Types of builders to return</typeparam>
        /// <remarks>
        /// 	Only the builders of the specific type are returned.
        ///		The collection might still contain different types of builders.
        /// </remarks>
        public IEnumerable<TBuilderType> GetBuilders<TBuilderType>() where TBuilderType : IModelBuilder<TModel>
        {
            return _builders.OfType<TBuilderType>();
        }

        /// <summary>
        ///		Builds all models from every type of builder.
        /// </summary>
        public IEnumerable<TModel> BuildModels()
        {
            return _builders.Select(s => s.Build());
        }


        /// <summary>
        ///		Recreates the builder collection, by calling Recreate() on each builder
        /// </summary>
        public BuilderCollection<TDefaultBuilder, TModel> Recreate()
        {
            var collectionCopy = new BuilderCollection<TDefaultBuilder, TModel>(_factory);
            foreach (IModelBuilder<TModel> builder in _builders)
            {
                if (builder is IRecreatable<IModelBuilder<TModel>> recreatable)
                {
                    collectionCopy.Add(recreatable.Recreate());
                }
                else
                {
                    throw new ValidationException("Recreate can onle by used, if all added builders Implement the " +
                                                 $"IRecreatable interface! Problematic type was: {builder.GetType()}");
                }
            }
            return collectionCopy;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetBuilders().GetEnumerator();
        }

        IEnumerator<IModelBuilder<TModel>> IEnumerable<IModelBuilder<TModel>>.GetEnumerator()
        {
            return GetBuilders().GetEnumerator();
        }
    }
}
