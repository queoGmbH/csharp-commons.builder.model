using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queo.Commons.Builders.Model.Builder
{
    /// <summary>
    ///     Interface describing a type that can be recreated (similar to clone or copy)
    /// </summary>
    /// <typeparam name="T">Type to recreate (BuilderType)</typeparam>
    public interface IRecreatable<out T>
    {
        T Recreate();
    }
}
