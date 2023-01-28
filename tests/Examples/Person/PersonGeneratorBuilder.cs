using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Examples.Person.Mocks;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Person
{
    public class PersonGeneratorBuilder : ModelBuilder<Person>
    {
        private readonly IDataGenerator _generator;
        private int _maxAge = 0;
        private int _minAge = 100;

        private BuilderCollection<PersonBuilder, Person> _children;

        public PersonGeneratorBuilder(IBuilderFactory factory, IDataGenerator generator) : base(factory)
        {
            _generator = generator;
            _children = new BuilderCollection<PersonBuilder, Person>(_factory);
        }

        protected override Person BuildModel()
        {
            var _model = new Person(_generator.GetString(),
                                    _generator.GetInt(_minAge, _maxAge));
            foreach (var child in _children)
            {
                _model.Add(child.Build());
            }
            return _model;
        }

        public PersonGeneratorBuilder WithMaxAge(int maxAge)
        {
            Validate();
            _maxAge = maxAge;
            return this;
        }

        public PersonGeneratorBuilder WithMinAge(int minAge)
        {
            Validate();
            _minAge = minAge;
            return this;
        }

        public PersonGeneratorBuilder HasChild(IModelBuilder<Person> builder)
        {
            Validate();
            _children.Add(builder);
            return this;
        }

        public PersonGeneratorBuilder HasChild(Action<PersonGeneratorBuilder> builderAction)
        {
            Validate();
            //TODO: Not sure if it's a good idea to reuse the same generator?
            //Because of concerns of changing the data because of different executions?
            PersonGeneratorBuilder builder = new(_factory, _generator);
            builderAction(builder);
            _children.Add(builder);
            return this;
        }
    }

}
