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

        public PersonGeneratorBuilder WithMaxAge(int maxAge) => Set(() => _maxAge = maxAge);
        public PersonGeneratorBuilder WithMinAge(int minAge) => Set(() => _minAge = minAge);
        public PersonGeneratorBuilder HasChild(IBuilder<Person> builder) => Set(() => _children.Add(builder));
        public PersonGeneratorBuilder HasChild(Action<PersonGeneratorBuilder> builderAction) => Set(() => _children.Add(builderAction));

        protected override PersonGeneratorBuilder Set(Action action) => Set<PersonGeneratorBuilder>(action);
        public override PersonGeneratorBuilder Recreate() => Recreate<PersonGeneratorBuilder>();
    }

}
