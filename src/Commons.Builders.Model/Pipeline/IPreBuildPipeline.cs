using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public interface IPreBuildPipeline
    {
        void Execute();
    }
}
