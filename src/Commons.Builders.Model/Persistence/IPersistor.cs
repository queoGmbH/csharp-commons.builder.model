using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Peristence
{
    public interface IPersistenceStrategy
    {
        /// <summary>
        ///     Saves the object according to persistence strategy.
        ///     Might need a connection to a database or other kind of storage
        /// </summary>
        /// <param name="entity">The object to be persistet</param>
        void Save(object entity);
    }
}
