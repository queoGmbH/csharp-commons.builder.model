using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Factory;
using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Factory
{
    public class InMemoryFactory : PersistableBuilderFactory
    {
        public InMemoryFactory() : base(new InMemory()) { }

        public override TBuilder Create<TBuilder>()
        {
            throw new InvalidOperationException("Create is not supposed to be called on an empty factory! " +
                                                "Your builder is configured with the wrong type of factory!");
        }
    }
}
