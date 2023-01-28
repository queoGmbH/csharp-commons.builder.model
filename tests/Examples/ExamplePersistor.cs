using System;

using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Examples
{
    public class ExamplePersistor : IPersistor
    {
        public void Save(object entity)
        {
            //persistence logic comes here
            Console.WriteLine($"Saving entity: {entity.GetType().Name}");
        }
    }
}
