using System;
using System.Linq;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Person
{
    public class PersonBuilder : ModelBuilder<Person>
    {
        private string _name;
        private int _age;
        private BuilderCollection<PersonBuilder, Person> _children;

        public PersonBuilder(IBuilderFactory factory) : base(factory)
        {
            _name = $"John Doe {BuilderIndex}";
            _age = BuilderIndex;
            _children = new BuilderCollection<PersonBuilder, Person>(_factory);
        }

        public PersonBuilder HasChild(Action<PersonBuilder> buildAction) => Set(() => _children.Add(buildAction));
        public PersonBuilder HasChild(IBuilder<Person> builder) => Set(() => _children.Add(builder));
        public PersonBuilder WithName(string name) => Set(() => _name = name);
        public PersonBuilder WithAge(int age) => Set(() => _age = age);

        protected override Person BuildModel()
        {
            var _model = new Person(_name, _age);
            foreach (var child in _children)
            {
                _model.Add(child.Build());
            }
            return _model;
        }

        protected override PersonBuilder Set(Action action) => Set<PersonBuilder>(action);
        public override PersonBuilder Recreate() => Recreate<PersonBuilder>();
    }
}
