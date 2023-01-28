using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Pipeline
{
    public class EmptyPipeline : IPreBuildPipeline, IPostBuildPipeline<object>
    {
        public void Execute()
        {
            //Do nothing
        }

        public void Execute(object model)
        {
            //do nothing
        }
    }
}
