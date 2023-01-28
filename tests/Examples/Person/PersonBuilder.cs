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
            _name = "John Doe";
            _age = 0;
            _children = new BuilderCollection<PersonBuilder, Person>(_factory);
        }


        public PersonBuilder HasChild(Action<PersonBuilder> buildAction)
        {
            Validate();
            _children.Add(buildAction);
            return this;
        }

        public PersonBuilder HasChild(IModelBuilder<Person> builder)
        {
            Validate();
            _children.Add(builder);
            return this;
        }

        public PersonBuilder WithName(string name)
        {
            Validate();
            _name = name;
            return this;
        }

        public PersonBuilder WithAge(int age)
        {
            Validate();
            _age = age;
            return this;
        }

        protected override Person BuildModel()
        {
            var _model = new Person(_name, _age);
            foreach (var child in _children)
            {
                _model.Add(child.Build());
            }
            return _model;
        }
    }
}
