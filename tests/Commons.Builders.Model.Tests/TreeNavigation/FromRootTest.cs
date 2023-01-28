using System;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.Tree;

namespace Queo.Commons.Builders.Model.Tests.TreeNavigation
{
    [TestFixture]
    public class ParentTest
    {
        [Test]
        public void BuildParentWithChilds()
        {
            RootBuilder parentBuilder = Create.Root()
                    .AddChild(c => c.WithName("child 1")
                            .AddChild(c1 => c1.WithName("subchild 1.1")
                                    .AddChild(c2 => c2.WithName("subsubchild 1.1.1")))
                            .AddChild(c3 => c3.WithName("subchild 1.2")))
                    .AddChild(c => c.WithName("child 2"));
            Root parent = parentBuilder.Build();
            Console.WriteLine(parent);
        }
    }
}
