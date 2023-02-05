using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Queo.Commons.Builders.Model.Factory;
using Queo.Commons.Builders.Model.Utils;

namespace Queo.Commons.Builders.Model.Builder
{
    /// <summary>
    ///     Base class for a model builder.
    /// </summary>
    /// <typeparam name="TModel">Model class type</typeparam>
    public abstract class ModelBuilder<TModel> : IBuilder<TModel>, IRecreatable<ModelBuilder<TModel>>
    {

        public static implicit operator TModel(ModelBuilder<TModel> builder) => builder.Build();

        /// <summary>
        ///		Counts the index per type, to ensure unique default names
        /// </summary>
        private static int BUILDER_INDEX = 0;

        /// <summary>
        ///     Preserve index information that this builder used to build it's model
        /// </summary>
        public int BuilderIndex { get; private set; }

        /// <summary>
        ///		'Cached' Model, to ensure the same instance is returned when calling 'Build()'
        /// </summary>
        private TModel? _model;

        /// <summary>
        ///		Checks if the Builder was already build once.
        ///		If that happens he will always return the same instance and can not be modified anymore.
        /// </summary>
        public bool IsFinal => _model is not null;

        /// <summary>
        ///    Factory for creating child builders that this model builder might use.
        ///    The factory also holds the Persistor which is used to specify how to persist the model.
        ///    All child builders will get the same persistor as it's parent by default.
        ///    The constructor of the specific ModelBuilder should call _factory.Create<Type>() to act this way.
        /// </summary>
        protected readonly IBuilderFactory _factory;

        /// <summary>
        ///     ModelBuilder that uses the same Persistor to Persist itself as it's children
        ///     Used by the common builders, that just build a model
        /// </summary>
        /// <param name="factory">The Factory with a defined persistor</param>
        protected ModelBuilder(IBuilderFactory factory)
        {
            if (factory is null) throw new ArgumentNullException("Factory can not be null!");
            _factory = factory;
            BuilderIndex = ++BUILDER_INDEX;
        }

        /// <inheritdoc/>
        public TModel Build()
        {
            if (_model is null)
            {
                _factory.PreBuild.Execute<TModel>(this);
                _model = BuildModel();
                _factory.PostBuild.Execute<TModel>(_model!);
            }
            return _model;
        }

        /// <summary>
        ///		Method where logic for building the Model will be implemented.
        /// </summary>
        protected abstract TModel BuildModel();

        /// <summary>
        ///		Validation if the builder is locked or not.
        ///		This method should be called in every builder method, to warn the user early
        ///		if he uses the builder incorrectly.
        ///
        /// 	A builder will return always the same instance by design, therefore a modification
        /// 	after the first buildis not possible anymore. Instead you can get a new builder via
        ///		the 'Recreate' method to get a builder with the same properties set.
        /// </summary>
        protected void Validate()
        {
            if (IsFinal)
            {
                throw new InvalidOperationException("The builder was already built and can not change anymore." +
                                                    "Use the 'Recreate' method to copy the builder configuration instead.");
            }
        }

        /// <summary>
        ///		QoL Method for simpler builder method definition
        ///		Override this method and call the generic version with your builder type
        /// </summary>
        /// <param name="action">Action that sets the member variable</param>
        protected virtual ModelBuilder<TModel> Set(Action action) => Set<ModelBuilder<TModel>>(action);

        /// <summary>
        ///		QoL Method for simpler builder method definition
        ///		It handles calling of the Validate method as well as returning the correct builder
        /// </summary>
        /// <param name="setAction">Action that sets the member variable</param>
        /// <typeparam name="TBuilder">The actual builder type</typeparam>
        /// <returns></returns>
        protected TBuilder Set<TBuilder>(Action setAction) where TBuilder : ModelBuilder<TModel>
        {
            Validate();
            setAction();
            return (TBuilder)this;
        }

        /// <summary>
        ///		Method proxy for easier calling of the Recreate method
        ///		Override this method, call it's generic variant
        ///		And use your builder type as generic and return parameter
        /// </summary>
        /// <returns>
        /// 	A new instance of the current builder configuration, that is not locked
        /// 	and can still be modified.
        /// </returns>
        public virtual ModelBuilder<TModel> Recreate() => Recreate<ModelBuilder<TModel>>();

        /// <summary>
        ///		Recreates the current builder, creates a new instance with the current configuration
        ///		The new instance is filled with the same DATA of the builder, but each sub-builder is recreated
        ///		That allows you to change and add values after the original builder was already locked.
        ///		If you call build on a recreated builder it will not return the same instance as the original
        ///		as would be usually the case.
        /// </summary>
        /// <typeparam name="TBuilder">Type of builder to recreate (The builder you're currently in)</typeparam>
        /// <returns>A new, unlocked instance of the current builder configuration</returns>
        protected TBuilder Recreate<TBuilder>() where TBuilder : ModelBuilder<TModel>
        {
            Type builderType = this.GetType();
            object builderCopyInstance = GetBuilderInstance(builderType);

            foreach (FieldInfo field in builderType.GetFields(BindingFlags.NonPublic |
                                                              BindingFlags.Instance |
                                                              BindingFlags.DeclaredOnly))
            {
                RecreateField(builderCopyInstance, field);
            }
            return (TBuilder)builderCopyInstance;
        }

        private object GetBuilderInstance(Type builderType)
        {
            object? builderCopyInstance;
            try
            {
                builderCopyInstance = Activator.CreateInstance(builderType, _factory);
            }
            catch (Exception)
            {
                //factory constructor not available, try default constructor
                builderCopyInstance = Activator.CreateInstance(builderType);
            }

            if (builderCopyInstance is null) throw new ArgumentNullException($"Activator could not create an instance of the type {builderType}!");

            return builderCopyInstance;
        }

        private void RecreateField(object builderCopyInstance, FieldInfo field)
        {
            if (field.FieldType.HandleAsValueType())
            {
                CopyValue(builderCopyInstance, field);
            }
            else if (GetObjectType(field).ImplementsInterface(typeof(IRecreatable<>)))
            {
                InvokeRecreatable(builderCopyInstance, field);
            }
            else
            {
                throw new ValidationException("Error during recreation of the builder: " +
                                              $"Only value types and {typeof(IRecreatable<>)} can be recreated!" +
                                              $"\nFailed on type: {GetObjectType(field)}");
            }
        }

        /// <summary>
        ///     Get's the actual type of the value that's assinged to the field (object type)
        /// </summary>
        /// <param name="field">The field (reference type)</param>
        private Type GetObjectType(FieldInfo field)
        {
            return GetFieldValue(field).GetType();
        }

        /// <summary>
        ///		Copies the values from the this builder to the new instance
        /// </summary>
        /// <param name="copyInstance">New builder instance</param>
        /// <param name="field">Field info of the value</param>
        private void CopyValue(object copyInstance, FieldInfo field)
        {
            object value = GetFieldValue(field);
            field.SetValue(copyInstance, value);
        }

        private object GetFieldValue(FieldInfo field)
        {
            object? value = field.GetValue(this);
            if (value is null) throw new ArgumentNullException($"The model builder doesn't contain a field withfthe name {field.Name}!");

            return value;
        }

        /// <summary>
        ///		Recreates the builder collection.
        ///		It will create a new <see cref="BuilderCollection<>"> and recreate all the builders in it.
        /// </summary>
        /// <param name="builderCopyInstance">Instance of the new builder</param>
        /// <param name="field">Field Info of the BuilderCollection</param>
        private void InvokeRecreatable(object builderCopyInstance, FieldInfo field)
        {
            object recreatable = GetFieldValue(field);
            string recreateMethodName = nameof(IRecreatable<object>.Recreate);
            MethodInfo? recreateMethod = recreatable.GetType().GetMethod(recreateMethodName);
            if (recreateMethod is null) throw new ArgumentNullException($"No method with the name {recreateMethodName} was found on type {recreatable.GetType().Name}");

            object? newCreated = recreateMethod.Invoke(recreatable, null);
            if (newCreated is null) throw new ArgumentNullException($"Invalid recreate implementation: The recreate method of type {recreatable.GetType().Name} returned a null value.");

            field.SetValue(builderCopyInstance, newCreated);
        }

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
    }
}
