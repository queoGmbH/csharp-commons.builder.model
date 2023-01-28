using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Peristence
{
    public interface IPersistor
    {
        void Save(object entity);
    }
}
