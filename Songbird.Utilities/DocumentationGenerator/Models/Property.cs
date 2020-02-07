using System.Text;

namespace DocumentationGenerator.Models
{
    public class Property
    {
        public string Name { get; set; }
        public string Summary { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Name}: {Summary}");
            return sb.ToString();
        }
    }
}