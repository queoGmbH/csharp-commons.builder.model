using System;

using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Examples
{
    public class ExamplePersistor : IPersistenceStrategy
    {
        public T Save<T>(T entity)
        {
            //persistence logic comes here
            Console.WriteLine($"Persisting entity: {entity?.GetType().Name}");
            return entity;
        }
    }
}
