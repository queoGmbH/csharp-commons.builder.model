using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.BuildAction
{
    public interface IPreBuildAction
    {
        void Execute<TModel>(IBuilder<TModel> builder);
    }
}
