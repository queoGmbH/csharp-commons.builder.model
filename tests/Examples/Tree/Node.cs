using System.Collections.Generic;
using System.Text;

namespace Queo.Commons.Builders.Model.Examples.Tree
{
    public class Node
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Node> Childs { get; } = new List<Node>();

        public Node(string name, string description)
        {
            Name = name;
            Description = description;
        }

        internal void Add(Node child)
        {
            Childs.Add(child);
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{prefix} Child: {Name}");
            foreach (Node child in Childs)
            {
                sb.Append($"{child.ToString(prefix + "--")}");
            }
            return sb.ToString();
        }

    }
}
