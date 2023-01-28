using System.Collections.Generic;
using System.Text;

namespace Queo.Commons.Builders.Model.Examples.Tree
{
    public class Root
    {
        public Root(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Node> Childs { get; } = new List<Node>();

        internal void Add(Node child)
        {
            Childs.Add(child);
        }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Parent: {Name}");
            foreach (Node child in Childs)
            {
                message.AppendLine($"{child.ToString("--")}");
            }
            return message.ToString();
        }
    }
}
