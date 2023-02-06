using System;

using FluentAssertions;

using NUnit.Framework;

using Queo.Commons.Builders.Model.Examples;
using Queo.Commons.Builders.Model.Examples.DAG;

namespace Queo.Commons.Builders.Model.Tests.DagNavigation
{
    [TestFixture]
    public class FromVertexTest
    {
        [Test]
        public void Test_SettingParentExplicitly()
        {
            SourceBuilder parent = Create.Source().WithName("RealParent");
            SourceBuilder otherParent = Create.Source().WithName("Other parent");

            Vertex child = Create.Vertex()
                                 .WithSource(parent).AddChild(c => c.WithName("OwnChild"))
                                                                    .AddChild(c => c.WithName("AdoptedChild").WithSource(otherParent))
                                                                    .Build();
            Console.WriteLine(child.ToString(" - "));
        }

        [Test]
        public void Test_SettingChildWithOtherParent()
        {

            SourceBuilder parent = Create.Source().WithName("RealParent");
            SourceBuilder otherParent = Create.Source().WithName("Other parent");

            VertexBuilder otherChild = Create.Vertex().WithSource(otherParent);
            Vertex child = Create.Vertex()
                                 .WithSource(parent).AddChild(c => c.WithName("OwnChild"))
                                                                    .AddChild(otherChild)
                                                                    .Build();
            Console.WriteLine(child.ToString(" - "));
        }

        //You can also only accept sub builder for add child
        // -> same as in AddChilde(BuildAction)
        // But the method still has to be available within the childbuilder -> for the ability to set it internally


        [Test]
        public void Test_TryingToInjectChildBuilder()
        {
            //In one case only add by action is allowed
            //In the other case only add by builder -> then we could have the desired behaviour
            SourceBuilder p1 = Create.Source().WithName("FirstParent");
            SourceBuilder p2 = Create.Source().WithName("OtherParent");

            VertexBuilder c1 = Create.Vertex().WithSource(p1).WithName("InjectedChild");
            Vertex c2 = Create.Vertex().WithSource(p2)
                                                             .WithName("OwnChild")
                                                             .AddChild(c => c = c1)
                                                             .Build();
            Console.WriteLine(c2.ToString(" - "));

            c2.Source.Should().Be(p2.Build());
            c2.Edges.Should().NotContain(c1.Build());
        }
    }
}
