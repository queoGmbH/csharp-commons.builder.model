using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.BuildAction;

namespace Queo.Commons.Builders.Model.Factory
{
    public interface IBuilderFactory
    {
        public IPreBuildAction PreBuild { get; }
        public IPostBuildAction PostBuild { get; }
        TBuilder Create<TBuilder>();

    }
}
