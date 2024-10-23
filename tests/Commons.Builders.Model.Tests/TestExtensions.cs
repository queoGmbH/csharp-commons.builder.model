using System;
using System.Reflection;

using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Tests.SameInstanceBehaviour.Util
{
    public static class TestExtensions
    {
        /// <summary>
        ///     Extracts the builder index for testing purposes
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <typeparam name="T"></typeparam>
        public static int BuilderIndex<T>(this ModelBuilder<T> modelBuilder)
        {
            Type type = modelBuilder.GetType();
            FieldInfo builderIndex = type.BaseType.GetField("BUILDER_INDEX", BindingFlags.NonPublic | BindingFlags.Static);
            return (int)builderIndex.GetValue(modelBuilder);
        }
    }
}
