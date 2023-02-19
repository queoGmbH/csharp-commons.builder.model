using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.BuildAction
{
    public interface IPostBuildAction
    {
        void Execute<TModel>(TModel model);
    }
}
