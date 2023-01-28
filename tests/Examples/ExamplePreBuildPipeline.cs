using System;

using Queo.Commons.Builders.Model.Pipeline;

namespace Queo.Commons.Builders.Model.Examples
{
    public class ExamplePreBuildPipeline : IPreBuildPipeline
    {
        //FIXME: this seems quite useless. I would need to know at least what type is executed?
        public void Execute()
        {
            Console.WriteLine("Pipeline: I'm about to build something, this is soo exciting!!!!");
        }
    }
}
