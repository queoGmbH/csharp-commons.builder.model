using System;

using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Examples
{
    public class ExamplePersistor : IPersistenceStrategy
    {
        public void Save(object entity)
        {
            //persistence logic comes here
            Console.WriteLine($"Persisting entity: {entity.GetType().Name}");
        }
    }
}
