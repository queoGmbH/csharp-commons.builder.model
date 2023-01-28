using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Peristence
{
    public class InMemory : IPersistor
    {
        public void Save(object entity)
        {
            //do not save anything
        }
    }
}
