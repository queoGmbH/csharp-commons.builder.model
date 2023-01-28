using System.Collections.Generic;
using System.Text;

namespace Queo.Commons.Builders.Model.Examples.DAG
{
    public class Vertex
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Source Source { get; set; }

        public IList<Vertex> Edges { get; } = new List<Vertex>();

        public Vertex(string name, string description, Source source)
        {
            Name = name;
            Description = description;
            Source = source;
        }

        internal void Add(Vertex target)
        {
            Edges.Add(target);
            if (Source is null)
            {
                Source = target.Source;
            }
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{prefix} Child: {Name} with parent {Source.Name}");
            foreach (Vertex child in Edges)
            {
                sb.Append($"{child.ToString(prefix + "--")}");
            }
            return sb.ToString();
        }

    }
}
