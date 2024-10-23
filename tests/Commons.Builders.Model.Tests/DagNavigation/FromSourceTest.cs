using System;
using System.Collections.Generic;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.DAG;

namespace Queo.Commons.Builders.Model.Tests.DagNavigation
{
    [TestFixture]
    public class FromSourceTest
    {
        [Test]
        public void BuildParentWithChilds()
        {
            SourceBuilder parentBuilder = Create.Source().WithName("Ein spezieller Parent")
                    .AddChild(c => c.WithName("child 1")
                            .AddChild(c1 => c1.WithName("subchild 1.1")
                                    .AddChild(c2 => c2.WithName("subsubchild 1.1.1")))
                            .AddChild(c3 => c3.WithName("subchild 1.2")))
                    .AddChild(c => c.WithName("child 2"));
            Source parent = parentBuilder;
            Console.WriteLine(parent);
            IEnumerable<Vertex> children = parentBuilder.GetChildren();
            foreach (Vertex child in children)
            {
                Console.WriteLine(child.ToString("--"));
            }
        }
    }
}
