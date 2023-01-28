using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queo.Commons.Builders.Model.Examples.Person
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public IList<Person> Children { get; } = new List<Person>();

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        internal void Add(Person child)
        {
            Children.Add(child);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Name} : {Age} =>\n");
            sb.Append(Children.Aggregate("", (a, b) => a + " - " + b.ToString() + "\n"));

            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                       Name == person.Name &&
                       Age == person.Age &&
                       EqualityComparer<IList<Person>>.Default.Equals(Children, person.Children);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age, Children);
        }
    }
}
