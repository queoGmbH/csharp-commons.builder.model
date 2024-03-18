using System.Text;

namespace Queo.Commons.Builders.Model.Examples.DAG
{
    public class Source
    {
        public Source(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"Parent: {Name}");

            return message.ToString();
        }
    }
}
