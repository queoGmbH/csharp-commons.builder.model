using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Utils
{
    public static class BuilderUtils
    {
        /// <summary>
        ///     Turns a builder action into an actual builder
        /// </summary>
        /// <param name="action">The builder action it is called upon</param>
        /// <param name="factory">The factory that is able to create the required type of builder</param>
        /// <typeparam name="TBuilder">Type of builder to create</typeparam>
        public static TBuilder ToBuilder<TBuilder>(this Action<TBuilder> action, IBuilderFactory factory)
        {
            TBuilder builder = factory.Create<TBuilder>();
            action(builder);
            return builder;
        }
    }
}
