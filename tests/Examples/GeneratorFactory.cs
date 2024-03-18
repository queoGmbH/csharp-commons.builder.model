using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.BuildAction;
using Queo.Commons.Builders.Model.Examples.Person;
using Queo.Commons.Builders.Model.Examples.Person.Mocks;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples
{
    public class GeneratorFactory : IBuilderFactory
    {
        private IDataGenerator _generator;
        public IPreBuildAction PreBuild { get; }
        public IPostBuildAction PostBuild { get; }

        public GeneratorFactory(IDataGenerator generator)
        {
            _generator = generator;
            PreBuild = new EmptyAction();
            PostBuild = new PersistenceAction(new ExamplePersistor());
        }

        public TBuilder Create<TBuilder>()
        {
            object result = typeof(TBuilder) switch
            {
                Type generatorType when typeof(PersonGeneratorBuilder) == generatorType => new PersonGeneratorBuilder(this, _generator),
                _ => throw new InvalidOperationException($"Factory doesn't know the type: {typeof(TBuilder).Name}")
            };
            return (TBuilder)result;
        }
    }
}
