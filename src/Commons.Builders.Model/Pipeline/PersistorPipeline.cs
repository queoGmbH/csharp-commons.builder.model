using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Peristence;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public class PersistorPipeline : Pipeline<object>
    {
        public PersistorPipeline(IPersistor persistor)
        {
            Add(persistor.Save);
        }
    }
}
