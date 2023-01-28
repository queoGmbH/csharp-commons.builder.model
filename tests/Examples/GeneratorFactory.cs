using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Examples.Person;
using Queo.Commons.Builders.Model.Examples.Person.Mocks;
using Queo.Commons.Builders.Model.Factory;
using Queo.Commons.Builders.Model.Pipeline;

namespace Queo.Commons.Builders.Model.Examples
{
    public class GeneratorFactory : IBuilderFactory
    {
        private IDataGenerator _generator;
        public IPreBuildPipeline PreBuildPipeline { get; }
        public IPostBuildPipeline<object> PostBuildPipeline { get; }

        public GeneratorFactory(IDataGenerator generator)
        {
            _generator = generator;
            PreBuildPipeline = new EmptyPipeline();
            PostBuildPipeline = new PersistorPipeline(new ExamplePersistor());
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
